namespace System1Group.CsvHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class CsvWriter
    {
        private readonly char delimiter;

        private IDictionary<string, IList<string>> data = new Dictionary<string, IList<string>>();
        private IList<string> headers = new List<string>();
        private int records;
        private int active;
        private bool finalised; // TODO: Guard against mutation after finalisation, or work around requiring finalisation

        // public CsvWriter(char delimiter) => this.delimiter = delimiter; // TODO VS2017
        public CsvWriter(char delimiter = ',')
        {
            this.delimiter = delimiter;
        }

        public IReadOnlyCollection<string> Headers => new ReadOnlyCollection<string>(this.headers);

        public void Reset()
        {
            this.headers = new List<string>();
            this.data = new Dictionary<string, IList<string>>();
            this.records = 0;
            this.active = 0;
            this.finalised = false;
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

        private void AddData(string header, string value)
        {
            IList<string> list;
            if (!this.data.TryGetValue(header, out /*var TODO VS2017*/ list))
            {
                this.headers.Add(header);
                list = Enumerable.Repeat(string.Empty, this.active).ToList();
                this.data[header] = list;
            }

            if (list.Count <= this.active)
            {
                list.Add(value);
                return;
            }

            list[this.active] = value;
        }

        public void AddData(CsvToken token)
        {
            if (token.Line == this.active)
            {
                this.AddData(token.Header, token.Value);
                return;
            }

            if (token.Line == this.active + 1)
            {
                this.NextRecord();
                this.AddData(token.Header, token.Value);
                return;
            }

            throw new ArgumentException($"Tokens must be provided in Line order. Got {token.Line} when expecting {this.active} or {this.active + 1}");
        }

        public void NextRecord()
        {
            this.records += 1;
            this.active += 1;

            foreach (var list in this.data.Values)
            {
                if (list.Count != this.records)
                {
                    list.Add(string.Empty);
                }
            }
        }

        public void Finalise()
        {
            this.NextRecord();
            this.finalised = true;
        }

        public string Build()
        {
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

            for (var i = 0; i < this.records; i++)
            {
                for (var j = 0; j < this.headers.Count; j++)
                {
                    sb.Append(this.data[this.headers[j]][i]);

                    if (j + 1 != this.headers.Count)
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
