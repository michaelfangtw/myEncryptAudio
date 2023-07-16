using System;
using System.IO;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        string inputFile = @"d:\temp\vm-cht.mp3"; // Replace with your input audio file path
        string encryptedFile = @"d:\temp\vm-cht.mp3.enc"; // Encrypted output file path
        string decryptedFile = @"d:\temp\vm-cht.decrypt.mp3"; // Decrypted output file path
        string key = "123456789012345678901234567890AB"; // Replace this with your own encryption key (must be 16, 24, or 32 bytes)

        EncryptFile(inputFile, encryptedFile, key);
        DecryptFile(encryptedFile, decryptedFile, key);

        Console.WriteLine("input file:" + inputFile);
        Console.WriteLine("encryptedFile file:" + encryptedFile);
        Console.WriteLine("decryptedFile file:" + decryptedFile);
        Console.WriteLine("mp3 Encryption and Decryption completed successfully!");
        Console.WriteLine("==========================");

        string inputTextFile = @"d:\temp\vm-cht.txt"; // Replace with your input audio file path
        string encryptedTextFile = @"d:\temp\vm-cht.txt.env"; // Encrypted output file path
        string decryptedTextFile = @"d:\temp\vm-cht.decrypt.txt"; // Decrypted output file path
        string txtKey = "123456789012345678901234567890AB"; // Replace this with your own encryption key (must be 16, 24, or 32 bytes)

        EncryptFile(inputTextFile, encryptedTextFile, txtKey);
        DecryptFile(encryptedTextFile, decryptedTextFile, txtKey);


        Console.WriteLine("input text file:" + inputTextFile);
        Console.WriteLine("encryptedFile text file:" + encryptedTextFile);
        Console.WriteLine("decryptedFile text file:" + decryptedTextFile);
        Console.WriteLine("text Encryption and Decryption completed successfully!");
        Console.WriteLine("==========================");


    }

    static void EncryptFile(string inputFile, string outputFile, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
            aes.GenerateIV();

            using (FileStream input = new FileStream(inputFile, FileMode.Open))
            using (FileStream output = new FileStream(outputFile, FileMode.Create))
            using (CryptoStream cryptoStream = new CryptoStream(output, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                // Write the IV to the output file
                output.Write(aes.IV, 0, aes.IV.Length);

                // Encrypt the audio file
                input.CopyTo(cryptoStream);
            }
        }
    }

    static void DecryptFile(string inputFile, string outputFile, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = System.Text.Encoding.UTF8.GetBytes(key);

            byte[] iv = new byte[aes.IV.Length];

            using (FileStream input = new FileStream(inputFile, FileMode.Open))
            using (FileStream output = new FileStream(outputFile, FileMode.Create))
            {
                // Read the IV from the input file
                input.Read(iv, 0, iv.Length);

                aes.IV = iv;

                using (CryptoStream cryptoStream = new CryptoStream(input, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    // Decrypt the audio file
                    cryptoStream.CopyTo(output);
                }
            }
        }
    }
}
