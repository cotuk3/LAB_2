using My_String;
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

        public Storages()
        {
            array = new T[0];
            list = new List<T>();
            arrList = new ArrayList();
            binarytree = new BinaryTree<T>();
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

        #region List
        List<T> CreateList()
        {
            list.AddRange(array);
            return list;
        }
        public List<T> List
        {
            get => list;
        }
        #endregion

        #region ArrayList
        ArrayList CreateArrayList()
        {
            arrList.AddRange(array);
            return arrList;
        }
        public ArrayList ArrayList
        {
            get => arrList;
        }
        #endregion

        #region BinaryTree
        BinaryTree<T> CreateBinaryTree()
        {
            binarytree = new BinaryTree<T>(array);
            return binarytree;
        }
        public BinaryTree<T> BinaryTree
        {
            get => binarytree;
        }
        #endregion

        #region Array
        public T[] Array
        {
            get => array;
        }
        public void AddToArray(T value)
        {
            if (array.Length == 0)
            {
                array = new T[1];
                array[0] = value;
                return;
            }

            T[] newArr = new T[array.Length + 1];
            int i = 0;
            for (; i < array.Length; i++)
                newArr[i] = array[i];

            newArr[newArr.Length - 1] = value;
            array = newArr;
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
        #endregion

        public void AddToStorage(dynamic storage, T value) => storage.Add(value);
        public void DeleteFromStorage(dynamic storage, T value) => storage.Delete(value);
    }
}
