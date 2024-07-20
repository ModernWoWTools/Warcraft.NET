using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warcraft.NET.Tests.Files.ADT;
using BfAWMO = Warcraft.NET.Files.WMO.WorldMapObject.BfA.WorldMapObjectRoot;

namespace Warcraft.NET.Tests.Files.WMO.Chunks.BfA
{
    [TestClass]
    public class MOHDTests
    {
        public BfAWMO BfAWMO;
        public BfAWMO BfAWrittenWMO;

        public MOHDTests()
        {
            // 8or_pvp_warsongbg_tower01.wmo
            BfAWMO = new BfAWMO(TestResource.Download(2395014, "8.3.7.35662"));
            BfAWrittenWMO = new BfAWMO(BfAWMO.Serialize());
        }

        [TestMethod]
        public void TestLoadBinaryData()
        {
            Assert.AreEqual(BfAWMO.Header.Materials, BfAWrittenWMO.Header.Materials);
            Assert.AreEqual(BfAWMO.Header.Portals, BfAWrittenWMO.Header.Portals);
            Assert.AreEqual(BfAWMO.Header.Lights, BfAWrittenWMO.Header.Lights);
            Assert.AreEqual(BfAWMO.Header.DoodadNames, BfAWrittenWMO.Header.DoodadNames);
            Assert.AreEqual(BfAWMO.Header.DoodadDefinitions, BfAWrittenWMO.Header.DoodadDefinitions);
            Assert.AreEqual(BfAWMO.Header.DoodadSets, BfAWrittenWMO.Header.DoodadSets);
            Assert.AreEqual(BfAWMO.Header.Color, BfAWrittenWMO.Header.Color);
            Assert.AreEqual(BfAWMO.Header.WMOId, BfAWrittenWMO.Header.WMOId);
            Assert.AreEqual(BfAWMO.Header.BoundingBox, BfAWrittenWMO.Header.BoundingBox);
            Assert.AreEqual(BfAWMO.Header.Flags, BfAWrittenWMO.Header.Flags);
            Assert.AreEqual(BfAWMO.Header.Groups, BfAWrittenWMO.Header.Groups);
        }

        [TestMethod]
        public void TestGetSignature()
        {
            Assert.AreEqual(BfAWMO.Header.GetSignature(), BfAWrittenWMO.Header.GetSignature());
        }

        [TestMethod]
        public void TestGetSize()
        {
            Assert.AreEqual(BfAWMO.Header.GetSize(), BfAWrittenWMO.Header.GetSize());
        }
    }
}
