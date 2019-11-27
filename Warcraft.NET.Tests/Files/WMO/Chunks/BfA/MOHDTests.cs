using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warcraft.NET.Tests.Files.WMO.Chunks.BfA
{
    [TestClass]
    public class MOHDTests
    {
        [TestMethod]
        public void TestLoadBinaryData()
        {
            Assert.AreEqual(Tests.BfAWMO.Header.Materials, Tests.BfAWrittenWMO.Header.Materials);
            Assert.AreEqual(Tests.BfAWMO.Header.Portals, Tests.BfAWrittenWMO.Header.Portals);
            Assert.AreEqual(Tests.BfAWMO.Header.Lights, Tests.BfAWrittenWMO.Header.Lights);
            Assert.AreEqual(Tests.BfAWMO.Header.DoodadNames, Tests.BfAWrittenWMO.Header.DoodadNames);
            Assert.AreEqual(Tests.BfAWMO.Header.DoodadDefinitions, Tests.BfAWrittenWMO.Header.DoodadDefinitions);
            Assert.AreEqual(Tests.BfAWMO.Header.DoodadSets, Tests.BfAWrittenWMO.Header.DoodadSets);
            Assert.AreEqual(Tests.BfAWMO.Header.Color, Tests.BfAWrittenWMO.Header.Color);
            Assert.AreEqual(Tests.BfAWMO.Header.WMOId, Tests.BfAWrittenWMO.Header.WMOId);
            Assert.AreEqual(Tests.BfAWMO.Header.BoundingBox, Tests.BfAWrittenWMO.Header.BoundingBox);
            Assert.AreEqual(Tests.BfAWMO.Header.Flags, Tests.BfAWrittenWMO.Header.Flags);
            Assert.AreEqual(Tests.BfAWMO.Header.Groups, Tests.BfAWrittenWMO.Header.Groups);
        }

        [TestMethod]
        public void TestGetSignature()
        {
            Assert.AreEqual(Tests.BfAWMO.Header.GetSignature(), Tests.BfAWrittenWMO.Header.GetSignature());
        }

        [TestMethod]
        public void TestGetSize()
        {
            Assert.AreEqual(Tests.BfAWMO.Header.GetSize(), Tests.BfAWrittenWMO.Header.GetSize());
        }
    }
}
