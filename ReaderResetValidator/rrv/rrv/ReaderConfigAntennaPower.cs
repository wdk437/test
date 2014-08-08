namespace rrv
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigAntennaPower
    {
        private ushort antennaIDField;
        private int antennaPowerValueField;

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
        public int AntennaPowerValue
        {
            get
            {
                return this.antennaPowerValueField;
            }
            set
            {
                this.antennaPowerValueField = value;
            }
        }
    }
}

