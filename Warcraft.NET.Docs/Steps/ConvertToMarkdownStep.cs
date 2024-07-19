using System.Text;
using Warcraft.NET.Attribute;
using Warcraft.NET.Docs.Structures;

namespace Warcraft.NET.Docs.Steps
{
    internal class ConvertToMarkdownStep
    {
        private const string Header = "✔ = Reading & Writing<br>\r\nR = Read Only<br>\r\nN/A = Not in this file for this expansion<br>\r\n**NYI** = Not Yet Implemented / TODO\r\n";

        internal static void Process(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<AutoDocChunkVersion, AutoDocChunkImplementation>>>> docData, string outputFolder)
        {
            if (Directory.Exists(outputFolder))
                Directory.Delete(outputFolder, true);

            Directory.CreateDirectory(outputFolder);

            foreach (var file in docData)
            {
                StringBuilder sb = new();
                sb.AppendLine(Header);

                foreach (var title in file.Value)
                {
                    sb.AppendLine($"### {title.Key}");
                    var usedChunkVersions = GetUsedChunkVersions(title.Value);

                    sb.AppendLine(GenerateTableHeader(usedChunkVersions));

                    foreach (var chunk in title.Value)
                        sb.AppendLine(GenerateTableRow(chunk, usedChunkVersions));

                    sb.AppendLine();
                }

                File.AppendAllText(Path.Combine(outputFolder, $"{file.Key.ToUpper()}.md"), sb.ToString());
            }
        }

        private static HashSet<AutoDocChunkVersion> GetUsedChunkVersions(Dictionary<string, Dictionary<AutoDocChunkVersion, AutoDocChunkImplementation>> chunks)
        {

            HashSet<AutoDocChunkVersion> usedChunkVersions = new HashSet<AutoDocChunkVersion>();

            foreach (var chunk in chunks)
            {
                foreach (AutoDocChunkVersion chunkVersion in Enum.GetValues(typeof(AutoDocChunkVersion)))
                {
                    if (chunk.Value.Keys.Contains(chunkVersion))
                        usedChunkVersions.Add(chunkVersion);
                }
            }

            return usedChunkVersions;
        }

        private static string GenerateTableHeader(HashSet<AutoDocChunkVersion> usedChunkVersions)
        {
            StringBuilder sb = new();

            sb.Append("|Chunk|");
            foreach (var version in usedChunkVersions)
                sb.Append($"{Enum.GetName(version.GetType(), version)}|");

            sb.AppendLine();

            sb.Append("|-----|");
            foreach (var _ in usedChunkVersions)
                sb.Append("-----|");

            return sb.ToString();
        }

        private static string GenerateTableRow(KeyValuePair<string, Dictionary<AutoDocChunkVersion, AutoDocChunkImplementation>> chunk, HashSet<AutoDocChunkVersion> usedChunkVersions)
        {
            StringBuilder sb = new();

            sb.Append($"|{chunk.Key}|");
            foreach (var version in usedChunkVersions)
            {
                if (!chunk.Value.ContainsKey(version))
                {
                    sb.Append("**NYI**");
                    continue;
                }

                sb.Append(chunk.Value[version] switch
                {
                    AutoDocChunkImplementation.Implemented => "✔",
                    AutoDocChunkImplementation.ReadOnly => "R",
                    AutoDocChunkImplementation.NotYetImplemented => "**NYI**",
                    AutoDocChunkImplementation.NotAvailableInVersion => "N/A",
                    _ => "**NYI**"
                });

                sb.Append("|");
            }

            return sb.ToString();
        }
    }
}
