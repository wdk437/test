using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LoggerEx;

namespace rrv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoggerExLog _lg = null;
        private PretRRVConfig _pretrrvconfig = null;
        public MainWindow()
        {
            InitializeComponent();
            _lg = LoggerExLog.Instance;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _pretrrvconfig = PretRRVConfig.Instance;
            MyReaderStatus.UpdateTable(_pretrrvconfig.rRdrRecList);
            _lg.LogMsg("RRV loaded", LoggerExLog.LogExLevels.Info);
            int h = 0;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _lg.LogMsg("RRV Shutdown", LoggerExLog.LogExLevels.Info);
        }

        private void btn_Start(object sender, RoutedEventArgs e)
        {
            // Test PING
            try
            {
                SetStatusInProgress(_pretrrvconfig.rRdrRecList);
                MyReaderStatus.UpdateTable(_pretrrvconfig.rRdrRecList);
                Thread.Sleep(1000);
                TestPing testping = new TestPing();
                testping.Run(_pretrrvconfig.rRdrRecList);
                MyReaderStatus.UpdateTable(_pretrrvconfig.rRdrRecList);
            }
            catch(Exception e2)
            {
                _lg.LogMsg("RRV: " + e2.Message, LoggerExLog.LogExLevels.Error);
            }
            // Test Connect
            try
            {
                TestReaderConnect testreaderconnect = new TestReaderConnect();
                testreaderconnect.Run(_pretrrvconfig.rRdrRecList);
            }
            catch (Exception e3)
            {
                _lg.LogMsg("RRV: " + e3.Message, LoggerExLog.LogExLevels.Error);
            }
            // TEST Firmware upgrade
            try
            {
                TestFWUpdate testfwupdate = new TestFWUpdate();
                testfwupdate.Run("ftp://192.168.1.93/7500_1_2_9", _pretrrvconfig.rRdrRecList[0].IP);
            }
            catch(Exception e4)
            {
                _lg.LogMsg("RRV: " + e4.Message, LoggerExLog.LogExLevels.Error);
            }
        }
        /// <summary>
        /// Alters rdrRecList
        /// </summary>
        /// <param name="rdrRecList"></param>
        private void SetStatusInProgress(List<rRdrRec > rdrRecList)
        {
            for(int i = 0; i < rdrRecList .Count; i++)
            {
                rdrRecList[i].ReaderStatus = rReaderStatus.INPROGRESS;
            }
        }

    }
}
