using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WifiMonitor
{
    public partial class AuthorityForm : Form
    {
        private string passwordHash = "";
        private string newPassword = "";

        public AuthorityForm()
        {
            InitializeComponent();
            //Get hash code
            //创建一个XmlTextReader对象，读取XML数据
            XmlTextReader xmlReader = new XmlTextReader(GlobalVar.xmlPath);

            while (xmlReader.Read())
            {
                if (true == xmlReader.Name.Equals("Password"))
                {
                    passwordHash = xmlReader.ReadString().Trim();
                }
            }

            xmlReader.Close();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                if (VerifyMd5Hash(md5Hash, textBoxPassword.Text, passwordHash))
                {
                    this.Height = 442;
                    this.Width = 444;
                }
                else
                {
                    MessageBox.Show("密码不正确，请联系设备管理员!", "修改设置", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                if (VerifyMd5Hash(md5Hash, textBoxPassword.Text, passwordHash))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("密码不正确，请联系设备管理员!", "修改设置", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.Compare(textBoxNewpassword.Text, textBoxConfirm.Text) == 0)
            {
                //Save new password
                XElement authorityElement = XDocument.Load(GlobalVar.xmlPath).Root.Element("Password");
                using (MD5 md5Hash = MD5.Create())
                {
                    newPassword = GetMd5Hash(md5Hash, textBoxConfirm.Text);
                    authorityElement.Value = newPassword;
                }
                authorityElement.Save(GlobalVar.xmlPath);
            }
            else
            {
                MessageBox.Show("两次输入不一致，请重新输入");
            }
        }
    }
}
