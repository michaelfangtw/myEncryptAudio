    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class MyEncrypt
    {
        public MyEncrypt()
        {

        }

        public void run()
        {
            //byte[] encryptionKey = new byte[32]; // Replace this with your encryption key
            //byte[] initializationVector = new byte[16]; // Replace this with your IV

            //長度:32  ==>1234567890123456789012
            byte[] encryptionKey = System.Text.Encoding.UTF8.GetBytes("123456789012345678901234567890AB"); //32
                                                                                                           //長度:16  ==>1234567890123456
            byte[] initializationVector = System.Text.Encoding.UTF8.GetBytes("1234567890123456");//16 

            string inputFile = @"d:\temp\vm-cht.mp3";
            string encryptedFile = @"d:\temp\vm-cht.mp3.enc";
            string decryptedFile = @"d:\temp\vm-cht.mp3.decrypt.mp3";

            // Encrypt the audio file
            Console.WriteLine("encrypt input:" + inputFile);
            Console.WriteLine("encrypt outputFile:" + encryptedFile);
            EncryptFile(inputFile, encryptedFile, encryptionKey, initializationVector);

            // Decrypt the encrypted audio file
            EncryptFile(encryptedFile, decryptedFile, encryptionKey, initializationVector);
            Console.WriteLine("decrypt input:" + encryptedFile);
            Console.WriteLine("decrypt outputFile:" + decryptedFile);
            Console.WriteLine("===============================");

            string inputTextFile = @"d:\temp\vm-cht.txt";
            string encryptedTextFile = @"d:\temp\vm-cht.txt.enc";
            string decryptedTextFile = @"d:\temp\vm-cht.decrypt.txt";

            // Encrypt the audio file
            Console.WriteLine("encrypt input:" + inputTextFile);
            Console.WriteLine("encrypt outputFile:" + encryptedTextFile);
            EncryptFile(inputTextFile, encryptedTextFile, encryptionKey, initializationVector);

            // Decrypt the encrypted audio file
            DecryptFile(encryptedTextFile, decryptedTextFile, encryptionKey, initializationVector);
            Console.WriteLine("decrypt input:" + encryptedTextFile);
            Console.WriteLine("decrypt outputFile:" + decryptedTextFile);

        }


        static void EncryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create("AesManaged"))
            {
                if (aes == null) return;
                aes.Key = key;
                aes.IV = iv;

                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    int bufferSize = 4096;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;

                    while ((bytesRead = inputFileStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void DecryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create("AesManaged"))
            {
                if (aes == null) return;
                aes.Key = key;
                aes.IV = iv;

                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    int bufferSize = 4096;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;

                    while ((bytesRead = cryptoStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        outputFileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

    }
