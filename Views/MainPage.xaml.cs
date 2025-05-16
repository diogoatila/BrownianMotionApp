using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using BrownianMotionApp.ViewModels;

namespace BrownianMotionApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var vm = new MainViewModel();
        BindingContext = vm;

        // Atualiza o gráfico sempre que Prices for alterado
        vm.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(vm.Prices))
                SkiaChartView.InvalidateSurface(); // força redesenho
        };
    }

    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColor.Parse("#24262e")); // Fundo preto

        if (BindingContext is not MainViewModel vm || vm.Prices == null || vm.Prices.Length < 2)
            return;

        var prices = vm.Prices;
        float width = e.Info.Width;
        float height = e.Info.Height;
        float margin = 40;
        float graphWidth = width - margin * 2;
        float graphHeight = height - margin * 2;
        float stepX = graphWidth / (prices.Length - 1);

        double max = prices.Max();
        double min = prices.Min();

        using var axisPaint = new SKPaint
        {
            Color = SKColors.LightGray, // Eixos em cinza claro
            StrokeWidth = 2
        };

        using var textPaint = new SKPaint
        {
            Color = SKColors.White,     // Texto branco
            TextSize = 18,
            IsAntialias = true
        };

        using var graphPaint = new SKPaint
        {
            Color = SKColors.Cyan,      // Linha do gráfico azul clara
            StrokeWidth = 2,
            IsAntialias = true,
            Style = SKPaintStyle.Stroke
        };

        // Eixo Y
        canvas.DrawLine(margin, margin, margin, height - margin, axisPaint);

        for (int i = 0; i <= 4; i++)
        {
            float y = margin + i * (graphHeight / 4);
            double label = max - i * ((max - min) / 4);
            canvas.DrawText(label.ToString("F2"), 5, y + 5, textPaint);
            canvas.DrawLine(margin - 5, y, margin, y, axisPaint);
        }

        // Eixo X
        canvas.DrawLine(margin, height - margin, width - margin, height - margin, axisPaint);

        int step = prices.Length / 4;
        for (int i = 0; i <= 4; i++)
        {
            int idx = i * step;
            float x = margin + idx * stepX;
            canvas.DrawText(idx.ToString(), x - 10, height - 5, textPaint);
            canvas.DrawLine(x, height - margin, x, height - margin + 5, axisPaint);
        }

        // Linha do gráfico
        var path = new SKPath();
        for (int i = 0; i < prices.Length; i++)
        {
            float x = margin + i * stepX;
            float normalized = (float)((prices[i] - min) / (max - min));
            float y = margin + (1 - normalized) * graphHeight;

            if (i == 0)
                path.MoveTo(x, y);
            else
                path.LineTo(x, y);
        }

        canvas.DrawPath(path, graphPaint); // Desenha como linha
    }

}
