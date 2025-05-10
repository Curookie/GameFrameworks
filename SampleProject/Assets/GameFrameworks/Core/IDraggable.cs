using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFrameworks
{
    public interface IDraggable {
        GameObject g_DraggingObject { get; }
        Vector2? beforeDragPos { get; }

        void OnDragBegin(Vector2 screenPos_);
        void OnDragging(Vector2 screenPos_);
        void OnDragDone(Vector2 screenPos_);
        void OnDragCancel();
    }
}