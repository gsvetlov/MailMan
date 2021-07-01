using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileParserWPF.Model
{
    internal class ChunkWriter
    {
        public static readonly string DEFAULT_FILENAME = "result.dat";
        private BlockingCollection<double> inbound = new();
        private string filename;
        private CancellationTokenSource cts = new();
        public string Filename { get => filename; set => filename = value; }
        public ChunkWriter(string filename = null)
        {
            this.filename = filename ?? DEFAULT_FILENAME;
            Task.Run(ConsumeInbound).ConfigureAwait(false);
        }

        public void Close() => cts.Cancel();

        public void Write(double value)
        {
            inbound.Add(value);
        }
        private Task ConsumeInbound()
        {
            using FileStream fs = new(filename, FileMode.Create, FileAccess.Write);
            using BinaryWriter writer = new(fs);
            while (!cts.IsCancellationRequested)
            {
                try
                {

                    var value = inbound.Take(cts.Token);
                    writer.Write(value);
                }
                catch (OperationCanceledException)
                {
                    writer.Flush();
                    fs.Flush();
                }
            }
            return Task.CompletedTask;
        }
    }
}
