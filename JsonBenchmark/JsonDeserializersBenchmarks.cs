using System.IO;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;

using Manatee.Json;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    [ClrJob(isBaseline: true)]
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
            Root ror;
            using (var sr = new StreamReader(ChuckPath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                ror = serializer.Deserialize<Root>(reader);
            }

            return ror;
        }


        [Benchmark]
        public Root Manatee_Deserialize()
        {
            var ser = new Manatee.Json.Serialization.JsonSerializer();
            var lol = JsonValue.Parse(JsonSampleString);
            return ser.Deserialize<Root>(lol);
        }


        [Benchmark]
        public Root ServiceStack_Deserialize()
        {
            return ServiceStack.Text.JsonSerializer.DeserializeFromString<Root>(JsonSampleString);
        }


        [Benchmark]
        public Root NewtonsoftJson_Deserialize_AnotherJson()
        {
            return JsonConvert.DeserializeObject<Root>(JsonSampleStringAnother);
        }
    }
}
