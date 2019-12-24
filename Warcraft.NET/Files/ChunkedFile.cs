using Warcraft.NET.Attribute;
using Warcraft.NET.Exceptions;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Warcraft.NET.Files
{
    public abstract class ChunkedFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkedFile"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public ChunkedFile(byte[] inData)
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
                        .Invoke(null, new object[] { br, false });

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
        public byte[] Serialize(long offset = 0)
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
                        .Invoke(null, new object[] { bw, chunkPropertie.GetValue(this), false, true });
                    }
                }

                return ms.ToArray();
            }
        }
    }
}


