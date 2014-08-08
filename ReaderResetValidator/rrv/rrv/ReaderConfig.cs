namespace rrv
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(Namespace="", IsNullable=false), XmlType(AnonymousType=true)]
    public class ReaderConfig
    {
        private ReaderConfigAntennaPower[] antennaListPowerSettingsField;
        [XmlIgnore]
        public bool AntennaListPowerSettingsSpecified;
        private ReaderConfigAntennaRFMode[] antennaListRFModeSettingsField;
        [XmlIgnore]
        public bool AntennaListRFModeSettingsSpecified;
        private ReaderConfigAntennaSingulation[] antennaListSingulationSettingsField;
        [XmlIgnore]
        public bool AntennaListSingulationSettingsSpecified;
        private string enabledAntennaListField;
        [XmlIgnore]
        public bool EnabledAntennaListSpecified;
        private uint maxTagIDLengthField;
        [XmlIgnore]
        public bool MaxTagIDLengthSpecified;
        private ReaderConfigPrefilterListSettings prefilterListSettingsField;
        [XmlIgnore]
        public bool PrefilterListSettingsSpecified;
        private string readerModelNameField;
        private ReaderConfigTriggerSettings triggerSettingsField;
        [XmlIgnore]
        public bool TriggerSettingsSpecified;

        [XmlArray(Form=XmlSchemaForm.Unqualified), XmlArrayItem("AntennaPower", Form=XmlSchemaForm.Unqualified, IsNullable=false)]
        public ReaderConfigAntennaPower[] AntennaListPowerSettings
        {
            get
            {
                return this.antennaListPowerSettingsField;
            }
            set
            {
                this.antennaListPowerSettingsField = value;
                this.AntennaListPowerSettingsSpecified = true;
            }
        }

        [XmlArray(Form=XmlSchemaForm.Unqualified), XmlArrayItem("AntennaRFMode", Form=XmlSchemaForm.Unqualified, IsNullable=false)]
        public ReaderConfigAntennaRFMode[] AntennaListRFModeSettings
        {
            get
            {
                return this.antennaListRFModeSettingsField;
            }
            set
            {
                this.antennaListRFModeSettingsField = value;
                this.AntennaListRFModeSettingsSpecified = true;
            }
        }

        [XmlArray(Form=XmlSchemaForm.Unqualified), XmlArrayItem("AntennaSingulation", Form=XmlSchemaForm.Unqualified, IsNullable=false)]
        public ReaderConfigAntennaSingulation[] AntennaListSingulationSettings
        {
            get
            {
                return this.antennaListSingulationSettingsField;
            }
            set
            {
                this.antennaListSingulationSettingsField = value;
                this.AntennaListSingulationSettingsSpecified = true;
            }
        }

        internal string CommunicationStandard { get; set; }

        [XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string EnabledAntennaList
        {
            get
            {
                return this.enabledAntennaListField;
            }
            set
            {
                this.enabledAntennaListField = value;
                this.EnabledAntennaListSpecified = true;
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified)]
        public uint MaxTagIDLength
        {
            get
            {
                return this.maxTagIDLengthField;
            }
            set
            {
                this.maxTagIDLengthField = value;
                this.MaxTagIDLengthSpecified = true;
            }
        }

        internal int NumberOfAntenna { get; set; }

        [XmlElement(Form=XmlSchemaForm.Unqualified)]
        public ReaderConfigPrefilterListSettings PrefilterListSettings
        {
            get
            {
                return this.prefilterListSettingsField;
            }
            set
            {
                this.prefilterListSettingsField = value;
                this.PrefilterListSettingsSpecified = true;
            }
        }

        internal string ReaderFirmwareVersion { get; set; }

        internal string ReaderModel { get; set; }

        [XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string ReaderModelName
        {
            get
            {
                return this.readerModelNameField;
            }
            set
            {
                this.readerModelNameField = value;
            }
        }

        internal string ReaderSerialNumber { get; set; }

        [XmlElement(Form=XmlSchemaForm.Unqualified)]
        public ReaderConfigTriggerSettings TriggerSettings
        {
            get
            {
                return this.triggerSettingsField;
            }
            set
            {
                this.triggerSettingsField = value;
                this.TriggerSettingsSpecified = true;
            }
        }
    }
}

