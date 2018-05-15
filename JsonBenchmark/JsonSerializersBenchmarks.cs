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