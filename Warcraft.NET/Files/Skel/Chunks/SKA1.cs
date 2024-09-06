using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.Skel.Chunks
{
    public class SKA1 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SKA1";

        /// <summary>
        /// Gets or Sets the attachments
        /// </summary>
        public List<AttachmentStruct> Attachments { get; set; }
        /// <summary>
        /// Gets or Sets the AttachmentLookupTable
        /// </summary>
        public List<AttachLookupStruct> AttachLookup { get; set; }
        /// <summary>
        /// Initializes a new instance of <see cref="SKA1"/>
        /// </summary>
        public SKA1() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SKA1"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SKA1(byte[] inData) => LoadBinaryData(inData);

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
                Attachments = ReadStructList<AttachmentStruct>(br.ReadUInt32(), br.ReadUInt32(), br);
                AttachLookup = ReadStructList<AttachLookupStruct>(br.ReadUInt32(), br.ReadUInt32(), br);
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                return null;
            }
        }

        private List<T> ReadStructList<T>(uint count, uint offset, BinaryReader br) where T : struct
        {
            br.BaseStream.Position = offset;
            List<T> list = new List<T>();

            for (var i = 0; i < count; i++)
                list.Add(br.ReadStruct<T>());

            return list;
        }
    }
}
