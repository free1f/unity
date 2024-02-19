namespace Freelf.Item.Interfaces
{
    public interface IPickup
    {
        bool IsPickedUp { get; }
        void Pickup();
    }
}