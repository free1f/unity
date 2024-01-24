using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// Presenter
[CreateAssetMenu(fileName = "HealthStat", menuName = "freelf/Stats/HealthStat")]
public class HealthStat : BaseStat
{
    public event Action<int> OnChanged;
    public void Change(int value)
    {
        OnChanged?.Invoke(Math.Clamp(value, MinValue, MaxValue));
    }
}
