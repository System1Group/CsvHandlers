namespace System1Group.CsvHandlers.Tests
{
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    public class CsvReader_Tests
    {
        [Test]
        public void Test()
        {
            var csv = "col1\tcol2\tcol3\nrow1\trow2\trow3\n";
            var expectedTokens = new string[] { "col1", "col2", "col3", "row1", "row2", "row3" };

            var reader = new CsvReader(new StringReader(csv), false, '\t');

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
