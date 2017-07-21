# CsvHandlers
Readers and Writers for CSV data

## Example
```csharp
public string FilterOutRowByHeader(string csv, string filter)
{
    using(var reader = new CsvReader(new StringReader(csv), false, '\t')) 
    {
        var writer = new CsvWriter('\t');
        
        while (reader.ReadOut(out var token))
        {
            if (token.Header != filter)
            {
                writer.AddData(token);
            }
        }

        writer.Finalise();
        return writer.Build();
    }
}

```