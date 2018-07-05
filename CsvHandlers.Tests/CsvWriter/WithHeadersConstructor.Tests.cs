namespace System1Group.Lib.CsvHandlers.Tests.CsvWriter
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class WithHeadersConstructor
    {
        [Test]
        public void DefaultDelimiter()
        {
            var headers = new List<string>()
            {
                "head1",
                "head2",
                "head3"
            };

            var writer = new CsvHandlers.CsvWriter(headers);

            Assert.AreEqual(3, writer.Headers.Count);
            Assert.AreEqual("head1", writer.Headers.ElementAt(0));
            Assert.AreEqual("head2", writer.Headers.ElementAt(1));
            Assert.AreEqual("head3", writer.Headers.ElementAt(2));

            writer.AddCell("head3", "r1c3");
            writer.AddCell("head4", "r1c4");

            var result = writer.Build();
            Assert.AreEqual("head1,head2,head3,head4\n,,r1c3,r1c4\n", result);
        }

        [Test]
        public void CustomDelimiter()
        {
            var headers = new List<string>()
            {
                "head1",
                "head2",
                "head3"
            };

            var writer = new CsvHandlers.CsvWriter(headers, '|');

            Assert.AreEqual(3, writer.Headers.Count);
            Assert.AreEqual("head1", writer.Headers.ElementAt(0));
            Assert.AreEqual("head2", writer.Headers.ElementAt(1));
            Assert.AreEqual("head3", writer.Headers.ElementAt(2));

            writer.AddCell("head3", "r1c3");
            writer.AddCell("head4", "r1c4");

            var result = writer.Build();
            Assert.AreEqual("head1|head2|head3|head4\n||r1c3|r1c4\n", result);
        }
    }
}
