using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    [ClrJob(true)]
    [RPlotExporter, RankColumn]
    [HtmlExporter]
    public class JsonSerializersBenchmarks : JsonBenchmarkBase
    {
        private Root _root;

        [GlobalSetup]
        public void SetUp()
        {
            _root = JsonConvert.DeserializeObject<Root>(JsonSampleString);
        }


        [Benchmark]
        public string NewtonsoftJson_Serialize()
        {
            return JsonConvert.SerializeObject(_root);
        }

        [Benchmark]
        public string NewtonsoftJson_Serialize_ButBetter()
        {
            var sw = new StringWriter();
            var writer = new JsonTextWriter(sw);

            writer.WriteStartObject();

            writer.WritePropertyName(nameof(Root.total));
            writer.WriteValue(_root.total);

            writer.WritePropertyName(nameof(Root.result));
            writer.WriteStartArray();
            foreach (Result result in _root.result)
            {
                writer.WritePropertyName(nameof(Result.id));
                writer.WriteValue(result.id);
                writer.WritePropertyName(nameof(Result.icon_url));
                writer.WriteValue(result.icon_url);
                writer.WritePropertyName(nameof(Result.url));
                writer.WriteValue(result.url);
                writer.WritePropertyName(nameof(Result.value));
                writer.WriteValue(result.value);
                writer.WritePropertyName(nameof(Result.category));
                writer.WriteStartArray();
                foreach (string cat in result.category)
                {
                    writer.WriteValue(cat);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();

            return sw.ToString();
        }


        [Benchmark]
        public string Manatee_Serialize()
        {
            return new Manatee.Json.Serialization.JsonSerializer().Serialize(_root).ToString();
        }


        [Benchmark]
        public string ServiceStack_Serialize()
        {
            return ServiceStack.Text.JsonSerializer.SerializeToString(_root);
        }


        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _root = null;
        }
    }
}