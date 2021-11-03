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
            firstVariable = new DoubleVariable(
                name: "FirstVariable",
                index: 0,
                subIndex: 0,
                value: 0.1,
                type: VariableType.Sgl,
                description: "",
                measureUnit: ""
            );
            secondVariable = new DoubleVariable(
                name: "SecondVariable", index: 0,
                subIndex: 0,
                value: 0.2,
                type: VariableType.Sgl,
                description: "",
                measureUnit: ""
            );

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

        [Test]
        public async Task TestExecuteAll()
        {
            Stopwatch sw = Stopwatch.StartNew();
            await scheduler.ExecuteAll();
            sw.Stop();

            scheduler.Instructions.Count.Should().Be(0);
            firstVariable.Value.Should().Be(-10d);
            sw.Elapsed.TotalMilliseconds.Should().BeApproximately(2000, 500); // 1s Wait + 1s WaitFor
        }
    }
}