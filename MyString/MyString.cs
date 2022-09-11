using System;
using System.Collections;

namespace My_String
{

    public class MyString : IComparable<MyString>, IComparable// +
    {
        CaseInsensitiveComparer comparer = new CaseInsensitiveComparer();
        char[] value;

        public MyString(string value)
        {
            this.value = value.ToCharArray();
        }

        public string Value
        {
            get { return new string(value); }
            private set
            {
                this.value = value.ToCharArray();
            }
        }
        public int Length
        {
            get
            {
                return Value.Length;
            }
        }

        #region Task Methods
        public (bool, char[]) IsSubString(string subString)
        {
            if (subString != null && subString.Length <= Value.Length)
            {
                int rows = Value.Length;
                int columns = subString.Length;
                char[] res = new char[Value.Length];

                int[,] lcs = new int[rows + 1, columns + 1];

                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= columns; j++)
                    {
                        if (Value[i - 1].Equals(subString[j - 1]))
                        {
                            lcs[i, j] = lcs[i - 1, j - 1] + 1;
                            res[i - 1] = Value[i - 1];
                        }
                        else
                            lcs[i, j] = Math.Max(lcs[i, j - 1], lcs[i - 1, j]);
                    }
                }

                if (subString.Length == lcs[rows, columns])
                    return (true, res);
            }
            return (false, null);
        } 

        public void InsertSubString(string subString, int index)
        {
            if (index <= Value.Length)
            {
                char[] res = new char[Value.Length + subString.Length];
                char[] chars = Value.ToCharArray();

                int i = 0;
                for (; i < index; i++)
                    res[i] = chars[i];

                for (int j = 0; j < subString.Length; j++, i++)
                    res[i] = subString.ToCharArray()[j];

                for (int j = index; j < chars.Length; j++, i++)
                    res[i] = chars[j];

                Value = new string(res);
            }
        } 

        public void ChangeSubString(string subString, string newSubString)
        {
            if (IsSubString(subString).Item1)
            {
                int index = 0;
                char[] sub = IsSubString(subString).Item2;
                while (sub[index] == '\0')
                    index++;


                char[] res = new char[Value.Length + subString.Length];
                char[] chars = Value.ToCharArray();

                int i = 0;
                for (; i < index; i++)
                    res[i] = chars[i];

                for (int j = 0; j < newSubString.Length; j++, i++)
                    res[i] = newSubString.ToCharArray()[j];

                for (int j = subString.Length + index; j < chars.Length; j++, i++)
                    res[i] = chars[j];

                Value = new string(res);
            }
            else
                throw new ArgumentException($"this string: {Value} does not contain subString: {subString}");
        }
        #endregion

        #region Object
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is MyString)
            {
                MyString my = (MyString)obj;
                return Value.Equals(my.Value);
            }
            return false;
        }
        public override string ToString()
        {
            string res = $"{Value}({Length})";
            return res;
        }
        #endregion

        public int CompareTo(object obj)
        {
            if (obj is MyString)
            {
                var my = obj as MyString;
                return Value.CompareTo(my);
            }
            return -1;
        }
        public int CompareTo(MyString other)
        {

            return comparer.Compare(Value, other.Value);
            //return Value.CompareTo(other.Value);
        }

        public static implicit operator MyString(string value)
        {
            MyString my = new MyString(value);
            return my;
        }
    }
}
