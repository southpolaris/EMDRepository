using System.Windows.Forms;

namespace WifiMonitor
{
    public partial class IPBox : TextBox
    {
        public IPBox()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassName = "SysIPAddress32";
                return cp;
            }
        }
    }
}
