namespace System1Group.Lib.CsvHandlers.Tests.CsvWriter
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class CsvHandlers_CsvWriter_ResetWithHEaders_Tests
    {
        [Test]
        public void ResetWithHeaders_Ok()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            writer.ResetWithHeaders(new List<string> { "head1", "head2" });
            Assert.AreEqual(2, writer.Headers.Count);
            Assert.AreEqual("head1", writer.Headers.ElementAt(0));
            Assert.AreEqual("head2", writer.Headers.ElementAt(1));

            writer.AddToken(new CsvToken() { Header = "head1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "head2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddToken(new CsvToken() { Header = "head3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "head4", Index = 3, Line = 0, Value = "row1col4" });

            Assert.AreEqual(4, writer.Headers.Count);
            Assert.AreEqual("head1", writer.Headers.ElementAt(0));
            Assert.AreEqual("head2", writer.Headers.ElementAt(1));
            Assert.AreEqual("head3", writer.Headers.ElementAt(2));
            Assert.AreEqual("head4", writer.Headers.ElementAt(3));

            var result = writer.Build();
            Assert.AreEqual("head1\thead2\thead3\thead4\nrow1col1\trow1col2\trow1col3\trow1col4\n", result);
        }

        [Test]
        public void ResetWithHeaders_SkippedColumns()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            writer.ResetWithHeaders(new List<string> { "head1", "head2" });

            Assert.AreEqual(2, writer.Headers.Count);
            Assert.AreEqual("head1", writer.Headers.ElementAt(0));
            Assert.AreEqual("head2", writer.Headers.ElementAt(1));

            writer.AddToken(new CsvToken() { Header = "head3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "head4", Index = 3, Line = 0, Value = "row1col4" });

            Assert.AreEqual(4, writer.Headers.Count);
            Assert.AreEqual("head1", writer.Headers.ElementAt(0));
            Assert.AreEqual("head2", writer.Headers.ElementAt(1));
            Assert.AreEqual("head3", writer.Headers.ElementAt(2));
            Assert.AreEqual("head4", writer.Headers.ElementAt(3));

            var result = writer.Build();
            Assert.AreEqual("head1\thead2\thead3\thead4\n\t\trow1col3\trow1col4\n", result);
        }
    }
}
