using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace System1Group.CsvHandlers.Tests.CsvReader
{
    [TestFixture]
    public class CsvReader_Read_Tests
    {
        [Test]
        public void SingleRowWithEndNewLine()
        {
            var csv = "col1\tcol2\tcol3\nrow1\trow2\trow3\n";
            var expectedTokens = new List<CsvToken>()
            {
                new CsvToken() { Header = "col1", Index = 0, Line = 0, Value = "row1" },
                new CsvToken() { Header = "col2", Index = 1, Line = 0, Value = "row2" },
                new CsvToken() { Header = "col3", Index = 2, Line = 0, Value = "row3" }
            };

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');

            int index = 0;
            while (reader.Read())
            {
                var token = reader.Current;
                Assert.AreEqual(expectedTokens[index].Header, token.Header);
                Assert.AreEqual(expectedTokens[index].Index, token.Index);
                Assert.AreEqual(expectedTokens[index].Line, token.Line);
                Assert.AreEqual(expectedTokens[index].Value, token.Value);
                index++;
            }

            Assert.AreEqual(expectedTokens.Count, index);
        }

        [Test]
        public void SingleRowWithoutEndNewLine()
        {
            var csv = "col1\tcol2\tcol3\nrow1\trow2\trow3";
            var expectedTokens = new List<CsvToken>()
            {
                new CsvToken() { Header = "col1", Index = 0, Line = 0, Value = "row1" },
                new CsvToken() { Header = "col2", Index = 1, Line = 0, Value = "row2" },
                new CsvToken() { Header = "col3", Index = 2, Line = 0, Value = "row3" }
            };

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');

            int index = 0;
            CsvToken token;
            while (reader.ReadOut(out token))
            {
                Assert.AreEqual(expectedTokens[index].Header, token.Header);
                Assert.AreEqual(expectedTokens[index].Index, token.Index);
                Assert.AreEqual(expectedTokens[index].Line, token.Line);
                Assert.AreEqual(expectedTokens[index].Value, token.Value);
                index++;
            }

            Assert.AreEqual(expectedTokens.Count, index);
        }

        [Test]
        public void MultiRowWithEndNewLine()
        {
            var csv = "col1\tcol2\tcol3\nrow1\trow2\trow3\nrow1c2\trow2c2\trow3c2\n";
            var expectedTokens = new List<CsvToken>()
            {
                new CsvToken() { Header = "col1", Index = 0, Line = 0, Value = "row1" },
                new CsvToken() { Header = "col2", Index = 1, Line = 0, Value = "row2" },
                new CsvToken() { Header = "col3", Index = 2, Line = 0, Value = "row3" },

                new CsvToken() { Header = "col1", Index = 0, Line = 1, Value = "row1c2" },
                new CsvToken() { Header = "col2", Index = 1, Line = 1, Value = "row2c2" },
                new CsvToken() { Header = "col3", Index = 2, Line = 1, Value = "row3c2" }
            };

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');

            int index = 0;
            CsvToken token;
            while (reader.ReadOut(out token))
            {
                Assert.AreEqual(expectedTokens[index].Header, token.Header);
                Assert.AreEqual(expectedTokens[index].Index, token.Index);
                Assert.AreEqual(expectedTokens[index].Line, token.Line);
                Assert.AreEqual(expectedTokens[index].Value, token.Value);
                index++;
            }

            Assert.AreEqual(expectedTokens.Count, index);
        }
    }
}
