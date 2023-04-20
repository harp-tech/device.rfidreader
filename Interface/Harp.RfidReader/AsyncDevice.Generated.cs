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
        public async Task<DigitalOutputState> ReadDO0StateAsync()
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
        public async Task<Timestamped<DigitalOutputState>> ReadTimestampedDO0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0State.Address));
            return DO0State.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0State register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0StateAsync(DigitalOutputState value)
        {
            var request = DO0State.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableHardwareNotifications register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HardwareNotifications> ReadEnableHardwareNotificationsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableHardwareNotifications.Address));
            return EnableHardwareNotifications.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableHardwareNotifications register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HardwareNotifications>> ReadTimestampedEnableHardwareNotificationsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableHardwareNotifications.Address));
            return EnableHardwareNotifications.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableHardwareNotifications register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableHardwareNotificationsAsync(HardwareNotifications value)
        {
            var request = EnableHardwareNotifications.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the TriggerHardwareNotifications register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<HardwareNotifications> ReadTriggerHardwareNotificationsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(TriggerHardwareNotifications.Address));
            return TriggerHardwareNotifications.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the TriggerHardwareNotifications register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<HardwareNotifications>> ReadTimestampedTriggerHardwareNotificationsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(TriggerHardwareNotifications.Address));
            return TriggerHardwareNotifications.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the TriggerHardwareNotifications register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteTriggerHardwareNotificationsAsync(HardwareNotifications value)
        {
            var request = TriggerHardwareNotifications.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BuzzerNotificationDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBuzzerNotificationDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerNotificationDuration.Address));
            return BuzzerNotificationDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BuzzerNotificationDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBuzzerNotificationDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerNotificationDuration.Address));
            return BuzzerNotificationDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BuzzerNotificationDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBuzzerNotificationDurationAsync(ushort value)
        {
            var request = BuzzerNotificationDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the TopLedNotificationDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadTopLedNotificationDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedNotificationDuration.Address));
            return TopLedNotificationDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the TopLedNotificationDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedTopLedNotificationDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedNotificationDuration.Address));
            return TopLedNotificationDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the TopLedNotificationDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteTopLedNotificationDurationAsync(ushort value)
        {
            var request = TopLedNotificationDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BottomLedNotificationDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBottomLedNotificationDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedNotificationDuration.Address));
            return BottomLedNotificationDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BottomLedNotificationDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBottomLedNotificationDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedNotificationDuration.Address));
            return BottomLedNotificationDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BottomLedNotificationDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBottomLedNotificationDurationAsync(ushort value)
        {
            var request = BottomLedNotificationDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BuzzerNotificationFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBuzzerNotificationFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerNotificationFrequency.Address));
            return BuzzerNotificationFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BuzzerNotificationFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBuzzerNotificationFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BuzzerNotificationFrequency.Address));
            return BuzzerNotificationFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BuzzerNotificationFrequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBuzzerNotificationFrequencyAsync(ushort value)
        {
            var request = BuzzerNotificationFrequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the TopLedNotificationPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadTopLedNotificationPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedNotificationPeriod.Address));
            return TopLedNotificationPeriod.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the TopLedNotificationPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedTopLedNotificationPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(TopLedNotificationPeriod.Address));
            return TopLedNotificationPeriod.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the TopLedNotificationPeriod register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteTopLedNotificationPeriodAsync(ushort value)
        {
            var request = TopLedNotificationPeriod.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the BottomLedNotificationPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadBottomLedNotificationPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedNotificationPeriod.Address));
            return BottomLedNotificationPeriod.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the BottomLedNotificationPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedBottomLedNotificationPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(BottomLedNotificationPeriod.Address));
            return BottomLedNotificationPeriod.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the BottomLedNotificationPeriod register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteBottomLedNotificationPeriodAsync(ushort value)
        {
            var request = BottomLedNotificationPeriod.FromPayload(MessageType.Write, value);
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
