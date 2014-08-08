using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbol.RFID3;
using LoggerEx;

namespace rrv
{
    public class TestReaderConnect
    {
        private LoggerExLog _lg = LoggerExLog.Instance;
        public void Run(List<rRdrRec> rdrRecList)
        {
            for(int i = 0; i < rdrRecList.Count ; i++)
            {
                //if (rdrRecList[i].ReaderStatus != rReaderStatus.PASS)
                //    continue;
                RFIDReaderEx reader = new RFIDReaderEx(rdrRecList[i].IP, rdrRecList[i].IP);
               // RFIDReader reader = new RFIDReader(rdrRecList[i].IP, 0, 0);
                
                //reader.TimeoutMilliseconds = 1000;
               // reader.HostName = rdrRecList[i].IP;
                try
                {
                    reader.Connect();
                }
                catch(Exception e2)
                {
                    _lg.LogMsg("TestReaderConnect: " + e2.Message, LoggerExLog.LogExLevels.Error);
                }
                // Print out Reader Capabilities
                _lg.LogMsg(reader.ReaderCapabilities.FirwareVersion.ToString(), LoggerExLog.LogExLevels.Info);
                _lg.LogMsg(reader.ReaderCapabilities.ModelName.ToString(), LoggerExLog.LogExLevels.Info);
                _lg.LogMsg(reader.ReaderCapabilities.CountryCode.ToString(), LoggerExLog.LogExLevels.Info);
                _lg.LogMsg(reader.ReaderCapabilities.IsHoppingEnabled.ToString(), LoggerExLog.LogExLevels.Info);
                reader.Disconnect();

            //    // List out Profiles on this Reader
            //    ReaderManagement rm = new ReaderManagement();
            //    LoginInfo loginInfo = new LoginInfo();
            //    loginInfo.HostName = rdrRecList[i].IP;
            //    loginInfo.UserName = "manu";
            //    loginInfo.Password = "Fxsfoo111213";
            //    loginInfo.SecureMode = SECURE_MODE.HTTP;

            //    try
            //    {
            //        rm.Login(loginInfo, READER_TYPE.FX);
            //    }
            //    catch(Exception e3)
            //    {
            //        _lg.LogMsg("ListProfiles: Login failed" + e3.Message, LoggerExLog.LogExLevels.Error);
            //    }

            //    // Print out the profiles on this reader
            //    try
            //    {
            //        int activeProfileIndex = -1;
            //        string[] profileList = rm.Profile.GetList(out activeProfileIndex);
            //        _lg.LogMsg("Profiles: ", LoggerExLog.LogExLevels.Info);
            //        for (int n = 0; n < profileList.Length; n++)
            //        {
            //            _lg.LogMsg(profileList[n], LoggerExLog.LogExLevels.Info);
            //        }
            //        rm.Profile.ImportFromReader("AdvReaderConfig.xml", "E:\\ReaderResetValidator\\rrv\\rrv\\bin\\Debug");
            //    }
            //    catch (Exception e4)
            //    {
            //        _lg.LogMsg("GetProfileList: " + e4.Message, LoggerExLog.LogExLevels.Error);
            //    }

            //    // check Local Time + TimeZone
            //    try
            //    {
            //        int activeTimeZoneIndex = -1;
            //        DateTime readerTime = rm.LocalTime;
            //        _lg.LogMsg("ReaderLocalTime: " + readerTime.ToShortDateString() + " " + readerTime.ToLongTimeString(), LoggerExLog.LogExLevels.Info);

            //        string[] timeZoneList = rm.TimeZone.GetTimeZoneList(out activeTimeZoneIndex);
            //        if (timeZoneList == null)
            //        {
            //            _lg.LogMsg("TimeZoneList is null", LoggerExLog.LogExLevels.Error);
            //        }
            //        else
            //        {
            //            _lg.LogMsg("TimeZone: " + timeZoneList[activeTimeZoneIndex], LoggerExLog.LogExLevels.Info);
            //        }
            //    }
            //    catch(Exception e5)
            //    {
            //        _lg.LogMsg("Check local Time failed: " + e5.Message, LoggerExLog.LogExLevels.Error);
            //    }

            //    try
            //    {
            //        rm.Logout();
            //    }
            //    catch (Exception e6)
            //    {
            //        _lg.LogMsg("ListProfiles: Login failed" + e6.Message, LoggerExLog.LogExLevels.Error);
            //    }
 
            //    int h = 0;
            }
        }
    }
}
