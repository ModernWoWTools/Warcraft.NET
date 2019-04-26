using Warcraft.NET.Attribute;
using Warcraft.NET.Exceptions;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.ADT.Chunks;
using Warcraft.NET.Files.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Warcraft.NET.Files.ADT.Terrain
{
    public abstract class TerrainBase
    {
        /// <summary>
        /// Gets or sets the contains the ADT version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        /// <summary>
        /// Gets or sets the contains the ADT Header with offsets. The header has offsets to the other chunks in the
        /// ADT.
        /// </summary>
        [ChunkOrder(2)]
        public MHDR Header { get; set; }

        /// <summary>
        /// Gets or sets the contains an array of offsets where MCNKs are in the file.
        /// </summary>
        //public MCNK Chunk { get; set; }

        /// <summary>
        /// Gets or sets the contains the ADT bounding box
        /// ADT.
        /// </summary>
        [ChunkOrder(99), ChunkOptional]
        public MFBO BoundingBox { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainBase(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <summary>
        /// Deserialzes the provided binary data of the object. This is the full data block which follows the data
        /// signature and data block length.
        /// </summary>
        /// <param name="inData">The binary data containing the object.</param>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var terrainChunkProperties = GetType()
                    .GetProperties()
                    .OrderBy(p => ((ChunkOrderAttribute)p.GetCustomAttributes(typeof(ChunkOrderAttribute), false).Single()).Order);

                foreach (PropertyInfo chunkPropertie in terrainChunkProperties)
                {
                    try
                    {
                        IIFFChunk chunk = (IIFFChunk)br
                        .GetType()
                        .GetExtensionMethod(Assembly.GetExecutingAssembly(), "ReadIFFChunk")
                        .MakeGenericMethod(chunkPropertie.PropertyType)
                        .Invoke(null, new[] { br });

                        chunkPropertie.SetValue(this, chunk);
                    }
                    catch (TargetInvocationException ex)
                    {
                        ChunkOptionalAttribute chuckIsOptional = (ChunkOptionalAttribute)chunkPropertie.GetCustomAttribute(typeof(ChunkOptionalAttribute), false);

                        // If chunk is not optional throw the exception
                        if (ex.InnerException.GetType() != typeof(ChunkSignatureNotFoundException) || chuckIsOptional == null || !chuckIsOptional.Optional)
                        {
                            throw ex.InnerException;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <summary>
        /// Serializes the current object into a byte array.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                var terrainChunkProperties = GetType()
                    .GetProperties()
                    .OrderBy(p => ((ChunkOrderAttribute)p.GetCustomAttributes(typeof(ChunkOrderAttribute), false).Single()).Order);

                foreach (PropertyInfo chunkPropertie in terrainChunkProperties)
                {
                    IIFFChunk chunk = (IIFFChunk)chunkPropertie.GetValue(this);

                    if (chunk != null)
                    {
                        bw
                        .GetType()
                        .GetExtensionMethod(Assembly.GetExecutingAssembly(), "WriteIFFChunk")
                        .MakeGenericMethod(chunkPropertie.PropertyType)
                        .Invoke(null, new[] { bw, chunkPropertie.GetValue(this) });
                    }
                }

                return ms.ToArray();
            }
        }
    }
}
