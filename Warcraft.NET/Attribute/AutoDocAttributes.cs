using System;

namespace Warcraft.NET.Attribute
{
    [Flags]
    internal enum AutoDocChunkVersion
    {
        LK = 1,
        WoD = 2,
        Legion = 4,
        BfA = 8,
        SL = 16,
        DF = 32,
        TWW = 64,
    }

    internal static class AutoDocChunkVersionHelper
    {
        internal const AutoDocChunkVersion VersionAll = AutoDocChunkVersion.LK | AutoDocChunkVersion.WoD | AutoDocChunkVersion.Legion | AutoDocChunkVersion.BfA | AutoDocChunkVersion.SL | AutoDocChunkVersion.DF | AutoDocChunkVersion.TWW;
        internal const AutoDocChunkVersion VersionAfterLK = AutoDocChunkVersion.WoD | AutoDocChunkVersion.Legion | AutoDocChunkVersion.BfA | AutoDocChunkVersion.SL | AutoDocChunkVersion.DF | AutoDocChunkVersion.TWW;
        internal const AutoDocChunkVersion VersionAfterWoD = AutoDocChunkVersion.Legion | AutoDocChunkVersion.BfA | AutoDocChunkVersion.SL | AutoDocChunkVersion.DF | AutoDocChunkVersion.TWW;
        internal const AutoDocChunkVersion VersionAfterLegion = AutoDocChunkVersion.BfA | AutoDocChunkVersion.SL | AutoDocChunkVersion.DF | AutoDocChunkVersion.TWW;
        internal const AutoDocChunkVersion VersionAfterBfA = AutoDocChunkVersion.SL | AutoDocChunkVersion.DF | AutoDocChunkVersion.TWW;
        internal const AutoDocChunkVersion VersionAfterSL = AutoDocChunkVersion.DF | AutoDocChunkVersion.TWW;
        internal const AutoDocChunkVersion VersionBeforeLegion = AutoDocChunkVersion.LK | AutoDocChunkVersion.WoD;
        internal const AutoDocChunkVersion VersionBeforeBfA = AutoDocChunkVersion.LK | AutoDocChunkVersion.WoD | AutoDocChunkVersion.Legion;
        internal const AutoDocChunkVersion VersionBeforeSL = AutoDocChunkVersion.LK | AutoDocChunkVersion.WoD | AutoDocChunkVersion.Legion | AutoDocChunkVersion.BfA;
        internal const AutoDocChunkVersion VersionBeforeDF = AutoDocChunkVersion.LK | AutoDocChunkVersion.WoD | AutoDocChunkVersion.Legion | AutoDocChunkVersion.BfA | AutoDocChunkVersion.SL;
        internal const AutoDocChunkVersion VersionBeforeTWW = AutoDocChunkVersion.LK | AutoDocChunkVersion.WoD | AutoDocChunkVersion.Legion | AutoDocChunkVersion.BfA | AutoDocChunkVersion.SL | AutoDocChunkVersion.DF;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class AutoDocFile : System.Attribute
    {
        private readonly string _fileExtension;
        private readonly string _title;

        internal AutoDocFile(string fileExtension, string title = null)
        {
            _fileExtension = fileExtension;
            _title = title ?? fileExtension.ToUpper();
        }

        internal string Extension { get { return _fileExtension; } }
        internal string Title { get { return _title; } }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class AutoDocChunk : System.Attribute
    {
        private readonly AutoDocChunkVersion _chunkVersion;
        private readonly AutoDocChunkVersion _chunkNotAvailableInVersion = 0;
        private readonly AutoDocChunkVersion _chunkReadOnlyInVersion = 0;

        internal AutoDocChunk(AutoDocChunkVersion chunkVersion, AutoDocChunkVersion chunkNotAvailableInVersion = 0, AutoDocChunkVersion chunkReadOnlyInVersion = 0)
        {
            _chunkVersion = chunkVersion;
            _chunkNotAvailableInVersion = chunkNotAvailableInVersion;
            _chunkReadOnlyInVersion = chunkReadOnlyInVersion;
        }

        internal AutoDocChunkVersion ChunkVersion { get { return _chunkVersion; } }

        internal AutoDocChunkVersion ChunkNotAvailableInVersion { get { return _chunkNotAvailableInVersion; } }

        internal AutoDocChunkVersion ChunkReadOnlyInVersion { get { return _chunkReadOnlyInVersion; } }
    }
}
