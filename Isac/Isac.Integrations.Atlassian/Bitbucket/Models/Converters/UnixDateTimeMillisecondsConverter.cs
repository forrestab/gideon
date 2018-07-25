using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Converters
{
    // Original, https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/Converters/UnixDateTimeConverter.cs
    // For some reason Bitbucket Server sends a 13 digit unix timestamp which throws and out of range exception when using the
    // converter from Newtonsoft.Json because of the `AddSeconds()`.
    public class UnixDateTimeMillisecondsConverter : DateTimeConverterBase
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool IsNullable = this.IsNullable(objectType);
            long Ticks;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!IsNullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
                }

                return null;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                Ticks = (long)reader.Value;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                if (!long.TryParse(reader.Value.ToString(), out Ticks))
                {
                    throw new JsonSerializationException($"Cannot convert invalid value to {objectType}.");
                }
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token parsing date. Expected Integer or String, got {reader.TokenType}.");
            }

            if (Ticks >= 0)
            {
                DateTime ParsedDateTime = UnixEpoch.AddMilliseconds(Ticks);
                Type UnderlyingType = objectType;

                if (IsNullable)
                {
                    UnderlyingType = Nullable.GetUnderlyingType(objectType);
                }

                if (UnderlyingType == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(ParsedDateTime, TimeSpan.Zero);
                }

                return ParsedDateTime;
            }
            else
            {
                throw new JsonSerializationException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {objectType}.");
            }
        }

        private bool IsNullable(Type objectType)
        {
            if (objectType.IsValueType)
            {
                return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            return true;
        }
    }
}
