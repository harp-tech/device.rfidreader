using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.RfidReader
{
    /// <summary>
    /// Generates events and processes commands for the RfidReader device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the RfidReader device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="RfidReader"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 2094;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(RfidReader);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(InboundDetectionId) },
            { 33, typeof(OutboundDetectionId) },
            { 34, typeof(DO0State) },
            { 35, typeof(HardwareNotificationsState) },
            { 36, typeof(HardwareNotificationsTrigger) },
            { 37, typeof(BuzzerDuration) },
            { 38, typeof(TopLedDuration) },
            { 39, typeof(BottomLedDuration) },
            { 40, typeof(BuzzerFrequency) },
            { 41, typeof(TopLedPeriod) },
            { 42, typeof(BottomLedPeriod) },
            { 45, typeof(MatchTagId0) },
            { 46, typeof(MatchTagId1) },
            { 47, typeof(MatchTagId2) },
            { 48, typeof(MatchTagId3) },
            { 49, typeof(MatchTagId0PulseWidth) },
            { 50, typeof(MatchTagId1PulseWidth) },
            { 51, typeof(MatchTagId2PulseWidth) },
            { 52, typeof(MatchTagId3PulseWidth) },
            { 53, typeof(AnyTagIdPulseWidth) },
            { 54, typeof(DO0PulseWidth) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="RfidReader"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of RfidReader messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="RfidReader"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="RfidReader"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="RfidReader"/> device.
    /// </summary>
    /// <seealso cref="InboundDetectionId"/>
    /// <seealso cref="OutboundDetectionId"/>
    /// <seealso cref="DO0State"/>
    /// <seealso cref="HardwareNotificationsState"/>
    /// <seealso cref="HardwareNotificationsTrigger"/>
    /// <seealso cref="BuzzerDuration"/>
    /// <seealso cref="TopLedDuration"/>
    /// <seealso cref="BottomLedDuration"/>
    /// <seealso cref="BuzzerFrequency"/>
    /// <seealso cref="TopLedPeriod"/>
    /// <seealso cref="BottomLedPeriod"/>
    /// <seealso cref="MatchTagId0"/>
    /// <seealso cref="MatchTagId1"/>
    /// <seealso cref="MatchTagId2"/>
    /// <seealso cref="MatchTagId3"/>
    /// <seealso cref="MatchTagId0PulseWidth"/>
    /// <seealso cref="MatchTagId1PulseWidth"/>
    /// <seealso cref="MatchTagId2PulseWidth"/>
    /// <seealso cref="MatchTagId3PulseWidth"/>
    /// <seealso cref="AnyTagIdPulseWidth"/>
    /// <seealso cref="DO0PulseWidth"/>
    [XmlInclude(typeof(InboundDetectionId))]
    [XmlInclude(typeof(OutboundDetectionId))]
    [XmlInclude(typeof(DO0State))]
    [XmlInclude(typeof(HardwareNotificationsState))]
    [XmlInclude(typeof(HardwareNotificationsTrigger))]
    [XmlInclude(typeof(BuzzerDuration))]
    [XmlInclude(typeof(TopLedDuration))]
    [XmlInclude(typeof(BottomLedDuration))]
    [XmlInclude(typeof(BuzzerFrequency))]
    [XmlInclude(typeof(TopLedPeriod))]
    [XmlInclude(typeof(BottomLedPeriod))]
    [XmlInclude(typeof(MatchTagId0))]
    [XmlInclude(typeof(MatchTagId1))]
    [XmlInclude(typeof(MatchTagId2))]
    [XmlInclude(typeof(MatchTagId3))]
    [XmlInclude(typeof(MatchTagId0PulseWidth))]
    [XmlInclude(typeof(MatchTagId1PulseWidth))]
    [XmlInclude(typeof(MatchTagId2PulseWidth))]
    [XmlInclude(typeof(MatchTagId3PulseWidth))]
    [XmlInclude(typeof(AnyTagIdPulseWidth))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [Description("Filters register-specific messages reported by the RfidReader device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new InboundDetectionId();
        }

        string INamedElement.Name
        {
            get => $"{nameof(RfidReader)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the RfidReader device.
    /// </summary>
    /// <seealso cref="InboundDetectionId"/>
    /// <seealso cref="OutboundDetectionId"/>
    /// <seealso cref="DO0State"/>
    /// <seealso cref="HardwareNotificationsState"/>
    /// <seealso cref="HardwareNotificationsTrigger"/>
    /// <seealso cref="BuzzerDuration"/>
    /// <seealso cref="TopLedDuration"/>
    /// <seealso cref="BottomLedDuration"/>
    /// <seealso cref="BuzzerFrequency"/>
    /// <seealso cref="TopLedPeriod"/>
    /// <seealso cref="BottomLedPeriod"/>
    /// <seealso cref="MatchTagId0"/>
    /// <seealso cref="MatchTagId1"/>
    /// <seealso cref="MatchTagId2"/>
    /// <seealso cref="MatchTagId3"/>
    /// <seealso cref="MatchTagId0PulseWidth"/>
    /// <seealso cref="MatchTagId1PulseWidth"/>
    /// <seealso cref="MatchTagId2PulseWidth"/>
    /// <seealso cref="MatchTagId3PulseWidth"/>
    /// <seealso cref="AnyTagIdPulseWidth"/>
    /// <seealso cref="DO0PulseWidth"/>
    [XmlInclude(typeof(InboundDetectionId))]
    [XmlInclude(typeof(OutboundDetectionId))]
    [XmlInclude(typeof(DO0State))]
    [XmlInclude(typeof(HardwareNotificationsState))]
    [XmlInclude(typeof(HardwareNotificationsTrigger))]
    [XmlInclude(typeof(BuzzerDuration))]
    [XmlInclude(typeof(TopLedDuration))]
    [XmlInclude(typeof(BottomLedDuration))]
    [XmlInclude(typeof(BuzzerFrequency))]
    [XmlInclude(typeof(TopLedPeriod))]
    [XmlInclude(typeof(BottomLedPeriod))]
    [XmlInclude(typeof(MatchTagId0))]
    [XmlInclude(typeof(MatchTagId1))]
    [XmlInclude(typeof(MatchTagId2))]
    [XmlInclude(typeof(MatchTagId3))]
    [XmlInclude(typeof(MatchTagId0PulseWidth))]
    [XmlInclude(typeof(MatchTagId1PulseWidth))]
    [XmlInclude(typeof(MatchTagId2PulseWidth))]
    [XmlInclude(typeof(MatchTagId3PulseWidth))]
    [XmlInclude(typeof(AnyTagIdPulseWidth))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(TimestampedInboundDetectionId))]
    [XmlInclude(typeof(TimestampedOutboundDetectionId))]
    [XmlInclude(typeof(TimestampedDO0State))]
    [XmlInclude(typeof(TimestampedHardwareNotificationsState))]
    [XmlInclude(typeof(TimestampedHardwareNotificationsTrigger))]
    [XmlInclude(typeof(TimestampedBuzzerDuration))]
    [XmlInclude(typeof(TimestampedTopLedDuration))]
    [XmlInclude(typeof(TimestampedBottomLedDuration))]
    [XmlInclude(typeof(TimestampedBuzzerFrequency))]
    [XmlInclude(typeof(TimestampedTopLedPeriod))]
    [XmlInclude(typeof(TimestampedBottomLedPeriod))]
    [XmlInclude(typeof(TimestampedMatchTagId0))]
    [XmlInclude(typeof(TimestampedMatchTagId1))]
    [XmlInclude(typeof(TimestampedMatchTagId2))]
    [XmlInclude(typeof(TimestampedMatchTagId3))]
    [XmlInclude(typeof(TimestampedMatchTagId0PulseWidth))]
    [XmlInclude(typeof(TimestampedMatchTagId1PulseWidth))]
    [XmlInclude(typeof(TimestampedMatchTagId2PulseWidth))]
    [XmlInclude(typeof(TimestampedMatchTagId3PulseWidth))]
    [XmlInclude(typeof(TimestampedAnyTagIdPulseWidth))]
    [XmlInclude(typeof(TimestampedDO0PulseWidth))]
    [Description("Filters and selects specific messages reported by the RfidReader device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new InboundDetectionId();
        }

        string INamedElement.Name => $"{nameof(RfidReader)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// RfidReader register messages.
    /// </summary>
    /// <seealso cref="InboundDetectionId"/>
    /// <seealso cref="OutboundDetectionId"/>
    /// <seealso cref="DO0State"/>
    /// <seealso cref="HardwareNotificationsState"/>
    /// <seealso cref="HardwareNotificationsTrigger"/>
    /// <seealso cref="BuzzerDuration"/>
    /// <seealso cref="TopLedDuration"/>
    /// <seealso cref="BottomLedDuration"/>
    /// <seealso cref="BuzzerFrequency"/>
    /// <seealso cref="TopLedPeriod"/>
    /// <seealso cref="BottomLedPeriod"/>
    /// <seealso cref="MatchTagId0"/>
    /// <seealso cref="MatchTagId1"/>
    /// <seealso cref="MatchTagId2"/>
    /// <seealso cref="MatchTagId3"/>
    /// <seealso cref="MatchTagId0PulseWidth"/>
    /// <seealso cref="MatchTagId1PulseWidth"/>
    /// <seealso cref="MatchTagId2PulseWidth"/>
    /// <seealso cref="MatchTagId3PulseWidth"/>
    /// <seealso cref="AnyTagIdPulseWidth"/>
    /// <seealso cref="DO0PulseWidth"/>
    [XmlInclude(typeof(InboundDetectionId))]
    [XmlInclude(typeof(OutboundDetectionId))]
    [XmlInclude(typeof(DO0State))]
    [XmlInclude(typeof(HardwareNotificationsState))]
    [XmlInclude(typeof(HardwareNotificationsTrigger))]
    [XmlInclude(typeof(BuzzerDuration))]
    [XmlInclude(typeof(TopLedDuration))]
    [XmlInclude(typeof(BottomLedDuration))]
    [XmlInclude(typeof(BuzzerFrequency))]
    [XmlInclude(typeof(TopLedPeriod))]
    [XmlInclude(typeof(BottomLedPeriod))]
    [XmlInclude(typeof(MatchTagId0))]
    [XmlInclude(typeof(MatchTagId1))]
    [XmlInclude(typeof(MatchTagId2))]
    [XmlInclude(typeof(MatchTagId3))]
    [XmlInclude(typeof(MatchTagId0PulseWidth))]
    [XmlInclude(typeof(MatchTagId1PulseWidth))]
    [XmlInclude(typeof(MatchTagId2PulseWidth))]
    [XmlInclude(typeof(MatchTagId3PulseWidth))]
    [XmlInclude(typeof(AnyTagIdPulseWidth))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [Description("Formats a sequence of values as specific RfidReader register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new InboundDetectionId();
        }

        string INamedElement.Name => $"{nameof(RfidReader)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that the ID of the tag that was detected as having entered the area of the reader.
    /// </summary>
    [Description("The ID of the tag that was detected as having entered the area of the reader.")]
    public partial class InboundDetectionId
    {
        /// <summary>
        /// Represents the address of the <see cref="InboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="InboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U64;

        /// <summary>
        /// Represents the length of the <see cref="InboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="InboundDetectionId"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ulong GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt64();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="InboundDetectionId"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt64();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="InboundDetectionId"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InboundDetectionId"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="InboundDetectionId"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InboundDetectionId"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// InboundDetectionId register.
    /// </summary>
    /// <seealso cref="InboundDetectionId"/>
    [Description("Filters and selects timestamped messages from the InboundDetectionId register.")]
    public partial class TimestampedInboundDetectionId
    {
        /// <summary>
        /// Represents the address of the <see cref="InboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const int Address = InboundDetectionId.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="InboundDetectionId"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetPayload(HarpMessage message)
        {
            return InboundDetectionId.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the ID of the tag that was detected as having exited the area of the reader.
    /// </summary>
    [Description("The ID of the tag that was detected as having exited the area of the reader.")]
    public partial class OutboundDetectionId
    {
        /// <summary>
        /// Represents the address of the <see cref="OutboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="OutboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U64;

        /// <summary>
        /// Represents the length of the <see cref="OutboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutboundDetectionId"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ulong GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt64();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutboundDetectionId"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt64();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutboundDetectionId"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutboundDetectionId"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutboundDetectionId"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutboundDetectionId"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutboundDetectionId register.
    /// </summary>
    /// <seealso cref="OutboundDetectionId"/>
    [Description("Filters and selects timestamped messages from the OutboundDetectionId register.")]
    public partial class TimestampedOutboundDetectionId
    {
        /// <summary>
        /// Represents the address of the <see cref="OutboundDetectionId"/> register. This field is constant.
        /// </summary>
        public const int Address = OutboundDetectionId.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutboundDetectionId"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetPayload(HarpMessage message)
        {
            return OutboundDetectionId.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that changes the state of the digital output pin.
    /// </summary>
    [Description("Changes the state of the digital output pin.")]
    public partial class DO0State
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0State"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0State"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0State"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0State"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalState GetPayload(HarpMessage message)
        {
            return (DigitalState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0State"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalState)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0State"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0State"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalState value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0State"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0State"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalState value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0State register.
    /// </summary>
    /// <seealso cref="DO0State"/>
    [Description("Filters and selects timestamped messages from the DO0State register.")]
    public partial class TimestampedDO0State
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0State"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0State.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0State"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalState> GetPayload(HarpMessage message)
        {
            return DO0State.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables or disables hardware notifications.
    /// </summary>
    [Description("Enables or disables hardware notifications.")]
    public partial class HardwareNotificationsState
    {
        /// <summary>
        /// Represents the address of the <see cref="HardwareNotificationsState"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="HardwareNotificationsState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="HardwareNotificationsState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="HardwareNotificationsState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HardwareNotifications GetPayload(HarpMessage message)
        {
            return (HardwareNotifications)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="HardwareNotificationsState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HardwareNotifications)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="HardwareNotificationsState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="HardwareNotificationsState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="HardwareNotificationsState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="HardwareNotificationsState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// HardwareNotificationsState register.
    /// </summary>
    /// <seealso cref="HardwareNotificationsState"/>
    [Description("Filters and selects timestamped messages from the HardwareNotificationsState register.")]
    public partial class TimestampedHardwareNotificationsState
    {
        /// <summary>
        /// Represents the address of the <see cref="HardwareNotificationsState"/> register. This field is constant.
        /// </summary>
        public const int Address = HardwareNotificationsState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="HardwareNotificationsState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetPayload(HarpMessage message)
        {
            return HardwareNotificationsState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that triggers hardware notifications.
    /// </summary>
    [Description("Triggers hardware notifications.")]
    public partial class HardwareNotificationsTrigger
    {
        /// <summary>
        /// Represents the address of the <see cref="HardwareNotificationsTrigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="HardwareNotificationsTrigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="HardwareNotificationsTrigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="HardwareNotificationsTrigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HardwareNotifications GetPayload(HarpMessage message)
        {
            return (HardwareNotifications)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="HardwareNotificationsTrigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HardwareNotifications)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="HardwareNotificationsTrigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="HardwareNotificationsTrigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="HardwareNotificationsTrigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="HardwareNotificationsTrigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// HardwareNotificationsTrigger register.
    /// </summary>
    /// <seealso cref="HardwareNotificationsTrigger"/>
    [Description("Filters and selects timestamped messages from the HardwareNotificationsTrigger register.")]
    public partial class TimestampedHardwareNotificationsTrigger
    {
        /// <summary>
        /// Represents the address of the <see cref="HardwareNotificationsTrigger"/> register. This field is constant.
        /// </summary>
        public const int Address = HardwareNotificationsTrigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="HardwareNotificationsTrigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetPayload(HarpMessage message)
        {
            return HardwareNotificationsTrigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the buzzer will stay on (sensitive to multiples of 2).
    /// </summary>
    [Description("The time the buzzer will stay on (sensitive to multiples of 2).")]
    public partial class BuzzerDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="BuzzerDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BuzzerDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BuzzerDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BuzzerDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BuzzerDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BuzzerDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BuzzerDuration register.
    /// </summary>
    /// <seealso cref="BuzzerDuration"/>
    [Description("Filters and selects timestamped messages from the BuzzerDuration register.")]
    public partial class TimestampedBuzzerDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = BuzzerDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BuzzerDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BuzzerDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the top LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [Description("The time the top LED will stay on (sensitive to multiples of 2).")]
    public partial class TopLedDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="TopLedDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="TopLedDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="TopLedDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="TopLedDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="TopLedDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="TopLedDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// TopLedDuration register.
    /// </summary>
    /// <seealso cref="TopLedDuration"/>
    [Description("Filters and selects timestamped messages from the TopLedDuration register.")]
    public partial class TimestampedTopLedDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = TopLedDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="TopLedDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return TopLedDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the bottom LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [Description("The time the bottom LED will stay on (sensitive to multiples of 2).")]
    public partial class BottomLedDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="BottomLedDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BottomLedDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BottomLedDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BottomLedDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BottomLedDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BottomLedDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BottomLedDuration register.
    /// </summary>
    /// <seealso cref="BottomLedDuration"/>
    [Description("Filters and selects timestamped messages from the BottomLedDuration register.")]
    public partial class TimestampedBottomLedDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = BottomLedDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BottomLedDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BottomLedDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the frequency of the buzzer notification.
    /// </summary>
    [Description("The frequency of the buzzer notification.")]
    public partial class BuzzerFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="BuzzerFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BuzzerFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BuzzerFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BuzzerFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BuzzerFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BuzzerFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BuzzerFrequency register.
    /// </summary>
    /// <seealso cref="BuzzerFrequency"/>
    [Description("Filters and selects timestamped messages from the BuzzerFrequency register.")]
    public partial class TimestampedBuzzerFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = BuzzerFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BuzzerFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BuzzerFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the blink period of the top LED notification (sensitive to multiples of 2).
    /// </summary>
    [Description("The blink period of the top LED notification (sensitive to multiples of 2).")]
    public partial class TopLedPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="TopLedPeriod"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="TopLedPeriod"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="TopLedPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="TopLedPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="TopLedPeriod"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedPeriod"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="TopLedPeriod"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedPeriod"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// TopLedPeriod register.
    /// </summary>
    /// <seealso cref="TopLedPeriod"/>
    [Description("Filters and selects timestamped messages from the TopLedPeriod register.")]
    public partial class TimestampedTopLedPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = TopLedPeriod.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="TopLedPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return TopLedPeriod.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the blink period of the bottom LED notification (sensitive to multiples of 2).
    /// </summary>
    [Description("The blink period of the bottom LED notification (sensitive to multiples of 2).")]
    public partial class BottomLedPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="BottomLedPeriod"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BottomLedPeriod"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BottomLedPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BottomLedPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BottomLedPeriod"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedPeriod"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BottomLedPeriod"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedPeriod"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BottomLedPeriod register.
    /// </summary>
    /// <seealso cref="BottomLedPeriod"/>
    [Description("Filters and selects timestamped messages from the BottomLedPeriod register.")]
    public partial class TimestampedBottomLedPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = BottomLedPeriod.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BottomLedPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BottomLedPeriod.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class MatchTagId0
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId0"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U64;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ulong GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt64();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt64();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId0 register.
    /// </summary>
    /// <seealso cref="MatchTagId0"/>
    [Description("Filters and selects timestamped messages from the MatchTagId0 register.")]
    public partial class TimestampedMatchTagId0
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId0"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetPayload(HarpMessage message)
        {
            return MatchTagId0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class MatchTagId1
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId1"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U64;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ulong GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt64();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt64();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId1 register.
    /// </summary>
    /// <seealso cref="MatchTagId1"/>
    [Description("Filters and selects timestamped messages from the MatchTagId1 register.")]
    public partial class TimestampedMatchTagId1
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId1"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetPayload(HarpMessage message)
        {
            return MatchTagId1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class MatchTagId2
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId2"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U64;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ulong GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt64();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt64();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId2"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId2"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId2"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId2"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId2 register.
    /// </summary>
    /// <seealso cref="MatchTagId2"/>
    [Description("Filters and selects timestamped messages from the MatchTagId2 register.")]
    public partial class TimestampedMatchTagId2
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId2"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId2.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetPayload(HarpMessage message)
        {
            return MatchTagId2.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class MatchTagId3
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId3"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U64;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ulong GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt64();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt64();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId3"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId3"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId3"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId3"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ulong value)
        {
            return HarpMessage.FromUInt64(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId3 register.
    /// </summary>
    /// <seealso cref="MatchTagId3"/>
    [Description("Filters and selects timestamped messages from the MatchTagId3 register.")]
    public partial class TimestampedMatchTagId3
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId3"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId3.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ulong> GetPayload(HarpMessage message)
        {
            return MatchTagId3.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class MatchTagId0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId0PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId0PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId0PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId0PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId0PulseWidth register.
    /// </summary>
    /// <seealso cref="MatchTagId0PulseWidth"/>
    [Description("Filters and selects timestamped messages from the MatchTagId0PulseWidth register.")]
    public partial class TimestampedMatchTagId0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId0PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId0PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class MatchTagId1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId1PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId1PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId1PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId1PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId1PulseWidth register.
    /// </summary>
    /// <seealso cref="MatchTagId1PulseWidth"/>
    [Description("Filters and selects timestamped messages from the MatchTagId1PulseWidth register.")]
    public partial class TimestampedMatchTagId1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId1PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId1PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class MatchTagId2PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId2PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId2PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId2PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId2PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId2PulseWidth register.
    /// </summary>
    /// <seealso cref="MatchTagId2PulseWidth"/>
    [Description("Filters and selects timestamped messages from the MatchTagId2PulseWidth register.")]
    public partial class TimestampedMatchTagId2PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId2PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId2PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class MatchTagId3PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId3PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId3PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId3PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId3PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId3PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId3PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId3PulseWidth register.
    /// </summary>
    /// <seealso cref="MatchTagId3PulseWidth"/>
    [Description("Filters and selects timestamped messages from the MatchTagId3PulseWidth register.")]
    public partial class TimestampedMatchTagId3PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId3PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId3PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId3PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if any tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if any tag is detected.")]
    public partial class AnyTagIdPulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="AnyTagIdPulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="AnyTagIdPulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="AnyTagIdPulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AnyTagIdPulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AnyTagIdPulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AnyTagIdPulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnyTagIdPulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AnyTagIdPulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnyTagIdPulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AnyTagIdPulseWidth register.
    /// </summary>
    /// <seealso cref="AnyTagIdPulseWidth"/>
    [Description("Filters and selects timestamped messages from the AnyTagIdPulseWidth register.")]
    public partial class TimestampedAnyTagIdPulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="AnyTagIdPulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = AnyTagIdPulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AnyTagIdPulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return AnyTagIdPulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that triggers the digital output pin for the specified duration (ms).
    /// </summary>
    [Description("Triggers the digital output pin for the specified duration (ms).")]
    public partial class DO0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0PulseWidth register.
    /// </summary>
    /// <seealso cref="DO0PulseWidth"/>
    [Description("Filters and selects timestamped messages from the DO0PulseWidth register.")]
    public partial class TimestampedDO0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO0PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// RfidReader device.
    /// </summary>
    /// <seealso cref="CreateInboundDetectionIdPayload"/>
    /// <seealso cref="CreateOutboundDetectionIdPayload"/>
    /// <seealso cref="CreateDO0StatePayload"/>
    /// <seealso cref="CreateHardwareNotificationsStatePayload"/>
    /// <seealso cref="CreateHardwareNotificationsTriggerPayload"/>
    /// <seealso cref="CreateBuzzerDurationPayload"/>
    /// <seealso cref="CreateTopLedDurationPayload"/>
    /// <seealso cref="CreateBottomLedDurationPayload"/>
    /// <seealso cref="CreateBuzzerFrequencyPayload"/>
    /// <seealso cref="CreateTopLedPeriodPayload"/>
    /// <seealso cref="CreateBottomLedPeriodPayload"/>
    /// <seealso cref="CreateMatchTagId0Payload"/>
    /// <seealso cref="CreateMatchTagId1Payload"/>
    /// <seealso cref="CreateMatchTagId2Payload"/>
    /// <seealso cref="CreateMatchTagId3Payload"/>
    /// <seealso cref="CreateMatchTagId0PulseWidthPayload"/>
    /// <seealso cref="CreateMatchTagId1PulseWidthPayload"/>
    /// <seealso cref="CreateMatchTagId2PulseWidthPayload"/>
    /// <seealso cref="CreateMatchTagId3PulseWidthPayload"/>
    /// <seealso cref="CreateAnyTagIdPulseWidthPayload"/>
    /// <seealso cref="CreateDO0PulseWidthPayload"/>
    [XmlInclude(typeof(CreateInboundDetectionIdPayload))]
    [XmlInclude(typeof(CreateOutboundDetectionIdPayload))]
    [XmlInclude(typeof(CreateDO0StatePayload))]
    [XmlInclude(typeof(CreateHardwareNotificationsStatePayload))]
    [XmlInclude(typeof(CreateHardwareNotificationsTriggerPayload))]
    [XmlInclude(typeof(CreateBuzzerDurationPayload))]
    [XmlInclude(typeof(CreateTopLedDurationPayload))]
    [XmlInclude(typeof(CreateBottomLedDurationPayload))]
    [XmlInclude(typeof(CreateBuzzerFrequencyPayload))]
    [XmlInclude(typeof(CreateTopLedPeriodPayload))]
    [XmlInclude(typeof(CreateBottomLedPeriodPayload))]
    [XmlInclude(typeof(CreateMatchTagId0Payload))]
    [XmlInclude(typeof(CreateMatchTagId1Payload))]
    [XmlInclude(typeof(CreateMatchTagId2Payload))]
    [XmlInclude(typeof(CreateMatchTagId3Payload))]
    [XmlInclude(typeof(CreateMatchTagId0PulseWidthPayload))]
    [XmlInclude(typeof(CreateMatchTagId1PulseWidthPayload))]
    [XmlInclude(typeof(CreateMatchTagId2PulseWidthPayload))]
    [XmlInclude(typeof(CreateMatchTagId3PulseWidthPayload))]
    [XmlInclude(typeof(CreateAnyTagIdPulseWidthPayload))]
    [XmlInclude(typeof(CreateDO0PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedInboundDetectionIdPayload))]
    [XmlInclude(typeof(CreateTimestampedOutboundDetectionIdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0StatePayload))]
    [XmlInclude(typeof(CreateTimestampedHardwareNotificationsStatePayload))]
    [XmlInclude(typeof(CreateTimestampedHardwareNotificationsTriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedBuzzerDurationPayload))]
    [XmlInclude(typeof(CreateTimestampedTopLedDurationPayload))]
    [XmlInclude(typeof(CreateTimestampedBottomLedDurationPayload))]
    [XmlInclude(typeof(CreateTimestampedBuzzerFrequencyPayload))]
    [XmlInclude(typeof(CreateTimestampedTopLedPeriodPayload))]
    [XmlInclude(typeof(CreateTimestampedBottomLedPeriodPayload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId0Payload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId1Payload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId2Payload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId3Payload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId0PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId1PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId2PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedMatchTagId3PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedAnyTagIdPulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0PulseWidthPayload))]
    [Description("Creates standard message payloads for the RfidReader device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateInboundDetectionIdPayload();
        }

        string INamedElement.Name => $"{nameof(RfidReader)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the ID of the tag that was detected as having entered the area of the reader.
    /// </summary>
    [DisplayName("InboundDetectionIdPayload")]
    [Description("Creates a message payload that the ID of the tag that was detected as having entered the area of the reader.")]
    public partial class CreateInboundDetectionIdPayload
    {
        /// <summary>
        /// Gets or sets the value that the ID of the tag that was detected as having entered the area of the reader.
        /// </summary>
        [Description("The value that the ID of the tag that was detected as having entered the area of the reader.")]
        public ulong InboundDetectionId { get; set; }

        /// <summary>
        /// Creates a message payload for the InboundDetectionId register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ulong GetPayload()
        {
            return InboundDetectionId;
        }

        /// <summary>
        /// Creates a message that the ID of the tag that was detected as having entered the area of the reader.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the InboundDetectionId register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.InboundDetectionId.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the ID of the tag that was detected as having entered the area of the reader.
    /// </summary>
    [DisplayName("TimestampedInboundDetectionIdPayload")]
    [Description("Creates a timestamped message payload that the ID of the tag that was detected as having entered the area of the reader.")]
    public partial class CreateTimestampedInboundDetectionIdPayload : CreateInboundDetectionIdPayload
    {
        /// <summary>
        /// Creates a timestamped message that the ID of the tag that was detected as having entered the area of the reader.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the InboundDetectionId register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.InboundDetectionId.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the ID of the tag that was detected as having exited the area of the reader.
    /// </summary>
    [DisplayName("OutboundDetectionIdPayload")]
    [Description("Creates a message payload that the ID of the tag that was detected as having exited the area of the reader.")]
    public partial class CreateOutboundDetectionIdPayload
    {
        /// <summary>
        /// Gets or sets the value that the ID of the tag that was detected as having exited the area of the reader.
        /// </summary>
        [Description("The value that the ID of the tag that was detected as having exited the area of the reader.")]
        public ulong OutboundDetectionId { get; set; }

        /// <summary>
        /// Creates a message payload for the OutboundDetectionId register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ulong GetPayload()
        {
            return OutboundDetectionId;
        }

        /// <summary>
        /// Creates a message that the ID of the tag that was detected as having exited the area of the reader.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutboundDetectionId register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.OutboundDetectionId.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the ID of the tag that was detected as having exited the area of the reader.
    /// </summary>
    [DisplayName("TimestampedOutboundDetectionIdPayload")]
    [Description("Creates a timestamped message payload that the ID of the tag that was detected as having exited the area of the reader.")]
    public partial class CreateTimestampedOutboundDetectionIdPayload : CreateOutboundDetectionIdPayload
    {
        /// <summary>
        /// Creates a timestamped message that the ID of the tag that was detected as having exited the area of the reader.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutboundDetectionId register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.OutboundDetectionId.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that changes the state of the digital output pin.
    /// </summary>
    [DisplayName("DO0StatePayload")]
    [Description("Creates a message payload that changes the state of the digital output pin.")]
    public partial class CreateDO0StatePayload
    {
        /// <summary>
        /// Gets or sets the value that changes the state of the digital output pin.
        /// </summary>
        [Description("The value that changes the state of the digital output pin.")]
        public DigitalState DO0State { get; set; }

        /// <summary>
        /// Creates a message payload for the DO0State register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalState GetPayload()
        {
            return DO0State;
        }

        /// <summary>
        /// Creates a message that changes the state of the digital output pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0State register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.DO0State.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that changes the state of the digital output pin.
    /// </summary>
    [DisplayName("TimestampedDO0StatePayload")]
    [Description("Creates a timestamped message payload that changes the state of the digital output pin.")]
    public partial class CreateTimestampedDO0StatePayload : CreateDO0StatePayload
    {
        /// <summary>
        /// Creates a timestamped message that changes the state of the digital output pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0State register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.DO0State.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables or disables hardware notifications.
    /// </summary>
    [DisplayName("HardwareNotificationsStatePayload")]
    [Description("Creates a message payload that enables or disables hardware notifications.")]
    public partial class CreateHardwareNotificationsStatePayload
    {
        /// <summary>
        /// Gets or sets the value that enables or disables hardware notifications.
        /// </summary>
        [Description("The value that enables or disables hardware notifications.")]
        public HardwareNotifications HardwareNotificationsState { get; set; }

        /// <summary>
        /// Creates a message payload for the HardwareNotificationsState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public HardwareNotifications GetPayload()
        {
            return HardwareNotificationsState;
        }

        /// <summary>
        /// Creates a message that enables or disables hardware notifications.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the HardwareNotificationsState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.HardwareNotificationsState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables or disables hardware notifications.
    /// </summary>
    [DisplayName("TimestampedHardwareNotificationsStatePayload")]
    [Description("Creates a timestamped message payload that enables or disables hardware notifications.")]
    public partial class CreateTimestampedHardwareNotificationsStatePayload : CreateHardwareNotificationsStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables or disables hardware notifications.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the HardwareNotificationsState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.HardwareNotificationsState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that triggers hardware notifications.
    /// </summary>
    [DisplayName("HardwareNotificationsTriggerPayload")]
    [Description("Creates a message payload that triggers hardware notifications.")]
    public partial class CreateHardwareNotificationsTriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that triggers hardware notifications.
        /// </summary>
        [Description("The value that triggers hardware notifications.")]
        public HardwareNotifications HardwareNotificationsTrigger { get; set; }

        /// <summary>
        /// Creates a message payload for the HardwareNotificationsTrigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public HardwareNotifications GetPayload()
        {
            return HardwareNotificationsTrigger;
        }

        /// <summary>
        /// Creates a message that triggers hardware notifications.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the HardwareNotificationsTrigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.HardwareNotificationsTrigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that triggers hardware notifications.
    /// </summary>
    [DisplayName("TimestampedHardwareNotificationsTriggerPayload")]
    [Description("Creates a timestamped message payload that triggers hardware notifications.")]
    public partial class CreateTimestampedHardwareNotificationsTriggerPayload : CreateHardwareNotificationsTriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that triggers hardware notifications.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the HardwareNotificationsTrigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.HardwareNotificationsTrigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the buzzer will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("BuzzerDurationPayload")]
    [Description("Creates a message payload that the time the buzzer will stay on (sensitive to multiples of 2).")]
    public partial class CreateBuzzerDurationPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the buzzer will stay on (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the time the buzzer will stay on (sensitive to multiples of 2).")]
        public ushort BuzzerDuration { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the BuzzerDuration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return BuzzerDuration;
        }

        /// <summary>
        /// Creates a message that the time the buzzer will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the BuzzerDuration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.BuzzerDuration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the buzzer will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TimestampedBuzzerDurationPayload")]
    [Description("Creates a timestamped message payload that the time the buzzer will stay on (sensitive to multiples of 2).")]
    public partial class CreateTimestampedBuzzerDurationPayload : CreateBuzzerDurationPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the buzzer will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the BuzzerDuration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.BuzzerDuration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the top LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TopLedDurationPayload")]
    [Description("Creates a message payload that the time the top LED will stay on (sensitive to multiples of 2).")]
    public partial class CreateTopLedDurationPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the top LED will stay on (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the time the top LED will stay on (sensitive to multiples of 2).")]
        public ushort TopLedDuration { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the TopLedDuration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return TopLedDuration;
        }

        /// <summary>
        /// Creates a message that the time the top LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the TopLedDuration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.TopLedDuration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the top LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TimestampedTopLedDurationPayload")]
    [Description("Creates a timestamped message payload that the time the top LED will stay on (sensitive to multiples of 2).")]
    public partial class CreateTimestampedTopLedDurationPayload : CreateTopLedDurationPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the top LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the TopLedDuration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.TopLedDuration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the bottom LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("BottomLedDurationPayload")]
    [Description("Creates a message payload that the time the bottom LED will stay on (sensitive to multiples of 2).")]
    public partial class CreateBottomLedDurationPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the bottom LED will stay on (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the time the bottom LED will stay on (sensitive to multiples of 2).")]
        public ushort BottomLedDuration { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the BottomLedDuration register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return BottomLedDuration;
        }

        /// <summary>
        /// Creates a message that the time the bottom LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the BottomLedDuration register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.BottomLedDuration.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the bottom LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TimestampedBottomLedDurationPayload")]
    [Description("Creates a timestamped message payload that the time the bottom LED will stay on (sensitive to multiples of 2).")]
    public partial class CreateTimestampedBottomLedDurationPayload : CreateBottomLedDurationPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the bottom LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the BottomLedDuration register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.BottomLedDuration.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the frequency of the buzzer notification.
    /// </summary>
    [DisplayName("BuzzerFrequencyPayload")]
    [Description("Creates a message payload that the frequency of the buzzer notification.")]
    public partial class CreateBuzzerFrequencyPayload
    {
        /// <summary>
        /// Gets or sets the value that the frequency of the buzzer notification.
        /// </summary>
        [Range(min: 200, max: 15000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the frequency of the buzzer notification.")]
        public ushort BuzzerFrequency { get; set; } = 200;

        /// <summary>
        /// Creates a message payload for the BuzzerFrequency register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return BuzzerFrequency;
        }

        /// <summary>
        /// Creates a message that the frequency of the buzzer notification.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the BuzzerFrequency register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.BuzzerFrequency.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the frequency of the buzzer notification.
    /// </summary>
    [DisplayName("TimestampedBuzzerFrequencyPayload")]
    [Description("Creates a timestamped message payload that the frequency of the buzzer notification.")]
    public partial class CreateTimestampedBuzzerFrequencyPayload : CreateBuzzerFrequencyPayload
    {
        /// <summary>
        /// Creates a timestamped message that the frequency of the buzzer notification.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the BuzzerFrequency register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.BuzzerFrequency.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the blink period of the top LED notification (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TopLedPeriodPayload")]
    [Description("Creates a message payload that the blink period of the top LED notification (sensitive to multiples of 2).")]
    public partial class CreateTopLedPeriodPayload
    {
        /// <summary>
        /// Gets or sets the value that the blink period of the top LED notification (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the blink period of the top LED notification (sensitive to multiples of 2).")]
        public ushort TopLedPeriod { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the TopLedPeriod register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return TopLedPeriod;
        }

        /// <summary>
        /// Creates a message that the blink period of the top LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the TopLedPeriod register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.TopLedPeriod.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the blink period of the top LED notification (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TimestampedTopLedPeriodPayload")]
    [Description("Creates a timestamped message payload that the blink period of the top LED notification (sensitive to multiples of 2).")]
    public partial class CreateTimestampedTopLedPeriodPayload : CreateTopLedPeriodPayload
    {
        /// <summary>
        /// Creates a timestamped message that the blink period of the top LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the TopLedPeriod register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.TopLedPeriod.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the blink period of the bottom LED notification (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("BottomLedPeriodPayload")]
    [Description("Creates a message payload that the blink period of the bottom LED notification (sensitive to multiples of 2).")]
    public partial class CreateBottomLedPeriodPayload
    {
        /// <summary>
        /// Gets or sets the value that the blink period of the bottom LED notification (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the blink period of the bottom LED notification (sensitive to multiples of 2).")]
        public ushort BottomLedPeriod { get; set; } = 2;

        /// <summary>
        /// Creates a message payload for the BottomLedPeriod register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return BottomLedPeriod;
        }

        /// <summary>
        /// Creates a message that the blink period of the bottom LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the BottomLedPeriod register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.BottomLedPeriod.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the blink period of the bottom LED notification (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TimestampedBottomLedPeriodPayload")]
    [Description("Creates a timestamped message payload that the blink period of the bottom LED notification (sensitive to multiples of 2).")]
    public partial class CreateTimestampedBottomLedPeriodPayload : CreateBottomLedPeriodPayload
    {
        /// <summary>
        /// Creates a timestamped message that the blink period of the bottom LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the BottomLedPeriod register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.BottomLedPeriod.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId0Payload")]
    [Description("Creates a message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId0Payload
    {
        /// <summary>
        /// Gets or sets the value that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends detection event notifications only when the tag with the specified ID is detected.")]
        public ulong MatchTagId0 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the MatchTagId0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ulong GetPayload()
        {
            return MatchTagId0;
        }

        /// <summary>
        /// Creates a message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId0Payload")]
    [Description("Creates a timestamped message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateTimestampedMatchTagId0Payload : CreateMatchTagId0Payload
    {
        /// <summary>
        /// Creates a timestamped message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId1Payload")]
    [Description("Creates a message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId1Payload
    {
        /// <summary>
        /// Gets or sets the value that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends detection event notifications only when the tag with the specified ID is detected.")]
        public ulong MatchTagId1 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the MatchTagId1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ulong GetPayload()
        {
            return MatchTagId1;
        }

        /// <summary>
        /// Creates a message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId1Payload")]
    [Description("Creates a timestamped message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateTimestampedMatchTagId1Payload : CreateMatchTagId1Payload
    {
        /// <summary>
        /// Creates a timestamped message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId2Payload")]
    [Description("Creates a message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId2Payload
    {
        /// <summary>
        /// Gets or sets the value that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends detection event notifications only when the tag with the specified ID is detected.")]
        public ulong MatchTagId2 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the MatchTagId2 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ulong GetPayload()
        {
            return MatchTagId2;
        }

        /// <summary>
        /// Creates a message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId2 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId2.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId2Payload")]
    [Description("Creates a timestamped message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateTimestampedMatchTagId2Payload : CreateMatchTagId2Payload
    {
        /// <summary>
        /// Creates a timestamped message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId2 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId2.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId3Payload")]
    [Description("Creates a message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId3Payload
    {
        /// <summary>
        /// Gets or sets the value that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends detection event notifications only when the tag with the specified ID is detected.")]
        public ulong MatchTagId3 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the MatchTagId3 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ulong GetPayload()
        {
            return MatchTagId3;
        }

        /// <summary>
        /// Creates a message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId3 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId3.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that sends detection event notifications only when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId3Payload")]
    [Description("Creates a timestamped message payload that sends detection event notifications only when the tag with the specified ID is detected.")]
    public partial class CreateTimestampedMatchTagId3Payload : CreateMatchTagId3Payload
    {
        /// <summary>
        /// Creates a timestamped message that sends detection event notifications only when the tag with the specified ID is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId3 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId3.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId0PulseWidthPayload")]
    [Description("Creates a message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId0PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort MatchTagId0PulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the MatchTagId0PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return MatchTagId0PulseWidth;
        }

        /// <summary>
        /// Creates a message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId0PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId0PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId0PulseWidthPayload")]
    [Description("Creates a timestamped message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateTimestampedMatchTagId0PulseWidthPayload : CreateMatchTagId0PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId0PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId0PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId1PulseWidthPayload")]
    [Description("Creates a message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId1PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort MatchTagId1PulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the MatchTagId1PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return MatchTagId1PulseWidth;
        }

        /// <summary>
        /// Creates a message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId1PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId1PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId1PulseWidthPayload")]
    [Description("Creates a timestamped message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateTimestampedMatchTagId1PulseWidthPayload : CreateMatchTagId1PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId1PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId1PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId2PulseWidthPayload")]
    [Description("Creates a message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId2PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort MatchTagId2PulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the MatchTagId2PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return MatchTagId2PulseWidth;
        }

        /// <summary>
        /// Creates a message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId2PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId2PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId2PulseWidthPayload")]
    [Description("Creates a timestamped message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateTimestampedMatchTagId2PulseWidthPayload : CreateMatchTagId2PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId2PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId2PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId3PulseWidthPayload")]
    [Description("Creates a message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId3PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort MatchTagId3PulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the MatchTagId3PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return MatchTagId3PulseWidth;
        }

        /// <summary>
        /// Creates a message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the MatchTagId3PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId3PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("TimestampedMatchTagId3PulseWidthPayload")]
    [Description("Creates a timestamped message payload that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateTimestampedMatchTagId3PulseWidthPayload : CreateMatchTagId3PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the MatchTagId3PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.MatchTagId3PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the time the digital output pin will stay on (ms) if any tag is detected.
    /// </summary>
    [DisplayName("AnyTagIdPulseWidthPayload")]
    [Description("Creates a message payload that the time the digital output pin will stay on (ms) if any tag is detected.")]
    public partial class CreateAnyTagIdPulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if any tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if any tag is detected.")]
        public ushort AnyTagIdPulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the AnyTagIdPulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return AnyTagIdPulseWidth;
        }

        /// <summary>
        /// Creates a message that the time the digital output pin will stay on (ms) if any tag is detected.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AnyTagIdPulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.AnyTagIdPulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the time the digital output pin will stay on (ms) if any tag is detected.
    /// </summary>
    [DisplayName("TimestampedAnyTagIdPulseWidthPayload")]
    [Description("Creates a timestamped message payload that the time the digital output pin will stay on (ms) if any tag is detected.")]
    public partial class CreateTimestampedAnyTagIdPulseWidthPayload : CreateAnyTagIdPulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that the time the digital output pin will stay on (ms) if any tag is detected.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AnyTagIdPulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.AnyTagIdPulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that triggers the digital output pin for the specified duration (ms).
    /// </summary>
    [DisplayName("DO0PulseWidthPayload")]
    [Description("Creates a message payload that triggers the digital output pin for the specified duration (ms).")]
    public partial class CreateDO0PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that triggers the digital output pin for the specified duration (ms).
        /// </summary>
        [Description("The value that triggers the digital output pin for the specified duration (ms).")]
        public ushort DO0PulseWidth { get; set; }

        /// <summary>
        /// Creates a message payload for the DO0PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO0PulseWidth;
        }

        /// <summary>
        /// Creates a message that triggers the digital output pin for the specified duration (ms).
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RfidReader.DO0PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that triggers the digital output pin for the specified duration (ms).
    /// </summary>
    [DisplayName("TimestampedDO0PulseWidthPayload")]
    [Description("Creates a timestamped message payload that triggers the digital output pin for the specified duration (ms).")]
    public partial class CreateTimestampedDO0PulseWidthPayload : CreateDO0PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that triggers the digital output pin for the specified duration (ms).
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RfidReader.DO0PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// The available hardware notifications.
    /// </summary>
    [Flags]
    public enum HardwareNotifications : byte
    {
        None = 0x0,
        Buzzer = 0x1,
        TopLed = 0x2,
        BottomLed = 0x4
    }

    /// <summary>
    /// The state of the digital output pin.
    /// </summary>
    public enum DigitalState : byte
    {
        Low = 0,
        High = 1
    }
}
