using Hardware.Can;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instructions
{
    /// <summary>
    /// Raw send on CAN bus
    /// </summary>
    public class CanRaw : Instruction
    {
        private CanFrame canFrame;

        /// <summary>
        /// Create a new instance of <see cref="CanRaw"/>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="id">The id</param>
        /// <param name="order">The order</param>
        /// <param name="canId">The can id</param>
        /// <param name="payload">The payload</param>
        /// <param name="resource">The <see cref="ICanResource"/></param>
        /// <param name="description">The description</param>
        public CanRaw(int id, int order, int canId, byte[] payload, string description = "")
            : base("CanRaw", id, order, timeout: 0, description: description)
        {
            canFrame = new CanFrame(canId, payload);
        }

        public override async Task Execute()
        {
            startTime = DateTime.Now;
            await Task.Run(() => result = Resource.Send(canFrame));
            stopTime = DateTime.Now;
        }

        public override string ToString()
        {
            string[] canFrameAsString = canFrame.ToString().Split(';');
            string description = $"{name}\t " +
                  $"{id}\t " +
                  $"{order}\t " +
                  $"\t " +
                  $"{canFrameAsString[1].Trim()}; {canFrameAsString[2].Trim()}\t \t " +
                  $"{startTime:HH:mm:ss.fff}\t " +
                  $"{stopTime:HH:mm:ss.fff}\t " +
                  $"{result}";
            return description;
        }
    }
}
