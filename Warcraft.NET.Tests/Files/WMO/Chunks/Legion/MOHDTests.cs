using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warcraft.NET.Tests.Files.WMO.Chunks.Legion
{
    [TestClass]
    public class MOHDTests
    {
        [TestMethod]
        public void TestLoadBinaryData()
        {
            Assert.AreEqual(Tests.LegionWMO.Header.Materials, Tests.LegionWrittenWMO.Header.Materials);
            Assert.AreEqual(Tests.LegionWMO.Header.Portals, Tests.LegionWrittenWMO.Header.Portals);
            Assert.AreEqual(Tests.LegionWMO.Header.Lights, Tests.LegionWrittenWMO.Header.Lights);
            Assert.AreEqual(Tests.LegionWMO.Header.DoodadNames, Tests.LegionWrittenWMO.Header.DoodadNames);
            Assert.AreEqual(Tests.LegionWMO.Header.DoodadDefinitions, Tests.LegionWrittenWMO.Header.DoodadDefinitions);
            Assert.AreEqual(Tests.LegionWMO.Header.DoodadSets, Tests.LegionWrittenWMO.Header.DoodadSets);
            Assert.AreEqual(Tests.LegionWMO.Header.Color, Tests.LegionWrittenWMO.Header.Color);
            Assert.AreEqual(Tests.LegionWMO.Header.WMOId, Tests.LegionWrittenWMO.Header.WMOId);
            Assert.AreEqual(Tests.LegionWMO.Header.BoundingBox, Tests.LegionWrittenWMO.Header.BoundingBox);
            Assert.AreEqual(Tests.LegionWMO.Header.Flags, Tests.LegionWrittenWMO.Header.Flags);
            Assert.AreEqual(Tests.LegionWMO.Header.Groups, Tests.LegionWrittenWMO.Header.Groups);
        }

        [TestMethod]
        public void TestGetSignature()
        {
            Assert.AreEqual(Tests.LegionWMO.Header.GetSignature(), Tests.LegionWrittenWMO.Header.GetSignature());
        }

        [TestMethod]
        public void TestGetSize()
        {
            Assert.AreEqual(Tests.LegionWMO.Header.GetSize(), Tests.LegionWrittenWMO.Header.GetSize());
        }
    }
}
