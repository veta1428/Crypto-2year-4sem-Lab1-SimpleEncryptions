using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions
{
    public static class FileManager
    {
        public static async Task<string> ReadAsync(string filePath)
        {
            using FileStream fstream = new FileStream(filePath, FileMode.Open);
            
            byte[] buffer = new byte[fstream.Length];

            await fstream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.Default.GetString(buffer);
        }

        public static async Task<bool> WriteAsync(string toWrite, string filePath)
        {
            try
            {
                using FileStream fstream = new FileStream(filePath, FileMode.Create);

                byte[] buffer = Encoding.Default.GetBytes(toWrite);

                await fstream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
