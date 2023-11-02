using System;
using System.Diagnostics;
using UnityEngine;

namespace GameFrameworks
{
    public static partial class DBug
    {
        private const string COLON = " : ";

        [SerializeField] private static IDebug iDebug;

        public static void Init(IDebug iDebug_) => DBug.iDebug = iDebug_;

        public static bool HasLogType(PrintLogType _logType) {
            if (DBug.iDebug == null)
            {
#if UNITY_EDITOR
                if (Application.isPlaying == false)
                    return true;
#endif

                return false;
            }
            else
            {
                return DBug.iDebug.HasLogType(_logType);
            }
        }


        [Conditional("DEBUG_MODE")]
        public static void Log(object _obj, PrintLogType _logType = PrintLogType.Common) {
            Log(_obj.ToString(), _logType);
        }

        [Conditional("DEBUG_MODE")]
        public static void Log(string _str, PrintLogType _logType = PrintLogType.Common)
        {
#if UNITY_EDITOR
            if (HasLogType(_logType) == true) {
                _Log();
            }
#else
            _Log();
#endif

            void _Log()
            {
                _str = SBuilder.Append_Func(_logType.ToString(), COLON, _str);
                Debug.Log(_str);
            }
        }

        [Conditional("DEBUG_MODE")]
        public static void Warning(object _obj, PrintLogType _logType = PrintLogType.Common)
        {
            Warning(_obj.ToString(), _logType);
        }
        [Conditional("DEBUG_MODE")]
        public static void Warning(string _str, PrintLogType _logType = PrintLogType.Common)
        {
#if UNITY_EDITOR
            if (HasLogType(_logType) == true)
                _Log();
#else
            _Log();
#endif

            void _Log()
            {
                _str = SBuilder.Append_Func(_logType.ToString(), COLON, _str);
                Debug.LogWarning(_str);
            }
        }

        [Conditional("DEBUG_MODE")]
        public static void Error(object _obj, PrintLogType _logType = PrintLogType.Common)
        {
            Error(_obj.ToString(), _logType);
        }
        [Conditional("DEBUG_MODE")]
        public static void Error(string _str, PrintLogType _logType = PrintLogType.Common)
        {
#if UNITY_EDITOR
            if (HasLogType(_logType) == true)
                _Log();
#else
            _Log();
#endif

            void _Log()
            {
                _str = SBuilder.Append_Func(_logType.ToString(), COLON, _str);
                Debug.LogError(_str);
            }
        }

        public interface IDebug
        {
            bool HasLogType(PrintLogType _logType = PrintLogType.Common);
        }
    }
}