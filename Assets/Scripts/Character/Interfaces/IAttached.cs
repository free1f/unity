using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Character.Interfaces
{
    public interface IAttached<T>
    {
        void Attached(ref T value);
    }
}