using System;

namespace Freelf.Item.Interfaces
{
    public interface IUse
    {
        bool IsInUse { get; }
        void Use(Action onSuccess = null);
        int StaminaCost { get; }
    }
}