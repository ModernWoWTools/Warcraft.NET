using Warcraft.NET.Docs.Steps;
using Warcraft.NET.Files.M2;
using Warcraft.NET.Files.phys;

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
            string iripath = "C:\\Users\\marce\\Desktop\\WoWStuff\\wow.export\\creature\\iridikron\\";
            byte[] data = File.ReadAllBytes(iripath+"iridikron.m2");
            Model m = new Model(data);

            byte[] phys_test = File.ReadAllBytes("D:\\Unity Projects\\M2Tool\\M2Godot\\buckle_panstart_a_01.phys");
            Physics phys = new Physics(phys_test);

            File.WriteAllBytes(iripath+"iridikron_output.m2", m.Serialize());
            File.WriteAllBytes("D:\\Unity Projects\\M2Tool\\M2Godot\\buckle_panstart_a_01_output.phys", phys.Serialize());



            if (1==2) {


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
}