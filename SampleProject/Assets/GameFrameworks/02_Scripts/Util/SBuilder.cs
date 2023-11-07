using System.Text;

namespace GameFrameworks
{
    public static class SBuilder
    {
        public const string PERCENT = "%";

        private static StringBuilder staticBuilder;
        public static StringBuilder AccessCarefully
        {
            get
            {
                if (staticBuilder == null)
                    staticBuilder = new StringBuilder(1024);

                return staticBuilder;
            }
        }

        public static string Append(params string[] _valueArr)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            for (int i = 0; i < _valueArr.Length; i++)
                staticBuilder.Append(_valueArr[i]);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);
            staticBuilder.Append(_value4);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4, string _value5)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);
            staticBuilder.Append(_value4);
            staticBuilder.Append(_value5);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4, string _value5, string _value6)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);
            staticBuilder.Append(_value4);
            staticBuilder.Append(_value5);
            staticBuilder.Append(_value6);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4, string _value5, string _value6, string _value7)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);
            staticBuilder.Append(_value4);
            staticBuilder.Append(_value5);
            staticBuilder.Append(_value6);
            staticBuilder.Append(_value7);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4, string _value5, string _value6, string _value7, string _value8)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);
            staticBuilder.Append(_value4);
            staticBuilder.Append(_value5);
            staticBuilder.Append(_value6);
            staticBuilder.Append(_value7);
            staticBuilder.Append(_value8);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4, string _value5, string _value6, string _value7, string _value8, string _value9)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);
            staticBuilder.Append(_value4);
            staticBuilder.Append(_value5);
            staticBuilder.Append(_value6);
            staticBuilder.Append(_value7);
            staticBuilder.Append(_value8);
            staticBuilder.Append(_value9);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4, string _value5, string _value6, string _value7, string _value8, string _value9, string _value10)
        {
            if (staticBuilder == null)
                staticBuilder = new StringBuilder(1024);

            staticBuilder.Append(_value1);
            staticBuilder.Append(_value2);
            staticBuilder.Append(_value3);
            staticBuilder.Append(_value4);
            staticBuilder.Append(_value5);
            staticBuilder.Append(_value6);
            staticBuilder.Append(_value7);
            staticBuilder.Append(_value8);
            staticBuilder.Append(_value9);
            staticBuilder.Append(_value10);

            string _resultStr = staticBuilder.ToString();

            staticBuilder.Clear();

            return _resultStr;
        }

        public static string Append(string _value1, int _value2)
        {
            return Append(_value1, _value2.ToString());
        }

        public static string Append(int _value1, int _value2)
        {
            return Append(_value1.ToString(), _value2.ToString());
        }
        public static string Append(int _value1, string _value2, int _value3)
        {
            return Append(_value1.ToString(), _value2, _value3.ToString());
        }

        public static string Append(string _value1, string _value2, string _value3, int _value4, string _value5, string _value6)
        {
            return Append(_value1, _value2, _value3, _value4.ToString(), _value5, _value6);
        }
        public static string Append(string _value1, string _value2, string _value3, string _value4, int _value5, string _value6, bool _isComma = true)
        {
            string _value5Str = _isComma == true ? _value5.ToString() : _value5.ToString();
            return Append(_value1, _value2, _value3, _value4, _value5Str, _value6);
        }

        // public static string GetTensionTime_Func(float _remainTime)
        // {
        //     return 1f <= _remainTime
        //                 ? ((int)_remainTime).ToString()
        //                 : _remainTime.ToString(1);
        // }

        // public static string GetPath_Func(string _separatorStr, params string[] _strArr)
        // {
        //     return GetPath_Func(_separatorStr, 0, _strArr: _strArr);
        // }
        // public static string GetPath_Func(string _separatorStr, int _minPath, params string[] _strArr)
        // {
        //     if (_strArr == null || _strArr.Length <= 0)
        //         return default;

        //     string _fullStr = _strArr[0];
        //     int _pathCnt = _minPath < _strArr.Length ? _strArr.Length : _minPath;

        //     for (int i = 1; i < _pathCnt; i++)
        //     {
        //         if(_strArr.TryGetItem_Func(i, out string _str) == true)
        //         {
        //             _fullStr = SBuilder.Append(_fullStr, _separatorStr, _str);
        //         }
        //         else
        //         {
        //             _fullStr = SBuilder.Append(_fullStr, _separatorStr);
        //         }
        //     }

        //     return _fullStr;
        // }
    } 
}