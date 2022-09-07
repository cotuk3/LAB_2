﻿using My_String;
using System.Collections;
using System.Collections.Generic;

namespace InteractWithStorages
{
    internal static class Storages<T> where T : MyString
    {


        static T[] array;
        static List<T> list;
        static ArrayList arrList;


        static List<T> CreateList()
        {
            list = new List<T>();
            list.AddRange(array);
            return list;
        }
        static ArrayList CreateArrayList()
        {
            arrList = new ArrayList();
            arrList.AddRange(array);
            return arrList;
        }

        static public void Init()
        {
            array = new T[5]
        {
            (T)new MyString("ABC"),
            (T)new MyString("A"),
            (T)new MyString("AB"),
            (T)new MyString("ABCD"),
            (T)new MyString("ABCDF")
        };
            CreateArrayList();
            CreateList();

        }

        public static List<T> List
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }

        public static ArrayList ArrayList
        {
            get
            {
                return arrList;
            }
            set
            {
                arrList = value;
            }
        }

        public static T[] Array
        {
            get
            {
                return array;
            }
            set
            {
                array = value;
            }
        }

        public static void AddToList(params T[] value)
        {
            list.AddRange(value);
        }
        public static void AddToArrayList(params T[] value)
        {
            arrList.AddRange(value);
        }
        public static void AddToArray(params T[] values)
        {
            T[] newArr = new T[array.Length + values.Length];

            int i = 0;
            for (; i < array.Length; i++)
                newArr[i] = array[i];

            for (int j = 0; j < values.Length; j++, i++)
            {
                newArr[i] = values[j];
            }

            array = newArr;
        }

        public static void DeleteFromList(int index)
        {
            list.RemoveAt(index);
        }
        public static void DeleteFromArrayList(int index)
        {
            arrList.RemoveAt(index);
        }
        public static void DeleteFromArray(int index)
        {
            if (index < array.Length)
            {
                System.Array.Copy(array, index, array, index - 1, array.Length - index);
            }
            else
                throw new System.IndexOutOfRangeException();
        }

    }
}
