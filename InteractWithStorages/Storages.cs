﻿using My_String;
using MyBinaryTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InteractWithStorages
{
    internal class Storages<T> where T : class, IComparable<T>
    {
        MyString[] initArray;
        T[] array;
        List<T> list;
        ArrayList arrList;
        BinaryTree<T> binarytree;

        List<T> CreateList()
        {
            list = new List<T>();
            list.AddRange(array);
            return list;
        }
        ArrayList CreateArrayList()
        {
            arrList = new ArrayList();
            arrList.AddRange(array);
            return arrList;
        }
        BinaryTree<T> CreateBinaryTree()
        {
            binarytree = new BinaryTree<T>(array);
            return binarytree;
        }

        public void Init()
        {
            initArray = new MyString[5]
            {
            new MyString("ABC"),
            new MyString("A"),
            new MyString("AB"),
            new MyString("ABCD"),
            new MyString("ABCDF")
            };
            array = new T[initArray.Length];
            System.Array.Copy(initArray, array, array.Length);
            CreateArrayList();
            CreateList();
            CreateBinaryTree();
        }

        public List<T> List
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

        public ArrayList ArrayList
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

        public T[] Array
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
        public BinaryTree<T> BinaryTree
        {
            get { return binarytree; }
            set { binarytree = value; }
        }

        public void AddToList(params T[] value)
        {
            list.AddRange(value);
        }
        public void AddToArrayList(params T[] value)
        {
            arrList.AddRange(value);
        }
        public void AddToArray(params T[] values)
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
        public void AddToBinaryTree(params T[] values)
        {
            foreach (T item in values)
            {
                binarytree.Add(item);
            }
        }

        public void DeleteFromList(T value)
        {
            list.Remove(value);
        }
        public void DeleteFromArrayList(T value)
        {
            arrList.Remove(value);
        }
        public void DeleteFromArray(T value)
        {
            if (array.Contains<T>(value))
            {
                int i = 0;
                for (; i < array.Length; i++)
                {
                    if (array[i].Equals(value))
                        break;
                }
                System.Array.Copy(array, i + 1, array, i - 1, array.Length - i);
            }


        }
        public void DeleteFromBinaryTree(T value) // HACK : implement method 
        {
            binarytree.Remove(value);
        }
    }
}
