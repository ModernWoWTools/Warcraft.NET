using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warcraft.NET.Tests.Files.ADT;
using LegionWMO = Warcraft.NET.Files.WMO.WorldMapObject.Legion.WorldMapObjectRoot;

namespace Warcraft.NET.Tests.Files.WMO.Chunks.Legion
{
    [TestClass]
    public class MOHDTests
    {
        public LegionWMO LegionWMO;
        public LegionWMO LegionWrittenWMO;

        public MOHDTests()
        {
            // 7du_violethold_trapdoor_transport.wmo
            LegionWMO = new LegionWMO(TestResource.Download(1326805, "7.3.5.26972"));
            LegionWrittenWMO = new LegionWMO(LegionWMO.Serialize());
        }

        [TestMethod]
        public void TestLoadBinaryData()
        {
            Assert.AreEqual(LegionWMO.Header.Materials, LegionWrittenWMO.Header.Materials);
            Assert.AreEqual(LegionWMO.Header.Portals, LegionWrittenWMO.Header.Portals);
            Assert.AreEqual(LegionWMO.Header.Lights, LegionWrittenWMO.Header.Lights);
            Assert.AreEqual(LegionWMO.Header.DoodadNames, LegionWrittenWMO.Header.DoodadNames);
            Assert.AreEqual(LegionWMO.Header.DoodadDefinitions, LegionWrittenWMO.Header.DoodadDefinitions);
            Assert.AreEqual(LegionWMO.Header.DoodadSets, LegionWrittenWMO.Header.DoodadSets);
            Assert.AreEqual(LegionWMO.Header.Color, LegionWrittenWMO.Header.Color);
            Assert.AreEqual(LegionWMO.Header.WMOId, LegionWrittenWMO.Header.WMOId);
            Assert.AreEqual(LegionWMO.Header.BoundingBox, LegionWrittenWMO.Header.BoundingBox);
            Assert.AreEqual(LegionWMO.Header.Flags, LegionWrittenWMO.Header.Flags);
            Assert.AreEqual(LegionWMO.Header.Groups, LegionWrittenWMO.Header.Groups);
        }

        [TestMethod]
        public void TestGetSignature()
        {
            Assert.AreEqual(LegionWMO.Header.GetSignature(), LegionWrittenWMO.Header.GetSignature());
        }

        [TestMethod]
        public void TestGetSize()
        {
            Assert.AreEqual(LegionWMO.Header.GetSize(), LegionWrittenWMO.Header.GetSize());
        }
    }
}
