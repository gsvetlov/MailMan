using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileParserWPF.Model
{
    internal class ChunkedDataReader
    {
        private Action<string> filter;
        public ChunkedDataReader(Action<string> filter)
        {
            this.filter = filter;
        }
        public async Task ReadAsync(string path, IProgress<int> progress)
        {
            int count = 0;
            await foreach (var chunk in GetFilenameAsync(path).ConfigureAwait(false))
            {
                Debug.WriteLine($"sending chunk to filter in {Thread.CurrentThread.ManagedThreadId}");
                filter.Invoke(chunk);
                progress.Report(++count);
            }
        }
        private static Random rand = new();
        private async IAsyncEnumerable<string> GetFilenameAsync(string path)
        {
            foreach (var file in Directory.EnumerateFiles(path).Select(f => f).Where(f => f.Contains(".txt")))
            {
                Debug.WriteLine($"processing {file} in {Thread.CurrentThread.ManagedThreadId}");
                var s = await File.ReadAllTextAsync(file).ConfigureAwait(false);
                await Task.Delay(rand.Next(500)).ConfigureAwait(false);
                Debug.WriteLine($"processed {file} in {Thread.CurrentThread.ManagedThreadId}");
                yield return s;
            }
        }
    }
}
