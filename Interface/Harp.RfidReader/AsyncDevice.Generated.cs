using Bonsai.Harp;
using System.Threading.Tasks;

namespace Harp.RfidReader
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with RfidReader devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a RfidReader device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with RfidReader devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the InboundDetectionId register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ulong> ReadInboundDetectionIdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(InboundDetectionId.Address));
            return InboundDetectionId.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the InboundDetectionId register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ulong>> ReadTimestampedInboundDetectionIdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(InboundDetectionId.Address));
            return InboundDetectionId.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutboundDetectionId register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ulong> ReadOutboundDetectionIdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(OutboundDetectionId.Address));
            return OutboundDetectionId.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutboundDetectionId register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ulong>> ReadTimestampedOutboundDetectionIdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(OutboundDetectionId.Address));
            return OutboundDetectionId.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalState> ReadDO0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0State.Address));
            return DO0State.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalState>> ReadTimestampedDO0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0State.Address));
            return DO0State.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0State register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0StateAsync(DigitalState value)
        {
            var request = DO0State.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the HardwareNotificationsState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HardwareNotifications> ReadHardwareNotificationsStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(HardwareNotificationsState.Address));
            return HardwareNotificationsState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the HardwareNotificationsState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HardwareNotifications>> ReadTimestampedHardwareNotificationsStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(HardwareNotificationsState.Address));
            return HardwareNotificationsState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the HardwareNotificationsState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteHardwareNotificationsStateAsync(HardwareNotifications value)
        {
            var request = HardwareNotificationsState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the HardwareNotificationsTrigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HardwareNotifications> ReadHardwareNotificationsTriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(HardwareNotificationsTrigger.Address));
            return HardwareNotificationsTrigger.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the HardwareNotificationsTrigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HardwareNotifications>> ReadTimestampedHardwareNotificationsTriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(HardwareNotificationsTrigger.Address));
            return HardwareNotificationsTrigger.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the HardwareNotificationsTrigger register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteHardwareNotificationsTriggerAsync(HardwareNotifications value)
        {
            var request = HardwareNotificationsTrigger.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BuzzerDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBuzzerDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerDuration.Address));
            return BuzzerDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BuzzerDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBuzzerDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerDuration.Address));
            return BuzzerDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BuzzerDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBuzzerDurationAsync(ushort value)
        {
            var request = BuzzerDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the TopLedDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadTopLedDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedDuration.Address));
            return TopLedDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the TopLedDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedTopLedDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedDuration.Address));
            return TopLedDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the TopLedDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteTopLedDurationAsync(ushort value)
        {
            var request = TopLedDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BottomLedDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBottomLedDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedDuration.Address));
            return BottomLedDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BottomLedDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBottomLedDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedDuration.Address));
            return BottomLedDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BottomLedDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBottomLedDurationAsync(ushort value)
        {
            var request = BottomLedDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BuzzerFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBuzzerFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerFrequency.Address));
            return BuzzerFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BuzzerFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBuzzerFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerFrequency.Address));
            return BuzzerFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BuzzerFrequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBuzzerFrequencyAsync(ushort value)
        {
            var request = BuzzerFrequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the TopLedPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadTopLedPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedPeriod.Address));
            return TopLedPeriod.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the TopLedPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedTopLedPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedPeriod.Address));
            return TopLedPeriod.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the TopLedPeriod register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteTopLedPeriodAsync(ushort value)
        {
            var request = TopLedPeriod.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BottomLedPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBottomLedPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedPeriod.Address));
            return BottomLedPeriod.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BottomLedPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBottomLedPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedPeriod.Address));
            return BottomLedPeriod.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BottomLedPeriod register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBottomLedPeriodAsync(ushort value)
        {
            var request = BottomLedPeriod.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ulong> ReadMatchTagId0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId0.Address));
            return MatchTagId0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ulong>> ReadTimestampedMatchTagId0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId0.Address));
            return MatchTagId0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId0Async(ulong value)
        {
            var request = MatchTagId0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ulong> ReadMatchTagId1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId1.Address));
            return MatchTagId1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ulong>> ReadTimestampedMatchTagId1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId1.Address));
            return MatchTagId1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId1Async(ulong value)
        {
            var request = MatchTagId1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ulong> ReadMatchTagId2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId2.Address));
            return MatchTagId2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ulong>> ReadTimestampedMatchTagId2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId2.Address));
            return MatchTagId2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId2Async(ulong value)
        {
            var request = MatchTagId2.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ulong> ReadMatchTagId3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId3.Address));
            return MatchTagId3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ulong>> ReadTimestampedMatchTagId3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt64(MatchTagId3.Address));
            return MatchTagId3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId3Async(ulong value)
        {
            var request = MatchTagId3.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMatchTagId0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId0PulseWidth.Address));
            return MatchTagId0PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMatchTagId0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId0PulseWidth.Address));
            return MatchTagId0PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId0PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId0PulseWidthAsync(ushort value)
        {
            var request = MatchTagId0PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId1PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMatchTagId1PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId1PulseWidth.Address));
            return MatchTagId1PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId1PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMatchTagId1PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId1PulseWidth.Address));
            return MatchTagId1PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId1PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId1PulseWidthAsync(ushort value)
        {
            var request = MatchTagId1PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId2PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMatchTagId2PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId2PulseWidth.Address));
            return MatchTagId2PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId2PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMatchTagId2PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId2PulseWidth.Address));
            return MatchTagId2PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId2PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId2PulseWidthAsync(ushort value)
        {
            var request = MatchTagId2PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the MatchTagId3PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadMatchTagId3PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId3PulseWidth.Address));
            return MatchTagId3PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the MatchTagId3PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedMatchTagId3PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(MatchTagId3PulseWidth.Address));
            return MatchTagId3PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the MatchTagId3PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteMatchTagId3PulseWidthAsync(ushort value)
        {
            var request = MatchTagId3PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AnyTagIdPulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadAnyTagIdPulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(AnyTagIdPulseWidth.Address));
            return AnyTagIdPulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AnyTagIdPulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedAnyTagIdPulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(AnyTagIdPulseWidth.Address));
            return AnyTagIdPulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AnyTagIdPulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAnyTagIdPulseWidthAsync(ushort value)
        {
            var request = AnyTagIdPulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0PulseWidth.Address));
            return DO0PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0PulseWidth.Address));
            return DO0PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0PulseWidthAsync(ushort value)
        {
            var request = DO0PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
