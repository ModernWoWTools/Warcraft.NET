using System;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys.Entries;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class PLYT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PLYT";

        /// <summary>
        /// sets or gets the polytope shapes
        /// </summary>
        public List<PLYTEntry> PLYTEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="PLYT"/>
        /// </summary>
        public PLYT() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PLYT"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PLYT(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var plyt_count = br.ReadUInt32();
                for (var i = 0; i < plyt_count; i++)
                {
                    PLYTEntry plyt_entry = new PLYTEntry(br.ReadBytes(80));
                    PLYTEntries.Add(plyt_entry);
                }
                for (var j = 0; j < plyt_count; j++)
                {
                    PLYTEntry plyt_entry = PLYTEntries[j];
                    plyt_entry.DeserializeData(br.ReadBytes(plyt_entry.DataSize));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((uint)PLYTEntries.Count);
                foreach (PLYTEntry obj in PLYTEntries)
                {
                    Console.WriteLine("Writing Plyt Header Length: " + obj.SerializeHeader().Length);
                    bw.Write(obj.SerializeHeader());
                }
                foreach (PLYTEntry obj in PLYTEntries)
                {
                    bw.Write(obj.SerializeData());
                    Console.WriteLine("Writing Plyt Data Length:" + obj.SerializeData().Length);
                }
                return ms.ToArray();
            }
        }
    }
}
