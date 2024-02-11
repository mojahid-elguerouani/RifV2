using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ProjectModel
{
    public class Class
    {
    }
    public partial class Temperatures
    {
        [JsonProperty("series")]
        public SeriesValues[] Series { get; set; }
    }

    public partial class SeriesValues
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public IList<Datum> Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }

        [JsonProperty("parent", NullValueHandling = NullValueHandling.Ignore)]
        public Parent? Parent { get; set; }

        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Start { get; set; }

        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? End { get; set; }

        [JsonProperty("completed", NullValueHandling = NullValueHandling.Ignore)]
        public Completed Completed { get; set; }

        [JsonProperty("dependency", NullValueHandling = NullValueHandling.Ignore)]
        public string Dependency { get; set; }

        [JsonProperty("milestone", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool? Milestone { get; set; }
    }

    public partial class Completed
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("fill", NullValueHandling = NullValueHandling.Ignore)]
        public string Fill { get; set; }
    }

    public enum Parent { NewOffices, NewProduct, Relocate };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ParentConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            bool b;
            if (Boolean.TryParse(value, out b))
            {
                return b;
            }
            throw new Exception("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ParentConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Parent) || t == typeof(Parent?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "new_offices":
                    return Parent.NewOffices;
                case "new_product":
                    return Parent.NewProduct;
                case "relocate":
                    return Parent.Relocate;
            }
            throw new Exception("Cannot unmarshal type Parent");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Parent)untypedValue;
            switch (value)
            {
                case Parent.NewOffices:
                    serializer.Serialize(writer, "new_offices");
                    return;
                case Parent.NewProduct:
                    serializer.Serialize(writer, "new_product");
                    return;
                case Parent.Relocate:
                    serializer.Serialize(writer, "relocate");
                    return;
            }
            throw new Exception("Cannot marshal type Parent");
        }

        public static readonly ParentConverter Singleton = new ParentConverter();
    }

}
