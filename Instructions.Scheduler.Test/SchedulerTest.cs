using DataStructures.VariablesDictionary;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Instructions.Scheduler.Test
{
    [TestFixture]
    public class SchedulerTest
    {
        private DoubleVariable firstVariable;
        private DoubleVariable secondVariable;
        private Scheduler scheduler;

        [OneTimeSetUp]
        public void Setup()
        {
            firstVariable = new DoubleVariable("FirstVariable", 0, 0, 0.1);
            secondVariable = new DoubleVariable("SecondVariable", 0, 0, 0.2);

            VariableDictionary.Initialize();
            VariableDictionary.Add(firstVariable);
            VariableDictionary.Add(secondVariable);

            VariableDictionary.Variables.Count.Should().Be(2);

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "test.csv"
            );
            scheduler = new Scheduler(path);
        }

        //[Test]
        //public void TestAdd()
        //{
        //    scheduler.Instructions.Clear();

        //    Set set = new Set("FirstVariable", 10d, 1);
        //    Get get = new Get("FirstVariable", 1);
        //    Wait wait = new Wait(1000, 1);

        //    scheduler.Add(set);
        //    scheduler.Add(get);
        //    scheduler.Add(wait);

        //    scheduler.Instructions.Count.Should().Be(3);
        //}

        [Test]
        public async Task TestExecuteAll()
        {
            //if (scheduler.Instructions.Count == 0)
            //    TestAdd();

            Stopwatch sw = Stopwatch.StartNew();
            await scheduler.ExecuteAll();
            sw.Stop();

            scheduler.Instructions.Count.Should().Be(0);
            firstVariable.Value.Should().Be(-10d);
            sw.Elapsed.TotalMilliseconds.Should().BeApproximately(2000, 200); // 1s Wait + 1s WaitFor
        }
    }
}