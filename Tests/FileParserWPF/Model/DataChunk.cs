using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParserWPF.Model
{
    internal record DataChunk(OperationType type, double first, double second)
    {
        
    }

    internal enum OperationType
    {
        Multiply = 1,
        Divide = 2
    }
}
