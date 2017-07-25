namespace System1Group.CsvHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class CsvWriter : ICsvWriter
    {
        private readonly char delimiter;

        private IDictionary<string, IList<string>> data = new Dictionary<string, IList<string>>();
        private IList<string> headers = new List<string>();
        private int rowIndex;

        // public CsvWriter(char delimiter) => this.delimiter = delimiter; // TODO VS2017
        public CsvWriter(IEnumerable<string> headers, char delimiter = ',')
        {
            this.delimiter = delimiter;
            this.ResetWithHeaders(headers);
        }

        public CsvWriter(char delimiter = ',')
        {
            this.delimiter = delimiter;
        }

        public IReadOnlyCollection<string> Headers => new ReadOnlyCollection<string>(this.headers);

        public void Reset()
        {
            this.headers = new List<string>();
            this.data = new Dictionary<string, IList<string>>();
            this.rowIndex = 0;
        }

        public void ResetWithHeaders(IEnumerable<string> headers)
        {
            this.Reset();
            this.headers = headers.ToList();
            foreach (var header in this.headers)
            {
                this.data[header] = new List<string>();
            }
        }

        public void AddCell(string header, string value)
        {
            IList<string> column;
            if (!this.data.TryGetValue(header, out /*var TODO VS2017*/ column))
            {
                this.headers.Add(header);
                column = Enumerable.Repeat(string.Empty, this.rowIndex).ToList();
                this.data[header] = column;
            }

            if (column.Count <= this.rowIndex)
            {
                column.Add(value);
                return;
            }

            column[this.rowIndex] = value;
        }

        public void AddToken(CsvToken token)
        {
            if (token.Line == this.rowIndex)
            {
                this.AddCell(token.Header, token.Value);
                return;
            }

            if (token.Line == this.rowIndex + 1)
            {
                this.AddRow();
                this.AddCell(token.Header, token.Value);
                return;
            }

            throw new ArgumentException($"Tokens must be provided in Line order. Got {token.Line} when expecting {this.rowIndex} or {this.rowIndex + 1}");
        }

        public void AddRow()
        {
            this.FinaliseRow();

            this.rowIndex += 1;
        }

        private void FinaliseRow()
        {
            foreach (var column in this.data.Values)
            {
                if (column.Count != this.rowIndex+1)
                {
                    column.Add(string.Empty);
                }
            }
        }

        public string Build()
        {
            this.FinaliseRow();

            var sb = new StringBuilder();

            for (var i = 0; i < this.headers.Count; i++)
            {
                sb.Append(this.headers[i]);

                if (i + 1 != this.headers.Count)
                {
                    sb.Append(this.delimiter);
                }
            }

            sb.Append('\n');

            for (var row = 0; row <= this.rowIndex; row++)
            {
                for (var col = 0; col < this.headers.Count; col++)
                {
                    sb.Append(this.data[this.headers[col]][row]);

                    if (col + 1 != this.headers.Count)
                    {
                        sb.Append(this.delimiter);
                    }
                }

                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}
