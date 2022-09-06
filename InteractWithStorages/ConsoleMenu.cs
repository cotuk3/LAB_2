using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_String;

namespace InteractWithStorages
{
    public static class ConsoleMenu
    {
        public static void Start() // TODO : Second
        {

        }
        static Exception wrongName = new Exception("Wrong name of collection");
        public static void Info() // TODO : First
        {

        }
        public static void ShowStorage()
        {
            Console.Write("Enter name of the storage you want to get:");
            string name = Console.ReadLine();

            switch (name.ToLower())
            {
                case "list":
                    Display(Storages<MyString>.List);
                    break;
                case "arraylist":
                    Display(Storages<MyString>.ArrayList);
                    break;
                case "array":
                    Display(Storages<MyString>.Array);
                    break;
                case "binarytree":
                    //Display(Storages<MyString>.List);
                    break;
                default:
                    throw wrongName;
            }
        }
        static void Display(IEnumerable storage)
        {
            int i = 0;
            foreach(MyString myString in storage)
            {
                Console.WriteLine($"{i++}." + myString);
            }
        }

        public static void AddToStorage()
        {
            Console.Write("Enter name of the storage where you want to add MyString:");
            string name = Console.ReadLine();
            switch (name.ToLower())
            {
                case "list":
                    Storages<MyString>.AddToList(add);
                    Display(Storages<MyString>.List);
                    break;
                case "arraylist":
                    Storages<MyString>.AddToArrayList(add);
                    Display(Storages<MyString>.List);
                    break;
                case "array":
                    Storages<MyString>.AddToArray(add);
                    Display(Storages<MyString>.List);
                    break;
                case "binarytree":
                    //Display(Storages<MyString>.List);
                    break;
                default:
                    throw wrongName;
            }



        }
        static MyString add
        {
            get
            {
                Console.Write("Enter string which you want to add:");
                string value = Console.ReadLine();
                MyString myString = new MyString(value);
                return myString;
            }
        }

        public static void DeleteFromStorage()
        {
            Console.Write("Enter name of storage from which you want to delete MyString:");
            string name = Console.ReadLine();

            switch (name.ToLower())
            {
                case "list":
                    Display(Storages<MyString>.List);
                    Storages<MyString>.DeleteFromList(del);
                    break;
                case "arraylist":
                    Display(Storages<MyString>.ArrayList);
                    Storages<MyString>.DeleteFromArrayList(del);
                    break;
                case "array":
                    Display(Storages<MyString>.Array);
                    Storages<MyString>.DeleteFromArray(del);
                    break;
                case "binarytree":
                    //Display(Storages<MyString>.Array);

                    break;
                default:
                    throw wrongName;
            }
        }
        static int del
        {
            get
            {
                Console.Write("Enter index of MyString you want to delete: ");
                int index = Int32.Parse(Console.ReadLine());
                return index;
            }
        }

        public static void SearchInStorage()
        {
            Console.Write("Enter name of storage where you want to find element: ");
            string name = Console.ReadLine();

           
                switch (name.ToLower())
            {
                case "list":
                    contains(Storages<MyString>.List);
                    break;
                case "arraylist":
                    contains(Storages<MyString>.ArrayList);
                    break;
                case "array":
                    contains(Storages<MyString>.Array);
                    break;
                case "binarytree":
                    
                    break;
                default:
                    throw wrongName;
            }


        }

        static void contains(IList stor)
        {
            Console.Write("Enter value of item which you want to find: ");
            string value = Console.ReadLine();
            Console.WriteLine($"{stor.ToString()} contains {value} : {stor.Contains(value)}");
        }
    }
}
