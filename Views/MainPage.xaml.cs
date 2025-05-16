using BrownianMotionApp.ViewModels;
using BrownianMotionApp.Views;
using System.ComponentModel;

namespace BrownianMotionApp.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    private readonly BindableDrawable _drawable;

    public MainPage()
    {
        InitializeComponent();

        _viewModel = new MainViewModel();
        BindingContext = _viewModel;

        _drawable = new BindableDrawable();
        ChartView.Drawable = _drawable;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.Prices))
        {
            _drawable.Prices = _viewModel.Prices ?? Array.Empty<double>();
            ChartView.Invalidate(); // força o redesenho
        }
    }
}
