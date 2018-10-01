using System;
using System.IO;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Encrypter.GenerateKeys();
            string text = File.ReadAllText(@"E:\Test\Test.txt");
            Console.WriteLine($"Text to encrypt: {text}");
            string enc = Encrypter.Encrypt(text);
            Console.WriteLine($"Encrypted Text: {enc}");
            string dec = Encrypter.Decrypt(enc);
            Console.WriteLine($"Decrypted Text: {dec}");
            Console.ReadLine();
        }
    }
}
