using MyTrees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InteractWithStorages
{
    internal class Storages<T> where T : class, IComparable<T>
    {
        T[] array;
        List<T> list;
        ArrayList arrList;
        BinaryTree<T> binarytree;
        AVLTree<T> avltree;

        public Storages()
        {
            array = new T[0];
            list = new List<T>();
            arrList = new ArrayList();
            binarytree = new BinaryTree<T>();
            avltree = new AVLTree<T>();
        }
        public void Init(T[] initArray)
        {
            array = initArray;
            CreateArrayList();
            CreateList();
            CreateBinaryTree();
            CreateAVLTree();
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
        void CreateBinaryTree() => binarytree = new BinaryTree<T>(array);

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
                System.Array.Copy(array, i + 1, array, i, array.Length - i - 1);

                T[] my = new T[array.Length - 1];
                System.Array.Copy(array, my, array.Length - 1);

                array = my;
            }


        }
        #endregion

        #region AVLTree
        void CreateAVLTree() => avltree = new AVLTree<T>(array);
        public AVLTree<T> AVLTree
        {
            get => avltree;
        }
        #endregion


        public void AddToStorage(dynamic storage, T value) => storage.Add(value);
        public void DeleteFromStorage(dynamic storage, T value) => storage.Remove(value);
    }
}
