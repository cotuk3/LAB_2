using My_String;
using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InteractWithStorages
{
    public static class ConsoleMenu
    {
        static Storages<MyString> my = new Storages<MyString>();
        static Regex regex = new Regex(@"/show\s+(?<name>[A-Za-z]+)");
        static Exception wrongName = new Exception("Wrong name of collection");
        public static void Start()
        {
            Info();
            string input;
            do
            {
                Console.Write("\nEnter the command:");
                input = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    Match match = regex.Match(input);
                    if (match.Success)
                    {
                        ShowStorage(match.Groups["name"].ToString());
                    }
                    else
                    {
                        switch (input.ToLower())
                        {
                            case "/info":
                                Info();
                                break;
                            case "/init":
                                Init();
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
                                Storages();
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
            " /init - inizialize storages with default values;\n" +
            " /add - add new MyString to selected storage;\n" +
            " /storages - list of available storages;" +
            " /delete - delete selected MyString from selected storage;\n" +
            " /show - show selected storage;\n" +
            " /search - search specific element in selected storage;\n" +

            " /end - end program.");
        }

        static void Storages()
        {
            PropertyInfo[] propertyInfos = typeof(Storages<MyString>).GetProperties();
            Console.Write("Storages are available: ");
            foreach (PropertyInfo property in propertyInfos)
            {
                Console.Write(property.Name + ", ");
            }
            Console.WriteLine();
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
                    Display(my.List);
                    break;
                case "arraylist":
                    Display(my.ArrayList);
                    break;
                case "array":
                    Display(my.Array);
                    break;
                case "binarytree":
                    Display(my.BinaryTree);
                    break;
                default:
                    throw wrongName;
            }

        }
        static void Display(IEnumerable storage)
        {
            int i = 0;
            if (storage == null)
                throw new Exception("Storage is Empty!");

            foreach (MyString myString in storage)
            {
                Console.WriteLine($"{i++}." + myString);
            }
        }
        static void Init()
        {
            my.Init();
        }
        static void AddToStorage()
        {
            Console.Write("Enter name of the storage where you want to add MyString:");
            string name = Console.ReadLine();
            switch (name.ToLower())
            {
                case "list":
                    my.AddToList(add);
                    Display(my.List);
                    break;
                case "arraylist":
                    my.AddToArrayList(add);
                    Display(my.List);
                    break;
                case "array":
                    my.AddToArray(add);
                    Display(my.List);
                    break;
                case "binarytree":
                    my.AddToBinaryTree(add);
                    Display(my.BinaryTree);
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
                    Display(my.List);
                    my.DeleteFromList(del);
                    break;
                case "arraylist":
                    Display(my.ArrayList);
                    my.DeleteFromArrayList(del);
                    break;
                case "array":
                    Display(my.Array);
                    my.DeleteFromArray(del);
                    break;
                case "binarytree":
                    Display(my.BinaryTree);
                    MyString value = my.BinaryTree.PostOrderTraversal()[del];
                    my.DeleteFromBinaryTree(value);
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
                    contains(my.List, "List");
                    break;
                case "arraylist":
                    contains(my.ArrayList, "ArrayList");
                    break;
                case "array":
                    contains(my.Array, "Array");
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
