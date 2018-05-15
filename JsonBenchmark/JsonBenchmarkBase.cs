using System;
using System.IO;

namespace JsonBenchmark
{
    public abstract class JsonBenchmarkBase
    {
        private const string TestFilesFolder = "TestFiles";
        private readonly string _jsonSampleString;
        private readonly string _jsonSampleStringAnother;

        private readonly string _chuckPath;
        private readonly string _lolPath;

        protected JsonBenchmarkBase()
        {
            _chuckPath = Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json");
            _lolPath = Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json");
            _jsonSampleString = File.ReadAllText(ChuckPath);
            _jsonSampleStringAnother = File.ReadAllText(LolPath);
        }

        protected string JsonSampleString => _jsonSampleString;

        protected string JsonSampleStringAnother => _jsonSampleStringAnother;

        protected string ChuckPath => _chuckPath;

        protected string LolPath => _lolPath;
    }
}
