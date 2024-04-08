using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Character.DataTransfer
{
    public struct PressedInput
    {
        public bool IsHold;
        public bool IsPressed;
        public bool IsReleased;
    }

    public struct AxisInput
    {
        public float Horizontal;
        public float Vertical;
    }
}