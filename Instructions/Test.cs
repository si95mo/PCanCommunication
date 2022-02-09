﻿using DataStructures.VariablesDictionary;
using Hardware.Can;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Instructions
{
    public class Test : Instruction
    {
        protected string variableName;
        protected double valueGot;
        protected double value;
        protected ConditionOperand operand;

        /// <summary>
        /// Create a new instance of <see cref="Test"/>
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        /// <param name="value">The value</param>
        /// <param name="operand">The <see cref="ConditionOperand"/></param>
        /// <param name="description">The description</param>
        public Test(string variableName, int id, int order, double value, ConditionOperand operand, string description = "") : base("Test", id, order, description: description)
        {
            this.variableName = variableName;
            this.value = value;
            this.operand = operand;

            inputParameters.Add(this.variableName);
            inputParameters.Add(this.value);
        }

        /// <summary>
        /// Execute the <see cref="Get"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            Rx.CanFrameChanged += LocalRx_CanFrameChanged;

            startTime = DateTime.Now;
            outputParameters.Clear();

            await Task.Run(() =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);

                    Tx.Cmd = 0;
                    (variable as Variable<double>).UpdateVariable(Tx);

                    outputParameters.Add(valueGot);
                }
            );

            Task<bool> waitTask = Task.Run(async () =>
                {
                    VariableDictionary.Get(variableName, out IVariable variable);
                    Tx.Cmd = 0;

                    Stopwatch time = Stopwatch.StartNew();

                    received = false;
                    (variable as Variable<double>).UpdateVariable(Tx);

                    while (!received && time.Elapsed.TotalMilliseconds <= timeout)
                        await Task.Delay(50);

                    time.Stop();

                    return received;
                }
            );

            result = received;

            if (result)
            {
                double threshold = 0.000001;
                switch (operand)
                {
                    case ConditionOperand.Equal:
                        result &= Math.Abs(valueGot - value) <= threshold;
                        break;

                    case ConditionOperand.NotEqual:
                        result &= Math.Abs(valueGot - value) > threshold;
                        break;

                    case ConditionOperand.Greather:
                        result &= valueGot > value + threshold;
                        break;

                    case ConditionOperand.Lesser:
                        result &= valueGot < value - threshold;
                        break;
                }
            }

            stopTime = DateTime.Now;

            Rx.CanFrameChanged -= LocalRx_CanFrameChanged;

            outputParameters.Add(result);
            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        private void LocalRx_CanFrameChanged(object sender, CanFrameChangedEventArgs e)
        {
            VariableDictionary.Get(variableName, out IVariable variable);
            (variable as DoubleVariable).Value = variable.Type == VariableType.Sgl ?
                BitConverter.ToSingle((e.NewCanFrame as CanFrame).Data, 4) : BitConverter.ToInt32((e.NewCanFrame as CanFrame).Data, 4);

            valueGot = Convert.ToDouble(variable.ValueAsObject);

            received = true;
        }

        public override string ToString()
        {
            string description = $"{name}\t " +
                $"{id}\t " +
                $"{order}\t " +
                $"{variableName}\t \t " +
                $"{variableName} ({valueGot}) is {operand} than {value}\t " +
                $"{startTime:HH:mm:ss.fff}\t " +
                $"{stopTime:HH:mm:ss.fff}\t " +
                $"{result}";
            return description;
        }
    }
}