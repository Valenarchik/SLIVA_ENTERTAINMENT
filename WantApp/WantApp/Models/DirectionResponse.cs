using System;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace WantApp.Models
{
    public static class Direction
    {
        public static double GetDistance(Position p1, Position p2)
        {
            return Math.Sqrt((p1.Longitude - p2.Longitude) * (p1.Longitude - p2.Longitude)
                             + (p1.Latitude - p2.Latitude) * (p1.Latitude - p2.Latitude))*111;
        }
    }
    public partial class DirectionResponse
    {
        [JsonProperty("routes")]
        public Route[] Routes { get; set; }

        [JsonProperty("waypoints")]
        public Waypoint[] Waypoints { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    public partial class Route
    {
        [JsonProperty("geometry")]
        public string Geometry { get; set; }

        [JsonProperty("legs")]
        public Leg[] Legs { get; set; }

        [JsonProperty("weight_name")]
        public string WeightName { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }

    public partial class Leg
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("steps")]
        public object[] Steps { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }

    public partial class Waypoint
    {
        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public double[] Location { get; set; }
    }
}
