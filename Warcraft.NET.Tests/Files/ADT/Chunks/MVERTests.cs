using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Tests.Files.ADT.Chunks
{
    [TestClass]
    public class MVERTests
    {
        protected byte[] adt = File.ReadAllBytes("Resources/ADT/Northrend_37_25.adt");

        [TestMethod]
        public void LoadBinaryData()
        {
            // Arrage
            MVER mver = new MVER();

            // Act
            byte[] dataWithoutSignature = adt.Skip(0x4).ToArray();
            mver.LoadBinaryData(dataWithoutSignature);

            // Assert
            Assert.AreEqual(mver.Version, (uint)4);
        }

        [TestMethod]
        public void GetSignature()
        {
            // Arrage
            var mver = new MVER();

            // Act
            string result = mver.GetSignature();
            
            // Assert
            Assert.AreEqual(result, "MVER");
        }

        [TestMethod]
        public void GetSize()
        {
            // Arrage
            var mver = new MVER()
            {
                Version = 4
            };

            // Act
            uint result = mver.GetSize();

            // Assert
            Assert.AreEqual(result, (uint)4);
        }

        [TestMethod]
        public void Serialize()
        {
            // Arrage
            var mver = new MVER()
            {
                Version = 4
            };

            // Act
            byte[] result = mver.Serialize();

            // Assert
            CollectionAssert.AreEqual(result, new byte[] { 0x4, 0x0, 0x0, 0x0 });
        }
    }
}
