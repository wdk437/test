namespace rrv
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigTriggerSettingsStartTriggerSettings
    {
        private bool gPIEventField;
        [XmlIgnore]
        public bool GPIEventSpecified;
        private ushort gPIPortNumberField;
        [XmlIgnore]
        public bool GPIPortNumberSpecified;
        private string startTriggerTypeField;

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
        public string StartTriggerType
        {
            get
            {
                return this.startTriggerTypeField;
            }
            set
            {
                this.startTriggerTypeField = value;
            }
        }
    }
}

