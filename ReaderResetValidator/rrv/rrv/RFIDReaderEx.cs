namespace rrv
{
    using Symbol.RFID3;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [Serializable]
    public class RFIDReaderEx : RFIDReader, IEquatable<RFIDReaderEx>, ISerializable
    {
        private ushort[] _connectedAntennas;
        private AntennaInfo _enabledAntennaInfo;
        private bool _inventoryInProgress;
        private READER_MODEL_NAME _readerModelName;
        internal AARAntennaBeamSequence aarAntennaBeamSequence;
        internal AARDebugConfigInfo aarDebugConfigInfo;
        public List<CheckBox> antennaGeneralConfigCheckboxList;
        public List<CheckBox> antennaZoneConfigCheckboxList;
        internal static Color[] DefaultZoneDisplayColorList = new Color[] { Color.PaleVioletRed, Color.DarkCyan, Color.IndianRed, Color.MediumSeaGreen, Color.DarkTurquoise, Color.Plum, Color.Gold, Color.Salmon };
        internal static ushort[] DefaultZoneIDList = new ushort[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        public string discoverHostName;
        public string firmwareVersion;
        public string friendlyName;
        public string ipAddress;
        public string manufacturer;
        public string manufacturerUrl;
        public string modelName;
        public string modelNumber;
        public string modelUrl;
        public PreFilterSetting preFilterSetting;
        public string presentationUrl;
        internal ReaderConfig readerConfig;
        public string readerDescription;
        internal ReaderRMLoginInfo readerLoginInfo;
        private Symbol.RFID3.ReaderManagement rm;
        public string serialNumber;
        public TriggerInfo triggerInfo;
        internal BindingList<ushort> zoneAvaiableIDList;
        public Dictionary<ushort, List<ushort>> zoneConfigAntennaSequenceList;
        //public BindingList<ZoneConfigInfo> zoneConfigInfoList;
        internal HashSet<Color> zoneDisplayAvaiableColorList;

        public RFIDReaderEx()
        {
            this.manufacturer = string.Empty;
            this.manufacturerUrl = string.Empty;
            this.modelName = string.Empty;
            this.modelNumber = string.Empty;
            this.modelUrl = string.Empty;
            this.presentationUrl = string.Empty;
            this.friendlyName = string.Empty;
            this.firmwareVersion = string.Empty;
            this.serialNumber = string.Empty;
            this.discoverHostName = string.Empty;
            this.readerDescription = string.Empty;
            this.ipAddress = string.Empty;
            this.triggerInfo = new TriggerInfo();
            this.readerLoginInfo = new ReaderRMLoginInfo();
            this.readerConfig = new ReaderConfig();
            this.aarDebugConfigInfo = new AARDebugConfigInfo();
            this.zoneConfigAntennaSequenceList = new Dictionary<ushort, List<ushort>>();
            this.antennaGeneralConfigCheckboxList = new List<CheckBox>();
            this.antennaZoneConfigCheckboxList = new List<CheckBox>();
            this.zoneDisplayAvaiableColorList = new HashSet<Color>(DefaultZoneDisplayColorList);
            this.zoneAvaiableIDList = new BindingList<ushort>();
            //this.zoneConfigInfoList = new BindingList<ZoneConfigInfo>();
            this.aarAntennaBeamSequence = new AARAntennaBeamSequence();
            this._readerModelName = READER_MODEL_NAME.OTHER_RFID_DEVICE;
        }

        public RFIDReaderEx(SerializationInfo info, StreamingContext ctxt)
        {
            this.manufacturer = string.Empty;
            this.manufacturerUrl = string.Empty;
            this.modelName = string.Empty;
            this.modelNumber = string.Empty;
            this.modelUrl = string.Empty;
            this.presentationUrl = string.Empty;
            this.friendlyName = string.Empty;
            this.firmwareVersion = string.Empty;
            this.serialNumber = string.Empty;
            this.discoverHostName = string.Empty;
            this.readerDescription = string.Empty;
            this.ipAddress = string.Empty;
            this.triggerInfo = new TriggerInfo();
            this.readerLoginInfo = new ReaderRMLoginInfo();
            this.readerConfig = new ReaderConfig();
            this.aarDebugConfigInfo = new AARDebugConfigInfo();
            this.zoneConfigAntennaSequenceList = new Dictionary<ushort, List<ushort>>();
            this.antennaGeneralConfigCheckboxList = new List<CheckBox>();
            this.antennaZoneConfigCheckboxList = new List<CheckBox>();
            this.zoneDisplayAvaiableColorList = new HashSet<Color>(DefaultZoneDisplayColorList);
            this.zoneAvaiableIDList = new BindingList<ushort>();
            //this.zoneConfigInfoList = new BindingList<ZoneConfigInfo>();
            this.aarAntennaBeamSequence = new AARAntennaBeamSequence();
            this._readerModelName = READER_MODEL_NAME.OTHER_RFID_DEVICE;
            base.HostName = (string) info.GetValue("HostName", typeof(string));
            this.discoverHostName = base.HostName;
            base.Port = (uint) info.GetValue("Port", typeof(uint));
            base.TimeoutMilliseconds = (uint) info.GetValue("TimeoutMilliseconds", typeof(uint));
            this.firmwareVersion = (string) info.GetValue("firmwareVersion", typeof(string));
            this.friendlyName = (string) info.GetValue("friendlyName", typeof(string));
            this.ipAddress = (string) info.GetValue("ipAddress", typeof(string));
            this.manufacturer = (string) info.GetValue("manufacturer", typeof(string));
            this.manufacturerUrl = (string) info.GetValue("manufacturerUrl", typeof(string));
            this.modelName = (string) info.GetValue("modelName", typeof(string));
            this.modelNumber = (string) info.GetValue("modelNumber", typeof(string));
            this.modelUrl = (string) info.GetValue("modelUrl", typeof(string));
            this.presentationUrl = (string) info.GetValue("presentationUrl", typeof(string));
            this.serialNumber = (string) info.GetValue("serialNumber", typeof(string));
            this.readerDescription = (string) info.GetValue("readerDescription", typeof(string));
            try
            {
                this.readerLoginInfo = (ReaderRMLoginInfo) info.GetValue("readerLoginInfo", typeof(ReaderRMLoginInfo));
            }
            catch (Exception)
            {
                this.readerLoginInfo = new ReaderRMLoginInfo();
            }
        }

        public RFIDReaderEx(string hostName, string ipAddress) : base(hostName, 0, 0)
        {
            this.manufacturer = string.Empty;
            this.manufacturerUrl = string.Empty;
            this.modelName = string.Empty;
            this.modelNumber = string.Empty;
            this.modelUrl = string.Empty;
            this.presentationUrl = string.Empty;
            this.friendlyName = string.Empty;
            this.firmwareVersion = string.Empty;
            this.serialNumber = string.Empty;
            this.discoverHostName = string.Empty;
            this.readerDescription = string.Empty;
            this.ipAddress = string.Empty;
            this.triggerInfo = new TriggerInfo();
            this.readerLoginInfo = new ReaderRMLoginInfo();
            this.readerConfig = new ReaderConfig();
            this.aarDebugConfigInfo = new AARDebugConfigInfo();
            this.zoneConfigAntennaSequenceList = new Dictionary<ushort, List<ushort>>();
            this.antennaGeneralConfigCheckboxList = new List<CheckBox>();
            this.antennaZoneConfigCheckboxList = new List<CheckBox>();
            this.zoneDisplayAvaiableColorList = new HashSet<Color>(DefaultZoneDisplayColorList);
            this.zoneAvaiableIDList = new BindingList<ushort>();
            //this.zoneConfigInfoList = new BindingList<ZoneConfigInfo>();
            this.aarAntennaBeamSequence = new AARAntennaBeamSequence();
            this._readerModelName = READER_MODEL_NAME.OTHER_RFID_DEVICE;
            this.discoverHostName = base.HostName;
            this.readerDescription = string.Empty;
            this.ipAddress = ipAddress;
            this.firmwareVersion = string.Empty;
            this.friendlyName = string.Empty;
            this.manufacturer = string.Empty;
            this.manufacturerUrl = string.Empty;
            this.modelName = string.Empty;
            this.modelNumber = string.Empty;
            this.modelUrl = string.Empty;
            this.presentationUrl = string.Empty;
            this.serialNumber = string.Empty;
        }

        public RFIDReaderEx(string s, uint llrpPort, uint t, bool resolveHostnames, int discoveryPort, out bool result) : base(s, llrpPort, t)
        {
            IPAddress address;
            string hostName;
            Uri uri;
            this.manufacturer = string.Empty;
            this.manufacturerUrl = string.Empty;
            this.modelName = string.Empty;
            this.modelNumber = string.Empty;
            this.modelUrl = string.Empty;
            this.presentationUrl = string.Empty;
            this.friendlyName = string.Empty;
            this.firmwareVersion = string.Empty;
            this.serialNumber = string.Empty;
            this.discoverHostName = string.Empty;
            this.readerDescription = string.Empty;
            this.ipAddress = string.Empty;
            this.triggerInfo = new TriggerInfo();
            this.readerLoginInfo = new ReaderRMLoginInfo();
            this.readerConfig = new ReaderConfig();
            this.aarDebugConfigInfo = new AARDebugConfigInfo();
            this.zoneConfigAntennaSequenceList = new Dictionary<ushort, List<ushort>>();
            this.antennaGeneralConfigCheckboxList = new List<CheckBox>();
            this.antennaZoneConfigCheckboxList = new List<CheckBox>();
            this.zoneDisplayAvaiableColorList = new HashSet<Color>(DefaultZoneDisplayColorList);
            this.zoneAvaiableIDList = new BindingList<ushort>();
           // this.zoneConfigInfoList = new BindingList<ZoneConfigInfo>();
            this.aarAntennaBeamSequence = new AARAntennaBeamSequence();
            this._readerModelName = READER_MODEL_NAME.OTHER_RFID_DEVICE;
            IPHostEntry hostEntry = new IPHostEntry();
            bool flag = false;
            try
            {
                hostEntry = Dns.GetHostEntry(s);
                flag = true;
            }
            catch (SocketException)
            {
            }
            result = true;
            if (IPAddress.TryParse(s, out address))
            {
                this.ipAddress = s;
                if (resolveHostnames && flag)
                {
                    base.HostName = hostEntry.HostName;
                    this.discoverHostName = base.HostName;
                }
                else
                {
                    base.HostName = s;
                    this.discoverHostName = base.HostName;
                }
            }
            else if (resolveHostnames && flag)
            {
                this.ipAddress = hostEntry.AddressList[0].ToString();
            }
            else
            {
                this.ipAddress = string.Empty;
            }
            base.HostName = base.HostName.ToLowerInvariant();
            this.discoverHostName = base.HostName;
            if (this.ipAddress == string.Empty)
            {
                hostName = base.HostName;
            }
            else
            {
                hostName = this.ipAddress;
            }
            //string str2 = PowerSessionMain.getMacAddress(hostName, discoveryPort);
            //if (str2 == string.Empty)
            //{
            //    str2 = "000000000000";
            //}
            //string urnUuid = "urn:uuid:00002694-0000-1000-8000-" + str2;
            //string content = PowerSessionMain.createGetMessage(urnUuid);
            //string uriString = string.Concat(new object[] { "http://", hostName, ":", discoveryPort, "/", urnUuid });
            //try
            //{
            //    uri = new Uri(uriString);
            //}
            //catch (UriFormatException)
            //{
            //    result = false;
            //    return;
            //}
            //string response = PowerSessionMain.httpPostAndGet(uri, content, true);
            //if (!(response == string.Empty))
            //{
            //    if (!PowerSessionMain.parseGetResponse(response, this))
            //    {
            //        this.manufacturer = "Unknown";
            //        this.manufacturerUrl = string.Empty;
            //        this.modelName = string.Empty;
            //        this.modelNumber = string.Empty;
            //        this.modelUrl = string.Empty;
            //        this.presentationUrl = "http://" + hostName;
            //        result = false;
            //    }
            //}
            //else
            //{
            //    this.manufacturer = "Unknown";
            //    this.manufacturerUrl = string.Empty;
            //    this.modelName = string.Empty;
            //    this.modelNumber = string.Empty;
            //    this.modelUrl = string.Empty;
            //    this.presentationUrl = "http://" + hostName;
            //    result = false;
            //}
        }

        internal void addZoneConfigInfo(Zones.ZoneConfig zoneConfig)
        {
            //if (this.isAARDevice && (zoneConfig != null))
            //{
            //    Color color = this.zoneDisplayAvaiableColorList.ElementAt<Color>(0);
            //    ZoneConfigInfo item = new ZoneConfigInfo(zoneConfig, color);
            //    this.zoneConfigInfoList.Add(item);
            //    this.zoneDisplayAvaiableColorList.Remove(color);
            //    this.zoneAvaiableIDList.Remove(zoneConfig.ZoneId);
            //}
        }

        internal void createAntennaInfoGUI()
        {
            //this.antennaGeneralConfigCheckboxList.Clear();
            //this.antennaZoneConfigCheckboxList.Clear();
            //ushort[] availableAntennas = base.Config.Antennas.AvailableAntennas;
            //this.enabledAntennaInfo = new AntennaInfo(availableAntennas);
            //this.aarAntennaBeamSequence.initiateAntennaBeamSequence();
            //foreach (ushort num in availableAntennas)
            //{
            //    CheckBox item = new CheckBox {
            //        CheckAlign = ContentAlignment.MiddleRight,
            //        TextAlign = ContentAlignment.MiddleRight,
            //        AutoEllipsis = true,
            //        Location = new Point(0, 0),
            //        Name = "generalAntennaCheckBox" + num.ToString(),
            //        Size = new Size(PowerSessionMain.DEFALUT_ANTENNA_CHECKBOX_WIDTH, PowerSessionMain.DEFAULT_ANTENNA_CHECKBOX_HEIGHT),
            //        Text = "A" + num.ToString(),
            //        Checked = true,
            //        Enabled = true,
            //        UseVisualStyleBackColor = true
            //    };
            //    this.antennaGeneralConfigCheckboxList.Add(item);
            //    if (this.isAARDevice)
            //    {
            //        item = new CheckBox {
            //            CheckAlign = ContentAlignment.MiddleRight,
            //            TextAlign = ContentAlignment.MiddleRight,
            //            AutoEllipsis = true,
            //            Location = new Point(0, 0),
            //            Name = "zoneAntennaCheckBox" + num.ToString(),
            //            Size = new Size(PowerSessionMain.DEFALUT_ZONE_ANTENNA_CHECKBOX_WIDTH, PowerSessionMain.DEFAULT_ZONE_ANTENNA_CHECKBOX_HEIGHT),
            //            Text = "A" + num.ToString(),
            //            Checked = false,
            //            Enabled = true,
            //            UseVisualStyleBackColor = true
            //        };
            //        this.antennaZoneConfigCheckboxList.Add(item);
            //    }
            //}
        }

        internal void deleteZoneConfigInfo(ushort zoneId)
        {
            //if (this.isAARDevice)
            //{
            //    int index = 0;
            //    index = 0;
            //    while (index < this.zoneConfigInfoList.Count)
            //    {
            //        if (this.zoneConfigInfoList[index].zoneID == zoneId)
            //        {
            //            break;
            //        }
            //        index++;
            //    }
            //    if (index < this.zoneConfigInfoList.Count)
            //    {
            //        this.zoneDisplayAvaiableColorList.Add(this.zoneConfigInfoList[index].zoneConfigColor);
            //        int num2 = 0;
            //        while (num2 < this.zoneAvaiableIDList.Count)
            //        {
            //            if (this.zoneConfigInfoList[index].zoneID < this.zoneAvaiableIDList[num2])
            //            {
            //                break;
            //            }
            //            num2++;
            //        }
            //        this.zoneAvaiableIDList.Insert(num2, this.zoneConfigInfoList[index].zoneID);
            //        this.zoneConfigInfoList.RemoveAt(index);
            //    }
            //}
        }

        public bool Equals(RFIDReaderEx other)
        {
            return (this.ipAddress == other.ipAddress);
        }

        internal string getHostPart()
        {
            IPAddress address;
            string str = string.Empty;
            if (!string.IsNullOrEmpty(this.discoverHostName) && !IPAddress.TryParse(this.discoverHostName, out address))
            {
                str = this.discoverHostName.Split(new char[] { '.' })[0];
                if (str.StartsWith("null", true, null))
                {
                    if (!string.IsNullOrEmpty(this.friendlyName))
                    {
                        return this.friendlyName;
                    }
                    if (!string.IsNullOrEmpty(this.modelName) && (str.Length >= 6))
                    {
                        str = "FX" + this.modelName + str.Substring(str.Length - 6).ToUpper();
                    }
                }
                return str;
            }
            if (!string.IsNullOrEmpty(this.ipAddress))
            {
                if (this.ipAddress == "169.254.10.1")
                {
                    return "USB FX";
                }
                if (this.ipAddress == "169.254.2.1")
                {
                    return "USB Mobile";
                }
                return (!string.IsNullOrEmpty(this.friendlyName) ? this.friendlyName : this.ipAddress);
            }
            return "<null>";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("HostName", this.discoverHostName);
            info.AddValue("Port", base.Port);
            info.AddValue("TimeoutMilliseconds", base.TimeoutMilliseconds);
            info.AddValue("firmwareVersion", this.firmwareVersion);
            info.AddValue("friendlyName", this.friendlyName);
            info.AddValue("ipAddress", this.ipAddress);
            info.AddValue("manufacturer", this.manufacturer);
            info.AddValue("manufacturerUrl", this.manufacturerUrl);
            info.AddValue("modelName", this.modelName);
            info.AddValue("modelNumber", this.modelNumber);
            info.AddValue("modelUrl", this.modelUrl);
            info.AddValue("presentationUrl", this.presentationUrl);
            info.AddValue("serialNumber", this.serialNumber);
            info.AddValue("readerDescription", this.readerDescription);
            info.AddValue("readerLoginInfo", this.readerLoginInfo);
        }

        internal void initiateReaderConfigInfo()
        {
            this.readerConfig.ReaderFirmwareVersion = base.ReaderCapabilities.FirwareVersion;
            this.readerConfig.ReaderModel = base.ReaderCapabilities.ModelName;
            this.readerConfig.CommunicationStandard = base.ReaderCapabilities.CommunicationStandard.ToString();
            this.readerConfig.NumberOfAntenna = base.ReaderCapabilities.NumAntennaSupported;
            this.readerConfig.ReaderSerialNumber = base.ReaderCapabilities.ReaderID.ID;
        }

        internal void initiateZoneConfigInfoList()
        {
            //if (this.isAARDevice)
            //{
            //    this.zoneConfigInfoList.Clear();
            //    this.zoneDisplayAvaiableColorList = new HashSet<Color>(DefaultZoneDisplayColorList);
            //    for (ushort i = 1; i <= 8; i = (ushort) (i + 1))
            //    {
            //        this.zoneAvaiableIDList.Add(i);
            //    }
            //    ushort[] zoneIDList = base.Config.Zones.GetZoneIDList();
            //    if ((zoneIDList != null) && (zoneIDList.Length != 0))
            //    {
            //        foreach (ushort num2 in zoneIDList)
            //        {
            //            Color color = this.zoneDisplayAvaiableColorList.ElementAt<Color>(0);
            //            Zones.ZoneConfig zoneConfig = base.Config.Zones.GetZoneConfig(num2);
            //            if (zoneConfig != null)
            //            {
            //                ZoneConfigInfo item = new ZoneConfigInfo(zoneConfig, color);
            //                this.zoneConfigInfoList.Add(item);
            //                this.zoneDisplayAvaiableColorList.Remove(color);
            //                this.zoneAvaiableIDList.Remove(num2);
            //            }
            //        }
            //    }
            //}
        }

        internal bool PerformRM(string merlinXML, out string XMLResponse)
        {
            string str = "";
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://" + this.getHostPart() + "/Control");
            request.KeepAlive = false;
            request.Method = "POST";
            string s = merlinXML;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            WebResponse response = request.GetResponse();
            requestStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(requestStream);
            string str4 = reader.ReadToEnd();
            reader.Close();
            requestStream.Close();
            response.Close();
            XMLResponse = str4;
            if (str4.Contains("<g1:resultCode>"))
            {
                string str5 = "<g1:resultCode>";
                int startIndex = str4.IndexOf(str5) + str5.Length;
                int index = str4.IndexOf("</g1:resultCode>");
                str = str4.Substring(startIndex, index - startIndex);
            }
            return (str == "0");
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.readerDescription))
            {
                if (this.ipAddress == "169.254.10.1")
                {
                    return (this.getHostPart() + "  |  169.254.10.1");
                }
                if (this.ipAddress == "169.254.2.1")
                {
                    return (this.getHostPart() + "  |  169.254.2.1");
                }
                return ((string.IsNullOrEmpty(this.ipAddress) ? base.HostName : this.ipAddress) + "  |  " + this.getHostPart() + ((base.Port != 0) ? (" : " + base.Port.ToString()) : string.Empty));
            }
            if (this.ipAddress == "169.254.10.1")
            {
                return (this.getHostPart() + "  |  " + this.readerDescription);
            }
            if (this.ipAddress == "169.254.2.1")
            {
                return (this.getHostPart() + " |  " + this.readerDescription);
            }
            return ((string.IsNullOrEmpty(this.ipAddress) ? base.HostName : this.ipAddress) + "  |  " + this.readerDescription);
        }

        internal void updateConnectedAntennaInfo()
        {
            try
            {
                ArrayList list = new ArrayList();
                foreach (ushort num in base.Config.Antennas.AvailableAntennas)
                {
                    if (base.Config.Antennas[num].GetPhysicalProperties().IsConnected)
                    {
                        list.Add(num);
                        this.antennaGeneralConfigCheckboxList[num - 1].ForeColor = Color.Blue;
                        if (this.isAARDevice)
                        {
                            this.antennaZoneConfigCheckboxList[num - 1].ForeColor = Color.Blue;
                        }
                    }
                    else
                    {
                        this.antennaGeneralConfigCheckboxList[num - 1].ForeColor = Color.Black;
                        if (this.isAARDevice)
                        {
                            this.antennaZoneConfigCheckboxList[num - 1].ForeColor = Color.Black;
                        }
                    }
                }
                this._connectedAntennas = (ushort[]) list.ToArray(typeof(ushort));
            }
            catch (Exception)
            {
            }
        }

        internal void updateReaderModel()
        {
            if (!base.IsConnected)
            {
                this._readerModelName = READER_MODEL_NAME.OTHER_RFID_DEVICE;
            }
            else
            {
                string modelName = base.ReaderCapabilities.ModelName;
                if (modelName.StartsWith("7400", StringComparison.OrdinalIgnoreCase) && !modelName.Equals("7400256"))
                {
                    this._readerModelName = READER_MODEL_NAME.FX7400;
                }
                else if (modelName.StartsWith("7500", StringComparison.OrdinalIgnoreCase))
                {
                    this._readerModelName = READER_MODEL_NAME.FX7500;
                }
                else if (modelName.StartsWith("9500", StringComparison.OrdinalIgnoreCase))
                {
                    this._readerModelName = READER_MODEL_NAME.FX9500;
                }
                else if (modelName.Equals("7400256") || (base.Config.Antennas.AvailableAntennas.Length > 8))
                {
                    this._readerModelName = READER_MODEL_NAME.AAR;
                }
                else
                {
                    this._readerModelName = READER_MODEL_NAME.OTHER_RFID_DEVICE;
                }
            }
        }

        internal ushort[] connectedAntennas
        {
            get
            {
                return this._connectedAntennas;
            }
        }

        internal AntennaInfo enabledAntennaInfo
        {
            get
            {
                return this._enabledAntennaInfo;
            }
            set
            {
                this._enabledAntennaInfo = value;
            }
        }

        internal bool inventoryInProgress
        {
            get
            {
                return this._inventoryInProgress;
            }
            set
            {
                this._inventoryInProgress = value;
            }
        }

        internal bool isAARDevice
        {
            get
            {
                if (!base.IsConnected)
                {
                    return false;
                }
                return (this.readerModelName == READER_MODEL_NAME.AAR);
            }
        }

        internal Symbol.RFID3.ReaderManagement ReaderManagement
        {
            get
            {
                if (this.rm == null)
                {
                    this.rm = new Symbol.RFID3.ReaderManagement();
                }
                return this.rm;
            }
            set
            {
                this.rm = value;
            }
        }

        internal READER_MODEL_NAME readerModelName
        {
            get
            {
                if (!base.IsConnected)
                {
                    this._readerModelName = READER_MODEL_NAME.OTHER_RFID_DEVICE;
                }
                return this._readerModelName;
            }
        }

        public class AARDebugConfigInfo : INotifyPropertyChanged
        {
            private bool _debugEnabled;
            private string _debugPacketTypesStr = string.Empty;
            private bool _displayDebugMsgInLogList;
            private int _numOfDebugPacketTypes;
            private int _numOfReportedDebugMessages;
            internal List<string> debugMessageList = new List<string>();

            public event PropertyChangedEventHandler PropertyChanged;

            public void addDebugMessage(string message)
            {
                this.debugMessageList.Add(message);
                this.numOfReportedDebugMessages = this.debugMessageList.Count;
            }

            public void clearDebugMessage()
            {
                this.debugMessageList.Clear();
                this.numOfReportedDebugMessages = 0;
            }

            private void NotifyPropertyChanged(string info)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(info));
                }
            }

            public bool debugEnabled
            {
                get
                {
                    return this._debugEnabled;
                }
                set
                {
                    this._debugEnabled = value;
                }
            }

            public string debugPacketTypesStr
            {
                get
                {
                    return this._debugPacketTypesStr;
                }
                set
                {
                    this._debugPacketTypesStr = value;
                }
            }

            public bool displayDebugMsgInLogList
            {
                get
                {
                    return this._displayDebugMsgInLogList;
                }
                set
                {
                    this._displayDebugMsgInLogList = value;
                }
            }

            public int numOfDebugPacketTypes
            {
                get
                {
                    return this._numOfDebugPacketTypes;
                }
                set
                {
                    this._numOfDebugPacketTypes = value;
                }
            }

            public int numOfReportedDebugMessages
            {
                get
                {
                    return this._numOfReportedDebugMessages;
                }
                set
                {
                    this._numOfReportedDebugMessages = value;
                    this.NotifyPropertyChanged("numOfReportedDebugMessages");
                }
            }
        }

        [Serializable]
        public class ReaderRMLoginInfo
        {
            private bool _isEnabled;
            private string _readerPassword = string.Empty;
            private string _readerUserName = string.Empty;

            public bool isEnabled
            {
                get
                {
                    return this._isEnabled;
                }
                set
                {
                    this._isEnabled = value;
                }
            }

            public string readerPassword
            {
                get
                {
                    return this._readerPassword;
                }
                set
                {
                    this._readerPassword = value;
                }
            }

            public string readerUserName
            {
                get
                {
                    return this._readerUserName;
                }
                set
                {
                    this._readerUserName = value;
                }
            }
        }

        //public class ZoneConfigInfo : IEquatable<RFIDReaderEx.ZoneConfigInfo>
        //{
        //    private ushort[] _zoneAntennaList;
        //    private Color _zoneConfigColor = Color.Transparent;
        //    private bool _zoneConfigEnabled;
        //    private ushort _zoneID;
        //    private string _zoneName = string.Empty;
        //    public CheckBox zoneConfigCheckBox;
        //    public Label zoneConfigLabel;
        //    public string zoneEpcFilterPattern = string.Empty;
        //    //public ZoneFilterSetting zoneEpcFilterSetting;

        //    //public ZoneConfigInfo(Zones.ZoneConfig zoneConfig, Color color)
        //    //{
        //    //    this._zoneID = zoneConfig.ZoneId;
        //    //    this._zoneName = zoneConfig.ZoneName;
        //    //    this._zoneConfigColor = color;
        //    //    this._zoneAntennaList = new ushort[zoneConfig.AntennaConfig.Length];
        //    //    for (int i = 0; i < zoneConfig.AntennaConfig.Length; i++)
        //    //    {
        //    //        this._zoneAntennaList[i] = zoneConfig.AntennaConfig[i].AntennaId;
        //    //    }
        //    //    ZoneConfigCheckBoxTag tag = new ZoneConfigCheckBoxTag(this._zoneID, this._zoneName, this._zoneAntennaList, this._zoneConfigColor);
        //    //    this.zoneConfigCheckBox = new CheckBox();
        //    //    this.zoneConfigCheckBox.Appearance = Appearance.Button;
        //    //    this.zoneConfigCheckBox.BackColor = Color.Transparent;
        //    //    this.zoneConfigCheckBox.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        //    //    this.zoneConfigCheckBox.Location = new Point(0, 0);
        //    //    this.zoneConfigCheckBox.Name = "zone" + this.zoneID.ToString() + "CheckBox";
        //    //    this.zoneConfigCheckBox.Size = new Size(PowerSessionMain.DEFALUT_ZONE_CHECKBOX_WIDTH, PowerSessionMain.DEFAULT_ZONE_CHECKBOX_HEIGHT);
        //    //    this.zoneConfigCheckBox.TabIndex = 0;
        //    //    this.zoneConfigCheckBox.Text = "0\r\n0";
        //    //    this.zoneConfigCheckBox.Enabled = this._zoneConfigEnabled;
        //    //    this.zoneConfigCheckBox.TextAlign = ContentAlignment.MiddleCenter;
        //    //    this.zoneConfigCheckBox.UseVisualStyleBackColor = false;
        //    //    this.zoneConfigCheckBox.Tag = tag;
        //    //    this.zoneConfigLabel = new Label();
        //    //    this.zoneConfigLabel.Location = new Point(0, 0);
        //    //    this.zoneConfigLabel.Name = "zone" + this.zoneID.ToString() + "Label";
        //    //    this.zoneConfigLabel.Size = new Size(PowerSessionMain.DEFALUT_ZONE_LABEL_WIDTH, PowerSessionMain.DEFAULT_ZONE_LABEL_HEIGHT);
        //    //    this.zoneConfigLabel.Text = "Zone " + this.zoneID.ToString();
        //    //}

        //    //internal bool displayTag(string tagId)
        //    //{
        //    //    try
        //    //    {
        //    //        if (this.zoneEpcFilterSetting == ZoneFilterSetting.NotSet)
        //    //        {
        //    //            return true;
        //    //        }
        //    //        if (!string.IsNullOrEmpty(this.zoneEpcFilterPattern))
        //    //        {
        //    //            Regex regex = new Regex(this.zoneEpcFilterPattern, RegexOptions.IgnoreCase);
        //    //            if (regex.IsMatch(tagId))
        //    //            {
        //    //                return (this.zoneEpcFilterSetting == ZoneFilterSetting.Include);
        //    //            }
        //    //        }
        //    //        return (this.zoneEpcFilterSetting == ZoneFilterSetting.Exclude);
        //    //    }
        //    //    catch (Exception)
        //    //    {
        //    //        return false;
        //    //    }
        //    //}

        //    //public bool Equals(RFIDReaderEx.ZoneConfigInfo other)
        //    //{
        //    //    return (this.zoneID == other.zoneID);
        //    //}

        //    //internal void setZoneConfigEPCFilter(Zones.ZoneConfig zoneConfig)
        //    //{
        //    //    if (((zoneConfig.ZoneAntennaConfig.SingulationControl != null) && (zoneConfig.ZoneAntennaConfig.PrefilterList != null)) && (zoneConfig.ZoneAntennaConfig.PrefilterList.Length != 0))
        //    //    {
        //    //        Antennas.SingulationControl.SingulationAction action = zoneConfig.ZoneAntennaConfig.SingulationControl.Action;
        //    //        PreFilters.PreFilter filter = zoneConfig.ZoneAntennaConfig.PrefilterList[0];
        //    //        switch (filter.FilterAction)
        //    //        {
        //    //            case FILTER_ACTION.FILTER_ACTION_STATE_AWARE:
        //    //                switch (action.InventoryState)
        //    //                {
        //    //                    case INVENTORY_STATE.INVENTORY_STATE_A:
        //    //                        switch (filter.StateAwareAction.Action)
        //    //                        {
        //    //                            case STATE_AWARE_ACTION.STATE_AWARE_ACTION_INV_A_NOT_INV_B:
        //    //                                this.zoneEpcFilterSetting = ZoneFilterSetting.Include;
        //    //                                break;

        //    //                            case STATE_AWARE_ACTION.STATE_AWARE_ACTION_INV_B_NOT_INV_A:
        //    //                                this.zoneEpcFilterSetting = ZoneFilterSetting.Exclude;
        //    //                                break;
        //    //                        }
        //    //                        break;

        //    //                    case INVENTORY_STATE.INVENTORY_STATE_B:
        //    //                    {
        //    //                        STATE_AWARE_ACTION state_aware_action2 = filter.StateAwareAction.Action;
        //    //                        if (state_aware_action2 == STATE_AWARE_ACTION.STATE_AWARE_ACTION_INV_A_NOT_INV_B)
        //    //                        {
        //    //                            this.zoneEpcFilterSetting = ZoneFilterSetting.Exclude;
        //    //                        }
        //    //                        else if (state_aware_action2 == STATE_AWARE_ACTION.STATE_AWARE_ACTION_INV_B_NOT_INV_A)
        //    //                        {
        //    //                            this.zoneEpcFilterSetting = ZoneFilterSetting.Include;
        //    //                        }
        //    //                        break;
        //    //                    }
        //    //                }
        //    //                break;

        //    //            case FILTER_ACTION.FILTER_ACTION_STATE_UNAWARE:
        //    //                switch (filter.StateUnawareAction.Action)
        //    //                {
        //    //                    case STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_SELECT_NOT_UNSELECT:
        //    //                        this.zoneEpcFilterSetting = ZoneFilterSetting.Include;
        //    //                        break;

        //    //                    case STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_UNSELECT_NOT_SELECT:
        //    //                        this.zoneEpcFilterSetting = ZoneFilterSetting.Exclude;
        //    //                        break;
        //    //                }
        //    //                break;
        //    //        }
        //    //        this.zoneEpcFilterPattern = string.Empty;
        //    //        for (int i = 0; i < zoneConfig.ZoneAntennaConfig.PrefilterList.Length; i++)
        //    //        {
        //    //            if (zoneConfig.ZoneAntennaConfig.PrefilterList[i].BitOffset == 0x20)
        //    //            {
        //    //                string str = Utilities.ByteArrayToHexString(zoneConfig.ZoneAntennaConfig.PrefilterList[i].TagPattern);
        //    //                this.zoneEpcFilterPattern = this.zoneEpcFilterPattern + @"\b" + str;
        //    //            }
        //    //            else
        //    //            {
        //    //                string str2 = Utilities.ByteArrayToHexString(zoneConfig.ZoneAntennaConfig.PrefilterList[i].TagPattern);
        //    //                this.zoneEpcFilterPattern = this.zoneEpcFilterPattern + str2 + @"\b";
        //    //            }
        //    //            this.zoneEpcFilterPattern = this.zoneEpcFilterPattern + "|";
        //    //        }
        //    //        if (!string.IsNullOrEmpty(this.zoneEpcFilterPattern))
        //    //        {
        //    //            this.zoneEpcFilterPattern = this.zoneEpcFilterPattern.Substring(0, this.zoneEpcFilterPattern.Length - 1);
        //    //        }
        //    //    }
        //    //}

        //    //internal bool setZoneConfigEPCFilter(CheckState checkBoxState, string epcPattern, out int rejectedFilters)
        //    //{
        //    //    rejectedFilters = 0;
        //    //    uint num = 0;
        //    //    string str = string.Empty;
        //    //    if (string.IsNullOrEmpty(epcPattern) && (checkBoxState != CheckState.Indeterminate))
        //    //    {
        //    //        this.zoneEpcFilterSetting = ZoneFilterSetting.NotSet;
        //    //        this.zoneEpcFilterPattern = string.Empty;
        //    //        return false;
        //    //    }
        //    //    foreach (string str2 in epcPattern.Split(new char[] { ',', ';' }))
        //    //    {
        //    //        string str5;
        //    //        string str3 = str2.Trim();
        //    //        if (str3.StartsWith("*"))
        //    //        {
        //    //            if (Utilities.InHexFormat(str3.Substring(1, str3.Length - 1)))
        //    //            {
        //    //                str = str + str3.Substring(1, str3.Length - 1) + @"\b";
        //    //                num++;
        //    //                goto Label_0102;
        //    //            }
        //    //            rejectedFilters++;
        //    //            goto Label_010E;
        //    //        }
        //    //        if (str3.EndsWith("*"))
        //    //        {
        //    //            str5 = str3.Substring(0, str3.Length - 1);
        //    //        }
        //    //        else
        //    //        {
        //    //            str5 = str3;
        //    //        }
        //    //        if (Utilities.InHexFormat(str5))
        //    //        {
        //    //            str = str + @"\b" + str5;
        //    //            num++;
        //    //        }
        //    //        else
        //    //        {
        //    //            rejectedFilters++;
        //    //            goto Label_010E;
        //    //        }
        //    //    Label_0102:
        //    //        str = str + "|";
        //    //    Label_010E:;
        //    //    }
        //    //    if (!string.IsNullOrEmpty(str))
        //    //    {
        //    //        str = str.Substring(0, str.Length - 1);
        //    //    }
        //    //    this.zoneEpcFilterPattern = str;
        //    //    switch (checkBoxState)
        //    //    {
        //    //        case CheckState.Unchecked:
        //    //            this.zoneEpcFilterSetting = ZoneFilterSetting.Exclude;
        //    //            break;

        //    //        case CheckState.Checked:
        //    //            this.zoneEpcFilterSetting = ZoneFilterSetting.Include;
        //    //            break;

        //    //        case CheckState.Indeterminate:
        //    //            this.zoneEpcFilterSetting = ZoneFilterSetting.NotSet;
        //    //            break;
        //    //    }
        //    //    if (this.zoneEpcFilterSetting != ZoneFilterSetting.NotSet)
        //    //    {
        //    //        if (num == 0)
        //    //        {
        //    //            this.zoneEpcFilterSetting = ZoneFilterSetting.NotSet;
        //    //            this.zoneEpcFilterPattern = string.Empty;
        //    //            return false;
        //    //        }
        //    //        return true;
        //    //    }
        //    //    if (num == 0)
        //    //    {
        //    //        this.zoneEpcFilterPattern = string.Empty;
        //    //    }
        //    //    return true;
        //    //}

        //    //public override string ToString()
        //    //{
        //    //    return ("Zone " + this.zoneID.ToString() + "  |  " + this.zoneName);
        //    //}

        //    internal ushort[] zoneAntennaList
        //    {
        //        get
        //        {
        //            return this._zoneAntennaList;
        //        }
        //        private set
        //        {
        //            this._zoneAntennaList = value;
        //            ((ZoneConfigCheckBoxTag) this.zoneConfigCheckBox.Tag).zoneAntennaList = this._zoneAntennaList;
        //        }
        //    }

        //    internal Color zoneConfigColor
        //    {
        //        get
        //        {
        //            return this._zoneConfigColor;
        //        }
        //    }

        //    internal bool zoneConfigEnabled
        //    {
        //        get
        //        {
        //            return this._zoneConfigEnabled;
        //        }
        //        set
        //        {
        //            this._zoneConfigEnabled = value;
        //            this.zoneConfigCheckBox.Enabled = this._zoneConfigEnabled;
        //            this.zoneConfigCheckBox.BackColor = this._zoneConfigEnabled ? this.zoneConfigColor : Color.Transparent;
        //        }
        //    }

        //    internal ushort zoneID
        //    {
        //        get
        //        {
        //            return this._zoneID;
        //        }
        //    }

        //    internal string zoneName
        //    {
        //        get
        //        {
        //            return this._zoneName;
        //        }
        //        set
        //        {
        //            this._zoneName = value;
        //            ((ZoneConfigCheckBoxTag) this.zoneConfigCheckBox.Tag).zoneName = this._zoneName;
        //        }
        //    }

        //    public class ZoneConfigCheckBoxTag
        //    {
        //        private ushort[] _zoneAntennaList;
        //        private Color _zoneConfigColor = Color.Transparent;
        //        private ushort _zoneID;
        //        private string _zoneName = string.Empty;

        //        public ZoneConfigCheckBoxTag(ushort zoneId, string zoneName, ushort[] zoneAntennaList, Color zoneColor)
        //        {
        //            this._zoneID = zoneId;
        //            this._zoneName = zoneName;
        //            this._zoneAntennaList = zoneAntennaList;
        //            this._zoneConfigColor = zoneColor;
        //        }

        //        public ushort[] zoneAntennaList
        //        {
        //            get
        //            {
        //                return this._zoneAntennaList;
        //            }
        //            set
        //            {
        //                this._zoneAntennaList = value;
        //            }
        //        }

        //        public Color zoneConfigColor
        //        {
        //            get
        //            {
        //                return this._zoneConfigColor;
        //            }
        //        }

        //        public ushort zoneID
        //        {
        //            get
        //            {
        //                return this._zoneID;
        //            }
        //        }

        //        public string zoneName
        //        {
        //            get
        //            {
        //                return this._zoneName;
        //            }
        //            set
        //            {
        //                this._zoneName = value;
        //            }
        //        }
        //    }
        //}
    }
}

