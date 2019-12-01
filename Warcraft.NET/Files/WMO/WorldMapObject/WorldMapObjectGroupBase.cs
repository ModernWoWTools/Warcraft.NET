namespace Warcraft.NET.Files.WMO.WorldMapObject
{
    public abstract class WorldMapObjectGroupBase : WorldMapObjectBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectGroupBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectGroupBase(byte[] inData) : base(inData)
        {
        }
    }
}
