using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Warcraft.NET.Attribute;
using Warcraft.NET.Exceptions;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files
{
    public abstract class ChunkedFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkedFile"/> class.
        /// </summary>
        public ChunkedFile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkedFile"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public ChunkedFile(byte[] inData)
        {
            if (inData != null)
            {
                LoadBinaryData(inData);
            }
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
                var chunkProperties = GetType()
                    .GetProperties()
                    .Where(p => (ChunkIgnoreAttribute)p.GetCustomAttribute(typeof(ChunkIgnoreAttribute), false) == null)
                    .OrderBy(p => ((ChunkOrderAttribute)p.GetCustomAttributes(typeof(ChunkOrderAttribute), false).FirstOrDefault())?.Order ?? short.MaxValue);

                foreach (PropertyInfo chunkProperty in chunkProperties)
                {
                    try
                    {
                        ChunkArrayAttribute chunkArray = (ChunkArrayAttribute)chunkProperty.GetCustomAttribute(typeof(ChunkArrayAttribute), false);

                        if (chunkArray != null && chunkProperty.PropertyType.IsArray)
                        {
                            var chunks = Array.CreateInstance(chunkProperty.PropertyType.GetElementType(), chunkArray.Length);
                            ms.Seek(0, SeekOrigin.Begin);

                            for (int i = 0; i < chunkArray.Length; i++)
                            {
                                IIFFChunk chunk = (IIFFChunk)br
                                .GetType()
                                .GetExtensionMethod(Assembly.GetExecutingAssembly(), "ReadIFFChunk")
                                .MakeGenericMethod(chunkProperty.PropertyType.GetElementType())
                                .Invoke(null, new object[] { br, false, false, IsReverseSignature() });

                                chunks.SetValue(chunk, i);
                            }

                            chunkProperty.SetValue(this, chunks);
                        }
                        else
                        {
                            IIFFChunk chunk = (IIFFChunk)br
                            .GetType()
                            .GetExtensionMethod(Assembly.GetExecutingAssembly(), "ReadIFFChunk")
                            .MakeGenericMethod(chunkProperty.PropertyType)
                            .Invoke(null, new object[] { br, false, true, IsReverseSignature() });

                            chunkProperty.SetValue(this, chunk);
                        }
                    }
                    catch (TargetInvocationException ex)
                    {
                        ChunkOptionalAttribute chunkIsOptional = (ChunkOptionalAttribute)chunkProperty.GetCustomAttribute(typeof(ChunkOptionalAttribute), false);

                        // If chunk is not optional throw the exception
                        if (ex.InnerException.GetType() != typeof(ChunkSignatureNotFoundException) || chunkIsOptional == null || !chunkIsOptional.Optional)
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
                    .Where(p => (ChunkIgnoreAttribute)p.GetCustomAttribute(typeof(ChunkIgnoreAttribute), false) == null)
                    .OrderBy(p => ((ChunkOrderAttribute)p.GetCustomAttributes(typeof(ChunkOrderAttribute), false).FirstOrDefault())?.Order ?? short.MaxValue);

                foreach (PropertyInfo chunkPropertie in terrainChunkProperties)
                {
                    ChunkArrayAttribute chunkArray = (ChunkArrayAttribute)chunkPropertie.GetCustomAttribute(typeof(ChunkArrayAttribute), false);

                    if (chunkArray != null && chunkPropertie.PropertyType.IsArray)
                    {
                        IIFFChunk[] chunks = (IIFFChunk[])chunkPropertie.GetValue(this);

                        if (chunks != null)
                        {
                            foreach (IIFFChunk chunk in chunks)
                            {
                                if (chunk != null)
                                {
                                    bw
                                    .GetType()
                                    .GetExtensionMethod(Assembly.GetExecutingAssembly(), "WriteIFFChunk")
                                    .MakeGenericMethod(chunkPropertie.PropertyType.GetElementType())
                                    .Invoke(null, new object[] { bw, chunk, false, IsReverseSignature() });
                                }
                            }
                        }
                    }
                    else
                    {
                        IIFFChunk chunk = (IIFFChunk)chunkPropertie.GetValue(this);

                        if (chunk != null)
                        {
                            bw
                            .GetType()
                            .GetExtensionMethod(Assembly.GetExecutingAssembly(), "WriteIFFChunk")
                            .MakeGenericMethod(chunkPropertie.PropertyType)
                            .Invoke(null, new object[] { bw, chunk, false, IsReverseSignature() });
                        }
                    }
                }

                return ms.ToArray();
            }
        }

        public virtual bool IsReverseSignature()
        {
            return true;
        }
    }
}


