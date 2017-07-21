using System.IO;
using NUnit.Framework;

namespace System1Group.CsvHandlers.Tests.CsvReader
{
    [TestFixture]
    public class CsvReader_ReadOut_Tests
    {
        [Test]
        public void SingleRowWithEndNewLine()
        {
            var csv = "col1\tcol2\tcol3\nrow1\trow2\trow3\n";
            var expectedTokens = new string[] { "row1", "row2", "row3" };

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');

            int index = 0;
            CsvToken token;
            while (reader.ReadOut(out token))
            {
                Assert.AreEqual(expectedTokens[index], token.Value);
                index++;
            }

            Assert.AreEqual(expectedTokens.Length, index);
        }

        [Test]
        public void SingleRowWithoutEndNewLine()
        {
            var csv = "col1\tcol2\tcol3\nrow1\trow2\trow3";
            var expectedTokens = new string[] { "row1", "row2", "row3" };

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');

            int index = 0;
            CsvToken token;
            while (reader.ReadOut(out token))
            {
                Assert.AreEqual(expectedTokens[index], token.Value);
                index++;
            }

            Assert.AreEqual(expectedTokens.Length, index);
        }
    }
}
