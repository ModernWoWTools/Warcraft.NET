using System.Reflection;
using Warcraft.NET.Attribute;
using Warcraft.NET.Docs.Structures;

namespace Warcraft.NET.Docs.Steps
{
    internal class GenerateAutoDocDataStep
    {
        internal static Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<AutoDocChunkVersion, AutoDocChunkImplementation>>>> Process()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<AutoDocChunkVersion, AutoDocChunkImplementation>>>> docData = new();

            var classWithDocs = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Warcraft.NET.dll"))
                .GetTypes()
                .Where(type => !type.IsAbstract)
                .Where(type => type.GetCustomAttribute(typeof(AutoDocFile), false) != null);


            AddAvailableChunks(docData);

            foreach (var classType in classWithDocs)
            {
                var docFile = classType.GetCustomAttribute(typeof(AutoDocFile), false) as AutoDocFile;
                if (docFile == null)
                    continue;


                if (!docData.ContainsKey(docFile.Extension))
                    docData[docFile.Extension] = new();

                if (!docData[docFile.Extension].ContainsKey(docFile.Title))
                    docData[docFile.Extension][docFile.Title] = new();

                AddImplementedChunks(docData[docFile.Extension][docFile.Title], classType);
            }

            return docData;
        }

        private static void AddAvailableChunks(Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<AutoDocChunkVersion, AutoDocChunkImplementation>>>> docData)
        {
            foreach (var file in ChunkAvailability.Chunks)
            {
                if (!docData.ContainsKey(file.Key))
                    docData[file.Key] = new();

                foreach (var title in file.Value)
                {
                    if (!docData[file.Key].ContainsKey(title.Key))
                        docData[file.Key][title.Key] = new();

                    foreach (var chunk in title.Value)
                    {
                        if (!docData[file.Key][title.Key].ContainsKey(chunk))
                            docData[file.Key][title.Key][chunk] = new();

                        foreach (AutoDocChunkVersion chunkVersion in Enum.GetValues(typeof(AutoDocChunkVersion)))
                            docData[file.Key][title.Key][chunk][chunkVersion] = AutoDocChunkImplementation.NotYetImplemented;
                    }
                }
            }
        }

        private static void AddImplementedChunks(Dictionary<string, Dictionary<AutoDocChunkVersion, AutoDocChunkImplementation>> chunks, Type classType)
        {
            foreach (var docChunk in classType.GetProperties())
            {
                foreach (AutoDocChunkVersion chunkVersion in Enum.GetValues(typeof(AutoDocChunkVersion)))
                {
                    var chunkType = docChunk.PropertyType;

                    if (docChunk.PropertyType.IsArray)
                        chunkType = docChunk.PropertyType.GetElementType();

                    if (docChunk.PropertyType.IsGenericType)
                        chunkType = docChunk.PropertyType.GetGenericArguments().First();

                    if (chunkType == null)
                        continue;

                    var chunkName = chunkType.Name;
                    var docChunkAttribute = chunkType.GetCustomAttribute(typeof(AutoDocChunk), false) as AutoDocChunk;

                    if (docChunkAttribute == null)
                        continue;

                    if (!chunks.ContainsKey(chunkName))
                        chunks[chunkName] = new();

                    if (!chunks[chunkName].ContainsKey(chunkVersion))
                        chunks[chunkName][chunkVersion] = AutoDocChunkImplementation.NotYetImplemented;

                    switch (chunks[chunkName][chunkVersion])
                    {
                        case AutoDocChunkImplementation.Implemented:
                            continue;

                        case AutoDocChunkImplementation.ReadOnly:
                            if (docChunkAttribute.ChunkVersion.HasFlag(chunkVersion))
                                chunks[chunkName][chunkVersion] = AutoDocChunkImplementation.Implemented;
                            break;

                        case AutoDocChunkImplementation.NotAvailableInVersion:
                            if (docChunkAttribute.ChunkVersion.HasFlag(chunkVersion))
                                chunks[chunkName][chunkVersion] = AutoDocChunkImplementation.Implemented;

                            if (docChunkAttribute.ChunkReadOnlyInVersion.HasFlag(chunkVersion))
                                chunks[chunkName][chunkVersion] = AutoDocChunkImplementation.ReadOnly;
                            break;

                        case AutoDocChunkImplementation.NotYetImplemented:
                            if (docChunkAttribute.ChunkNotAvailableInVersion.HasFlag(chunkVersion))
                                chunks[chunkName][chunkVersion] = AutoDocChunkImplementation.NotAvailableInVersion;

                            if (docChunkAttribute.ChunkVersion.HasFlag(chunkVersion))
                                chunks[chunkName][chunkVersion] = AutoDocChunkImplementation.Implemented;

                            if (docChunkAttribute.ChunkReadOnlyInVersion.HasFlag(chunkVersion))
                                chunks[chunkName][chunkVersion] = AutoDocChunkImplementation.ReadOnly;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }
            }
        }
    }
}
