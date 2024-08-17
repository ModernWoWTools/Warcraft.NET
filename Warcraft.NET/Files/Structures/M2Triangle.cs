namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// Triangle read from skin. </para> 
    /// Default = Right Handed </para> 
    /// To make it left handed, swap the second and third vertex
    /// </summary>
    public struct M2Triangle
    {
        /// <summary>
        /// the first vertex of the Triangle
        /// </summary>
        public ushort Vertex1;

        /// <summary>
        /// the second vertex of the Triangle
        /// </summary>
        public ushort Vertex2;

        /// <summary>
        /// the third vertex of the Triangle
        /// </summary>
        public ushort Vertex3;
    }
}
