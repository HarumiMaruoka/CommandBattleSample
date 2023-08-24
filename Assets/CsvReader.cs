// 日本語対応
using System.Collections.Generic;
using System.IO;
using System;

public static class CsvReader
{
    public static List<string[]> ReadCsvFile(string filePath)
    {
        List<string[]> csvData = new List<string[]>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    csvData.Add(values);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading CSV file: " + ex.Message);
        }

        return csvData;
    }

    public static List<string[]> ParseCsv(string csvText)
    {
        List<string[]> csvData = new List<string[]>();

        string[] lines = csvText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] fields = line.Split(',');
            csvData.Add(fields);
        }

        return csvData;
    }
}
