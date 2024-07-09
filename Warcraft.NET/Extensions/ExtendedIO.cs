using Warcraft.NET.Files.Interfaces;
using System;
using System.IO;
using System.Text;
using Warcraft.NET.Files.Structures;
using System.Collections.Generic;
using System.Numerics;
using Warcraft.NET.Exceptions;
using System.Runtime.CompilerServices;

namespace Warcraft.NET.Extensions
{
    public static class ExtendedIO
    {
        #region Reader
        /// <summary>
        /// Reads a standard null-terminated string from the data stream.
        /// </summary>
        /// <returns>The null terminated string.</returns>
        /// <param name="binaryReader">The reader.</param>
        public static string ReadNullTerminatedString(this BinaryReader binaryReader)
        {
            var sb = new StringBuilder();

            char c;
            while ((c = binaryReader.ReadChar()) != 0)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Reads a 12-byte <see cref="Vector3"/> structure from the data stream and advances the position of the stream by
        /// 12 bytes.
        /// </summary>
        /// <returns>The vector3f.</returns>
        /// <param name="binaryReader">The reader.</param>
        /// <param name="convertTo">Which axis configuration the read vector should be converted to.</param>
        public static Vector3 ReadVector3(this BinaryReader binaryReader, AxisConfiguration convertTo = AxisConfiguration.YUp)
        {
            switch (convertTo)
            {
                case AxisConfiguration.Native:
                    {
                        return new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
                    }
                case AxisConfiguration.YUp:
                    {
                        var x1 = binaryReader.ReadSingle();
                        var y1 = binaryReader.ReadSingle();
                        var z1 = binaryReader.ReadSingle();

                        var x = x1;
                        var y = z1;
                        var z = -y1;

                        return new Vector3(x, y, z);
                    }
                case AxisConfiguration.ZUp:
                    {
                        var x1 = binaryReader.ReadSingle();
                        var y1 = binaryReader.ReadSingle();
                        var z1 = binaryReader.ReadSingle();

                        var x = x1;
                        var y = -z1;
                        var z = y1;

                        return new Vector3(x, y, z);
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(convertTo), convertTo, null);
                    }
            }
        }

        /// <summary>
        /// Reads a 12-byte <see cref="Rotator"/> from the data stream and advances the position of the stream by
        /// 12 bytes.
        /// </summary>
        /// <returns>The rotator.</returns>
        /// <param name="binaryReader">The reader.</param>
        public static Rotator ReadRotator(this BinaryReader binaryReader)
        {
            return new Rotator(binaryReader.ReadVector3(AxisConfiguration.Native));
        }

        /// <summary>
        /// Reads a 24-byte <see cref="BoundingBox"/> structure from the data stream and advances the position of the stream by
        /// 24 bytes.
        /// </summary>
        /// <returns>The box.</returns>
        /// <param name="binaryReader">The reader.</param>
        public static BoundingBox ReadBoundingBox(this BinaryReader binaryReader, AxisConfiguration convertTo = AxisConfiguration.YUp)
        {
            return new BoundingBox(binaryReader.ReadVector3(convertTo), binaryReader.ReadVector3(convertTo));
        }

        /// <summary>
        /// Reads an 18-byte <see cref="ShortPlane"/> from the data stream.
        /// </summary>
        /// <returns>The plane.</returns>
        /// <param name="binaryReader">The reader.</param>
        public static ShortPlane ReadShortPlane(this BinaryReader binaryReader)
        {
            ShortPlane shortPlane;
            shortPlane.Coordinates = new List<List<short>>();

            for (var y = 0; y < 3; ++y)
            {
                var coordinateRow = new List<short>();
                for (var x = 0; x < 3; ++x)
                {
                    coordinateRow.Add(binaryReader.ReadInt16());
                }

                shortPlane.Coordinates.Add(coordinateRow);
            }

            return shortPlane;
        }

        /// <summary>
        /// Reads a 4-byte <see cref="RGBA"/> structure from the data stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The color.</returns>
        public static RGBA ReadRGBA(this BinaryReader reader)
        {
            return new RGBA
            {
                R = reader.ReadByte(),
                G = reader.ReadByte(),
                B = reader.ReadByte(),
                A = reader.ReadByte()
            };
        }

        /// <summary>
        /// Reads a 4-byte <see cref="RGBA"/> structure from the data stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The color.</returns>
        public static RGBA ReadBGRA(this BinaryReader reader)
        {
            return new RGBA
            {
                B = reader.ReadByte(),
                G = reader.ReadByte(),
                R = reader.ReadByte(),
                A = reader.ReadByte()
            };
        }

        /// Reads ab 4.byte <see cref="UVMapEntry"/> from the data stream.
        /// </summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The UV map entry.</returns>
        public static UVMapEntry ReadUVMapEntry(this BinaryReader binaryReader)
        {
            return new UVMapEntry
            {
                X = binaryReader.ReadUInt16(),
                Y = binaryReader.ReadUInt16()
            };
        }

        /// <summary>
        /// Reads a 4-byte RIFF chunk signature from the data stream.
        /// </summary>
        /// <param name="binaryReader"></param>
        /// <param name="reverseSignature"></param>
        /// <returns>The signature as a string.</returns>
        public static string ReadBinarySignature(this BinaryReader binaryReader, bool reverseSignature = true)
        {
            // The signatures are stored in reverse in the file, so we'll need to read them backwards into
            // the buffer.
            var signatureBuffer = new char[4];
            for (var i = 0; i < 4; ++i)
            {
                signatureBuffer[(reverseSignature ? 3 - i : i)] = binaryReader.ReadChar();
            }

            var signature = new string(signatureBuffer);
            return signature;
        }

        /// <summary>
        /// Peeks a 4-byte RIFF chunk signature from the data stream. This does not
        /// advance the position of the stream.
        /// </summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The signature.</returns>
        public static string PeekChunkSignature(this BinaryReader binaryReader)
        {
            var chunkSignature = binaryReader.ReadBinarySignature();
            binaryReader.BaseStream.Position -= chunkSignature.Length;

            return chunkSignature;
        }

        /// <summary>
        /// Reads an IFF-style chunk from the stream. The chunk must have the <see cref="IIFFChunk"/>
        /// interface, and implement a parameterless constructor.
        /// </summary>
        /// <param name="reader">The current <see cref="BinaryReader"/>.</param>
        /// <param name="reader">The current <see cref="BinaryReader"/>.</param>
        /// <param name="returnDefault"></param>
        /// <param name="fromBegin"></param>
        /// <typeparam name="T">The chunk type.</typeparam>
        /// <returns>The chunk.</returns>
        /// <summary>
        public static T ReadIFFChunk<T>(this BinaryReader reader, bool returnDefault = false, bool fromBegin = true, bool reverseSignature = true) where T : IIFFChunk, new()
        {
            T chunk = new T();

            if (!reader.SeekChunk(chunk.GetSignature(), fromBegin, false, reverseSignature))
            {
                if (returnDefault)
                    return default(T);

                throw new ChunkSignatureNotFoundException($"Chunk \"{chunk.GetSignature()}\" not found.");
            }

            string chunkSignature = reader.ReadBinarySignature(reverseSignature);
            var chunkSize = reader.ReadUInt32();
            var chunkData = reader.ReadBytes((int)chunkSize);

            if (chunk.GetSignature() != chunkSignature)
            {
                throw new InvalidChunkSignatureException($"An unknown chunk with the signature \"{chunkSignature}\" was read.");
            }

            chunk.LoadBinaryData(chunkData);

            return chunk;
        }

        /// <summary>
        /// Reads a 16-byte 32-bit <see cref="Quaternion"/> structure from the data stream, and advances the position of the stream by
        /// 16 bytes.
        /// </summary>
        /// <returns>The quaternion.</returns>
        /// <param name="binaryReader">The reader.</param>
        public static Quaternion ReadQuaternion(this BinaryReader binaryReader)
        {
            return new Quaternion(binaryReader.ReadVector3(), binaryReader.ReadSingle());
        }

        /// <summary>
        /// Reads Size and Offset from the stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static M2Array ReadM2Array(this BinaryReader reader)
        {
            return new M2Array(size: reader.ReadUInt32(), offset: reader.ReadUInt32());
        }

        /// <summary>
        /// Reads a 6-byte <see cref="ShortVector3"/> structure from the data stream and advances the position of the stream by
        /// 6 bytes.
        /// </summary>
        /// <returns>The vector3s.</returns>
        /// <param name="binaryReader">The reader.</param>
        /// <param name="convertTo">Which axis configuration the read vector should be converted to.</param>
        public static ShortVector3 ReadShortVector3(this BinaryReader binaryReader, AxisConfiguration convertTo = AxisConfiguration.YUp)
        {
            switch (convertTo)
            {
                case AxisConfiguration.Native:
                {
                    return new ShortVector3(binaryReader.ReadInt16(), binaryReader.ReadInt16(), binaryReader.ReadInt16());
                }
                case AxisConfiguration.YUp:
                {
                    var x1 = binaryReader.ReadInt16();
                    var y1 = binaryReader.ReadInt16();
                    var z1 = binaryReader.ReadInt16();

                    var x = x1;
                    var y = z1;
                    var z = (short)-y1;

                    return new ShortVector3(x, y, z);
                }
                case AxisConfiguration.ZUp:
                {
                    var x1 = binaryReader.ReadInt16();
                    var y1 = binaryReader.ReadInt16();
                    var z1 = binaryReader.ReadInt16();

                    var x = x1;
                    var y = (short)-z1;
                    var z = y1;

                    return new ShortVector3(x, y, z);
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(convertTo), convertTo, null);
                }
            }
        }

        /// <summary>
        /// Reads a 3-byte <see cref="ByteVector3"/> structure from the data stream and advances the position of the stream by
        /// 3 bytes.
        /// </summary>
        /// <returns>The vector3s.</returns>
        /// <param name="binaryReader">The reader.</param>
        /// <param name="convertTo">Which axis configuration the read vector should be converted to.</param>
        public static ByteVector3 ReadByteVector3(this BinaryReader binaryReader, AxisConfiguration convertTo = AxisConfiguration.YUp)
        {
            switch (convertTo)
            {
                case AxisConfiguration.Native:
                    {
                        return new ByteVector3(binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte());
                    }
                case AxisConfiguration.YUp:
                    {
                        var x1 = binaryReader.ReadByte();
                        var y1 = binaryReader.ReadByte();
                        var z1 = binaryReader.ReadByte();

                        var x = x1;
                        var y = z1;
                        var z = (byte)-y1;

                        return new ByteVector3(x, y, z);
                    }
                case AxisConfiguration.ZUp:
                    {
                        var x1 = binaryReader.ReadByte();
                        var y1 = binaryReader.ReadByte();
                        var z1 = binaryReader.ReadByte();

                        var x = x1;
                        var y = (byte)-z1;
                        var z = y1;

                        return new ByteVector3(x, y, z);
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(convertTo), convertTo, null);
                    }
            }
        }

        /// <summary>
        /// Reads struct
        /// </summary>
        /// <param name="binaryReader">The reader.</param>
        public static T ReadStruct<T>(this BinaryReader reader) where T : struct
        {
            byte[] result = reader.ReadBytes(Unsafe.SizeOf<T>());

            return Unsafe.ReadUnaligned<T>(ref result[0]);
        }
        #endregion

        #region Write
        /// <summary>
        /// Writes the provided string to the data stream as a C-style null-terminated string.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="inputString">Input string.</param>
        public static void WriteNullTerminatedString(this BinaryWriter binaryWriter, string inputString)
        {
            foreach (var c in inputString)
            {
                binaryWriter.Write(c);
            }

            binaryWriter.Write((char)0);
        }
        /// <summary>
        /// Writes the provided string to the data stream as a C-style null-terminated string.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="inputStrings">Input string array.</param>
        public static void WriteNullTerminatedString(this BinaryWriter binaryWriter, string[] inputStrings)
        {
            foreach (var s in inputStrings)
            {
                binaryWriter.WriteNullTerminatedString(s);
            }
        }

        /// <summary>
        /// Writes an RIFF-style chunk signature to the data stream.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="signature">Signature.</param>
        public static void WriteChunkSignature(this BinaryWriter binaryWriter, string signature, bool reverseSignature = true)
        {
            if (signature.Length != 4)
            {
                throw new InvalidDataException("The signature must be an ASCII string of exactly four characters.");
            }

            for (var i = 0; i < 4; ++i)
            {
                binaryWriter.Write(signature[(reverseSignature ? 3 - i : i)]);
            }
        }

        /// <summary>
        /// Writes a 12-byte <see cref="Rotator"/> value to the data stream in Pitch/Yaw/Roll order.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="inRotator">Rotator.</param>
        public static void WriteRotator(this BinaryWriter binaryWriter, Rotator inRotator)
        {
            binaryWriter.Write(inRotator.Pitch);
            binaryWriter.Write(inRotator.Yaw);
            binaryWriter.Write(inRotator.Roll);
        }

        /// <summary>
        /// Writes a 24-byte <see cref="BoundingBox"/> to the data stream.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="box">In box.</param>
        public static void WriteBoundingBox(this BinaryWriter binaryWriter, BoundingBox box, AxisConfiguration storeAs = AxisConfiguration.ZUp)
        {
            binaryWriter.WriteVector3(box.Minimum, storeAs);
            binaryWriter.WriteVector3(box.Maximum, storeAs);
        }

        /// <summary>
        /// Writes an 18-byte <see cref="ShortPlane"/> to the data stream.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="shortPlane">The plane to write.</param>
        public static void WriteShortPlane(this BinaryWriter binaryWriter, ShortPlane shortPlane)
        {
            for (var y = 0; y < 3; ++y)
            {
                for (var x = 0; x < 3; ++x)
                {
                    binaryWriter.Write(shortPlane.Coordinates[y][x]);
                }
            }
        }

        /// <summary>
        /// Writes a 4-byte <see cref="RGBA"/> to the data stream.
        /// </summary>
        /// <param name="binaryWriter"></param>
        /// <param name="color"></param>
        public static void WriteRGBA(this BinaryWriter binaryWriter, RGBA color)
        {
            binaryWriter.Write(color.R);
            binaryWriter.Write(color.G);
            binaryWriter.Write(color.B);
            binaryWriter.Write(color.A);
        }

        /// <summary>
        /// Writes a 4-byte <see cref="RGBA"/> to the data stream.
        /// </summary>
        /// <param name="binaryWriter"></param>
        /// <param name="color"></param>
        public static void WriteBGRA(this BinaryWriter binaryWriter, RGBA color)
        {
            binaryWriter.Write(color.B);
            binaryWriter.Write(color.G);
            binaryWriter.Write(color.R);
            binaryWriter.Write(color.A);
        }

        /// Writes an 4-byte <see cref="UVMapEntry"/> to the data stream.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="uvMapEntry">The UV map entry to write.</param>
        public static void WriteUVMapEntry(this BinaryWriter binaryWriter, UVMapEntry uvMapEntry)
        {
            binaryWriter.Write(uvMapEntry.X);
            binaryWriter.Write(uvMapEntry.Y);
        }

        /// <summary>
        /// Writes an RIFF-style chunk to the data stream.
        /// </summary>
        /// <typeparam name="T">The chunk type.</typeparam>
        /// <param name="binaryWriter">The writer.</param>
        /// <param name="chunk">The chunk.</param>
        public static void WriteIFFChunk<T>(this BinaryWriter binaryWriter, T chunk, bool writeAtEOF = false, bool reverseSignature = true) where T : IIFFChunk, IBinarySerializable
        {
            if (writeAtEOF)
                binaryWriter.Seek(0, SeekOrigin.End);

            var serializedChunk = chunk.Serialize(binaryWriter.BaseStream.Position + (sizeof(uint) * 2));

            binaryWriter.WriteChunkSignature(chunk.GetSignature(), reverseSignature);
            binaryWriter.Write((uint)serializedChunk.Length);
            binaryWriter.Write(serializedChunk);
        }

        /// <summary>
        /// Writes a 12-byte <see cref="Vector3"/> value to the data stream. This function
        /// expects a Y-up vector. By default, this function will store the vector in a Z-up axis configuration, which
        /// is what World of Warcraft expects. This can be overridden. Passing <see cref="AxisConfiguration.Native"/> is
        /// considered Y-up, as it is the way vectors are handled internally in the library.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="vector">The Vector to write.</param>
        /// <param name="storeAs">Which axis configuration the read vector should be stored as.</param>
        public static void WriteVector3(this BinaryWriter binaryWriter, Vector3 vector, AxisConfiguration storeAs = AxisConfiguration.ZUp)
        {
            switch (storeAs)
            {
                case AxisConfiguration.Native:
                case AxisConfiguration.YUp:
                    {
                        binaryWriter.Write(vector.X);
                        binaryWriter.Write(vector.Y);
                        binaryWriter.Write(vector.Z);
                        break;
                    }

                case AxisConfiguration.ZUp:
                    {
                        binaryWriter.Write(vector.X);
                        binaryWriter.Write(vector.Z * -1.0f);
                        binaryWriter.Write(vector.Y);
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(storeAs), storeAs, null);
            }
        }

        /// <summary>
        /// Writes a 16-byte <see cref="Quaternion"/> to the data stream.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="quaternion">The quaternion to write.</param>
        public static void WriteQuaternion(this BinaryWriter binaryWriter, Quaternion quaternion)
        {
            var vector = new Vector3(quaternion.X, quaternion.Y, quaternion.Z);
            binaryWriter.WriteVector3(vector);

            binaryWriter.Write(quaternion.W);
        }

        /// <summary>
        /// Writes Size and Offset to the stream. <see cref="M2Array"/>
        /// </summary>
        /// <param name="Size">The Size to write.</param>
        /// <param name="Offset">The Offset to write.</param>
        public static void WriteM2Array(this BinaryWriter binaryWriter, uint Size, uint Offset)
        {
            binaryWriter.Write(Size);
            binaryWriter.Write(Offset);
        }

        /// <summary>
        /// Writes a 6-byte <see cref="ShortVector3"/> value to the data stream in XYZ order. This function
        /// expects a Y-up vector.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="vector">The Vector to write.</param>
        /// <param name="storeAs">Which axis configuration the read vector should be stored as.</param>
        public static void WriteShortVector3(this BinaryWriter binaryWriter, ShortVector3 vector, AxisConfiguration storeAs = AxisConfiguration.ZUp)
        {
            switch (storeAs)
            {
                case AxisConfiguration.Native:
                case AxisConfiguration.YUp:
                {
                    binaryWriter.Write(vector.X);
                    binaryWriter.Write(vector.Y);
                    binaryWriter.Write(vector.Z);
                    break;
                }
                case AxisConfiguration.ZUp:
                {
                    binaryWriter.Write(vector.X);
                    binaryWriter.Write((short)(vector.Z * -1));
                    binaryWriter.Write(vector.Y);
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(storeAs), storeAs, null);
                }
            }
        }

        /// <summary>
        /// Writes a 3-byte <see cref="ByteVector3"/> value to the data stream in XYZ order. This function
        /// expects a Y-up vector.
        /// </summary>
        /// <param name="binaryWriter">The current <see cref="BinaryWriter"/> object.</param>
        /// <param name="vector">The Vector to write.</param>
        /// <param name="storeAs">Which axis configuration the read vector should be stored as.</param>
        public static void WriteByteVector3(this BinaryWriter binaryWriter, ByteVector3 vector, AxisConfiguration storeAs = AxisConfiguration.ZUp)
        {
            switch (storeAs)
            {
                case AxisConfiguration.Native:
                case AxisConfiguration.YUp:
                    {
                        binaryWriter.Write(vector.X);
                        binaryWriter.Write(vector.Y);
                        binaryWriter.Write(vector.Z);
                        break;
                    }
                case AxisConfiguration.ZUp:
                    {
                        binaryWriter.Write(vector.X);
                        binaryWriter.Write((byte)(vector.Z * -1));
                        binaryWriter.Write(vector.Y);
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(storeAs), storeAs, null);
                    }
            }
        }
        #endregion

        /// <summary>
        /// Try to seek to given chunkSignature
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="chunkSignature"></param>
        /// <param name="fromBegin"></param>
        /// <param name="skipSignature"></param>
        /// <param name="reverseSignature"></param>
        /// <returns></returns>
        public static bool SeekChunk(this BinaryReader reader, string chunkSignature, bool fromBegin = true, bool skipSignature = false, bool reverseSignature = true)
        {
            if (fromBegin)
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

            try
            {
                var foundChunkSignature = reader.ReadBinarySignature(reverseSignature);
                while (foundChunkSignature != chunkSignature)
                {
                    var size = reader.ReadUInt32();

                    // Return if we are about to seek outside of range
                    if ((reader.BaseStream.Position + size) > reader.BaseStream.Length)
                        return false;

                    reader.BaseStream.Position += size;

                    // Return if we're done reading
                    if (reader.BaseStream.Position == reader.BaseStream.Length)
                        return false;

                    foundChunkSignature = reader.ReadBinarySignature(reverseSignature);
                }

                if (foundChunkSignature == chunkSignature)
                {
                    if (!skipSignature)
                        reader.BaseStream.Position -= sizeof(uint);
                    return true;
                }
            }
            catch (Exception) { }

            return false;
        }
    }
}
