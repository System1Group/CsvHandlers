namespace System1Group.Lib.CsvHandlers
{
    using System.Collections.Generic;

    public interface ICsvWriter
    {
        void Reset();

        void ResetWithHeaders(IEnumerable<string> headers);

        void AddCell(string header, string value);

        void AddToken(CsvToken token);

        void NextRow();

        string Build();
    }
}
