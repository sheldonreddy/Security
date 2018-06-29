/*
 *      Author:         Sheldon Reddy
 *      Date:           20180626
 *      
 *      Description:    This form is dedicated to implementation of the AES256 Demo GUI
 *                      Direct calls are available using the generated Class Library (Assembly .dll file)
 *                      There is also a version of this will initiates a GUI via a call to the Class Library file
 *                      
 *                      The GUI makes use of a custom SecureString TextBox which Stores the Input Data as an Encrypted Byte [] in memory
 *                      The plaintext password is never stored.
 *                      The encrypted Byte [] will not expose the plaintext password in any way.
 *                      
 *                      The SSTextBox.CS file implements the functionality for the SecureTextBox
 *                      
 *                      For Detailed info on the AES implementations I have completed, 
 *                      Look at the GitHub README accessible from:
 *                      https://github.com/sheldonreddy/Security  
 */

#region NameSpaces
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
using System.Security.Cryptography;
#endregion


namespace cryptoAES
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        private void DemoForm_Load(object sender, EventArgs e)
        {

        }

        #region exitButton
        private void button2_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }
        #endregion

        #region EncryptButton
        private void button1_Click(object sender, EventArgs e)
        {
            // Get & Set username input
            // This was needed in a different application - left here as an example. 
            string username = textBox1.Text;
            AES.setUsername(username);

            // Intialise $ Set byte string pointer
            IntPtr bstrPointer = Marshal.SecureStringToBSTR(SecureInput.SecureString);
            AES.setBstrPointer(bstrPointer);

            // AES 256 Password Encryption
            AES.encryptPassword();

            // Close form
            Form.ActiveForm.Close();

        }
        #endregion


        private void SecureInput_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
