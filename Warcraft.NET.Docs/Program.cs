using Warcraft.NET.Docs.Steps;

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
            if (args.Length == 0)
                throw new System.Exception("Please provide an output folder");

            string outputFolder = Path.GetFullPath(args[0]);
            if (!Directory.Exists(outputFolder))
                throw new Exception("Output folder does not exist");

            var autoDocData = GenerateAutoDocDataStep.Process();
            ConvertToMarkdownStep.Process(autoDocData, outputFolder);
        }
    }
}