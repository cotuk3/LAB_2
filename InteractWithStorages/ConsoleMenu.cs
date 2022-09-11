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
        static Exception wrongName = new Exception("Wrong name of collection");

        #region Regexes
        static Regex regexShow = new Regex(@"/show\s+(?<name>[A-Za-z]+)");
        static Regex regexSearch = new Regex(@"/search\s+(?<name>[A-Za-z]+)\s+(?<value>\w+)");
        static Regex regexAdd = new Regex(@"/add\s+(?<name>[A-Za-z]+)\s+(?<value>\w+)");
        static Regex regexDelete = new Regex(@"/delete\s+(?<name>[A-Za-z]+)\s+(?<value>\w+)");
        #endregion

        #region Start
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
                    CheckForLongExpression(input);
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
        static void Init()
        {
            my.Init();
        }
        static void CheckForLongExpression(string input)
        {
            bool isMatch = regexShow.IsMatch(input) || regexSearch.IsMatch(input)
                || regexDelete.IsMatch(input) || regexAdd.IsMatch(input);

            if (isMatch)
            {
                Match show = regexShow.Match(input);
                Match search = regexSearch.Match(input);
                Match add = regexAdd.Match(input);
                Match delete = regexDelete.Match(input);

                if (show.Success)
                {
                    ShowStorage(show.Groups["name"].ToString());
                }
                else if (add.Success)
                {
                    AddToStorage(add.Groups["name"].ToString().ToLower(),
                        add.Groups["value"].ToString());
                }
                else if (search.Success)
                {
                    SearchInStorage(search.Groups["name"].ToString().ToLower(),
                        search.Groups["value"].ToString());
                }
                else if (delete.Success)
                {
                    DeleteFromStorage(delete.Groups["name"].ToString().ToLower(),
                        delete.Groups["value"].ToString());
                }
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
                        Info();
                        break;
                    default:
                        Console.WriteLine("Ther is no such a command");
                        break;
                }
            }
        }
        #endregion

        #region Show
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
            int i = -1;
            foreach (MyString myString in storage)
            {
                Console.WriteLine($"{++i}." + myString);
            }

            if (i == -1)
                throw new Exception("Storage is Empty!");

            Console.WriteLine();
        }
        #endregion

        #region Add
        static void AddToStorage(string name = null, string value = null)
        {
            if (name == null)
            {
                Console.Write("Enter name of the storage where you want to add MyString:");
                name = Console.ReadLine();
            }
            switch (name)
            {
                case "list":
                    my.AddToStorage(my.List, add(value));
                    Display(my.List);
                    break;
                case "arraylist":
                    my.AddToStorage(my.ArrayList, add(value));
                    Display(my.ArrayList);
                    break;
                case "array":
                    my.AddToArray(add(value));
                    Display(my.Array);
                    break;
                case "binarytree":
                    my.AddToStorage(my.BinaryTree, add(value));
                    Display(my.BinaryTree);
                    break;
                default:
                    throw wrongName;
            }
        }
        static MyString add(string value)
        {
            if (value == null)
            {
                Console.Write("Enter value which you want to add:");
                value = Console.ReadLine();
            }

            MyString myString = new MyString(value);
            return myString;
        }
        #endregion

        #region Delete
        static void DeleteFromStorage(string name = null, string value = null)
        {
            if (name == null)
            {
                Console.Write("Enter name of storage from which you want to delete MyString:");
                name = Console.ReadLine().ToLower();
            }
            switch (name)
            {
                case "list":
                    Display(my.List);
                    my.DeleteFromStorage(my.List, del(value));
                    break;
                case "arraylist":
                    Display(my.ArrayList);
                    my.DeleteFromStorage(my.ArrayList, del(value));
                    break;
                case "array":
                    Display(my.Array);
                    my.DeleteFromArray(del(value));
                    break;
                case "binarytree":
                    Display(my.BinaryTree);
                    my.DeleteFromStorage(my.BinaryTree, del(value));
                    break;
                default:
                    throw wrongName;
            }
            ShowStorage(name);
        }
        static MyString del(string value)
        {
            if (value == null)
            {
                Console.Write("Enter value of MyString you want to delete: ");
                value = Console.ReadLine();
            }

            return value;

        }
        #endregion

        #region Search
        static void SearchInStorage(string name = null, string value = null)
        {
            if (name == null)
            {
                Console.Write("Enter name of storage where you want to find element: ");
                name = Console.ReadLine().ToLower();
            }
            switch (name)
            {
                case "list":
                    contains(name, value, my.List);
                    break;
                case "arraylist":
                    contains(name, value, my.ArrayList);
                    break;
                case "array":
                    contains(name, value, my.Array);
                    break;
                case "binarytree":
                    contains(name, value);
                    break;
                default:
                    throw wrongName;
            }
        }
        static void contains(string name, string value, IList stor = null)
        {

            if (name == "binarytree" && value == null)
            {
                Console.Write("Enter value of item which you want to find: ");
                value = Console.ReadLine();
            }
            else if (name == "binarytree" && value != null)
            {
                Console.WriteLine($"{name} contains {value}: {my.BinaryTree.Contains(new MyString(value))}");
                return;
            }


            if (stor == null)
                throw new Exception("Storage is Empty!");
            else if (value == null)
            {
                Console.Write("Enter value of item which you want to find: ");
                value = Console.ReadLine();
            }
            Console.WriteLine($"{name} contains {value}: {stor.Contains(new MyString(value))}");

        }
        #endregion
    }
}
