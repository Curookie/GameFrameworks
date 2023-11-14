using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameFrameworks
{
	public class UI_Popup : MonoBehaviour
	{
		[NonSerialized] public PopupState _PopupState;
		protected Action _callBack;

		public void SetPopup(PopupState popUpState) => _PopupState = popUpState;
		
		public virtual void ShowUI()
		{
			transform.SetAsLastSibling();

			gameObject.SetActive(true);
		}

		public virtual void HideUI()
		{
			gameObject.SetActive(false);

			if (null != _callBack)
			{
				Action temp = _callBack;
				_callBack = null;

				temp.Invoke();
			}
		}

		protected virtual void Awake()
		{
		}

		// 팝업이 Off 될 때 CallBack 등록
		public UI_Popup Add_CallBack(Action callBack)
		{
			if (null == _callBack) _callBack = callBack;
			else _callBack += callBack;
			return this;
		}

		// Android '뒤로가기' 키 눌렀을 때
		protected bool _closeByBackKey = true;

		public bool Is_CloseByEsc()
		{
			return _closeByBackKey;
		}
	}
}
