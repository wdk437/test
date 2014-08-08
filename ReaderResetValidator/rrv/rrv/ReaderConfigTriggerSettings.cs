namespace rrv
{
    using System;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigTriggerSettings
    {
        private ReaderConfigTriggerSettingsAutonomousMode autonomousModeField;
        [XmlIgnore]
        public bool AutonomousModeSpecified;
        private ReaderConfigTriggerSettingsStartTriggerSettings startTriggerSettingsField;
        private ReaderConfigTriggerSettingsStopTriggerSettings stopTriggerSettingsField;

    

        [XmlElement(Form=XmlSchemaForm.Unqualified)]
        public ReaderConfigTriggerSettingsStartTriggerSettings StartTriggerSettings
        {
            get
            {
                return this.startTriggerSettingsField;
            }
            set
            {
                this.startTriggerSettingsField = value;
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified)]
        public ReaderConfigTriggerSettingsStopTriggerSettings StopTriggerSettings
        {
            get
            {
                return this.stopTriggerSettingsField;
            }
            set
            {
                this.stopTriggerSettingsField = value;
            }
        }
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public ReaderConfigTriggerSettingsAutonomousMode AutonomousMode
        {
            get
            {
                return this.autonomousModeField;
            }
            set
            {
                this.autonomousModeField = value;
                this.AutonomousModeSpecified = true;
            }
        }
    }
}

