using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WMO.Chunks
{
    /// <summary>
    /// MODN Chunk - Contains pathes to used models
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionBeforeBfA, AutoDocChunkVersionHelper.VersionAfterLegion)]
    public class MODN : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MODN";

        /// <summary>
        /// Gets a dictionary of the model offsets mapped to model file pathes.
        /// </summary>
        public Dictionary<long, string> Models { get; } = new();

        /// <summary>
        /// Next model offset
        /// </summary>
        protected long NextOffset = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MODN"/> class.
        /// </summary>
        public MODN()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODN"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MODN(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                while (ms.Position < ms.Length)
                {
                    if (ms.Position % 4 == 0)
                        Models.Add(ms.Position, br.ReadNullTerminatedString());
                    else
                        ms.Position += 4 - (ms.Position % 4);
                }

                // Set next model offset
                NextOffset = ms.Position;
                if (NextOffset % 4 != 0)
                    NextOffset += 4 - (NextOffset % 4);
            }
        }

        public long Add(string model)
        {
            // Return stored model offset
            if (Models.ContainsValue(model))
                return Models.FirstOrDefault(m => m.Value == model).Key;

            // Set model offset
            long modelOffset = NextOffset;
            Models.Add(NextOffset, model);

            // Calc next offset
            NextOffset += Encoding.ASCII.GetBytes(model).LongLength + 1;
            if (NextOffset % 4 != 0)
                NextOffset += 4 - (NextOffset % 4);

            // Return model offset
            return modelOffset;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var models in Models)
                {
                    if (ms.Position % 4 == 0)
                    {
                        bw.WriteNullTerminatedString(models.Value);
                    }
                    else
                    {
                        var stringPadCount = 4 - (ms.Position % 4);
                        for (var i = 0; i < stringPadCount; i++)
                            bw.Write('\0');

                        bw.WriteNullTerminatedString(models.Value);
                    }
                }

                // Insert padding until next alignment
                var padCount = 4 - (ms.Position % 4);
                for (var i = 0; i < padCount; i++)
                    bw.Write('\0');

                return ms.ToArray();
            }
        }
    }
}
