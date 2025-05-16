namespace BrownianMotionApp.Models
{
    public class SimulationParameters
    {
        public double InitialPrice { get; set; }
        public int VolatilityPercent { get; set; }
        public int MeanPercent { get; set; }
        public int Time { get; set; }
    }
}
