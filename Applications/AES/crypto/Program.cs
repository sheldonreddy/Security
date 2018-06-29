﻿/*
 *      Author:             Sheldon Reddy
 *      Date:               20180626
 *      
 *      Description:       Class implementation of AES 256 Cryptography functionality
 *      
 *      
 *      Instructions:      1. If you wish to use a persistent KEY and IV then simply change line 21 to #DEFINE USEPERSISTENTKEY
 *                              The default uses a session-generated KEY and IV.
 *                         
 *                         2. If you are using the Class Library (.dll) and you make modifications to this project, ensure you rebuild the Project with the output set to Class Library.
 *                              To do this:
 *                                      1. Right-Click on aes (in solution explorer) -> Properties -> OutputType -> Class Library
 *                                      2. Right-Click on aes (in solution explorer) -> Build
 *                                      3. Open ...AES\crypto\obj\Debug
 *                                      4. aes.dll is the updated Class Library for your use
 *                                      5. You can then change the OutputType in (1) back to Windows Form to continue using this project as before.
 */

// Undefined by default - uses a session-generated KEY and IV valid only for the session which it was created in (DUE TO BEING GENERATED BY RANDOM FUNCTIONALITY)
#undef USEPERSISTENTKEY

#region NameSpaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using System.Security.Cryptography;
#endregion


namespace cryptoAES
{
    public static class AES
    {

        #region ClassMembers
        public static string username;
        public static IntPtr bstrPointer_p;
        public static byte[] encryptedPassword_p;
        private static AesManaged myAes_p = new AesManaged();
        private static string plaintext;

        #if USEPERSISTENTKEY
                private static byte[] key_p = { 106, 144, 247, 227, 136, 132, 226, 137, 93, 19, 120, 141, 62, 144, 211, 51, 240, 97, 76, 117, 166, 200, 185, 164, 28, 93, 84, 119, 63, 225, 29, 121 };
                private static byte[] iv_p = { 118, 12, 149, 60, 29, 182, 110, 98, 207, 47, 209, 248, 246, 124, 49, 186};
        #endif
    
        #endregion

        #region Main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new DemoForm());
        }
        #endregion

        #region Username_Get&Set
        public static void setUsername(string username_p)
        {
            username = username_p;
        }

        public static string getUsername()
        {
            return username;
        }
        #endregion

        #region EncryptedPassword Get
        public static byte [] getEncryptedPassword()
        {
            return encryptedPassword_p;
        }

        #endregion

        #region EncryptPassword
        public static void encryptPassword()
        {
            myAes_p.KeySize = 256;
            myAes_p.BlockSize = 128;

            #if USEPERSISTENTKEY
                //Using Persistent Key
                encryptedPassword_p = EncryptStringToBytes_Aes(ExtractStringFromSecureString(bstrPointer_p), key_p, iv_p);
            #else
                // Using Session - Generated Key
                encryptedPassword_p = EncryptStringToBytes_Aes(ExtractStringFromSecureString(bstrPointer_p), myAes_p.Key, myAes_p.IV);
            #endif

        }
        #endregion

        #region DecryptPassword
        public static string getDecryptedPassword_()
        {
            // This functionality was removed
            return "";
        }
        #endregion

        #region DecryptPassword
        public static string getDecryptedPassword(byte[] encryptedPassword)
        {
            #if USEPERSISTENTKEY 
                //Using persistent key
                return DecryptStringFromBytes_Aes(encryptedPassword, key_p, iv_p);
            #else
                // Using Session - Generated Key
                return DecryptStringFromBytes_Aes(encryptedPassword, myAes_p.Key, myAes_p.IV);
            #endif      
        }
        #endregion

#region SecureString Operations

        #region setBstrPointer
        public static void setBstrPointer(IntPtr bstrPointer)
        {
            bstrPointer_p = bstrPointer;
        }
        #endregion

        #region DecryptSecureString
        public static string ExtractStringFromSecureString(IntPtr bstrPointer_p)
        {
            string pt_password = ""; //PlainText Password
            for (int i = 0; ; i += 2)
            {
                byte lo = Marshal.ReadByte(bstrPointer_p, i);
                byte hi = Marshal.ReadByte(bstrPointer_p, i + 1);
                long l = lo + hi * 256;
                if (lo != 0)
                    pt_password += (char)l;
                else
                    break;
            }
            Marshal.ZeroFreeBSTR(bstrPointer_p);
            return pt_password;
        }
        #endregion

#endregion

#region AES Operations

        #region Encrypt
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an AesManaged object
            // with the specified key and IV.

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
        #endregion

        #region Decrypt
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }
        #endregion

        #region CleanUp
        public static void cleanup()
        {
            // Scrub the memory location and replace with nonsensical data
            plaintext = null;
            plaintext = "soikv78u356ji32w!";
        }
        #endregion

#endregion

    }
}
