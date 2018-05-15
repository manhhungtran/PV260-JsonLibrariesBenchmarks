using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using JsonBenchmark.TestDTOs;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    [ClrJob(isBaseline: true)]
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


        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _root = null;
        }
    }
}