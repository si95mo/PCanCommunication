using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Hardware.Can.Peak.Tests
{
    public class IndexedCanChannelTestClass
    {
        private IndexedCanChannel canChannel;
        private byte cmd = 0x0;
        private byte index = 0xA;
        private ushort subIndex = 0xB;

        [OneTimeSetUp]
        public void Setup()
        {
            ICanResource resource = new PeakCanResource(81, BaudRate.K500);
            canChannel = new IndexedCanChannel(0x0, index, subIndex, resource);
        }

        [Test]
        [TestCase(98.76F)]
        [TestCase(5F)]
        [TestCase(-12.34F)]
        public void TestSetValue(float value)
        {
            byte[] data = BitConverter.GetBytes(value);
            canChannel.Data = data;

            canChannel.Index.Should().Be(index);
            canChannel.SubIndex.Should().Be(subIndex);

            byte[] fullData = ItemsToByteArray(cmd, index, subIndex, data);
            canChannel.Data.Should().BeEquivalentTo(fullData);
        }

        /// <summary>
        /// Convert the index, sub index and data into a single byte array
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        /// <param name="data">The byte array of data</param>
        /// <returns>The converted byte array</returns>
        private byte[] ItemsToByteArray(byte cmd, byte index, ushort subIndex, byte[] data)
        {
            byte[] cmdAsArray = new byte[] { cmd };
            byte[] indexAsArray = new byte[] { index }; // Should be 2 bytes
            byte[] subIndexAsArray = BitConverter.GetBytes(subIndex); // Should be 2 bytes
            byte[] firstHalf = new byte[cmdAsArray.Length + indexAsArray.Length + subIndexAsArray.Length]; // Should be 4 bytes
            cmdAsArray.CopyTo(firstHalf, 0);
            indexAsArray.CopyTo(firstHalf, 1);
            subIndexAsArray.CopyTo(firstHalf, cmdAsArray.Length + indexAsArray.Length);

            byte[] fullData = firstHalf.Concat(data).ToArray();

            return fullData;
        }
    }
}