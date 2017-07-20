namespace System1Group.CsvHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;

    public class CsvReader : IDisposable
    {
        private readonly StringReader reader;

        private readonly bool ignoreCommasInExplicitStrings;

        private List<string> headers;

        private int line;

        private int index;

        public CsvReader(StringReader reader, bool ignoreCommasInExplicitStrings, char delimiter)
        {
            this.reader = reader;
            this.ignoreCommasInExplicitStrings = ignoreCommasInExplicitStrings;
            this.Delimiter = delimiter;
            this.InitialiseHeaders();
        }

        public CsvReader(StringReader reader, bool ignoreCommasInExplicitStrings) : this(reader, ignoreCommasInExplicitStrings, ',')
        {
        }

        public char Delimiter { get; set; }

        public CsvToken Current { get; private set; }

        public IReadOnlyCollection<string> Headers => new ReadOnlyCollection<string>(this.headers);

        public bool ReadOut(out CsvToken token)
        {
            if (this.Read())
            {
                token = this.Current;
                return true;
            }

            token = null;
            return false;
        }

        public bool Read()
        {
            int c;
            var token = new CsvToken { Line = this.line, Index = this.index, Header = this.index < this.headers.Count ? this.headers[this.index] : null };
            var sb = new StringBuilder();
            var inExplicitString = false;
            while ((c = this.reader.Read()) != -1)
            {
                if (c == '\r' || c == '\n' || c == this.Delimiter)
                {
                    if (c == '\r' && this.reader.Peek() == '\n')
                    {
                        this.reader.Read();
                    }

                    if (c == '\r' || c == '\n')
                    {
                        this.line += 1;
                        this.index = 0;
                    }

                    if (c == this.Delimiter)
                    {
                        if (inExplicitString)
                        {
                            sb.Append(this.Delimiter);
                            continue;
                        }

                        this.index += 1;
                    }

                    token.Value = sb.ToString();
                    this.Current = token;
                    return true;
                }

                if (this.ignoreCommasInExplicitStrings && c == '"')
                {
                    inExplicitString = !inExplicitString;
                }

                sb.Append((char)c);
            }

            if (token.Value == null)
            {
                this.Current = null;
                return false;
            }

            this.Current = token;
            return true;
        }

        private void InitialiseHeaders()
        {
            this.headers = new List<string>();

            int c;
            var sb = new StringBuilder();
            while ((c = this.reader.Read()) != -1)
            {
                if (c == '\r' || c == '\n')
                {
                    if (c == '\r' && this.reader.Peek() == '\n')
                    {
                        this.reader.Read();
                    }

                    this.headers.Add(sb.ToString());
                    return;
                }

                if (c == this.Delimiter)
                {
                    this.headers.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }

                sb.Append((char)c);
            }
        }

        public void Dispose()
        {
            this.reader?.Dispose();
        }
    }
}
