using System;
using System.Collections.Generic;
using System.Collections;
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
            MyString string1 = new MyString("Hello ");
            MyString string2 = new MyString("World ");
            MyString string3 = new MyString("My name ");
            MyString string4 = new MyString("is ");
            MyString string5 = new MyString("Bohdan");
            MyString string6 = new MyString("Liashenko");


            Console.WriteLine("Work with List<T>:");
            List<MyString> list = new List<MyString>()
            {
                string1, string2, string3, string4, string5
            };
            list.Add(string6);// add to Collection
            list.Remove(string6);//remove from Collection
            Console.WriteLine(list.Contains(string6));
            foreach(MyString str in list)
                Console.Write(str);

            Console.WriteLine("\nWork with ArrayList:");
            ArrayList arrayList = new ArrayList()
            {
                string1, string2, string3, string4, string5
            };
            arrayList.Add(string6);
            arrayList.Remove(string6);
            Console.WriteLine(arrayList.Contains(string6));
            foreach(MyString str in arrayList)
                Console.Write(str);
            /*foreach (object str in arrayList)
            {
                if(str is MyString)
                    Console.WriteLine(str);
            }*/

            Console.WriteLine("\nWork with array: ");
            MyString[] array = { string1, string2, string3, string4, string5 };
            array = new MyString[] { string1, string2, string3, string4, string5, string6 }; // in C# array is immutable 
            array = new MyString[] { string1, string2, string3, string4, string6 };          // so to add or remove item
            //array[5] = string6;                                                            // you need to create new arr
            Console.WriteLine(array.Contains(string6)); 
            foreach(MyString str in array)
                Console.Write(str);

            Console.Read();
        }
    }
}
