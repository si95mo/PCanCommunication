﻿using DataStructures.VariablesDictionary;
using Hardware.Can;
using System;
using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Implement an <see cref="Instruction"/> for getting a value
    /// </summary>
    public class Get : Instruction
    {
        protected string variableName;
        protected double valueGot;

        /// <summary>
        /// Create a new instance of <see cref="Get"/>
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order index</param>
        public Get(string variableName, int id, int order, int timeout = 1000, IndexedCanChannel rx = null,
            IndexedCanChannel tx = null) : base("Get", id, order, timeout, rx, tx)
        {
            this.variableName = variableName;

            inputParameters.Add(this.variableName);
        }

        /// <summary>
        /// Execute the <see cref="Get"/> instruction
        /// </summary>
        public override async Task Execute()
        {
            startTime = DateTime.Now;
            outputParameters.Clear();

            if (tx != null)
            {
                tx.Data = new byte[] { 0, 0, 0, 0 };
                tx.Cmd = 0;
            }

            // Timeout handling
            Task t = await Task.WhenAny(waitTask, Task.Delay(timeout));
            result = waitTask == t;

            // Get instruction
            if (result)
            {
                await Task.Run(() =>
                    {
                        VariableDictionary.Get(variableName, out IVariable variable);
                        valueGot = Convert.ToDouble(variable.ValueAsObject);

                        outputParameters.Add(valueGot);
                    }
                );
            }

            stopTime = DateTime.Now;

            outputParameters.Add(startTime);
            outputParameters.Add(stopTime);
        }

        public override string ToString()
        {
            string description = $"{name}; " +
                $"{id}; " +
                $"{order}; " +
                $"{variableName}; " +
                $"{valueGot}; ; " +
                $"{startTime:HH:mm:ss.fff}; " +
                $"{stopTime:HH:mm:ss.fff}; " +
                $"{result}"; 
            return description;
        }
    }
}