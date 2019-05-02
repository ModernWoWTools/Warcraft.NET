using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warcraft.NET.Tests.Files.WMO.Chunks.Wotlk
{
    [TestClass]
    public class MOHDTests
    {
        [TestMethod]
        public void LoadBinaryData()
        {
            Assert.AreEqual(Tests.WotlkWMO.Header.Materials, Tests.WotlkWrittenWMO.Header.Materials);
            Assert.AreEqual(Tests.WotlkWMO.Header.Portals, Tests.WotlkWrittenWMO.Header.Portals);
            Assert.AreEqual(Tests.WotlkWMO.Header.Lights, Tests.WotlkWrittenWMO.Header.Lights);
            Assert.AreEqual(Tests.WotlkWMO.Header.DoodadNames, Tests.WotlkWrittenWMO.Header.DoodadNames);
            Assert.AreEqual(Tests.WotlkWMO.Header.DoodadDefinitions, Tests.WotlkWrittenWMO.Header.DoodadDefinitions);
            Assert.AreEqual(Tests.WotlkWMO.Header.DoodadSets, Tests.WotlkWrittenWMO.Header.DoodadSets);
            Assert.AreEqual(Tests.WotlkWMO.Header.Color, Tests.WotlkWrittenWMO.Header.Color);
            Assert.AreEqual(Tests.WotlkWMO.Header.WMOId, Tests.WotlkWrittenWMO.Header.WMOId);
            Assert.AreEqual(Tests.WotlkWMO.Header.BoundingBox, Tests.WotlkWrittenWMO.Header.BoundingBox);
            Assert.AreEqual(Tests.WotlkWMO.Header.Flags, Tests.WotlkWrittenWMO.Header.Flags);
            Assert.AreEqual(Tests.WotlkWMO.Header.Groups, Tests.WotlkWrittenWMO.Header.Groups);
        }

        [TestMethod]
        public void GetSignature()
        {
            Assert.AreEqual(Tests.WotlkWMO.Header.GetSignature(), Tests.WotlkWrittenWMO.Header.GetSignature());
        }

        [TestMethod]
        public void GetSize()
        {
            Assert.AreEqual(Tests.WotlkWMO.Header.GetSize(), Tests.WotlkWrittenWMO.Header.GetSize());
        }
    }
}
