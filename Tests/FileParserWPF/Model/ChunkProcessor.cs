using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FileParserWPF.Model
{
    internal class ChunkProcessor
    {
        private ConcurrentQueue<DataChunk> inbound = new();
        private Action<double> writer;
        public ChunkProcessor(Action<double> writer)
        {
            this.writer = writer;
        }

        public void Enqueue(DataChunk chunk)
        {
            inbound.Enqueue(chunk);
            Task.Run(Process).ConfigureAwait(false);
        }

        private Task Process()
        {
            while (!inbound.IsEmpty)
            {
                if (inbound.TryDequeue(out DataChunk chunk))
                {
                    var result = chunk.type switch
                    {
                        OperationType.Divide => chunk.second switch
                        {
                            0d => double.NaN,
                            _ => chunk.first / chunk.second,
                        },
                        OperationType.Multiply => chunk.first * chunk.second,
                        _ => double.NaN
                    };
                    if (!double.IsNaN(result))
                        writer.Invoke(result);
                }
            }
            return Task.CompletedTask;
        }
    }
}
