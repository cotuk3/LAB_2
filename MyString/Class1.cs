using System;

namespace My_String
{
    public class MyString
    {
        public MyString(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
        public int Length
        {
            get
            {
                return Value.Length;
            }
        }

        public bool IsSubString(string subString)
        {


            return false;
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

                for (int j = 0; j < index; j++, i++)
                    res[i] = subString.ToCharArray()[j];

                for (int j = index; j < res.Length; j++, i++)
                    res[i] = chars[i];

                Value = res.ToString();
            }
        }

        public void ChangeSubString(string subString, string newSubString)
        {
            if (IsSubString(subString))
            {

            }
            else
                throw new ArgumentException($"this string: {Value} does not contain subString: {subString}");
        }

        public override string ToString()
        {
            return Value;
        }

    }
}
