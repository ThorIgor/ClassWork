using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IgorPr_3_
{
    class Program
    {
        static class ConsoleSym
        {

            static void Creat(string FileName)
            {
                try
                {
                    string dop = "";
                    while (true)
                    {
                        dop += Console.ReadLine() + Environment.NewLine;
                        if (dop.ToLower().Contains("end"))
                            break;
                    }
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + FileName, dop.Remove(dop.Length - 5, 5));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            static void Type(string FileName)
            {
                try
                {
                    Console.WriteLine(File.ReadAllText(Directory.GetCurrentDirectory() + "\\" + FileName));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            static void Cd(string path)
            {
                try
                {
                    Directory.SetCurrentDirectory(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            static void Dir(string dop = null)
            {
                try
                {
                    if (dop == null)
                    {
                        DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
                        foreach (var el in info.GetDirectories())
                            Console.WriteLine($"{el.CreationTime,15} <DIR> {el.Name,10}");
                        foreach (var el in info.GetFiles())
                            Console.WriteLine($"{el.CreationTime,15} {el.Length,10} {el.Name}");
                    }
                    else if (dop == "\\")
                    {
                        DirectoryInfo info = new DirectoryInfo("C:\\");
                        foreach (var el in info.GetDirectories())
                            Console.WriteLine($"{el.CreationTime,15} <DIR> {el.Name,10}");
                        foreach (var el in info.GetFiles())
                            Console.WriteLine($"{el.CreationTime,15} {el.Length,10} {el.Name}");
                    }
                    else if (dop == "..")
                    {
                        DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
                        info = info.Parent;
                        foreach (var el in info.GetDirectories())
                            Console.WriteLine($"{el.CreationTime,15} <DIR> {el.Name,10}");
                        foreach (var el in info.GetFiles())
                            Console.WriteLine($"{el.CreationTime,15} {el.Length,10} {el.Name}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            static void Md(string path)
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            static public void Work()
            {
                string str = "";
                bool exit = false;
                while (!exit)
                {
                    try
                    {


                        Console.Write(Directory.GetCurrentDirectory() + ">");
                        str = Console.ReadLine();
                        string[] strArr = str.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                        switch (strArr[0].ToLower())
                        {
                            case "creat":
                                Creat(strArr[1]);
                                break;
                            case "type":
                                Type(strArr[1]);
                                break;
                            case "cd":
                                Cd(strArr[1]);
                                break;
                            case "dir":
                                if (strArr.Length > 1)
                                    Dir(strArr[1]);
                                else
                                    Dir();
                                break;
                            case "md":
                                Md(strArr[1]);
                                break;
                            case "exit":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Error");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }
        static void Main(string[] args)
        {
            ConsoleSym.Work();

        }
    }
}
