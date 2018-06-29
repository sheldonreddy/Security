using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SecureStringTextBox
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        private void ShowStringButton_Click(object sender, EventArgs e)
        {
            IntPtr bstrPointer = Marshal.SecureStringToBSTR(SecureInput.SecureString);
            string noLongerSecure = ExtractStringFromSecureString(bstrPointer);
            MessageBox.Show(noLongerSecure);
        }

        private static string ExtractStringFromSecureString(IntPtr bstrPointer)
        {
            string noLongerSecure = "";
            for (int i = 0; ; i += 2)
            {
                byte lo = Marshal.ReadByte(bstrPointer, i);
                byte hi = Marshal.ReadByte(bstrPointer, i + 1);
                long l = lo + hi * 256;
                if (lo != 0)
                    noLongerSecure += (char)l;
                else
                    break;
            }
            Marshal.ZeroFreeBSTR(bstrPointer);
            return noLongerSecure;
        }

        private void BlackWaspLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.blackwasp.co.uk/");
        }
    }
}
