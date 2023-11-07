using System.Collections.Generic;
using UnityEngine;

namespace GameFrameworks {
    public enum PopupState {
        Popup_Network
    }

    public class PopupManager : MonoSingleton<PopupManager>
    {
        [SerializeField] private Transform _popups;
    
        private Dictionary<PopupState, UI_Popup> _popUpDic;

        protected override void Awake()
        {
            base.Awake();
            
            _popUpDic = new Dictionary<PopupState, UI_Popup>();
        }

        // 외부에서 PopupManager.ShowPopup(PopupState.Network);
        public static UI_Popup ShowPopup(PopupState state)
        {
            return _inst.Show(state);
        }
        
        private UI_Popup Show(PopupState state)
        {
            if(!_popUpDic.ContainsKey(state))
                CreatePopup(state);
        
            _popUpDic[state].ShowUI();
            
            return _popUpDic[state];
        }
        
        private void CreatePopup(PopupState state)
        {
            UI_Popup Popup = (Instantiate(Resources.Load(state.ToString(), typeof(GameObject)), _popups) as GameObject).
                GetComponent<UI_Popup>();

            Popup.SetPopup(state);
            Popup.transform.SetParent(_popups, true);

            _popUpDic.Add(state, Popup);
        }

        public void Release_Resource()
        {
            foreach(var pair in _popUpDic)
            {
                Destroy(pair.Value.gameObject);   
            }
            
            _popUpDic.Clear();
        }
        
        // 맨 앞 팝업
        public UI_Popup GetFrontPopup()
        {
            for (int i = _popups.childCount - 1; i >= 0; --i)
            {
                Transform child = _popups.GetChild(i);

                if (child.gameObject.activeSelf)
                    return child.GetComponent<UI_Popup>();
            }
            
            return null;
        }
    }
}
