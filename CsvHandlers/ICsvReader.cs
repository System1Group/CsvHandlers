namespace System1Group.CsvHandlers
{
    using System.Collections.Generic;

    public interface ICsvReader
    {
        char Delimiter { get; }

        CsvToken Current { get; }

        IReadOnlyCollection<string> Headers { get; }

        bool ReadOut(out CsvToken token);

        bool Read();

        void Dispose();
    }
}
