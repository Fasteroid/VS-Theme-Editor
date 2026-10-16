using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VS_Theme_Editor {

    public static class Json {
        
        public interface IDictionaryable {
            public string? Id { get; set; }
        }

        public class DictionaryableConverter<T> : JsonConverter< IList<T> > where T : IDictionaryable
        {
            public override IList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var dict = JsonSerializer.Deserialize< Dictionary<string, T> >(ref reader, options);
                return dict!
                    .Select( kv => { 
                        kv.Value.Id = kv.Key;
                        return kv.Value;
                    })
                    .ToList()
                ;
            }

            public override void Write(Utf8JsonWriter writer, IList<T> value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                foreach (var entry in value) {
                    if( entry.Id is null ) continue;
                    writer.WritePropertyName(entry.Id);
                    JsonSerializer.Serialize(writer, entry, options);
                }
                writer.WriteEndObject();
            }

            // match all IList implementers
            public override bool CanConvert(Type typeToConvert) => typeof(IList<T>).IsAssignableFrom(typeToConvert);    

        }

        private static JsonSerializerOptions GetSerializerOptions() {
            JsonSerializerOptions ret = new() {
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true, 
                IndentCharacter = '\t', 
                IndentSize=1
            };
            ret.Converters.Add( new DictionaryableConverter<ThemeColorEntry>() );
            ret.Converters.Add( new DictionaryableConverter<CategoryData>() );
            return ret;
        }

        public static readonly JsonSerializerOptions Options = GetSerializerOptions();

    }
    
   
}
