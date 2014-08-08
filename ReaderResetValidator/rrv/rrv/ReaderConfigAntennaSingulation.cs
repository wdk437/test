namespace rrv
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true)]
    public class ReaderConfigAntennaSingulation
    {
        private ushort antennaIDField;
        private string inventoryStateField;
        [XmlIgnore]
        public bool InventoryStateSpecified;
        private bool performStateAwareSingulationField;
        private string sessionField;
        private string sLFlagField;
        [XmlIgnore]
        public bool SLFlagSpecified;
        private ushort tagPopulationField;
        private ushort tagTransitTimeField;

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
        public string InventoryState
        {
            get
            {
                return this.inventoryStateField;
            }
            set
            {
                this.inventoryStateField = value;
                this.InventoryStateSpecified = true;
            }
        }

        [XmlAttribute]
        public bool PerformStateAwareSingulation
        {
            get
            {
                return this.performStateAwareSingulationField;
            }
            set
            {
                this.performStateAwareSingulationField = value;
            }
        }

        [XmlAttribute]
        public string Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }

        [XmlAttribute]
        public string SLFlag
        {
            get
            {
                return this.sLFlagField;
            }
            set
            {
                this.sLFlagField = value;
                this.SLFlagSpecified = true;
            }
        }

        [XmlAttribute]
        public ushort TagPopulation
        {
            get
            {
                return this.tagPopulationField;
            }
            set
            {
                this.tagPopulationField = value;
            }
        }

        [XmlAttribute]
        public ushort TagTransitTime
        {
            get
            {
                return this.tagTransitTimeField;
            }
            set
            {
                this.tagTransitTimeField = value;
            }
        }
    }
}

