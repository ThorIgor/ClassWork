using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IgorPr_4_
{
    class Program
    {
        class Student
        {
            public string Name { get; set; }
            public int[] Marks { get; set; }

            public Student(string name, int[] marks)
            {
                Name = name;
                Marks = marks;
            }
            public override string ToString()
            {
                string student = "";
                student += Name + ": ";
                foreach (var el in Marks)
                    student += el + " ";
                return student;

            }
            public byte[] ByteArray
            {
                get { return Encoding.Default.GetBytes(this.ToString()); }
            }
        }
        static void CodeFile(string file, int num)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] arr = new byte[fs.Length];
                    fs.Read(arr, 0, arr.Length);
                    string str = Encoding.Default.GetString(arr);
                    char[] chArr = str.ToCharArray();
                    for (int i = 0; i < chArr.Length; i++)
                        chArr[i] = (char)((int)chArr[i] + num);
                    str = "";
                    foreach (var el in chArr)
                        str += el;
                    fs.Seek(0, SeekOrigin.Begin);
                    fs.Write(Encoding.Default.GetBytes(str), 0, Encoding.Default.GetBytes(str).Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void PrintFile(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                byte[] result = new byte[fs.Length];
                fs.Read(result, 0, result.Length);
                Console.WriteLine(Encoding.Default.GetString(result));
            }
        }
        static void Main(string[] args)
        {
            Student student = new Student("Igor", new int[]{10, 12, 9, 12});
            using (FileStream fs = new FileStream("File.txt", FileMode.Create))
                fs.Write(student.ByteArray, 0, student.ByteArray.Length);
            PrintFile("File.txt");
            CodeFile("File.txt", 2);
            PrintFile("File.txt");
            CodeFile("File.txt", -2);
            PrintFile("File.txt");
        }
    }
}
