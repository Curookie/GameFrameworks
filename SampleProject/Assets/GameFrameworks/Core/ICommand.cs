using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFrameworks 
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Use();
    }
}