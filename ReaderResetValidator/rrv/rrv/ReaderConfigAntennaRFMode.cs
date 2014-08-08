namespace rrv
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigAntennaRFMode
    {
        private ushort antennaIDField;
        private uint rFModeTableIndexField;

        [XmlAttribute]
        public ushort AntennaID
        {
            get
            {
                return this.antennaIDField;
            }
            set
            {
                this.antennaIDField = value;
            }
        }

        [XmlAttribute]
        public uint RFModeTableIndex
        {
            get
            {
                return this.rFModeTableIndexField;
            }
            set
            {
                this.rFModeTableIndexField = value;
            }
        }
    }
}

