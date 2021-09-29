using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Hardware.Can.Peak.Tests
{
    public class IndexedCanChannelTestClass
    {
        private IndexedCanChannel canChannel;
        private ushort index = 0xA;
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

            byte[] fullData = ItemsToByteArray(index, subIndex, data);
            canChannel.Data.Should().BeEquivalentTo(fullData);
        }

        /// <summary>
        /// Convert the index, sub index and data into a single byte array
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="subIndex">The sub index</param>
        /// <param name="data">The byte array of data</param>
        /// <returns>The converted byte array</returns>
        private byte[] ItemsToByteArray(ushort index, ushort subIndex, byte[] data)
        {
            byte[] indexAsArray = BitConverter.GetBytes(index); // Should be 2 bytes
            byte[] subIndexAsArray = BitConverter.GetBytes(subIndex); // Should be 2 bytes
            byte[] firstHalf = new byte[indexAsArray.Length + subIndexAsArray.Length]; // Should be 4 bytes
            indexAsArray.CopyTo(firstHalf, 0);
            subIndexAsArray.CopyTo(firstHalf, indexAsArray.Length);

            byte[] fullData = firstHalf.Concat(data).ToArray();

            return fullData;
        }
    }
}