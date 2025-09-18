using Gpx;
using System.Reflection.Metadata.Ecma335;

namespace Rando
{
    public partial class Rando : Form
    {
        string gpxFile = @"Ballade_châtaignère.gpx";
        List<Trackpoint> trackpoints = new();
        public Rando()
        {
            InitializeComponent();

            if (!File.Exists(gpxFile))
            {
                MessageBox.Show("Pas trouve");
            }

            StreamReader streamReader = new StreamReader(gpxFile);

            using (GpxReader reader = new GpxReader(streamReader.BaseStream))
            {

                while (reader.Read())
                {
                    switch (reader.ObjectType)
                    {
                        case GpxObjectType.Track:
                            //writer.WriteTrack(reader.Track);
                            GpxPointCollection<GpxPoint> gpxPoints = reader.Track.ToGpxPoints();

                            // convertir les gpxpoint en trackpoint

                            trackpoints.AddRange(gpxPoints.Select(p => new Trackpoint(p.Latitude * 10000, p.Longitude * 10000, p.Elevation)).ToList());


                            break;
                    }
                }
            }
        }

        private void Rando_Form_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Red);
            myPen.Width = 2;

            var minLat = trackpoints.Min(p => p.Latitude);
            var maxLat = trackpoints.Max(p => p.Latitude);
            var rangeLat = maxLat - minLat;
            var ratioLat = rangeLat / Width;

            var minLong = trackpoints.Min(tp => tp.Longitude);
            var maxLong = trackpoints.Max(tp => tp.Longitude);
            var rangeLong = maxLong - minLong;
            var ratioLong = rangeLong / Height;


            Point[] points = trackpoints.Select(tp => new Point()
            {
                X = Convert.ToInt32((tp.Latitude - minLat) * ratioLat),
                Y = Convert.ToInt32((tp.Longitude - minLong) * ratioLong)

            }).ToArray();

            this.CreateGraphics().DrawLines(myPen, points);

        }
    }
}
