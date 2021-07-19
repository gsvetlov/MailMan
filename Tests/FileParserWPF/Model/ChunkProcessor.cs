using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FileParserWPF.Model
{
    internal class ChunkProcessor
    {
        private readonly Action<double> writer;
        public ChunkProcessor(Action<double> writer)
        {
            this.writer = writer;
        }

        public void Process(DataChunk chunk)
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
}
