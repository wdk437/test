namespace rrv
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigPrefilterListSettings
    {
        private string filterMaskValueField;
        [XmlIgnore]
        public bool FilterMaskValueSpecified;
        private string statusField;

        [XmlAttribute]
        public string FilterMaskValue
        {
            get
            {
                return this.filterMaskValueField;
            }
            set
            {
                this.filterMaskValueField = value;
                this.FilterMaskValueSpecified = true;
            }
        }

        [XmlAttribute]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
}

