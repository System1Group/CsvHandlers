using System.Collections.Generic;
using NUnit.Framework;

namespace System1Group.CsvHandlers.Tests.CsvWriter
{
    [TestFixture]
    public class CsvHandlers_CsvWriter_Reset_Tests
    {
        [Test]
        public void Reset_Ok()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddData(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddData(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddData(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddData(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });
            writer.AddData(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col3" });
            writer.AddData(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            writer.Reset();

            writer.AddData(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddData(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });

            writer.Finalise();
            var result = writer.Build();
            Assert.AreEqual("header1\theader2\nrow1col1\trow1col2\n", result);
        }

        [Test]
        public void ResetWithHeaders_Ok()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddData(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddData(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddData(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddData(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });
            writer.AddData(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col3" });
            writer.AddData(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            writer.ResetWithHeaders(new List<string> { "head1", "head2" });

            writer.AddData(new CsvToken() { Header = "head1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddData(new CsvToken() { Header = "head2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddData(new CsvToken() { Header = "head3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddData(new CsvToken() { Header = "head4", Index = 3, Line = 0, Value = "row1col4" });

            writer.Finalise();
            var result = writer.Build();
            Assert.AreEqual("head1\thead2\thead3\thead4\nrow1col1\trow1col2\trow1col3\trow1col4\n", result);
        }

        [Test]
        public void ResetWithHeaders_SkippedColumns()
        {
            var writer = new CsvHandlers.CsvWriter('\t');

            writer.AddData(new CsvToken() { Header = "header1", Index = 0, Line = 0, Value = "row1col1" });
            writer.AddData(new CsvToken() { Header = "header2", Index = 1, Line = 0, Value = "row1col2" });
            writer.AddData(new CsvToken() { Header = "header3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddData(new CsvToken() { Header = "header1", Index = 0, Line = 1, Value = "row2col3" });
            writer.AddData(new CsvToken() { Header = "header2", Index = 1, Line = 1, Value = "row2col3" });
            writer.AddData(new CsvToken() { Header = "header3", Index = 2, Line = 1, Value = "row2col3" });

            writer.ResetWithHeaders(new List<string> { "head1", "head2" });

            writer.AddData(new CsvToken() { Header = "head3", Index = 2, Line = 0, Value = "row1col3" });
            writer.AddData(new CsvToken() { Header = "head4", Index = 3, Line = 0, Value = "row1col4" });

            writer.Finalise();
            var result = writer.Build();
            Assert.AreEqual("head1\thead2\thead3\thead4\n\t\trow1col3\trow1col4\n", result);
        }


    }
}
