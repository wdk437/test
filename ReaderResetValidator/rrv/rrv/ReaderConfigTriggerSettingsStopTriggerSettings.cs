namespace rrv
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigTriggerSettingsStopTriggerSettings
    {
        private bool gPIEventField;
        [XmlIgnore]
        public bool GPIEventSpecified;
        private ushort gPIPortNumberField;
        [XmlIgnore]
        public bool GPIPortNumberSpecified;
        private string stopTriggerTypeField;
        private uint stopTriggerValueField;
        [XmlIgnore]
        public bool StopTriggerValueSpecified;

        [XmlAttribute]
        public bool GPIEvent
        {
            get
            {
                return this.gPIEventField;
            }
            set
            {
                this.gPIEventField = value;
                this.GPIEventSpecified = true;
            }
        }

        [XmlAttribute]
        public ushort GPIPortNumber
        {
            get
            {
                return this.gPIPortNumberField;
            }
            set
            {
                this.gPIPortNumberField = value;
                this.GPIPortNumberSpecified = true;
            }
        }

        [XmlAttribute]
        public string StopTriggerType
        {
            get
            {
                return this.stopTriggerTypeField;
            }
            set
            {
                this.stopTriggerTypeField = value;
            }
        }

        [XmlAttribute]
        public uint StopTriggerValue
        {
            get
            {
                return this.stopTriggerValueField;
            }
            set
            {
                this.stopTriggerValueField = value;
                this.StopTriggerValueSpecified = true;
            }
        }
    }
}

