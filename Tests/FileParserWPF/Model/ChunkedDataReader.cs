using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileParserWPF.Model
{
    internal class ChunkedDataReader
    {
        private readonly Action<string> filter;
        public ChunkedDataReader(Action<string> filter) => this.filter = filter;

        public async Task ReadAsync(string path, CancellationToken token = default)
        {
            var files = Directory.EnumerateFiles(path).Select(f => f).Where(f => f.Contains(".txt")).ToArray();

            Task[] tasks = new Task[files.Length];
            _ = Parallel.For(0, files.Length, (i, state) =>
               {
                   token.ThrowIfCancellationRequested();
                   tasks[i] = File.ReadAllTextAsync(files[i]).ContinueWith(s => filter.Invoke(s.Result));
               });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
