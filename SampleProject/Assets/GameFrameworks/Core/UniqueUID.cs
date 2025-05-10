using System;
using UnityEngine;

namespace GameFrameworks
{
    [Serializable]
    public class UniqueUID {
        public const string FieldName = "uid";
        [HideInInspector, SerializeField] protected string uid;
        public string UID => uid;
    }
}