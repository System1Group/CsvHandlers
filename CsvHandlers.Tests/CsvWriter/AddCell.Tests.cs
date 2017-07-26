namespace System1Group.CsvHandlers.Tests.CsvWriter
{
    using NUnit.Framework;

    [TestFixture]
    public class CsvHandlers_CsvWriter_AddCell_Tests
    {
        [Test]
        public void SingleColSingleRow()
        {
            var writer = new CsvHandlers.CsvWriter('\t');
            writer.AddCell("header1", "row1col1");
            var result = writer.Build();
            Assert.AreEqual("header1\nrow1col1\n", result);
        }

        [Test]
        public void MultiColMuliRow()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddCell("header1", "row1col1");
            writer.AddCell("header2", "row1col2");
            writer.AddCell("header3", "row1col3");

            writer.AddRow();

            writer.AddCell("header1", "row2col1");
            writer.AddCell("header2", "row2col2");
            writer.AddCell("header3", "row2col3");

            var result = writer.Build();
            Assert.AreEqual("header1\theader2\theader3\nrow1col1\trow1col2\trow1col3\nrow2col1\trow2col2\trow2col3\n", result);
        }

        [Test]
        public void DefaultDelimiter()
        {
            var writer = new CsvHandlers.CsvWriter();

            writer.AddCell("header1", "row1col1");
            writer.AddCell("header2", "row1col2");
            writer.AddCell("header3", "row1col3");

            writer.AddRow();

            writer.AddCell("header1", "row2col1");
            writer.AddCell("header2", "row2col2");
            writer.AddCell("header3", "row2col3");

            var result = writer.Build();
            Assert.AreEqual("header1,header2,header3\nrow1col1,row1col2,row1col3\nrow2col1,row2col2,row2col3\n", result);
        }

        [Test]
        public void LastRowIncomplete()
        {
            var writer = new CsvHandlers.CsvWriter();

            writer.AddCell("header1", "row1col1");
            writer.AddCell("header2", "row1col2");
            writer.AddCell("header3", "row1col3");

            writer.AddRow();

            writer.AddCell("header1", "row2col1");

            var result = writer.Build();
            Assert.AreEqual("header1,header2,header3\nrow1col1,row1col2,row1col3\nrow2col1,,\n", result);
        }
    }
}
