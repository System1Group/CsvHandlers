namespace System1Group.Lib.CsvHandlers.Tests.CsvReader
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class CsvReader_Headers_Tests
    {
        [Test]
        public void HeadersOnly()
        {
            var csv = "col1\tcol2\tcol3";

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');
            var headers = reader.Headers;

            Assert.AreEqual(headers.ElementAt(0), "col1");
            Assert.AreEqual(headers.ElementAt(1), "col2");
            Assert.AreEqual(headers.ElementAt(2), "col3");

            Assert.AreEqual(headers.Count, 3);
        }

        [Test]
        public void HeadersOnlyEndNewLine()
        {
            var csv = "col1\tcol2\tcol3\n";

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');
            var headers = reader.Headers;

            Assert.AreEqual(headers.ElementAt(0), "col1");
            Assert.AreEqual(headers.ElementAt(1), "col2");
            Assert.AreEqual(headers.ElementAt(2), "col3");

            Assert.AreEqual(headers.Count, 3);
        }

        [Test]
        public void WithRows()
        {
            var csv = "col1\tcol2\tcol3\nr1\tr2\tr3";

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');
            var headers = reader.Headers;

            Assert.AreEqual(headers.ElementAt(0), "col1");
            Assert.AreEqual(headers.ElementAt(1), "col2");
            Assert.AreEqual(headers.ElementAt(2), "col3");

            Assert.AreEqual(headers.Count, 3);
        }

        [Test]
        public void DefaultSeperator()
        {
            var csv = "col1,col2,col3\nr1,r2,r3";

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false);
            var headers = reader.Headers;

            Assert.AreEqual(headers.ElementAt(0), "col1");
            Assert.AreEqual(headers.ElementAt(1), "col2");
            Assert.AreEqual(headers.ElementAt(2), "col3");

            Assert.AreEqual(headers.Count, 3);
        }

        [Test]
        public void CarrigeReturnNewLine()
        {
            var csv = "col1,col2,col3\r\nr1,r2,r3";

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false);
            var headers = reader.Headers;

            Assert.AreEqual(headers.ElementAt(0), "col1");
            Assert.AreEqual(headers.ElementAt(1), "col2");
            Assert.AreEqual(headers.ElementAt(2), "col3");

            Assert.AreEqual(headers.Count, 3);
        }

        [Test]
        public void CarrigeReturn()
        {
            var csv = "col1,col2,col3\rr1,r2,r3";

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false);
            var headers = reader.Headers;

            Assert.AreEqual(headers.ElementAt(0), "col1");
            Assert.AreEqual(headers.ElementAt(1), "col2");
            Assert.AreEqual(headers.ElementAt(2), "col3");

            Assert.AreEqual(headers.Count, 3);
        }

        [Test]
        public void RowLongerThanHeaders()
        {
            var csv = "col1\tcol2\tcol3\nr1\tr2\tr3\tr4";

            var reader = new CsvHandlers.CsvReader(new StringReader(csv), false, '\t');
            var headers = reader.Headers;

            Assert.AreEqual(headers.ElementAt(0), "col1");
            Assert.AreEqual(headers.ElementAt(1), "col2");
            Assert.AreEqual(headers.ElementAt(2), "col3");

            Assert.AreEqual(headers.Count, 3);
        }
    }
}
