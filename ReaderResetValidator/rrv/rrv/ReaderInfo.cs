using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbol.RFID2;
using Symbol.RFID3;

namespace rrv
{
    public class ReaderInfo
    {
        public ReaderInfo()
        {
        }
        public void GetReaderInfo(string IPAddress)
        {
            //ReaderInfo readerinfo = new ReaderInfo();
            RFIDReaderEx reader = new RFIDReaderEx();
            reader.TimeoutMilliseconds = 1000;
            reader.HostName = IPAddress;
            reader.Connect();

        }
    }
}
