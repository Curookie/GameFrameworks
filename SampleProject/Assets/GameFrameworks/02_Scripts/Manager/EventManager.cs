using System.Collections.Generic;
using UnityEngine;

namespace GameFrameworks {
    public static partial class EventManager
    {
        public delegate void EventDelegate(params object[] parameters_);

        private static Dictionary<EventType, EventDelegate> _dicEvents;
        private static Dictionary<EventType, EventDelegate> dicEvents 
        {
            get {
                if(_dicEvents == null) Init();
                return _dicEvents;
            }
            set => _dicEvents = value;
        }

        public static void Init() {
            dicEvents = new Dictionary<EventType, EventDelegate>();
        }

        public static void Add_Event(EventType type_, EventDelegate callBack_) {
            if(dicEvents.ContainsKey(type_)) {
                if(dicEvents[type_].Equals(callBack_)) {
                    Debug.LogWarning($"{type_} Event's Delegate Duplicated");

                    dicEvents[type_] += callBack_;
                } else {
                    dicEvents[type_] += callBack_;
                }
            } else {
                dicEvents.Add(type_, callBack_);
            }
        }

        public static void Remove_Event(EventType type_, EventDelegate callBack_) {
            if(dicEvents.ContainsKey(type_)) {
                if(dicEvents[type_].GetInvocationList().Length == 1 && dicEvents[type_].Equals(callBack_)) {
                    dicEvents.Remove(type_);
                } else {
                    dicEvents[type_] -= callBack_;
                }
            }
        }

        public static void Invoke_Event(EventType type_, params object[] value_) {
            switch(type_) 
            {
                case EventType.NONE:
                {
                    Debug.Log($"EVENT : {type_}");
                    break;
                }
                default:
                {
                    break;
                }
            }

            if(dicEvents.ContainsKey(type_)) {
                dicEvents[type_].Invoke(value_);
            }
        }
    }
}
