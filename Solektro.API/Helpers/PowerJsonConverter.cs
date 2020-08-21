using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Solektro.Core.Models;

namespace Solektro.API.Helpers
{
    public class PowerJsonConverter : JsonConverter<Power>
    {
        public override Power Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Power() { Value = reader.GetDouble() };
        }

        public override void Write(Utf8JsonWriter writer, Power power, JsonSerializerOptions options)
        {
            writer.WriteStringValue(power.Value.ToString());
        }
    }
}
