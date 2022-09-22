using My_String;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InteractWithStorages
{
    public static class ConsoleMenu
    {
        static Storages<MyString> my = new Storages<MyString>();

        static Exception wrongName = new Exception("Wrong name of collection");
        static Exception unknownCommand = new Exception("Unknown command!");

        static void nameIsNull(ref string name)
        {
            if (name == null)
            {
                Console.Write("Enter name of the storage with which you want to interact: ");
                name = Console.ReadLine();
                Console.WriteLine();
            }
        }
        static void Clear()
        {
            my = new Storages<MyString>();
        }

        #region Regexes
        static Regex regexShow = new Regex(@"/show\s+(?<name>[A-Za-z]+)$");
        static Regex regexShowBT = new Regex(@"/show\s+(?<name>[A-Za-z]+)\s+(?<order>[A-Za-z]+)");
        static Regex regexSearch = new Regex(@"/search\s+(?<name>[A-Za-z]+)\s+(?<value>\w+)");
        static Regex regexAdd = new Regex(@"/add\s+(?<name>[A-Za-z]+)\s+(?<value>\w+)");
        static Regex regexDelete = new Regex(@"/delete\s+(?<name>[A-Za-z]+)\s+(?<value>\w+)");
        #endregion

        #region Info
        static void Info()
        {
            Console.WriteLine(
            "\tInteract with Storages\n" +
            "All commands:\n" +
            " /init - inizialize storages with default values;\n" +
            " /storages - list of available storages;\n\n" +

            " /add - adds new MyString to selected storage;\n" +
            " /delete - deletes selected MyString from selected storage;\n" +
            " /clear - clears all storages;\n\n" +

            " /show - shows selected storage;\n" +
            " /search - searches specific element in selected storage;\n\n" +
            " /interact - interacts with MyString;\n\n" +

            " /end - ends program.");
        }
        static void Storages()
        {
            PropertyInfo[] propertyInfos = typeof(Storages<MyString>).GetProperties();
            Console.Write("\nStorages are available: ");
            foreach (PropertyInfo property in propertyInfos)
            {
                Console.Write(property.Name + ", ");
            }
            Console.WriteLine();
        }
        #endregion

        #region Start
        public static void Start()
        {
            Info();
            Storages();
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
        static void CheckForLongExpression(string input)
        {
            bool isMatch = regexShow.IsMatch(input) || regexSearch.IsMatch(input)
                || regexDelete.IsMatch(input) || regexAdd.IsMatch(input) || regexShowBT.IsMatch(input);

            if (isMatch)
            {
                Match show = regexShow.Match(input);
                Match search = regexSearch.Match(input);
                Match add = regexAdd.Match(input);
                Match delete = regexDelete.Match(input);
                Match showBT = regexShowBT.Match(input);

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
                else if (showBT.Success)
                {
                    string name = showBT.Groups["name"].ToString() + " " +
                        showBT.Groups["order"].ToString();
                    ShowStorage(name);
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
                        AddToStorage();
                        break;
                    case "/storages":
                        Storages();
                        break;
                    case "/show":
                        ShowStorage();
                        break;
                    case "/search":
                        SearchInStorage();
                        break;
                    case "/delete":
                        DeleteFromStorage();
                        break;
                    case "/clear":
                        Clear();
                        break;
                    case "/cls":
                        Console.Clear();
                        Info();
                        break;
                    case "/interact":
                        StartInteract();
                        break;
                    default:
                        throw unknownCommand;
                }
            }
        }
        static void Init()
        {
            MyString[] initArray = new MyString[]
            {
                new MyString("ABCDE"),
                new MyString("ABC"),
                new MyString("AB"),
                new MyString("ABCD"),
                new MyString("ABCDEFG"),
                new MyString("ABCDEF"),
                new MyString("ABCDEFGK")
            };

            my.Init(initArray);
        }
        #endregion

        #region Show
        static void ShowStorage(string name = null)
        {
            nameIsNull(ref name);
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
                case "binarytree postorder":
                    Display(my.BinaryTree.PostOrderTraversal());
                    break;
                case "binarytree preorder":
                    Display(my.BinaryTree.PreOrderTraversal());
                    break;
                case "binarytree inorder":
                    Display(my.BinaryTree.InOrderTraversal());
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
            nameIsNull(ref name);
            switch (name)
            {
                case "list":
                    my.AddToStorage(my.List, add(value));
                    break;
                case "arraylist":
                    my.AddToStorage(my.ArrayList, add(value));
                    break;
                case "array":
                    my.AddToArray(add(value));
                    break;
                case "binarytree":
                    my.AddToStorage(my.BinaryTree, add(value));
                    break;
                default:
                    throw wrongName;
            }
            ShowStorage(name);
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
            nameIsNull(ref name);
            ShowStorage(name);
            switch (name)
            {
                case "list":
                    my.DeleteFromStorage(my.List, del(value));
                    break;
                case "arraylist":
                    my.DeleteFromStorage(my.ArrayList, del(value));
                    break;
                case "array":
                    my.DeleteFromArray(del(value));
                    break;
                case "binarytree":
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
            nameIsNull(ref name);
            switch (name)
            {
                case "list":
                    contains(name, value, my.List);
                    break;
                case "arraylist":
                    contains(name, value, null, my.ArrayList);
                    break;
                case "array":
                    contains(name, value, my.Array);
                    break;
                case "binarytree":
                    contains(name, value, my.BinaryTree);
                    break;
                default:
                    throw wrongName;
            }
        }
        static void contains(string name, string value, IEnumerable<MyString> stor, ArrayList arr = null)
        {
            if (stor == null && arr == null)
                throw new Exception("Storage is Empty!");
            else if (name == "arraylist")
            {
                if (value == null)
                {
                    Console.Write("Enter value of item which you want to find: ");
                    value = Console.ReadLine();
                }
                Console.WriteLine($"{name} contains {value}: {my.ArrayList.Contains(new MyString(value))}");
                return;
            }
            else if (value == null)
            {
                Console.Write("Enter value of item which you want to find: ");
                value = Console.ReadLine();
            }

            Console.WriteLine($"{name} contains {value}: {stor.Contains<MyString>(new MyString(value))}");
        }
        #endregion

        #region Interact With MyString

        static void InteractInfo()
        {
            Console.Clear();
            Console.WriteLine("\tInteract with MyString\n" +
                "All commands:\n" +
                " /issubstring - defines whether string contains substring or not;\n" +
                " /insert - inserts substring in chosen string;\n" +
                " /change - changes substring to a new one;\n\n" +
                " /return - returns to interact with storages.");
        }
        static void StartInteract()
        {
            InteractInfo();
            string input;
            do
            {
                Console.Write("\nEnter the command:");
                input = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    switch (input)
                    {
                        case "/issubstring":
                            IsSubString();
                            break;
                        case "/insert":
                            Insert();
                            break;
                        case "/change":
                            Change();
                            break;
                        case "/return":
                            Console.Clear();
                            Info();
                            break;
                        default:
                            throw unknownCommand;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (input != "/return");
        }
        static (string name, int index) Interact()
        {
            string name = null;
            int index;
            nameIsNull(ref name);
            ShowStorage(name);

            Console.Write("\nEnter index of MyString with which you want to interact: ");
            index = int.Parse(Console.ReadLine());

            return (name, index);
        }
        static void IsSubString()
        {
            var interact = Interact();

            Console.Write("Enter substring: ");
            string substring = Console.ReadLine();

            switch (interact.name.ToLower())
            {
                case "list":
                    Console.WriteLine($"{substring} is substring of {my.List[interact.index]}: " +
                        $"{my.List[interact.index].IsSubString(substring).Item1}");
                    break;
                case "arraylist":
                    Console.WriteLine($"{substring} is substring of {my.ArrayList[interact.index]}: " +
                        $"{my.List[interact.index].IsSubString(substring).Item1}");
                    break;
                case "array":
                    Console.WriteLine($"{substring} is substring of {my.Array[interact.index]}: " +
                        $"{my.List[interact.index].IsSubString(substring).Item1}");
                    break;
                case "binarytree":
                    Console.WriteLine($"{substring} is substring of {my.BinaryTree[interact.index]}: " +
                        $"{my.List[interact.index].IsSubString(substring).Item1}");
                    break;
                default:
                    throw wrongName;
            }
        }
        static void Insert()
        {
            var interact = Interact();

            Console.Write("Enter substring you want to insert: ");
            string substring = Console.ReadLine();

            Console.Write("Enter index where you want to insert your substring: ");
            int index = int.Parse(Console.ReadLine());
            switch (interact.name)
            {
                case "list":
                    my.List[interact.index] = my.List[interact.index].InsertSubString(substring, index);
                    Display(my.List);
                    break;
                case "arraylist":
                    my.ArrayList[interact.index] = (my.ArrayList[interact.index] as MyString).InsertSubString(substring, index);
                    Display(my.ArrayList);
                    break;
                case "array":
                    my.Array[interact.index] = my.Array[interact.index].InsertSubString(substring, index);
                    Display(my.Array);
                    break;
                case "binarytree":
                    my.BinaryTree[interact.index] = my.BinaryTree[interact.index].InsertSubString(substring, index);
                    Display(my.BinaryTree);
                    break;
                default:
                    throw wrongName;
            }
        }
        static void Change()
        {
            var interact = Interact();
            Console.Write("Enter substring you want to change: ");
            string subString = Console.ReadLine();

            Console.Write("Enter new substring: ");
            string newSubString = Console.ReadLine();
            switch (interact.name)
            {
                case "list":
                    my.List[interact.index] = my.List[interact.index].ChangeSubString(subString, newSubString);
                    Display(my.List);
                    break;
                case "arraylist":
                    my.ArrayList[interact.index] = (my.ArrayList[interact.index] as MyString).ChangeSubString(subString, newSubString);
                    Display(my.ArrayList);
                    break;
                case "array":
                    my.Array[interact.index] = my.Array[interact.index].ChangeSubString(subString, newSubString);
                    Display(my.Array);
                    break;
                case "binarytree":
                    my.BinaryTree[interact.index] = my.BinaryTree[interact.index].ChangeSubString(subString, newSubString);
                    Display(my.BinaryTree);
                    break;
                default:
                    throw wrongName;
            }
        }
        #endregion
    }
}
