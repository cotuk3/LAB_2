using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_String;

namespace MainProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyString my = new MyString("Hello");
            Console.WriteLine(my);

            my.InsertSubString("aaa", 2);

            Console.WriteLine(my);

            Console.Read();
        }
    }
}
