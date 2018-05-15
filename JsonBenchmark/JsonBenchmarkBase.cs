using System;
using System.IO;

namespace JsonBenchmark
{
    public abstract class JsonBenchmarkBase
    {
        private const string TestFilesFolder = "TestFiles";
        protected string JsonSampleString;
        protected string JsonSampleStringAnother;

        protected string ChuckPath = Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json");
        protected string LolPath = Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json");

        protected JsonBenchmarkBase()
        {
            JsonSampleString = File.ReadAllText(ChuckPath);
            JsonSampleStringAnother = File.ReadAllText(LolPath);
        }
    }
}
