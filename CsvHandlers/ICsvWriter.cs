using System.Collections.Generic;

namespace System1Group.CsvHandlers
{
    public interface ICsvWriter
    {
        void Reset();

        void ResetWithHeaders(IEnumerable<string> headers);

        void AddCell(string header, string value);

        void AddToken(CsvToken token);

        void AddRow();

        string Build();
    }
}
