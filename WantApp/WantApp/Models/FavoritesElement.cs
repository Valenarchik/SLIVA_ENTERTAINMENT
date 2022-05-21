using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace WantApp
{
    public class FavoritesElement
    {
        public string Request { get; set; }
        public List<Position> Response { get; set; } = new List<Position>();

        private int currentResponse;
        public int CurrentResponse
        {
            get => currentResponse;
            set
            {
                if (value < 0 || value >= Response.Count)
                    currentResponse = 0;
                else
                    currentResponse = value;
            }
        }
    }
}