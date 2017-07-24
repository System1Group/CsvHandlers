using System.Collections.Generic;

namespace System1Group.CsvHandlers
{
    public interface ICsvWriter
    {
        void Reset();

        void ResetWithHeaders(IEnumerable<string> headers);

        void AddData(string header, string value);

        void AddData(CsvToken token);

        void NextRecord();

        string Build();
    }
}
