using My_String;
using System;
using System.Collections;

namespace InteractWithStorages
{
    public static class ConsoleMenu
    {

        static Exception wrongName = new Exception("Wrong name of collection");
        public static void Start()
        {
            Info();
            Storages<MyString>.Init();
            string input;
            do
            {
                Console.Write("\nEnter the command:");
                input = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    switch (input.ToLower())
                    {
                        case "/info":
                            Info();
                            break;
                        case "/add":
                            Storages();
                            AddToStorage();
                            break;
                        case "/storages":
                            Storages();
                            break;
                        case "/show":
                            Storages();
                            ShowStorage();
                            break;
                        case "/search":
                            SearchInStorage();
                            break;
                        case "/delete":
                            DeleteFromStorage();
                            break;
                        case "/cls":
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Ther is no such a command");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (input.ToLower() != "/end");

        }
        static void Info()
        {
            Console.WriteLine(
            "All commands:\n" +
            " /add - add new MyString to selected storage;\n" +
            " /storages - list of available storages;" +
            " /delete - delete selected MyString from selected storage;\n" +
            " /show - show selected storage;\n" +
            " /search - search specific element in selected storage;\n" +

            " /end - end program.");
        }

        static void Storages()
        {
            Console.WriteLine("Available storages: List, ArrayList, Array, BinaryTree;");
        }

        static void ShowStorage(string name = null)
        {
            if (name == null)
            {
                Console.Write("Enter name of the storage you want to get:");
                name = Console.ReadLine();
            }

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
                    Display(Storages<MyString>.BinaryTree);
                    break;
                default:
                    throw wrongName;
            }

        }
        static void Display(IEnumerable storage)
        {
            int i = 0;
            foreach (MyString myString in storage)
            {
                Console.WriteLine($"{i++}." + myString);
            }
        }

        static void AddToStorage()
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

        static void DeleteFromStorage()
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
                    Display(Storages<MyString>.BinaryTree);

                    break;
                default:
                    throw wrongName;
            }
            ShowStorage(name);
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

        static void SearchInStorage()
        {
            Console.Write("Enter name of storage where you want to find element: ");
            string name = Console.ReadLine();
            switch (name.ToLower())
            {
                case "list":
                    contains(Storages<MyString>.List, "List");
                    break;
                case "arraylist":
                    contains(Storages<MyString>.ArrayList, "ArrayList");
                    break;
                case "array":
                    contains(Storages<MyString>.Array, "Array");
                    break;
                case "binarytree":

                    break;
                default:
                    throw wrongName;
            }
        }
        static void contains(IList stor, string name)
        {
            Console.Write("Enter value of item which you want to find: ");
            string value = Console.ReadLine();
            Console.WriteLine($"{name} contains {value} : {stor.Contains(new MyString(value))}");
        }
    }
}
