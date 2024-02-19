using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Stats
{
    public abstract class BaseStat : ScriptableObject
    {
        public int MinValue;
        public int MaxValue;
    }
}