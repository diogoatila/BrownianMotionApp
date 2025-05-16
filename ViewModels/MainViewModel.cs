using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;

namespace BrownianMotionApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double _initialPrice = 100;
        private int _volatilityPercent;
        private int _meanPercent;
        private int _time = 100;
        private double[]? _prices;

        public double InitialPrice
        {
            get => _initialPrice;
            set { _initialPrice = value; OnPropertyChanged(); }
        }

        public int VolatilityPercent
        {
            get => _volatilityPercent;
            set { _volatilityPercent = value; OnPropertyChanged(); }
        }

        public int MeanPercent
        {
            get => _meanPercent;
            set { _meanPercent = value; OnPropertyChanged(); }
        }

        public int Time
        {
            get => _time;
            set { _time = value; OnPropertyChanged(); }
        }

        public double[]? Prices
        {
            get => _prices;
            set { _prices = value; OnPropertyChanged(); }
        }

        public ICommand GenerateCommand => new Command(GeneratePrices);

        private void GeneratePrices()
        {

            double volatility = VolatilityPercent / 100.0;
            double mean = MeanPercent / 100.0;
            // Gera o movimento browniano
            Prices = BrownianMotionGenerator.GenerateBrownianMotion(volatility, mean, InitialPrice, Time);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }



}
