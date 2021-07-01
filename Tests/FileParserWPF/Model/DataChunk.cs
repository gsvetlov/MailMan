namespace FileParserWPF.Model
{
    internal record DataChunk(OperationType type, double first, double second) { }

    internal enum OperationType
    {
        Multiply = 1,
        Divide = 2
    }
}
