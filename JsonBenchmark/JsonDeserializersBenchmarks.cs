using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using JsonBenchmark.TestDTOs;
using Newtonsoft.Json;
using Json;

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
        public Root Json_Deserialize()
        {
            return JsonParser.Deserialize<Root>(JsonSampleString);
        }


        [Benchmark]
        public Root NewtonsoftJson_Deserialize_AnotherJson()
        {
            return JsonConvert.DeserializeObject<Root>(JsonSampleStringAnother);
        }


    }
}
