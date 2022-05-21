using System;
using Newtonsoft.Json;

namespace WantApp.Models
{
    public partial class YandexResponse
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("properties")] public TemperaturesProperties Properties { get; set; }

        [JsonProperty("features")] public Feature[] Features { get; set; }
    }

    public partial class Feature
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("geometry")] public Geometry Geometry { get; set; }

        [JsonProperty("properties")] public FeatureProperties Properties { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("coordinates")] public double[] Coordinates { get; set; }
    }

    public partial class FeatureProperties
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("boundedBy")] public double[][] BoundedBy { get; set; }

        [JsonProperty("CompanyMetaData")] public CompanyMetaData CompanyMetaData { get; set; }
    }

    public partial class CompanyMetaData
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("Categories")] public Category[] Categories { get; set; }

        [JsonProperty("Hours")] public Hours Hours { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("class")] public string Class { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public partial class Hours
    {
        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("Availabilities")] public Availability[] Availabilities { get; set; }
    }

    public partial class Availability
    {
        [JsonProperty("Intervals")] public Interval[] Intervals { get; set; }

        [JsonProperty("Monday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Monday { get; set; }

        [JsonProperty("Tuesday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Tuesday { get; set; }

        [JsonProperty("Wednesday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Wednesday { get; set; }

        [JsonProperty("Thursday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Thursday { get; set; }

        [JsonProperty("Friday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Friday { get; set; }

        [JsonProperty("Saturday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Saturday { get; set; }

        [JsonProperty("Sunday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Sunday { get; set; }
    }

    public partial class Interval
    {
        [JsonProperty("from")] public DateTimeOffset From { get; set; }

        [JsonProperty("to")] public DateTimeOffset To { get; set; }
    }

    public partial class TemperaturesProperties
    {
        [JsonProperty("ResponseMetaData")] public ResponseMetaData ResponseMetaData { get; set; }
    }

    public partial class ResponseMetaData
    {
        [JsonProperty("SearchResponse")] public SearchResponse SearchResponse { get; set; }

        [JsonProperty("SearchRequest")] public SearchRequest SearchRequest { get; set; }
    }

    public partial class SearchRequest
    {
        [JsonProperty("request")] public string Request { get; set; }

        [JsonProperty("skip")] public long Skip { get; set; }

        [JsonProperty("results")] public long Results { get; set; }

        [JsonProperty("boundedBy")] public double[][] BoundedBy { get; set; }
    }

    public partial class SearchResponse
    {
        [JsonProperty("found")] public long Found { get; set; }

        [JsonProperty("display")] public string Display { get; set; }
    }
}