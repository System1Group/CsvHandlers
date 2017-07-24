namespace System1Group.CsvHandlers
{
    public interface ICsvReader
    {
        bool ReadOut(out CsvToken token);

        bool Read();

        void Dispose();
    }
}
