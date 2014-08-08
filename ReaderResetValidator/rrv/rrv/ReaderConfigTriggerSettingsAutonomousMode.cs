namespace rrv
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigTriggerSettingsAutonomousMode
    {
        private bool enabledField;
        private ushort newTagEventModeratedTimeoutField;
        [XmlIgnore]
        public bool NewTagEventModeratedTimeoutSpecified;
        private string newTagEventReportTriggerField;
        [XmlIgnore]
        public bool NewTagEventReportTriggerSpecified;

        [XmlAttribute]
        public bool Enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }

        [XmlAttribute]
        public ushort NewTagEventModeratedTimeout
        {
            get
            {
                return this.newTagEventModeratedTimeoutField;
            }
            set
            {
                this.newTagEventModeratedTimeoutField = value;
                this.NewTagEventModeratedTimeoutSpecified = true;
            }
        }

        [XmlAttribute]
        public string NewTagEventReportTrigger
        {
            get
            {
                return this.newTagEventReportTriggerField;
            }
            set
            {
                this.newTagEventReportTriggerField = value;
                this.NewTagEventReportTriggerSpecified = true;
            }
        }
    }
}

