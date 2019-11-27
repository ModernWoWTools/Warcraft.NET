using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Tests.Files.Structures
{
    [TestClass]
    public class RotatorTests
    {
        [TestMethod]
        public void TestToString() 
        {
            // Arrage
            float Pitch = .1f;
            float Yaw = .2f;
            float Roll = .3f;

            Rotator rotator = new Rotator(Pitch, Yaw, Roll);

            // Act
            string result = rotator.ToString();

            // Assert
            Assert.AreEqual(result, $"Pitch: {Pitch}, Yaw: {Yaw}, Roll: {Roll}");
        }

        [TestMethod]
        public void TestFlatten()
        {
            // Arrage
            float Pitch = .1f;
            float Yaw = .2f;
            float Roll = .3f;

            var rotator = new Rotator(Pitch, Yaw, Roll);

            // Act
            float[] result = rotator.Flatten().ToArray();

            // Assert
            CollectionAssert.AreEqual(result, new float[] { Pitch, Yaw, Roll });
        }
    }
}
