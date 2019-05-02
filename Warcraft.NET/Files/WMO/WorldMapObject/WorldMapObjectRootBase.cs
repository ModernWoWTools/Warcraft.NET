namespace Warcraft.NET.Files.WMO.WorldMapObject
{
    public abstract class WorldMapObjectRootBase : WorldMapObjectBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectRootBase"/> class.
        /// </summary>
        public WorldMapObjectRootBase() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectRootBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectRootBase(byte[] inData) : base(inData)
        {
        }
    }
}
