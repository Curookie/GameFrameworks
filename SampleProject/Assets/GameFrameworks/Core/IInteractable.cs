using UnityEngine;

namespace GameFrameworks
{
    public interface IInteractable {
        void Interact();

        bool IsInteractable { get; }

        bool IsShowKeyUI { get; }

        GameObject g_KeyUI { get; }

        void Focus();

        void UnFocus();
    }
}