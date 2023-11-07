using UnityEngine;

namespace GameFrameworks
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
#region VARIABLES & PROPERTIES
    protected static T _inst;
    protected static bool _alive = true;

    public static T Inst {
        get {
            // Check App Exit or Destroy
            if (!_alive) {
                Debug.LogWarning (typeof (T) + "' is already destroyed on application quit.");
                return null;
            }

            return _inst ??= FindObjectOfType<T> ();
        }
    }
#endregion

#region UNITY_EVENTS
    ///<summary>USED :: protected override void Awake() { base.Awake(); ~~~ }</summary>
    virtual protected void Awake() {
        if (_inst == null) {
            _inst = this as T;
            DontDestroyOnLoad (this.gameObject);
        } else if (_inst != this) {
            Destroy (this.gameObject);
            return;
        }
    }

    protected void OnApplicationExit () {
        _alive = false;
    }
#endregion

#region MAIN_FUNCTIONS
#endregion

#region SUB_FUNCTIONS
#endregion

#region UI_FUNCTIONS
#endregion

#region COROUTINE_FUNCTIONS
#endregion
    }
}