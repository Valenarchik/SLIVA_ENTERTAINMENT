using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WantApp.Models
{
    public partial class CategorySearchResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public CategorySearchResponseProperties Properties { get; set; }

        [JsonProperty("features")]
        public Feature[] Features { get; set; }
    }

    public partial class Feature
    {
        [JsonProperty("type")]
        public FeatureType Type { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public FeatureProperties Properties { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("type")]
        public GeometryType Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }

    public partial class FeatureProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("boundedBy")]
        public double[][] BoundedBy { get; set; }

        [JsonProperty("CompanyMetaData")]
        public CompanyMetaData CompanyMetaData { get; set; }
    }

    public partial class CompanyMetaData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("Categories")]
        public Category[] Categories { get; set; }

        [JsonProperty("Hours")]
        public Hours Hours { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }

        [JsonProperty("Phones", NullValueHandling = NullValueHandling.Ignore)]
        public Phone[] Phones { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Hours
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("Availabilities")]
        public Availability[] Availabilities { get; set; }
    }

    public partial class Availability
    {
        [JsonProperty("Intervals", NullValueHandling = NullValueHandling.Ignore)]
        public Interval[] Intervals { get; set; }

        [JsonProperty("Everyday", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Everyday { get; set; }

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

        [JsonProperty("TwentyFourHours", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TwentyFourHours { get; set; }
    }

    public partial class Interval
    {
        [JsonProperty("from")]
        public DateTimeOffset From { get; set; }

        [JsonProperty("to")]
        public DateTimeOffset To { get; set; }
    }

    public partial class Phone
    {
        [JsonProperty("type")]
        public PhoneType Type { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }
    }

    public partial class CategorySearchResponseProperties
    {
        [JsonProperty("ResponseMetaData")]
        public ResponseMetaData ResponseMetaData { get; set; }
    }

    public partial class ResponseMetaData
    {
        [JsonProperty("SearchResponse")]
        public SearchResponse SearchResponse { get; set; }

        [JsonProperty("SearchRequest")]
        public SearchRequest SearchRequest { get; set; }
    }

    public partial class SearchRequest
    {
        [JsonProperty("request")]
        public string Request { get; set; }

        [JsonProperty("skip")]
        public long Skip { get; set; }

        [JsonProperty("results")]
        public long Results { get; set; }

        [JsonProperty("boundedBy")]
        public double[][] BoundedBy { get; set; }
    }

    public partial class SearchResponse
    {
        [JsonProperty("found")]
        public long Found { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("boundedBy")]
        public double[][] BoundedBy { get; set; }
    }

    public enum GeometryType { Point };

    public enum PhoneType { Phone };

    public enum FeatureType { Feature };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                GeometryTypeConverter.Singleton,
                PhoneTypeConverter.Singleton,
                FeatureTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GeometryTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GeometryType) || t == typeof(GeometryType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Point")
            {
                return GeometryType.Point;
            }
            throw new Exception("Cannot unmarshal type GeometryType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GeometryType)untypedValue;
            if (value == GeometryType.Point)
            {
                serializer.Serialize(writer, "Point");
                return;
            }
            throw new Exception("Cannot marshal type GeometryType");
        }

        public static readonly GeometryTypeConverter Singleton = new GeometryTypeConverter();
    }

    internal class PhoneTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PhoneType) || t == typeof(PhoneType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "phone")
            {
                return PhoneType.Phone;
            }
            throw new Exception("Cannot unmarshal type PhoneType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PhoneType)untypedValue;
            if (value == PhoneType.Phone)
            {
                serializer.Serialize(writer, "phone");
                return;
            }
            throw new Exception("Cannot marshal type PhoneType");
        }

        public static readonly PhoneTypeConverter Singleton = new PhoneTypeConverter();
    }

    internal class FeatureTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FeatureType) || t == typeof(FeatureType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Feature")
            {
                return FeatureType.Feature;
            }
            throw new Exception("Cannot unmarshal type FeatureType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FeatureType)untypedValue;
            if (value == FeatureType.Feature)
            {
                serializer.Serialize(writer, "Feature");
                return;
            }
            throw new Exception("Cannot marshal type FeatureType");
        }

        public static readonly FeatureTypeConverter Singleton = new FeatureTypeConverter();
    }
}
