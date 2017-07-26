namespace System1Group.CsvHandlers.Tests.CsvWriter
{
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class CsvHandlers_CsvWriter_Reset_Tests
    {
        [Test]
        public void Reset_Ok()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            writer.Reset();
            Assert.AreEqual(0, writer.Headers.Count);

            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });

            Assert.AreEqual(2, writer.Headers.Count);
            Assert.AreEqual("header1", writer.Headers.ElementAt(0));
            Assert.AreEqual("header2", writer.Headers.ElementAt(1));

            var result = writer.Build();
            Assert.AreEqual("header1\theader2\nrow1col1\trow1col2\n", result);
        }
    }
}
