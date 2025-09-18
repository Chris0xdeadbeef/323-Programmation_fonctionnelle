
namespace Rando
{
    internal class Trackpoint
    {
        private double _latitude;
        private double _longitude;
        private double? _elevation;

        public double Latitude
        {
            get => _latitude;
            set => _latitude = value;
        }
        public double Longitude
        {
            get => _longitude;
            set => _longitude = value;
        }
        public double? Elevation
        {
            get => _elevation;
            set => _elevation = value;
        }

        public Trackpoint(double latitude, double longitude, double? elevation)
        {
            _latitude = latitude;
            _longitude = longitude;
            _elevation = elevation;
        }
        
    }
}
