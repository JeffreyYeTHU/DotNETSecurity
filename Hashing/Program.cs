using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

public class HashDirectory
{
    public static async Task Main()
    {
        // SHA256 example
        Console.WriteLine("'j' in hash:");
        PrintByteArray(ComputeSha256(Encoding.Unicode.GetBytes("j")));

        // SHA256 file
        string fileName = @"C:\Users\Jeffery.Ye\source\repos\DotNETSecurity\Hashing\Program.cs";
        var fileBytes = await File.ReadAllBytesAsync(fileName);
        Console.WriteLine("this file's hash:");
        PrintByteArray(ComputeSha256(fileBytes));

        // HMAC SHA256
        string key = "jeff";
        Console.WriteLine("this file's hmac wiht key 'jeff':");
        PrintByteArray(ComputeHmac(fileBytes, Encoding.Unicode.GetBytes(key)));

        // store hashed password
        string password = "123456";
        string salt = "jeff";
        Console.WriteLine("Hashed password:");
        PrintByteArray(HashPassword(Encoding.Unicode.GetBytes(password), Encoding.Unicode.GetBytes(salt)));
    }

    // Display the byte array in a readable format.
    static void PrintByteArray(byte[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"{array[i]:X2}");
            if ((i % 4) == 3) Console.Write(" ");
        }
        Console.WriteLine();
    }

    static byte[] ComputeHmac(byte[] toBeHashed, byte[] key)
    {
        using var hmac = new HMACSHA256(key);
        return hmac.ComputeHash(toBeHashed);
    }

    static byte[] ComputeSha256(byte[] toBeHashed)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(toBeHashed);
    }

    static byte[] ComputeSha256(Stream inputStream)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(inputStream);
    }

    static byte[] HashPassword(byte[] password, byte[] salt)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(password.Concat(salt).ToArray());
    }

    
}
