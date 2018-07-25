namespace WpfApp1
{
    public class DistributedLoad
    {
        private double _startMagnitude;

        public double StartLocation { get; set; }

        public double EndLocation { get; set; }

        public string StartLabelLoad { get; set; } = "-5k/ft";

        public string EndLabelLoad { get; set; } = "-25k/ft";

        public LoadDirection Direction { get; set; }

        public double StartMagnitude { get; set; }

        public double EndMagnitude { get; set; }
    }
}