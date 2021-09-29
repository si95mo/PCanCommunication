using DataStructures.VariablesDictionary;
using FluentAssertions;
using NUnit.Framework;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Instructions.Tests
{
    [TestFixture]
    public class InstructionTest
    {
        private DoubleVariable firstVariable;
        private FloatVariable secondVariable;

        [OneTimeSetUp]
        public void Setup()
        {
            firstVariable = new DoubleVariable("FirstVariable", 0, 0, 0.1);
            secondVariable = new FloatVariable("SecondVariable", 0, 0, 0.2F);

            VariableDictionary.Initialize();
            VariableDictionary.Add(firstVariable);
            VariableDictionary.Add(secondVariable);

            VariableDictionary.Variables.Count.Should().Be(2);
        }

        [Test]
        public async Task TestGet()
        {
            Get get = new Get("FirstVariable", 1, 1);
            await get.Execute();
            get.OutputParameters[0].Should().Be(firstVariable.Value);

            get = new Get("SecondVariable", 1, 1);
            await get.Execute();
            get.OutputParameters[0].Should().Be(secondVariable.Value);
        }

        [Test]
        [TestCase(1d)]
        public async Task TestSet(double value)
        {
            Set set = new Set("FirstVariable", value, 1, 1);

            await set.Execute();
            firstVariable.Value.Should().Be(value);
        }

        [Test]
        [TestCase(1000)]
        public async Task TestWait(int delay)
        {
            Wait wait = new Wait(delay, 1, 1);

            Stopwatch sw = Stopwatch.StartNew();
            await wait.Execute();
            sw.Stop();

            sw.Elapsed.TotalMilliseconds.Should().BeApproximately(delay, 100);
        }

        [Test]
        public async Task TestWaitFor()
        {
            WaitFor waitFor = new WaitFor(
                "FirstVariable",
                "SecondVariable",
                ConditionOperand.Equal,
                1000,
                10000,
                1,
                1
            );
            Stopwatch sw = Stopwatch.StartNew();

            secondVariable.Value = (float)firstVariable.Value;
            await waitFor.Execute();

            bool result = (bool)waitFor.OutputParameters[0];
            result.Should().BeTrue();
            firstVariable.Value.Should().BeApproximately(secondVariable.Value, 0.000001); // Approximation

            sw.Elapsed.TotalMilliseconds.Should().BeApproximately(1000, 100);

            sw = Stopwatch.StartNew();

            secondVariable.Value = 2F * secondVariable.Value;
            await waitFor.Execute();

            result = (bool)waitFor.OutputParameters[0];
            result.Should().BeFalse();
            firstVariable.Value.Should().NotBeApproximately(secondVariable.Value, 0.000001); // Approximation

            sw.Elapsed.TotalMilliseconds.Should().BeApproximately(10000, 100);
        }
    }
}