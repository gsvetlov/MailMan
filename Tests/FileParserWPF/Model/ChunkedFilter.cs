using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace FileParserWPF.Model
{
    internal class ChunkedFilter
    {
        private Action<DataChunk> processor;
        private ConcurrentQueue<string> inbound = new();

        public ChunkedFilter(Action<DataChunk> processor)
        {
            this.processor = processor;
        }
        public void Enque(string str)
        {
            inbound.Enqueue(str);
            Task.Run(Filter);
        }

        public Task Filter()
        {
            while (!inbound.IsEmpty)
            {
                if (inbound.TryDequeue(out string str))
                {
                    Debug.WriteLine($"filter: dequeued in {Thread.CurrentThread.ManagedThreadId}");
                    var parts = str.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var chunk = ChunkDecode(parts);
                    if (chunk.valid)
                        processor.Invoke(new DataChunk(chunk.type, chunk.first, chunk.second));
                }
            }
            Debug.WriteLine($"filter shutting down in {Thread.CurrentThread.ManagedThreadId}");
            return Task.CompletedTask;
        }

        private (bool valid, OperationType type, double first, double second) ChunkDecode(string[] parts)
        {
            int type = 0;
            double first = double.NaN;
            double second = double.NaN;

            bool valid =
                parts.Length == 3 &&
                int.TryParse(parts[0], out type) &&
                Enum.IsDefined(typeof(OperationType), type) &&
                double.TryParse(parts[1], out first) &&
                double.TryParse(parts[2], out second);

            return (valid, (OperationType)type, first, second);
        }
    }
}
