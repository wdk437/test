namespace rrv
{
    using System;
    using System.Runtime.CompilerServices;

    internal class AARAntennaBeamSequence
    {
        internal AARAntennaBeamSequence()
        {
            this.initiateAntennaBeamSequence();
        }

        internal void initiateAntennaBeamSequence()
        {
            this.antennaBeamSequenceStr = string.Empty;
            this.antennaBeamSequenceSet = false;
            this.numOfBeamsInSequence = 0;
        }

        internal bool antennaBeamSequenceSet { get; set; }

        internal string antennaBeamSequenceStr { get; set; }

        internal ushort numOfBeamsInSequence { get; set; }
    }
}

