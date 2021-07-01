using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FileParserWPF.Model;

namespace FileParserWPF.Data
{
    internal static class DataGenerator
    {
        private static Random random = new Random();
        private static StringBuilder sbPath = new();
        private static StringBuilder sbData = new();
        public static async Task GenerateData(string path, int count, CancellationToken token = default)
        {
            CreateDirectoryIfNotExist(path);
            var tasks = Enumerable.Range(1, count).Select(i =>
            {
                token.ThrowIfCancellationRequested();

                sbPath.Clear().Append(path).Append($"\\datachunk-{i}.txt");
                var chunk = NextChunk();
                sbData.Clear().AppendJoin(" ", (int)chunk.type, chunk.first, chunk.second);

                Task task = File.WriteAllTextAsync(sbPath.ToString(), sbData.ToString(), Encoding.UTF8, token);
                return task;
            }).ToArray();
            await Task.WhenAll(tasks);
        }

        private static DataChunk NextChunk() => new((OperationType)(random.Next(2) + 1),
                                                    random.NextDouble() * double.MaxValue,
                                                    random.NextDouble() * double.MaxValue);
        public static void CreateDirectoryIfNotExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
