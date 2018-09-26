using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACLTest_Lib;

namespace ACLTest_Console
{
    class Program
    {
        #region Create Locations
        private static List<Location> _Locations = new List<Location>{
            new Location{
                Name ="KMCO",
                Latitude ="N28-25.8",
                Longitude ="W081-18.5"
            },
            new Location {
                Name ="KMIA",
                Latitude ="N25-47.7",
                Longitude ="W080-17.4"
            },
            new Location{
                Name ="KCVG",
                Latitude ="N39-02.9",
                Longitude ="W084-40.1"
            },
            new Location{
                Name ="KCMH",
                Latitude ="N39-59.8",
                Longitude ="W082-53.5"
            },
            new Location{
                Name ="KCMI",
                Latitude ="N40-02.3",
                Longitude ="W088-16.7"
            },
            new Location{
                Name ="KJFK",
                Latitude ="N40-38.4",
                Longitude ="W073-46.7"
            },
            new Location{
                Name ="KLAS",
                Latitude ="N36-04.8",
                Longitude ="W115-09.1"
            },
            new Location{
                Name ="KLAX",
                Latitude ="N33-56.5",
                Longitude ="W118-24.5"
            },
            new Location{
                Name ="KKNB",
                Latitude ="N37-00.6",
                Longitude ="W112-31.9"
            },
            new Location{
                Name ="KATL",
                Latitude ="N33-38.2",
                Longitude ="W084-25.7"
            }
        };
        #endregion 

        private static List<Trip> _Trips = new List<Trip>();

        /*
            MCO
            28° 33' 55.584'' N
            81° 18' 47.268'' W

            MIA
            28° 9' 41.832'' N
            80° 39' 9.144'' W

            MCO
            Latitude	        Longitude	        Format
            N 28.43116°	        W 81.30808°	        h ddd.ddddd°
            N 28° 25.869'	    W 81° 18.485'	    h ddd° mm.mmm′
            N 28° 25' 52.2''	W 81° 18' 29.1''	h dd° mm′ ss.s″
            
            MIA
            Latitude	        Longitude	        Format
            N 25.79587°	        W 80.28705°	        h ddd.ddddd°
            N 25° 47.752'	    W 80° 17.223'	    h ddd° mm.mmm′
            N 25° 47' 45.1''	W 80° 17' 13.4''	h dd° mm′ ss.s″

            AirportCode         Latitude     Longitude
            KMCO                N28-25.8    W081-18.5
            KMIA                N25-47.7    W080-17.4
        */

        static void Main(string[] args)
        {
            AddTrip("KCVG", "KMIA", decimal.Parse("948.26"), decimal.Parse("823.86"));
            AddTrip("KJFK", "KCVG", decimal.Parse("589.12"), decimal.Parse("511.83"));
            AddTrip("KMCO", "KATL", decimal.Parse("403.66"), decimal.Parse("350.7"));
            AddTrip("KLAS", "KLAX", decimal.Parse("236.27"), decimal.Parse("205.27"));
            AddTrip("KCMH", "KCMI", decimal.Parse("571.44"), decimal.Parse("496.48"));
            AddTrip("KCMI", "KKNB", decimal.Parse("1326.71"), decimal.Parse("1152.66"));
            AddTrip("KKNB", "KCMH", decimal.Parse("1612.42"), decimal.Parse("1400.89"));

            foreach (Trip trip in _Trips)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Distance from " + trip.Origin.Name + " to " + trip.Destination.Name + ": ");
                trip.Mileage = CalculateValues(trip.Origin, trip.Destination, DistanceCalc.DistanceType.StatuteMiles);
                trip.NMileage = CalculateValues(trip.Origin, trip.Destination, DistanceCalc.DistanceType.NauticalMiles);
                Console.Write("Miles/");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Dundas");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(": " + trip.Mileage.ToString() + " /");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(trip.DundasMileage.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", Nautical Miles/");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Dundas");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(": " + trip.NMileage.ToString() + " / ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(trip.DundasNMileage.ToString());
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        private static decimal CalculateValues(Location from, Location to, DistanceCalc.DistanceType type)
        {
            DistanceCalc dc = new DistanceCalc();

            return dc.Calculate(from.Latitude, from.Longitude,
                to.Latitude, to.Longitude, type, true);
        }
        /// <summary>
        /// Create a new Trip
        /// </summary>
        /// <param name="origin">Origination Airport Icao</param>
        /// <param name="destination">Destination Airport Icao</param>
        /// <param name="expectedMileage">Dundas Comparison Mileage</param>
        /// <param name="expectedNMileage">Dundas Comparison Nautical Mileage</param>
        private static void AddTrip(string origin, string destination, decimal expectedMileage, decimal expectedNMileage)
        {
            _Trips.Add(
                new Trip
                {
                    Origin = _Locations.Where(x => x.Name.ToUpper() == origin).SingleOrDefault(),
                    Destination = _Locations.Where(x => x.Name.ToUpper() == destination).SingleOrDefault(),
                    DundasMileage = expectedMileage,
                    DundasNMileage = expectedNMileage
                }
            );
        }
    }
    public class Trip
    {
        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public decimal Mileage { get; set; }
        public decimal NMileage { get; set; }
        public decimal DundasMileage { get; set; }
        public decimal DundasNMileage { get; set; }
    }
    public class Location
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
