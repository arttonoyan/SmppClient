namespace Artnix.Smpp.Client.Binds
{
    public interface IAnBind
    {
        string Tel { get; }
        string SystemId { get; }
        string Password { get; }
        string CallBackPhone { get; }
        string ServiceType { get; }
    }
}