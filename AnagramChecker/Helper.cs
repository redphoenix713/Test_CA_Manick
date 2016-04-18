using System;
using System.Collections.Generic;
using System.IO;

namespace AnagramChecker
{
    public class Helper
    {
        public static IEnumerable<string> ReadFile(string inputFilePath)
        {
            var lines = new List<string>();
            if (File.Exists(inputFilePath))
            {
                using (var sr = new StreamReader(inputFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: The file {0} doesn't exist\n", inputFilePath);
            }
            return lines;
        }
    }
}