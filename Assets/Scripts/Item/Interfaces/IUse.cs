namespace Freelf.Item.Interfaces
{
    public interface IUse
    {
        bool IsInUse { get; }
        void Use();
    }
}