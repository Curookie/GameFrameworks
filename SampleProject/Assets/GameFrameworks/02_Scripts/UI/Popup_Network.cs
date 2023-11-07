using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameFrameworks
{
    public class Popup_Network : UI_Popup
    {
        protected override void Awake()
        {
            base.Awake();

            _closeByBackKey = false;
        }
    }
}
