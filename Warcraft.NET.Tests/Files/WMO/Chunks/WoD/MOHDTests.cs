using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warcraft.NET.Tests.Files.WMO.Chunks.WoD
{
    [TestClass]
    public class MOHDTests
    {
        [TestMethod]
        public void LoadBinaryData()
        {
            Assert.AreEqual(Tests.WoDWMO.Header.Materials, Tests.WoDWrittenWMO.Header.Materials);
            Assert.AreEqual(Tests.WoDWMO.Header.Portals, Tests.WoDWrittenWMO.Header.Portals);
            Assert.AreEqual(Tests.WoDWMO.Header.Lights, Tests.WoDWrittenWMO.Header.Lights);
            Assert.AreEqual(Tests.WoDWMO.Header.DoodadNames, Tests.WoDWrittenWMO.Header.DoodadNames);
            Assert.AreEqual(Tests.WoDWMO.Header.DoodadDefinitions, Tests.WoDWrittenWMO.Header.DoodadDefinitions);
            Assert.AreEqual(Tests.WoDWMO.Header.DoodadSets, Tests.WoDWrittenWMO.Header.DoodadSets);
            Assert.AreEqual(Tests.WoDWMO.Header.Color, Tests.WoDWrittenWMO.Header.Color);
            Assert.AreEqual(Tests.WoDWMO.Header.WMOId, Tests.WoDWrittenWMO.Header.WMOId);
            Assert.AreEqual(Tests.WoDWMO.Header.BoundingBox, Tests.WoDWrittenWMO.Header.BoundingBox);
            Assert.AreEqual(Tests.WoDWMO.Header.Flags, Tests.WoDWrittenWMO.Header.Flags);
            Assert.AreEqual(Tests.WoDWMO.Header.Groups, Tests.WoDWrittenWMO.Header.Groups);
        }

        [TestMethod]
        public void GetSignature()
        {
            Assert.AreEqual(Tests.WoDWMO.Header.GetSignature(), Tests.WoDWrittenWMO.Header.GetSignature());
        }

        [TestMethod]
        public void GetSize()
        {
            Assert.AreEqual(Tests.WoDWMO.Header.GetSize(), Tests.WoDWrittenWMO.Header.GetSize());
        }
    }
}
