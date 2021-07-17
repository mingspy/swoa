using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebMvc.PlatformApiControllers
{
    public class JsonNetFormatter : MediaTypeFormatter
    {
        private static readonly MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private System.Text.Encoding Encoding;
        public JsonNetFormatter()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();

            SupportedMediaTypes.Add(mediaType);
            Encoding = new UTF8Encoding(false, true);
        }

        public static MediaTypeHeaderValue DefaultMediaType
        {
            get { return mediaType; }
        }

        public override bool CanReadType(Type type)
        {
            return CanReadTypeCore(type);
        }

        public override bool CanWriteType(Type type)
        {
            return CanReadTypeCore(type);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            var serializer = JsonSerializer.Create(jsonSerializerSettings);

            return Task.Factory.StartNew(() =>
            {
                using (var streamReader = new StreamReader(readStream, Encoding))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        return serializer.Deserialize(jsonTextReader, type);
                    }
                }
            });
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, TransportContext transportContext)
        {
            var serializer = JsonSerializer.Create(jsonSerializerSettings);

            return Task.Factory.StartNew(() =>
            {
                using (var streamWriter = new StreamWriter(writeStream, Encoding))
                {
                    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                    {
                        serializer.Serialize(jsonTextWriter, value);
                    }
                }
            });
        }

        private static bool CanReadTypeCore(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return true;
        }
    }
}