using NUnit.Framework;

namespace System1Group.CsvHandlers.Tests.CsvWriter
{
    [TestFixture]
    public class DuplicateColumnNames
    {
        [Test]
        public void DuplicateTest()
        {
            var writer = new CsvHandlers.CsvWriter('\t');
        }
    }
}
