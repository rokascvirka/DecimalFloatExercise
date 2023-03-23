using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DecimalFloatExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string FOLDER_PATH = @"C:\Users\rokas.cvirka\Documents\";
            const string FILE_NAME = "Numbers.txt";
            const int COUNT = 100000;


            FilesManager file = new FilesManager(FOLDER_PATH, FILE_NAME);
            file.CreateFile();
            file.CreateFloats(COUNT);
            file.SumAsDouble();
            Console.WriteLine();
            file.SumAsDecimal();

        }
    }

    public class FilesManager
    {
        const int MIN_VALUE = 1;
        const int MAX_VALUE = 15;

        private string _folderPath;
        private string _fileName;
        private string _filePath => _folderPath + _fileName;
        public FilesManager(string path, string fileName)
        {
            _folderPath = path;
            _fileName = fileName;
        }

        public void CreateFile()
        {
            var myFile = File.Create(_filePath);
            myFile.Close();
        }

        public void CreateFloats(int count)
        {
            string text = GenerateRandomFloats(MIN_VALUE, MAX_VALUE, count);

            using (StreamWriter writer = new StreamWriter(_filePath, append: false))
            {
                writer.Write(text);
            }
        }


        public void SumAsDecimal()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            decimal sumAsDecimal = 0;

            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sumAsDecimal += Convert.ToDecimal(line, CultureInfo.InvariantCulture);
                }
            }

            Console.WriteLine($"Sum as decimal: {sumAsDecimal}");
            stopwatch.Stop();
            Console.WriteLine("Time taken: " + stopwatch.Elapsed.TotalSeconds);
        }
    

        public void SumAsDouble()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            double sumAsDouble = 0;

            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sumAsDouble += Convert.ToDouble(line, CultureInfo.InvariantCulture);
                }
            }

            Console.WriteLine($"Sum as double: {sumAsDouble}");
            stopwatch.Stop();
            Console.WriteLine("Time taken: " + stopwatch.Elapsed.TotalSeconds);
        }

        private string GenerateRandomFloats(int minValue, int maxValue, int count)
        {
            Random random = new Random();
            double range = maxValue - minValue;
            string text = "";
            for (int i = 0; i < count; i++)
            {
                double sample = random.NextDouble();
                double scaled = (sample * range) + minValue;
                double f = scaled; ;

                text += f.ToString(CultureInfo.InvariantCulture) + "\n";  
            }
            return text;
        }
    }
}