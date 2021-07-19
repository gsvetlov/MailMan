using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FileParserWPF.Model
{
    internal class ChunkedFilter
    {
        private readonly Action<DataChunk> processor;

        public ChunkedFilter(Action<DataChunk> processor)
        {
            this.processor = processor;
        }
        public void Filter(string str)
        {
            var parts = str.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var (valid, type, first, second) = ChunkDecode(parts);
            if (valid)
                processor.Invoke(new DataChunk(type, first, second));
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
