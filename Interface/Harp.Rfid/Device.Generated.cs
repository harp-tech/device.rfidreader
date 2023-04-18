using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.Rfid
{
    /// <summary>
    /// Generates events and processes commands for the Rfid device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the Rfid device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="Rfid"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 2094;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(Rfid);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(InboundDetectionId) },
            { 33, typeof(OutboundDetectionId) },
            { 34, typeof(DO0State) },
            { 35, typeof(EnableHardwareNotifications) },
            { 36, typeof(TriggerHardwareNotifications) },
            { 37, typeof(BuzzerNotificationDuration) },
            { 38, typeof(TopLedNotificationDuration) },
            { 39, typeof(BottomLedNotificationDuration) },
            { 40, typeof(BuzzerNotificationFrequency) },
            { 41, typeof(TopLedNotificationPeriod) },
            { 42, typeof(BottomLedNotificationPeriod) },
            { 45, typeof(MatchTagId0) },
            { 46, typeof(MatchTagId1) },
            { 47, typeof(MatchTagId2) },
            { 48, typeof(MatchTagId3) },
            { 49, typeof(MatchTagId0PulseDuration) },
            { 50, typeof(MatchTagId1PulseDuration) },
            { 51, typeof(MatchTagId2PulseDuration) },
            { 52, typeof(MatchTagId3PulseDuration) },
            { 53, typeof(AnyTagIdPulseDuration) },
            { 54, typeof(PulseDO0Duration) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="Rfid"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of Rfid messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="Rfid"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="Rfid"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="Rfid"/> device.
    /// </summary>
    /// <seealso cref="InboundDetectionId"/>
    /// <seealso cref="OutboundDetectionId"/>
    /// <seealso cref="DO0State"/>
    /// <seealso cref="EnableHardwareNotifications"/>
    /// <seealso cref="TriggerHardwareNotifications"/>
    /// <seealso cref="BuzzerNotificationDuration"/>
    /// <seealso cref="TopLedNotificationDuration"/>
    /// <seealso cref="BottomLedNotificationDuration"/>
    /// <seealso cref="BuzzerNotificationFrequency"/>
    /// <seealso cref="TopLedNotificationPeriod"/>
    /// <seealso cref="BottomLedNotificationPeriod"/>
    /// <seealso cref="MatchTagId0"/>
    /// <seealso cref="MatchTagId1"/>
    /// <seealso cref="MatchTagId2"/>
    /// <seealso cref="MatchTagId3"/>
    /// <seealso cref="MatchTagId0PulseDuration"/>
    /// <seealso cref="MatchTagId1PulseDuration"/>
    /// <seealso cref="MatchTagId2PulseDuration"/>
    /// <seealso cref="MatchTagId3PulseDuration"/>
    /// <seealso cref="AnyTagIdPulseDuration"/>
    /// <seealso cref="PulseDO0Duration"/>
    [XmlInclude(typeof(InboundDetectionId))]
    [XmlInclude(typeof(OutboundDetectionId))]
    [XmlInclude(typeof(DO0State))]
    [XmlInclude(typeof(EnableHardwareNotifications))]
    [XmlInclude(typeof(TriggerHardwareNotifications))]
    [XmlInclude(typeof(BuzzerNotificationDuration))]
    [XmlInclude(typeof(TopLedNotificationDuration))]
    [XmlInclude(typeof(BottomLedNotificationDuration))]
    [XmlInclude(typeof(BuzzerNotificationFrequency))]
    [XmlInclude(typeof(TopLedNotificationPeriod))]
    [XmlInclude(typeof(BottomLedNotificationPeriod))]
    [XmlInclude(typeof(MatchTagId0))]
    [XmlInclude(typeof(MatchTagId1))]
    [XmlInclude(typeof(MatchTagId2))]
    [XmlInclude(typeof(MatchTagId3))]
    [XmlInclude(typeof(MatchTagId0PulseDuration))]
    [XmlInclude(typeof(MatchTagId1PulseDuration))]
    [XmlInclude(typeof(MatchTagId2PulseDuration))]
    [XmlInclude(typeof(MatchTagId3PulseDuration))]
    [XmlInclude(typeof(AnyTagIdPulseDuration))]
    [XmlInclude(typeof(PulseDO0Duration))]
    [Description("Filters register-specific messages reported by the Rfid device.")]
    public class FilterMessage : FilterMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMessage"/> class.
        /// </summary>
        public FilterMessage()
        {
            Register = new InboundDetectionId();
        }

        string INamedElement.Name
        {
            get => $"{nameof(Rfid)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the Rfid device.
    /// </summary>
    /// <seealso cref="InboundDetectionId"/>
    /// <seealso cref="OutboundDetectionId"/>
    /// <seealso cref="DO0State"/>
    /// <seealso cref="EnableHardwareNotifications"/>
    /// <seealso cref="TriggerHardwareNotifications"/>
    /// <seealso cref="BuzzerNotificationDuration"/>
    /// <seealso cref="TopLedNotificationDuration"/>
    /// <seealso cref="BottomLedNotificationDuration"/>
    /// <seealso cref="BuzzerNotificationFrequency"/>
    /// <seealso cref="TopLedNotificationPeriod"/>
    /// <seealso cref="BottomLedNotificationPeriod"/>
    /// <seealso cref="MatchTagId0"/>
    /// <seealso cref="MatchTagId1"/>
    /// <seealso cref="MatchTagId2"/>
    /// <seealso cref="MatchTagId3"/>
    /// <seealso cref="MatchTagId0PulseDuration"/>
    /// <seealso cref="MatchTagId1PulseDuration"/>
    /// <seealso cref="MatchTagId2PulseDuration"/>
    /// <seealso cref="MatchTagId3PulseDuration"/>
    /// <seealso cref="AnyTagIdPulseDuration"/>
    /// <seealso cref="PulseDO0Duration"/>
    [XmlInclude(typeof(InboundDetectionId))]
    [XmlInclude(typeof(OutboundDetectionId))]
    [XmlInclude(typeof(DO0State))]
    [XmlInclude(typeof(EnableHardwareNotifications))]
    [XmlInclude(typeof(TriggerHardwareNotifications))]
    [XmlInclude(typeof(BuzzerNotificationDuration))]
    [XmlInclude(typeof(TopLedNotificationDuration))]
    [XmlInclude(typeof(BottomLedNotificationDuration))]
    [XmlInclude(typeof(BuzzerNotificationFrequency))]
    [XmlInclude(typeof(TopLedNotificationPeriod))]
    [XmlInclude(typeof(BottomLedNotificationPeriod))]
    [XmlInclude(typeof(MatchTagId0))]
    [XmlInclude(typeof(MatchTagId1))]
    [XmlInclude(typeof(MatchTagId2))]
    [XmlInclude(typeof(MatchTagId3))]
    [XmlInclude(typeof(MatchTagId0PulseDuration))]
    [XmlInclude(typeof(MatchTagId1PulseDuration))]
    [XmlInclude(typeof(MatchTagId2PulseDuration))]
    [XmlInclude(typeof(MatchTagId3PulseDuration))]
    [XmlInclude(typeof(AnyTagIdPulseDuration))]
    [XmlInclude(typeof(PulseDO0Duration))]
    [XmlInclude(typeof(TimestampedInboundDetectionId))]
    [XmlInclude(typeof(TimestampedOutboundDetectionId))]
    [XmlInclude(typeof(TimestampedDO0State))]
    [XmlInclude(typeof(TimestampedEnableHardwareNotifications))]
    [XmlInclude(typeof(TimestampedTriggerHardwareNotifications))]
    [XmlInclude(typeof(TimestampedBuzzerNotificationDuration))]
    [XmlInclude(typeof(TimestampedTopLedNotificationDuration))]
    [XmlInclude(typeof(TimestampedBottomLedNotificationDuration))]
    [XmlInclude(typeof(TimestampedBuzzerNotificationFrequency))]
    [XmlInclude(typeof(TimestampedTopLedNotificationPeriod))]
    [XmlInclude(typeof(TimestampedBottomLedNotificationPeriod))]
    [XmlInclude(typeof(TimestampedMatchTagId0))]
    [XmlInclude(typeof(TimestampedMatchTagId1))]
    [XmlInclude(typeof(TimestampedMatchTagId2))]
    [XmlInclude(typeof(TimestampedMatchTagId3))]
    [XmlInclude(typeof(TimestampedMatchTagId0PulseDuration))]
    [XmlInclude(typeof(TimestampedMatchTagId1PulseDuration))]
    [XmlInclude(typeof(TimestampedMatchTagId2PulseDuration))]
    [XmlInclude(typeof(TimestampedMatchTagId3PulseDuration))]
    [XmlInclude(typeof(TimestampedAnyTagIdPulseDuration))]
    [XmlInclude(typeof(TimestampedPulseDO0Duration))]
    [Description("Filters and selects specific messages reported by the Rfid device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new InboundDetectionId();
        }

        string INamedElement.Name => $"{nameof(Rfid)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// Rfid register messages.
    /// </summary>
    /// <seealso cref="InboundDetectionId"/>
    /// <seealso cref="OutboundDetectionId"/>
    /// <seealso cref="DO0State"/>
    /// <seealso cref="EnableHardwareNotifications"/>
    /// <seealso cref="TriggerHardwareNotifications"/>
    /// <seealso cref="BuzzerNotificationDuration"/>
    /// <seealso cref="TopLedNotificationDuration"/>
    /// <seealso cref="BottomLedNotificationDuration"/>
    /// <seealso cref="BuzzerNotificationFrequency"/>
    /// <seealso cref="TopLedNotificationPeriod"/>
    /// <seealso cref="BottomLedNotificationPeriod"/>
    /// <seealso cref="MatchTagId0"/>
    /// <seealso cref="MatchTagId1"/>
    /// <seealso cref="MatchTagId2"/>
    /// <seealso cref="MatchTagId3"/>
    /// <seealso cref="MatchTagId0PulseDuration"/>
    /// <seealso cref="MatchTagId1PulseDuration"/>
    /// <seealso cref="MatchTagId2PulseDuration"/>
    /// <seealso cref="MatchTagId3PulseDuration"/>
    /// <seealso cref="AnyTagIdPulseDuration"/>
    /// <seealso cref="PulseDO0Duration"/>
    [XmlInclude(typeof(InboundDetectionId))]
    [XmlInclude(typeof(OutboundDetectionId))]
    [XmlInclude(typeof(DO0State))]
    [XmlInclude(typeof(EnableHardwareNotifications))]
    [XmlInclude(typeof(TriggerHardwareNotifications))]
    [XmlInclude(typeof(BuzzerNotificationDuration))]
    [XmlInclude(typeof(TopLedNotificationDuration))]
    [XmlInclude(typeof(BottomLedNotificationDuration))]
    [XmlInclude(typeof(BuzzerNotificationFrequency))]
    [XmlInclude(typeof(TopLedNotificationPeriod))]
    [XmlInclude(typeof(BottomLedNotificationPeriod))]
    [XmlInclude(typeof(MatchTagId0))]
    [XmlInclude(typeof(MatchTagId1))]
    [XmlInclude(typeof(MatchTagId2))]
    [XmlInclude(typeof(MatchTagId3))]
    [XmlInclude(typeof(MatchTagId0PulseDuration))]
    [XmlInclude(typeof(MatchTagId1PulseDuration))]
    [XmlInclude(typeof(MatchTagId2PulseDuration))]
    [XmlInclude(typeof(MatchTagId3PulseDuration))]
    [XmlInclude(typeof(AnyTagIdPulseDuration))]
    [XmlInclude(typeof(PulseDO0Duration))]
    [Description("Formats a sequence of values as specific Rfid register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new InboundDetectionId();
        }

        string INamedElement.Name => $"{nameof(Rfid)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that the ID of the tag that was detected as in entered the area of the reader.
    /// </summary>
    [Description("The ID of the tag that was detected as in entered the area of the reader.")]
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
    /// Represents a register that the ID of the tag that was detected as in exited the area of the reader.
    /// </summary>
    [Description("The ID of the tag that was detected as in exited the area of the reader.")]
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
        public static DigitalOutputState GetPayload(HarpMessage message)
        {
            return (DigitalOutputState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0State"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputState)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputState value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputState value)
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
        public static Timestamped<DigitalOutputState> GetPayload(HarpMessage message)
        {
            return DO0State.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables or disables hardware notifications.
    /// </summary>
    [Description("Enables or disables hardware notifications.")]
    public partial class EnableHardwareNotifications
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableHardwareNotifications"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HardwareNotifications GetPayload(HarpMessage message)
        {
            return (HardwareNotifications)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableHardwareNotifications"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HardwareNotifications)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableHardwareNotifications"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableHardwareNotifications"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableHardwareNotifications"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableHardwareNotifications"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableHardwareNotifications register.
    /// </summary>
    /// <seealso cref="EnableHardwareNotifications"/>
    [Description("Filters and selects timestamped messages from the EnableHardwareNotifications register.")]
    public partial class TimestampedEnableHardwareNotifications
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableHardwareNotifications.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableHardwareNotifications"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetPayload(HarpMessage message)
        {
            return EnableHardwareNotifications.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that triggers hardware notifications.
    /// </summary>
    [Description("Triggers hardware notifications.")]
    public partial class TriggerHardwareNotifications
    {
        /// <summary>
        /// Represents the address of the <see cref="TriggerHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="TriggerHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="TriggerHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="TriggerHardwareNotifications"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static HardwareNotifications GetPayload(HarpMessage message)
        {
            return (HardwareNotifications)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="TriggerHardwareNotifications"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((HardwareNotifications)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="TriggerHardwareNotifications"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TriggerHardwareNotifications"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="TriggerHardwareNotifications"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TriggerHardwareNotifications"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, HardwareNotifications value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// TriggerHardwareNotifications register.
    /// </summary>
    /// <seealso cref="TriggerHardwareNotifications"/>
    [Description("Filters and selects timestamped messages from the TriggerHardwareNotifications register.")]
    public partial class TimestampedTriggerHardwareNotifications
    {
        /// <summary>
        /// Represents the address of the <see cref="TriggerHardwareNotifications"/> register. This field is constant.
        /// </summary>
        public const int Address = TriggerHardwareNotifications.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="TriggerHardwareNotifications"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<HardwareNotifications> GetPayload(HarpMessage message)
        {
            return TriggerHardwareNotifications.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the buzzer will stay on (sensitive to multiples of 2).
    /// </summary>
    [Description("The time the buzzer will stay on (sensitive to multiples of 2).")]
    public partial class BuzzerNotificationDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="BuzzerNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BuzzerNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BuzzerNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BuzzerNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BuzzerNotificationDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerNotificationDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BuzzerNotificationDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerNotificationDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BuzzerNotificationDuration register.
    /// </summary>
    /// <seealso cref="BuzzerNotificationDuration"/>
    [Description("Filters and selects timestamped messages from the BuzzerNotificationDuration register.")]
    public partial class TimestampedBuzzerNotificationDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = BuzzerNotificationDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BuzzerNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BuzzerNotificationDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the top LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [Description("The time the top LED will stay on (sensitive to multiples of 2).")]
    public partial class TopLedNotificationDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="TopLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="TopLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="TopLedNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="TopLedNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="TopLedNotificationDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedNotificationDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="TopLedNotificationDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedNotificationDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// TopLedNotificationDuration register.
    /// </summary>
    /// <seealso cref="TopLedNotificationDuration"/>
    [Description("Filters and selects timestamped messages from the TopLedNotificationDuration register.")]
    public partial class TimestampedTopLedNotificationDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = TopLedNotificationDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="TopLedNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return TopLedNotificationDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the bottom LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [Description("The time the bottom LED will stay on (sensitive to multiples of 2).")]
    public partial class BottomLedNotificationDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="BottomLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BottomLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BottomLedNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BottomLedNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BottomLedNotificationDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedNotificationDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BottomLedNotificationDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedNotificationDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BottomLedNotificationDuration register.
    /// </summary>
    /// <seealso cref="BottomLedNotificationDuration"/>
    [Description("Filters and selects timestamped messages from the BottomLedNotificationDuration register.")]
    public partial class TimestampedBottomLedNotificationDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedNotificationDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = BottomLedNotificationDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BottomLedNotificationDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BottomLedNotificationDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the frequency of the buzzer notification.
    /// </summary>
    [Description("The frequency of the buzzer notification.")]
    public partial class BuzzerNotificationFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerNotificationFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="BuzzerNotificationFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BuzzerNotificationFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BuzzerNotificationFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BuzzerNotificationFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BuzzerNotificationFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerNotificationFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BuzzerNotificationFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BuzzerNotificationFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BuzzerNotificationFrequency register.
    /// </summary>
    /// <seealso cref="BuzzerNotificationFrequency"/>
    [Description("Filters and selects timestamped messages from the BuzzerNotificationFrequency register.")]
    public partial class TimestampedBuzzerNotificationFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="BuzzerNotificationFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = BuzzerNotificationFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BuzzerNotificationFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BuzzerNotificationFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the blink period of the top LED notification (sensitive to multiples of 2).
    /// </summary>
    [Description("The blink period of the top LED notification (sensitive to multiples of 2).")]
    public partial class TopLedNotificationPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="TopLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="TopLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="TopLedNotificationPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="TopLedNotificationPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="TopLedNotificationPeriod"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedNotificationPeriod"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="TopLedNotificationPeriod"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="TopLedNotificationPeriod"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// TopLedNotificationPeriod register.
    /// </summary>
    /// <seealso cref="TopLedNotificationPeriod"/>
    [Description("Filters and selects timestamped messages from the TopLedNotificationPeriod register.")]
    public partial class TimestampedTopLedNotificationPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="TopLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = TopLedNotificationPeriod.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="TopLedNotificationPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return TopLedNotificationPeriod.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the blink period of the bottom LED notification (sensitive to multiples of 2).
    /// </summary>
    [Description("The blink period of the bottom LED notification (sensitive to multiples of 2).")]
    public partial class BottomLedNotificationPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="BottomLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="BottomLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="BottomLedNotificationPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="BottomLedNotificationPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="BottomLedNotificationPeriod"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedNotificationPeriod"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="BottomLedNotificationPeriod"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="BottomLedNotificationPeriod"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// BottomLedNotificationPeriod register.
    /// </summary>
    /// <seealso cref="BottomLedNotificationPeriod"/>
    [Description("Filters and selects timestamped messages from the BottomLedNotificationPeriod register.")]
    public partial class TimestampedBottomLedNotificationPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="BottomLedNotificationPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = BottomLedNotificationPeriod.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="BottomLedNotificationPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return BottomLedNotificationPeriod.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends a notification when the tag with the specified ID is detected.")]
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
    /// Represents a register that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends a notification when the tag with the specified ID is detected.")]
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
    /// Represents a register that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends a notification when the tag with the specified ID is detected.")]
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
    /// Represents a register that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [Description("Sends a notification when the tag with the specified ID is detected.")]
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
    public partial class MatchTagId0PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId0PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId0PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId0PulseDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId0PulseDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId0PulseDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId0PulseDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId0PulseDuration register.
    /// </summary>
    /// <seealso cref="MatchTagId0PulseDuration"/>
    [Description("Filters and selects timestamped messages from the MatchTagId0PulseDuration register.")]
    public partial class TimestampedMatchTagId0PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId0PulseDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId0PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId0PulseDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class MatchTagId1PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId1PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId1PulseDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId1PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId1PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId1PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId1PulseDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId1PulseDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId1PulseDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId1PulseDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId1PulseDuration register.
    /// </summary>
    /// <seealso cref="MatchTagId1PulseDuration"/>
    [Description("Filters and selects timestamped messages from the MatchTagId1PulseDuration register.")]
    public partial class TimestampedMatchTagId1PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId1PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId1PulseDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId1PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId1PulseDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class MatchTagId2PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId2PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId2PulseDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId2PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId2PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId2PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId2PulseDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId2PulseDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId2PulseDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId2PulseDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId2PulseDuration register.
    /// </summary>
    /// <seealso cref="MatchTagId2PulseDuration"/>
    [Description("Filters and selects timestamped messages from the MatchTagId2PulseDuration register.")]
    public partial class TimestampedMatchTagId2PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId2PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId2PulseDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId2PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId2PulseDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class MatchTagId3PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId3PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="MatchTagId3PulseDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="MatchTagId3PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="MatchTagId3PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="MatchTagId3PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="MatchTagId3PulseDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId3PulseDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="MatchTagId3PulseDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="MatchTagId3PulseDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// MatchTagId3PulseDuration register.
    /// </summary>
    /// <seealso cref="MatchTagId3PulseDuration"/>
    [Description("Filters and selects timestamped messages from the MatchTagId3PulseDuration register.")]
    public partial class TimestampedMatchTagId3PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="MatchTagId3PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = MatchTagId3PulseDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="MatchTagId3PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return MatchTagId3PulseDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the time the digital output pin will stay on (ms) if any tag is detected.
    /// </summary>
    [Description("The time the digital output pin will stay on (ms) if any tag is detected.")]
    public partial class AnyTagIdPulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="AnyTagIdPulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="AnyTagIdPulseDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="AnyTagIdPulseDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AnyTagIdPulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AnyTagIdPulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AnyTagIdPulseDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnyTagIdPulseDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AnyTagIdPulseDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnyTagIdPulseDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AnyTagIdPulseDuration register.
    /// </summary>
    /// <seealso cref="AnyTagIdPulseDuration"/>
    [Description("Filters and selects timestamped messages from the AnyTagIdPulseDuration register.")]
    public partial class TimestampedAnyTagIdPulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="AnyTagIdPulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = AnyTagIdPulseDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AnyTagIdPulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return AnyTagIdPulseDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that triggers the digital output pin for the specified duration (ms).
    /// </summary>
    [Description("Triggers the digital output pin for the specified duration (ms).")]
    public partial class PulseDO0Duration
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO0Duration"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="PulseDO0Duration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PulseDO0Duration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PulseDO0Duration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PulseDO0Duration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PulseDO0Duration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO0Duration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PulseDO0Duration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PulseDO0Duration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PulseDO0Duration register.
    /// </summary>
    /// <seealso cref="PulseDO0Duration"/>
    [Description("Filters and selects timestamped messages from the PulseDO0Duration register.")]
    public partial class TimestampedPulseDO0Duration
    {
        /// <summary>
        /// Represents the address of the <see cref="PulseDO0Duration"/> register. This field is constant.
        /// </summary>
        public const int Address = PulseDO0Duration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PulseDO0Duration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return PulseDO0Duration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// Rfid device.
    /// </summary>
    /// <seealso cref="CreateInboundDetectionIdPayload"/>
    /// <seealso cref="CreateOutboundDetectionIdPayload"/>
    /// <seealso cref="CreateDO0StatePayload"/>
    /// <seealso cref="CreateEnableHardwareNotificationsPayload"/>
    /// <seealso cref="CreateTriggerHardwareNotificationsPayload"/>
    /// <seealso cref="CreateBuzzerNotificationDurationPayload"/>
    /// <seealso cref="CreateTopLedNotificationDurationPayload"/>
    /// <seealso cref="CreateBottomLedNotificationDurationPayload"/>
    /// <seealso cref="CreateBuzzerNotificationFrequencyPayload"/>
    /// <seealso cref="CreateTopLedNotificationPeriodPayload"/>
    /// <seealso cref="CreateBottomLedNotificationPeriodPayload"/>
    /// <seealso cref="CreateMatchTagId0Payload"/>
    /// <seealso cref="CreateMatchTagId1Payload"/>
    /// <seealso cref="CreateMatchTagId2Payload"/>
    /// <seealso cref="CreateMatchTagId3Payload"/>
    /// <seealso cref="CreateMatchTagId0PulseDurationPayload"/>
    /// <seealso cref="CreateMatchTagId1PulseDurationPayload"/>
    /// <seealso cref="CreateMatchTagId2PulseDurationPayload"/>
    /// <seealso cref="CreateMatchTagId3PulseDurationPayload"/>
    /// <seealso cref="CreateAnyTagIdPulseDurationPayload"/>
    /// <seealso cref="CreatePulseDO0DurationPayload"/>
    [XmlInclude(typeof(CreateInboundDetectionIdPayload))]
    [XmlInclude(typeof(CreateOutboundDetectionIdPayload))]
    [XmlInclude(typeof(CreateDO0StatePayload))]
    [XmlInclude(typeof(CreateEnableHardwareNotificationsPayload))]
    [XmlInclude(typeof(CreateTriggerHardwareNotificationsPayload))]
    [XmlInclude(typeof(CreateBuzzerNotificationDurationPayload))]
    [XmlInclude(typeof(CreateTopLedNotificationDurationPayload))]
    [XmlInclude(typeof(CreateBottomLedNotificationDurationPayload))]
    [XmlInclude(typeof(CreateBuzzerNotificationFrequencyPayload))]
    [XmlInclude(typeof(CreateTopLedNotificationPeriodPayload))]
    [XmlInclude(typeof(CreateBottomLedNotificationPeriodPayload))]
    [XmlInclude(typeof(CreateMatchTagId0Payload))]
    [XmlInclude(typeof(CreateMatchTagId1Payload))]
    [XmlInclude(typeof(CreateMatchTagId2Payload))]
    [XmlInclude(typeof(CreateMatchTagId3Payload))]
    [XmlInclude(typeof(CreateMatchTagId0PulseDurationPayload))]
    [XmlInclude(typeof(CreateMatchTagId1PulseDurationPayload))]
    [XmlInclude(typeof(CreateMatchTagId2PulseDurationPayload))]
    [XmlInclude(typeof(CreateMatchTagId3PulseDurationPayload))]
    [XmlInclude(typeof(CreateAnyTagIdPulseDurationPayload))]
    [XmlInclude(typeof(CreatePulseDO0DurationPayload))]
    [Description("Creates standard message payloads for the Rfid device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateInboundDetectionIdPayload();
        }

        string INamedElement.Name => $"{nameof(Rfid)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the ID of the tag that was detected as in entered the area of the reader.
    /// </summary>
    [DisplayName("InboundDetectionIdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the ID of the tag that was detected as in entered the area of the reader.")]
    public partial class CreateInboundDetectionIdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the ID of the tag that was detected as in entered the area of the reader.
        /// </summary>
        [Description("The value that the ID of the tag that was detected as in entered the area of the reader.")]
        public ulong Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the ID of the tag that was detected as in entered the area of the reader.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the ID of the tag that was detected as in entered the area of the reader.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => InboundDetectionId.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the ID of the tag that was detected as in exited the area of the reader.
    /// </summary>
    [DisplayName("OutboundDetectionIdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the ID of the tag that was detected as in exited the area of the reader.")]
    public partial class CreateOutboundDetectionIdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the ID of the tag that was detected as in exited the area of the reader.
        /// </summary>
        [Description("The value that the ID of the tag that was detected as in exited the area of the reader.")]
        public ulong Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the ID of the tag that was detected as in exited the area of the reader.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the ID of the tag that was detected as in exited the area of the reader.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutboundDetectionId.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that changes the state of the digital output pin.
    /// </summary>
    [DisplayName("DO0StatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that changes the state of the digital output pin.")]
    public partial class CreateDO0StatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that changes the state of the digital output pin.
        /// </summary>
        [Description("The value that changes the state of the digital output pin.")]
        public DigitalOutputState Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that changes the state of the digital output pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that changes the state of the digital output pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0State.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables or disables hardware notifications.
    /// </summary>
    [DisplayName("EnableHardwareNotificationsPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables or disables hardware notifications.")]
    public partial class CreateEnableHardwareNotificationsPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables or disables hardware notifications.
        /// </summary>
        [Description("The value that enables or disables hardware notifications.")]
        public HardwareNotifications Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables or disables hardware notifications.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables or disables hardware notifications.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableHardwareNotifications.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that triggers hardware notifications.
    /// </summary>
    [DisplayName("TriggerHardwareNotificationsPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that triggers hardware notifications.")]
    public partial class CreateTriggerHardwareNotificationsPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that triggers hardware notifications.
        /// </summary>
        [Description("The value that triggers hardware notifications.")]
        public HardwareNotifications Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that triggers hardware notifications.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that triggers hardware notifications.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => TriggerHardwareNotifications.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the buzzer will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("BuzzerNotificationDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the buzzer will stay on (sensitive to multiples of 2).")]
    public partial class CreateBuzzerNotificationDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the buzzer will stay on (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the time the buzzer will stay on (sensitive to multiples of 2).")]
        public ushort Value { get; set; } = 2;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the buzzer will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the buzzer will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => BuzzerNotificationDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the top LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TopLedNotificationDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the top LED will stay on (sensitive to multiples of 2).")]
    public partial class CreateTopLedNotificationDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the top LED will stay on (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the time the top LED will stay on (sensitive to multiples of 2).")]
        public ushort Value { get; set; } = 2;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the top LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the top LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => TopLedNotificationDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the bottom LED will stay on (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("BottomLedNotificationDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the bottom LED will stay on (sensitive to multiples of 2).")]
    public partial class CreateBottomLedNotificationDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the bottom LED will stay on (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the time the bottom LED will stay on (sensitive to multiples of 2).")]
        public ushort Value { get; set; } = 2;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the bottom LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the bottom LED will stay on (sensitive to multiples of 2).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => BottomLedNotificationDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the frequency of the buzzer notification.
    /// </summary>
    [DisplayName("BuzzerNotificationFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the frequency of the buzzer notification.")]
    public partial class CreateBuzzerNotificationFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the frequency of the buzzer notification.
        /// </summary>
        [Range(min: 200, max: 15000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the frequency of the buzzer notification.")]
        public ushort Value { get; set; } = 200;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the frequency of the buzzer notification.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the frequency of the buzzer notification.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => BuzzerNotificationFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the blink period of the top LED notification (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("TopLedNotificationPeriodPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the blink period of the top LED notification (sensitive to multiples of 2).")]
    public partial class CreateTopLedNotificationPeriodPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the blink period of the top LED notification (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the blink period of the top LED notification (sensitive to multiples of 2).")]
        public ushort Value { get; set; } = 2;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the blink period of the top LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the blink period of the top LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => TopLedNotificationPeriod.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the blink period of the bottom LED notification (sensitive to multiples of 2).
    /// </summary>
    [DisplayName("BottomLedNotificationPeriodPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the blink period of the bottom LED notification (sensitive to multiples of 2).")]
    public partial class CreateBottomLedNotificationPeriodPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the blink period of the bottom LED notification (sensitive to multiples of 2).
        /// </summary>
        [Range(min: 2, max: long.MaxValue)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the blink period of the bottom LED notification (sensitive to multiples of 2).")]
        public ushort Value { get; set; } = 2;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the blink period of the bottom LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the blink period of the bottom LED notification (sensitive to multiples of 2).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => BottomLedNotificationPeriod.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sends a notification when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends a notification when the tag with the specified ID is detected.")]
        public ulong Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sends a notification when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends a notification when the tag with the specified ID is detected.")]
        public ulong Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId2Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sends a notification when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId2Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends a notification when the tag with the specified ID is detected.")]
        public ulong Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId2.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sends a notification when the tag with the specified ID is detected.
    /// </summary>
    [DisplayName("MatchTagId3Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sends a notification when the tag with the specified ID is detected.")]
    public partial class CreateMatchTagId3Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        [Description("The value that sends a notification when the tag with the specified ID is detected.")]
        public ulong Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sends a notification when the tag with the specified ID is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId3.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId0PulseDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId0PulseDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId0PulseDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId1PulseDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId1PulseDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId1PulseDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId2PulseDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId2PulseDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId2PulseDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
    /// </summary>
    [DisplayName("MatchTagId3PulseDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
    public partial class CreateMatchTagId3PulseDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if the corresponding tag is detected.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the digital output pin will stay on (ms) if the corresponding tag is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => MatchTagId3PulseDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that the time the digital output pin will stay on (ms) if any tag is detected.
    /// </summary>
    [DisplayName("AnyTagIdPulseDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that the time the digital output pin will stay on (ms) if any tag is detected.")]
    public partial class CreateAnyTagIdPulseDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that the time the digital output pin will stay on (ms) if any tag is detected.
        /// </summary>
        [Description("The value that the time the digital output pin will stay on (ms) if any tag is detected.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that the time the digital output pin will stay on (ms) if any tag is detected.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that the time the digital output pin will stay on (ms) if any tag is detected.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AnyTagIdPulseDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that triggers the digital output pin for the specified duration (ms).
    /// </summary>
    [DisplayName("PulseDO0DurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that triggers the digital output pin for the specified duration (ms).")]
    public partial class CreatePulseDO0DurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that triggers the digital output pin for the specified duration (ms).
        /// </summary>
        [Description("The value that triggers the digital output pin for the specified duration (ms).")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that triggers the digital output pin for the specified duration (ms).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that triggers the digital output pin for the specified duration (ms).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PulseDO0Duration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// The available hardware notifications.
    /// </summary>
    [Flags]
    public enum HardwareNotifications : byte
    {
        Buzzer = 0x1,
        TopLed = 0x1,
        BottomLed = 0x1
    }

    /// <summary>
    /// The state of the digital output pin.
    /// </summary>
    public enum DigitalOutputState : byte
    {
        Low = 0,
        High = 1
    }
}
