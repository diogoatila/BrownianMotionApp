using Microsoft.Maui.Graphics;

namespace BrownianMotionApp.Services.Graphics
{

    public class BindableDrawable : BindableObject, IDrawable
    {
      
        public static readonly BindableProperty PricesProperty =
            BindableProperty.Create(
                nameof(Prices),                 
                typeof(double[]),              
                typeof(BindableDrawable),       
                default(double[]));             

      
        public double[] Prices
        {
            get => (double[])GetValue(PricesProperty);
            set => SetValue(PricesProperty, value);
        }

        // Método Draw para desenhar o gráfico
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Prices == null || Prices.Length < 2)
                return;

            float width = dirtyRect.Width;
            float height = dirtyRect.Height;
            float stepX = width / (Prices.Length - 1);
            double maxPrice = Prices.Max();
            double minPrice = Prices.Min();

            PathF path = new();

            for (int i = 0; i < Prices.Length; i++)
            {
                float x = i * stepX;
                float normalized = (float)((Prices[i] - minPrice) / (maxPrice - minPrice));
                float y = height - normalized * height;

                if (i == 0)
                    path.MoveTo(x, y);
                else
                    path.LineTo(x, y);
            }

            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 2;
            canvas.DrawPath(path);
        }
    }
}
