/*
 *      Author:         Sheldon Reddy
 *      Date:           20180626
 *      
 *      Description:                          
 *                      The SSTextBox.CS file implements the functionality for the SecureTextBox.
 *                      
 *                      I have not documented the code but's its pretty obvious to follow what is going on. 
 *                      
 *                      This implementation ensures the password, when entered, is masked.
 *                      This also ensures the plaintext password is never stored in memory - rather it is immediately encrypted and stored
 *                      It is advisible to keep this encrypted password stored temporarily and thereafter use a stronger encryption algorithm such as AES 128 or 256 etc
 *                        
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;

namespace cryptoAES
{
    public partial class SSTextBox : UserControl
    {
        // Change the masking character here
        char _passwordChar = '●';
        SecureString _secureString = new SecureString();

        public SecureString SecureString
        {
            get { return _secureString; }
        }

        public char PasswordChar
        {
            get { return _passwordChar; }
            set { _passwordChar = value; }
        }
        
        public SSTextBox()
        {
            InitializeComponent();
        }

        private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                ProcessBackspace();
            else
                ProcessNewCharacter(e.KeyChar);

            e.Handled = true;
        }

        private void ProcessNewCharacter(char character)
        {
            if (InputBox.SelectionLength > 0)
            {
                RemoveSelectedCharacters();
            }

            _secureString.InsertAt(InputBox.SelectionStart, character);
            ResetDisplayCharacters(InputBox.SelectionStart + 1);
        }

        private void RemoveSelectedCharacters()
        {
            for (int i = 0; i < InputBox.SelectionLength; i++)
            {
                _secureString.RemoveAt(InputBox.SelectionStart);
            }
        }

        private void ResetDisplayCharacters(int caretPosition)
        {
            InputBox.Text = new string(_passwordChar, _secureString.Length);
            InputBox.SelectionStart = caretPosition;
        }

        private void ProcessBackspace()
        {
            if (InputBox.SelectionLength > 0)
            {
                RemoveSelectedCharacters();
                ResetDisplayCharacters(InputBox.SelectionStart);
            }
            else if (InputBox.SelectionStart > 0)
            {
                _secureString.RemoveAt(InputBox.SelectionStart - 1);
                ResetDisplayCharacters(InputBox.SelectionStart - 1);
            }
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ProcessDelete();
                e.Handled = true;
            }
            else if (IsIgnorableKey(e.KeyCode))
            {
                e.Handled = true;
            }
        }

        private bool IsIgnorableKey(Keys key)
        {
            return key == Keys.Escape || key == Keys.Enter;
        }

        private void ProcessDelete()
        {
            if (InputBox.SelectionLength > 0)
            {
                RemoveSelectedCharacters();
            }
            else if (InputBox.SelectionStart < InputBox.Text.Length)
            {
                _secureString.RemoveAt(InputBox.SelectionStart);
            }

            ResetDisplayCharacters(InputBox.SelectionStart);
        }

        private void InputBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
