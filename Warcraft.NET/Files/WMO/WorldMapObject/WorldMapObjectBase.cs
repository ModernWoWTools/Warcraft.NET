using System.IO;
using System.Linq;
using System.Reflection;
using Warcraft.NET.Attribute;
using Warcraft.NET.Exceptions;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.WMO.Chunks;

namespace Warcraft.NET.Files.WMO.WorldMapObject
{
    public abstract class WorldMapObjectBase
    {
        /// <summary>
        /// Gets or sets the contains the WMO version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectBase"/> class.
        /// </summary>
        public WorldMapObjectBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectBase(byte[] inData)
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

                foreach (PropertyInfo chunkProperty in terrainChunkProperties)
                {
                    try
                    {
                        var chunk = (IIFFChunk)br
                        .GetType()
                        .GetExtensionMethod(Assembly.GetExecutingAssembly(), "ReadIFFChunk")
                        .MakeGenericMethod(chunkProperty.PropertyType)
                        .Invoke(null, new[] { br });

                        chunkProperty.SetValue(this, chunk);
                    }
                    catch (TargetInvocationException ex)
                    {
                        var chuckIsOptional = (ChunkOptionalAttribute)chunkProperty.GetCustomAttribute(typeof(ChunkOptionalAttribute), false);

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
        public abstract byte[] Serialize();
    }
}
