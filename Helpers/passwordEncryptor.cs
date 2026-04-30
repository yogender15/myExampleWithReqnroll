using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class passwordEncryptor
    {
        // Key and IV should be securely stored (e.g., in environment variables or secure vault)
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("A1B2C3D4E5F6G7H8"); // 16 bytes for AES-128
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1H2G3F4E5D6C7B8A");  // 16 bytes IV

        public static void Main()
        {
            Console.Write("Enter password to encrypt: ");
            string originalPassword = "Newpassword04!";
                //Console.ReadLine();

            try
            {
                string encrypted = Encrypt(originalPassword);
                Console.WriteLine($"Encrypted: {encrypted}");

                string decrypted = Decrypt(encrypted);
                Console.WriteLine($"Decrypted: {decrypted}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Encrypts a string using AES
        public static string Encrypt(string plainText)
        {
            byte xorConstant = 0x53;
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("Text to encrypt cannot be null or empty.");

            byte[] data = Encoding.UTF8.GetBytes(plainText);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            string output = Convert.ToBase64String(data);
            return output;
        }

        // Decrypts a string using AES
        public static string Decrypt(string input)
        {
            if (input == null) throw new ArgumentNullException("cipher");

            byte xorConstant = 0x53;
            byte[] data = Convert.FromBase64String(input);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            string plainText = Encoding.UTF8.GetString(data);

            ////parse base64 string
            //byte[] data = Convert.FromBase64String(cipher);

            ////decrypt data
            //byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return plainText;
        }




    }
}