using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GameFrameworks
{
    public enum AnimType { NONE, SCALE_PUNCH,  }

    public class UI_Button : Button, IBaseUI
    {

        public AnimType animType = AnimType.NONE;
    }
}