using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warcraft.NET.Tests.Files.ADT;
using WoDWMO = Warcraft.NET.Files.WMO.WorldMapObject.WoD.WorldMapObjectRoot;

namespace Warcraft.NET.Tests.Files.WMO.Chunks.WoD
{
    [TestClass]
    public class MOHDTests
    {
        public WoDWMO WoDWMO;
        public WoDWMO WoDWrittenWMO;

        public MOHDTests()
        {
            // duskwood_lumbermill.wmo
            WoDWMO = new WoDWMO(TestResource.Download(106759, "6.2.4.21742"));
            WoDWrittenWMO = new WoDWMO(WoDWMO.Serialize());
        }

        [TestMethod]
        public void TestLoadBinaryData()
        {
            Assert.AreEqual(WoDWMO.Header.Materials, WoDWrittenWMO.Header.Materials);
            Assert.AreEqual(WoDWMO.Header.Portals, WoDWrittenWMO.Header.Portals);
            Assert.AreEqual(WoDWMO.Header.Lights, WoDWrittenWMO.Header.Lights);
            Assert.AreEqual(WoDWMO.Header.DoodadNames, WoDWrittenWMO.Header.DoodadNames);
            Assert.AreEqual(WoDWMO.Header.DoodadDefinitions, WoDWrittenWMO.Header.DoodadDefinitions);
            Assert.AreEqual(WoDWMO.Header.DoodadSets, WoDWrittenWMO.Header.DoodadSets);
            Assert.AreEqual(WoDWMO.Header.Color, WoDWrittenWMO.Header.Color);
            Assert.AreEqual(WoDWMO.Header.WMOId, WoDWrittenWMO.Header.WMOId);
            Assert.AreEqual(WoDWMO.Header.BoundingBox, WoDWrittenWMO.Header.BoundingBox);
            Assert.AreEqual(WoDWMO.Header.Flags, WoDWrittenWMO.Header.Flags);
            Assert.AreEqual(WoDWMO.Header.Groups, WoDWrittenWMO.Header.Groups);
        }

        [TestMethod]
        public void TestGetSignature()
        {
            Assert.AreEqual(WoDWMO.Header.GetSignature(), WoDWrittenWMO.Header.GetSignature());
        }

        [TestMethod]
        public void TestGetSize()
        {
            Assert.AreEqual(WoDWMO.Header.GetSize(), WoDWrittenWMO.Header.GetSize());
        }
    }
}
