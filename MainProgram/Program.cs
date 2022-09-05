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

            var item = my.IsSubString("aaa");
            Console.WriteLine(item.Item1 + " " + new string(item.Item2));

            my.ChangeSubString("H", "bb");
            Console.WriteLine(my);

            Console.Read();
        }
    }
}
