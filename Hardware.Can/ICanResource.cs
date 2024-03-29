﻿using System.Collections.Generic;

namespace Hardware.Can
{
    /// <summary>
    /// Define a basic prototype for a can resource
    /// </summary>
    public interface ICanResource
    {
        /// <summary>
        /// The <see cref="ICanResource"/> subscribed
        /// <see cref="ICanChannel"/>
        /// </summary>
        List<ICanChannel> Channels { get; }

        /// <summary>
        /// Start the <see cref="ICanResource"/>
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the <see cref="ICanResource"/>
        /// </summary>
        void Stop();

        /// <summary>
        /// Send a <see cref="CanFrame"/> via the <see cref="ICanResource"/>
        /// </summary>
        /// <param name="canFrame">The can frame to send</param>
        /// <returns>
        /// <see langword="true"/> if the operation succeeded,
        /// <see langword="false"/> otherwise
        /// </returns>
        bool Send(CanFrame canFrame);

        /// <summary>
        /// Enable the <see cref="ICanResource"/> log for
        /// received <see cref="CanFrame"/>
        /// </summary>
        /// <param name="maxLogSize">The maximum log size</param>
        void EnableLog(int maxLogSize = 65535);

        /// <summary>
        /// Disable the <see cref="ICanResource"/> log for
        /// received <see cref="CanFrame"/>
        /// </summary>
        void DisableLog();

        /// <summary>
        /// Read the current <see cref="ICanResource"/> log
        /// </summary>
        /// <returns>The <see cref="string"/> containing the current log</returns>
        string ReadLog();
    }
}