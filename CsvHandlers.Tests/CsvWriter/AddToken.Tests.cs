using NUnit.Framework;

namespace System1Group.CsvHandlers.Tests.CsvWriter
{
    [TestFixture]
    public class CsvHandlers_CsvWriter_AddData_Tests
    {
        [Test]
        public void SingleColSingleRow()
        {
            var writer = new CsvHandlers.CsvWriter('\t');
            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line =  0, Value = "row1col1" });
            var result = writer.Build();
            Assert.AreEqual("header1\nrow1col1\n", result);
        }

        [Test]
        public void MultiColMuliRow()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col2" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            var result = writer.Build();
            Assert.AreEqual("header1\theader2\theader3\nrow1col1\trow1col2\trow1col3\nrow2col1\trow2col2\trow2col3\n", result);
        }

        [Test]
        public void DefaultDelimiter()
        {
            var writer = new CsvHandlers.CsvWriter();

            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col3" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            var result = writer.Build();
            Assert.AreEqual("header1,header2,header3\nrow1col1,row1col2,row1col3\nrow2col3,row2col3,row2col3\n", result);
        }

        [Test]
        public void LastRowNotComplete()
        {
            var writer = new CsvHandlers.CsvWriter();

            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddToken(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddToken(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddToken(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });

            var result = writer.Build();
            Assert.AreEqual("header1,header2,header3\nrow1col1,row1col2,row1col3\nrow2col3,,\n", result);
        }

    }
}
