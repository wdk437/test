using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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
using System.ComponentModel;
using System.Data;
using LoggerEx;

namespace rrv
{
    /// <summary>
    /// Interaction logic for ReaderStatus.xaml
    /// </summary>
    public partial class ReaderStatus : UserControl
    {
        private DataTable _dtRdrDesc = new DataTable();
        private object _dtRdrDescLock = new object();

        private LoggerExLog _lg = LoggerExLog.Instance;
        public ReaderStatus()
        {
            InitializeComponent();
            lock(_dtRdrDescLock )
            {
                DataColumn dt0Col = _dtRdrDesc.Columns.Add("Label", typeof(string));
                DataColumn dt1Col = _dtRdrDesc.Columns.Add("IP", typeof(string));
                DataColumn dt2Col = _dtRdrDesc.Columns.Add("StatusColor", typeof(Brush));
            }
        }


        public void UpdateTable(List<rRdrRec > rdrRecList)
        {
            if (Dispatcher .CheckAccess())
            {
                for(int i = 0; i < rdrRecList .Count; i++)
                {
                    string selectstring = "IP = " + "'" + rdrRecList[i].IP + "'";
                    DataRow[] drowlist = _dtRdrDesc.Select(selectstring);
                    if (drowlist .Length == 0) // new add
                    {
                        _dtRdrDesc.Rows.Add(new object[] {
                            rdrRecList [i].Label , 
                            rdrRecList [i].IP,
                            rdrRecList [i].StatusColor
                        });
                        //_lg.LogMsg("Setting status : " + i + rdrRecList[i].StatusColor.ToString(), LoggerExLog.LogExLevels.Info);
                    }
                    else
                    {
                        drowlist[0]["StatusColor"] = rdrRecList[i].StatusColor;
                       // _lg.LogMsg("Modifying status : " + i + rdrRecList[i].StatusColor.ToString(), LoggerExLog.LogExLevels.Info);
                    }
                }
                _dtRdrDesc.AcceptChanges();
                ReaderListView.DataContext = _dtRdrDesc.DefaultView;
            }
            else
            {
                Dispatcher.BeginInvoke(new Action<List<rRdrRec>>(UpdateTable), rdrRecList);
            }
        }
    }
}
