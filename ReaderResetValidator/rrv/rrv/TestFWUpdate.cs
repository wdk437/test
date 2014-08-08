using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbol.RFID3;
using LoggerEx;

namespace rrv
{
    public class TestFWUpdate
    {
        private LoggerExLog _lg = LoggerExLog.Instance;


        public void Run(string ftpAddress, string IPAddr)
        {
            try
            {
                RFIDReaderEx reader = new RFIDReaderEx(IPAddr, IPAddr);
                reader.Connect();
                // Print out Software version
                _lg.LogMsg("FW Version: " + reader.ReaderCapabilities.FirwareVersion.ToString(), LoggerExLog.LogExLevels.Info);
                reader.Disconnect();
                ReaderManagement rm = new ReaderManagement();
                LoginInfo loginInfo = new LoginInfo();
                loginInfo.HostName = IPAddr;
                loginInfo.UserName = "manu";
                loginInfo.Password = "Fxsfoo111213";
                loginInfo.SecureMode = SECURE_MODE.HTTP;
                rm.Login(loginInfo, READER_TYPE.FX);

                SoftwareUpdateInfo ftpServerInfo = new SoftwareUpdateInfo();
                ftpServerInfo.HostName = ftpAddress;
                ftpServerInfo.UserName = "Anonymous";
                ftpServerInfo.Password = "1234";
                rm.SoftwareUpdate.Update(ftpServerInfo);
                UpdateStatus updatestatus;
                while (true)
                {
                    updatestatus = rm.SoftwareUpdate.UpdateStatus;
                    _lg.LogMsg("Update Status: " + updatestatus.Percentage.ToString(), LoggerExLog.LogExLevels.Info);
                    if (updatestatus.Percentage == 100)
                        break;
                }
                rm.Logout();
            }
            catch (Exception e)
            {
                _lg.LogMsg("TestFWUpdate: " + e.Message, LoggerExLog.LogExLevels.Error);
                
            }
        }
    }
}
