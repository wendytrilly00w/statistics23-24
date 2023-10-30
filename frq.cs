using System;
using System.IO;
using OfficeOpenXml;

class ExcelAnalyzer
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Inserisci il percorso del file Excel (.xlsx):");
        string filePath = Console.ReadLine();

        if (File.Exists(filePath) && filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Considera il primo foglio

                // Analizza i dati e calcola le frequenze
                var data = GetDataFromWorksheet(worksheet);
                CalculateFrequency(data, "Hard Worker (0-5)");
                CalculateFrequency(data, "Age");
                CalculateFrequency(data, "Background (degree)");
                CalculateJointDistribution(data, "Hard Worker (0-5)", "Age");
            }
        }
        else
        {
            Console.WriteLine("File non valido. Assicurati che il percorso sia corretto e che il file sia in formato .xlsx.");
        }
    }

    private static dynamic GetDataFromWorksheet(ExcelWorksheet worksheet)
    {
        var data = new System.Dynamic.ExpandoObject();

        int rowCount = worksheet.Dimension.Rows;cv  
        int colCount = worksheet.Dimension.Columns;

        for (int row = 2; row <= rowCount; row++)
        {
            var rowData = new System.Dynamic.ExpandoObject();

            for (int col = 1; col <= colCount; col++)
            {
                var header = worksheet.Cells[1, col].Text;
                var cellValue = worksheet.Cells[row, col].Text;

                ((IDictionary<string, object>)rowData)[header] = cellValue;
            }

            ((IDictionary<string, object>)data)[row.ToString()] = rowData;
        }

        return data;
    }

    private static void CalculateFrequency(dynamic data, string variableName)
    {
        var frequencies = new Dictionary<string, int>();
        var totalEntries = ((IDictionary<string, object>)data).Count;

        foreach (var row in (IEnumerable<KeyValuePair<string, object>>)data)
        {
            var value = ((IDictionary<string, object>)row.Value)[variableName].ToString();

            if (frequencies.ContainsKey(value))
            {
                frequencies[value]++;
            }
            else
            {
                frequencies[value] = 1;
            }
        }

        Console.WriteLine($"Analisi della variabile: {variableName}");
        Console.WriteLine("Valore\tFrequenza Assoluta\tFrequenza Relativa\tPercentuale");
        foreach (var entry in frequencies)
        {
            var value = entry.Key;
            var frequency = entry.Value;
            var relativeFrequency = (double)frequency / totalEntries;
            var percentage = (relativeFrequency * 100).ToString("F2");
            Console.WriteLine($"{value}\t{frequency}\t{relativeFrequency:F2}\t{percentage}%");
        }
        Console.WriteLine();
    }

    private static void CalculateJointDistribution(dynamic data, string variable1, string variable2)
    {
        var jointDistribution = new Dictionary<string, int>();

        foreach (var row in (IEnumerable<KeyValuePair<string, object>)data)
        {
            var value1 = ((IDictionary<string, object>)row.Value)[variable1].ToString();
            var value2 = ((IDictionary<string, object>)row.Value)[variable2].ToString();

            var key = $"{value1} | {value2}";

            if (jointDistribution.ContainsKey(key))
            {
                jointDistribution[key]++;
            }
            else
            {
                jointDistribution[key] = 1;
            }
        }

        Console.WriteLine($"Distribuzione Congiunta tra {variable1} e {variable2}");
        Console.WriteLine("Valore\tFrequenza Congiunta\tFrequenza Relativa Congiunta\tPercentuale Congiunta");
        var totalEntries = ((IDictionary<string, object>)data).Count;
        foreach (var entry in jointDistribution)
        {
            var key = entry.Key;
            var frequency = entry.Value;
            var relativeFrequency = (double)frequency / totalEntries;
            var percentage = (relativeFrequency * 100).ToString("F2");
            Console.WriteLine($"{key}\t{frequency}\t{relativeFrequency:F2}\t{percentage}%");
        }
    }
}
