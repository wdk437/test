using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace rrv
{
    public class TestPing
    {
        
        public TestPing()
        {
        }
        /// <summary>
        /// Alters rdrRecList to add the result status
        /// </summary>
        /// <param name="rdrRecList"></param>
        /// <returns></returns>
        public bool Run(List<rRdrRec > rdrRecList)
        {
            string pingData =  "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(pingData);
            bool bfail = false;
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            int timeout = 120;
            for(int i = 0 ; i < rdrRecList .Count; i++)
            {
                Ping p = new Ping();
                PingReply reply = p.Send(rdrRecList  [i].IP, timeout, buffer, options);
                if (reply.Status != IPStatus .Success )
                {
                    bfail = true;
                    rdrRecList[i].ReaderStatus = rReaderStatus.FAIL; 
                }
                else
                {
                    rdrRecList[i].ReaderStatus = rReaderStatus.PASS;
                }
            }
            return bfail;
        }
    }
}
