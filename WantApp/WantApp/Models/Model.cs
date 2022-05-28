using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace WantApp.Models
{
    public class Model
    {
        public static double GetDistance(Position p1, Position p2)
        {
            return Math.Sqrt((p1.Longitude - p2.Longitude) * (p1.Longitude - p2.Longitude)
                             + (p1.Latitude - p2.Latitude) * (p1.Latitude - p2.Latitude)) * 111;
        }

        public static IEnumerable<Position> Decode(string encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
                throw new ArgumentNullException("encodedPoints");

            var polylineChars = encodedPoints.ToCharArray();
            var index = 0;

            var currentLat = 0;
            var currentLng = 0;

            while (index < polylineChars.Length)
            {
                // calculate next latitude
                var sum = 0;
                var shifter = 0;
                int next5Bits;
                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                } while (next5Bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                } while (next5Bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5Bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                yield return new Position(Convert.ToDouble(currentLat) / 1E5, Convert.ToDouble(currentLng) / 1E5);
            }
        }
    }
}