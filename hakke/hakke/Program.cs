using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using hakkePluginInterface;

namespace hakke
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if(args.Length < 1)
                {
                    Console.WriteLine($"Usage: {System.Reflection.Assembly.GetExecutingAssembly().Location} <path to encrypted data> <path to plugin for decryption> <path to output file to write to (optional)> <path to decryption key (optional)>");
                    Console.ReadKey();
                }
                else if(args.Length >= 2)
                {
                    string outputPath = null;
                    string key = null;
                    string data = File.ReadAllText(args[0]);
                    string algorithm = args[1];
                    if (args.Length > 2) { outputPath = args[2]; }
                    if (args.Length > 3) { key = File.ReadAllText(args[3]); }
                    var asm = Assembly.LoadFrom(algorithm);
                    var instance = Activator.CreateInstance(asm.GetExportedTypes()[0]);
                    string output = ((hakkePluginInterface.Class1.hakkeInterface)instance).Decrypt(data, key);
                    if(outputPath != null)
                    {
                        File.Create(outputPath).Dispose();
                        File.WriteAllText(outputPath, output);
                    }
                    Console.WriteLine("(c) NXF\nWHOEVER STEALS IT IS HOMO\n\n\n[+] DECRYPT SUCCESSFUL\n\n\n" + output);
                    Console.ReadKey();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("(c) NXF\nWHOEVER STEALS IT IS HOMO\n\n\n[-] FAILED, CHECK PASSED ARGUMENTS");
            }

        }
    }
}
