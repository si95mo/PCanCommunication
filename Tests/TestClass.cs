using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestSerialNumber()
        {
            string serialNumber = SerialNumbers.SerialNumbers.CreateNew("Italy", 0);
            Console.WriteLine(serialNumber);
        }
    }
}