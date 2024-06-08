using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Item;
using UnityEngine;

namespace Freelf.Character.DataTransfer
{
  public class DamageData
  {
    // event is a read-only to invoke the event from outside
    public event Action<int> OnDamage;

    public void Damage(int value)
    {
      OnDamage?.Invoke(value);
    }
  }
}