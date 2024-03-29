using UnityEngine;
using System;

// Presenter
[CreateAssetMenu(fileName = "VitalityStat", menuName = "freelf/Stats/VitalityStat")]
public class VitalityStat : BaseStat
{
    public event Action<int> OnChanged;
    public void Change(int value)
    {
        OnChanged?.Invoke(Math.Clamp(value, MinValue, MaxValue));
    }
}
