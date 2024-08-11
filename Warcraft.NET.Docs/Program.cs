using Warcraft.NET.Docs.Steps;
using Warcraft.NET.Files.M2;
using Warcraft.NET.Files.phys;
using Warcraft.NET.Files.SKIN;

namespace Warcraft.NET.Docs
{
    internal class Program
    {
        /// <summary>
        /// @TODO: Add output folder as argument
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine($"BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");

            if (args.Length == 0)
                throw new System.Exception("Please provide an output folder");

            string outputFolder = Path.GetFullPath(args[0]);
            if (!Directory.Exists(outputFolder))
                throw new Exception("Output folder does not exist");

            Console.WriteLine($"Output folder: {outputFolder}");

            Console.WriteLine("Generating documentation...");
            var autoDocData = GenerateAutoDocDataStep.Process();

            Console.WriteLine("Converting to markdown...");
            ConvertToMarkdownStep.Process(autoDocData, outputFolder);

            Console.WriteLine("Done!");
            
        }
    }
}