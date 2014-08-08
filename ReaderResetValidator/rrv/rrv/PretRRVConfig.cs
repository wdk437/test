using System;
using System.Windows .Media;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LoggerEx;

namespace rrv
{
    public enum rReaderStatus { UNKNOWN, PASS, FAIL, INPROGRESS };
    // 
    // Singleton class to retrieve configuration information
    //
    public class PretRRVConfig
    {
        private static PretRRVConfig thisClass = null;
        private static readonly object _rrvLock = new object();

        private string _RRVCONFIGFILE = "RRVConfig.xml";

        private bool _CONSTERROR = false; // set to true if there is a constructor error
        private string _LASTERRSTRING = "";

        // exposed configuration objects
        public List<rFWRec> rFWRecList { get; private set; }
        public List<rRdrRec> rRdrRecList { get; private set; }
        public string rStaticRoot { get; private set; }

        // Logger
        private LoggerExLog _lg = null;



        public static PretRRVConfig Instance
        {
            get { return PretRRVConfig.GetPretRRVConfigObj();  }
        }

        private static PretRRVConfig GetPretRRVConfigObj()
        {
            if (PretRRVConfig .thisClass == null)
            {
                PretRRVConfig.thisClass = new PretRRVConfig();
            }
            return PretRRVConfig.thisClass;
        }
        
        // private constructor
        private PretRRVConfig()
        {
            _lg = LoggerExLog.Instance;
            rFWRecList = new List<rFWRec>();
            rRdrRecList = new List<rRdrRec>();
            if (GetConfigurationData() == false)
            {
                _lg.LogMsg("PretRRVConfig: Constructor Failure", LoggerExLog.LogExLevels.Error);
                _CONSTERROR = true;
            }
        }
        public  string GetLastError()
        {
            return _LASTERRSTRING; 
        }
        // Returns false if Configuration is not valid
        private bool GetConfigurationData()
        {
            RRVConfig rrvconfig = null;
            if (!File.Exists (_RRVCONFIGFILE ))
            {
                _lg.LogMsg("PretRRVConfig: File not found: " + _RRVCONFIGFILE, LoggerExLog.LogExLevels.Error);
                return false;
            }
            FileStream fs = new FileStream(_RRVCONFIGFILE, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(RRVConfig));
            try
            {
                rrvconfig = (RRVConfig)xs.Deserialize(fs);
            }
            catch(Exception e)
            {
                _lg.LogMsg("PretRRVConfig: " + e.Message , LoggerExLog.LogExLevels.Error);
                return false;
            }
            //PretRRVConfig pretrrvconfig = new PretRRVConfig();
            try
            {
                RRVConfigRStaticRoot staticRoot = (RRVConfigRStaticRoot)rrvconfig.Items[0];
                rStaticRoot = staticRoot.rsRoot[0].sRootLoc.ToString();
                RRVConfigRReaderList rreaderList = (RRVConfigRReaderList)rrvconfig.Items[1];
                for (int i = 0; i < rreaderList.rReader.Length; i++)
                {
                    rRdrRec rdrrec = new rRdrRec();
                    rdrrec.IP = rreaderList.rReader[i].IP;
                    rdrrec.Label = rreaderList.rReader[i].Label;
                    rRdrRecList.Add(rdrrec);
                }
                RRVConfigRFWList fwList = (RRVConfigRFWList)rrvconfig.Items[2];
                for (int i = 0; i < fwList .rFW .Length ; i++)
                {
                    rFWRec fwrec = new rFWRec();
                    fwrec.Name = fwList.rFW[i].Name;
                    fwrec.Label = fwList.rFW[i].Label;
                    fwrec.FWLoc = fwList.rFW[i].FWLoc;
                    rFWRecList.Add(fwrec);
                }
            }
            catch(Exception e2)
            {
                _lg.LogMsg("PretRRVConfig: " + e2.Message, LoggerExLog.LogExLevels.Error);
                return false;
            }
            return true;
        }
    }
    public class rFWRec
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string FWLoc { get; set;  }
    }
    public class rRdrRec
    {
        public string IP { get; set;  }
        public string Label { get;set; }
        private rReaderStatus _readerStatus;
        public rReaderStatus ReaderStatus 
        { 
            set
            {
                switch(value)
                {
                    case rReaderStatus.UNKNOWN:
                        this.StatusColor = Brushes.White;
                        break;
                    case  rReaderStatus.INPROGRESS :
                        this.StatusColor = Brushes.Yellow;
                        break;
                    case rReaderStatus.PASS :
                        this.StatusColor = Brushes.Green;
                        break;
                    case rReaderStatus.FAIL:
                        this.StatusColor = Brushes.Red;
                        break;
                }
                _readerStatus = value;
            }
            get
            {
                return _readerStatus;
            }
        }
        public Brush StatusColor { get; private set;  }
        public rRdrRec ()
        {
            ReaderStatus = rReaderStatus.UNKNOWN;
        }
    }
}
