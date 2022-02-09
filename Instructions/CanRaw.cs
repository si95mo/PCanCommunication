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
        public CanRaw(string name, int id, int order, int canId, byte[] payload, string description = "")
            : base(name, id, order, timeout: 0, description: description)
        {
            canFrame = new CanFrame(canId, payload);
        }

        public override async Task Execute()
        {
            await Task.Run(() => Resource.Send(canFrame));
        }
    }
}
