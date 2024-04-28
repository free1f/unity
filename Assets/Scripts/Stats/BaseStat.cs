using System.Collections;
using System.Collections.Generic;
using Freelf.Patterns;
using UnityEngine;

namespace Freelf.Stats
{
    public abstract class BaseStat : ScriptableObject
    {
        public int MinValue;
        public int MaxValue;
        public Observer<int> CurrentValue;
    }
}