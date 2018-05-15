using System.IO;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;

using Manatee.Json;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    [ClrJob(true)]
    [RPlotExporter, RankColumn]
    [HtmlExporter]
    public class JsonDeserializersBenchmarks : JsonBenchmarkBase
    {
        [Benchmark]
        public Root NewtonsoftJson_Deserialize()
        {
            return JsonConvert.DeserializeObject<Root>(JsonSampleString);
        }


        [Benchmark]
        public Root NewtonsoftJson_Deserialize_ButBetter()
        {
            Root result;
            using (var sr = new StreamReader(ChuckPath))
            using (var reader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                result = serializer.Deserialize<Root>(reader);
            }

            return result;
        }


        [Benchmark]
        public Root Manatee_Deserialize()
        {
            var serializer = new Manatee.Json.Serialization.JsonSerializer();
            var jsonValue = JsonValue.Parse(JsonSampleString);
            return serializer.Deserialize<Root>(jsonValue);
        }


        [Benchmark]
        public Root ServiceStack_Deserialize_String()
        {
            return ServiceStack.Text.JsonSerializer.DeserializeFromString<Root>(JsonSampleString);
        }

        [Benchmark]
        public Root ServiceStack_Deserialize_TextReader()
        {
            Root result;

            using (var sr = new StreamReader(ChuckPath))
            {
                result = ServiceStack.Text.JsonSerializer.DeserializeFromReader<Root>(sr);
            }

            return result;
        }


        [Benchmark]
        public Root NewtonsoftJson_Deserialize_AnotherJson()
        {
            return JsonConvert.DeserializeObject<Root>(JsonSampleStringAnother);
        }
    }
}
