namespace Artnix.Smpp.Client.Binds
{
    public class AnBind : IAnBind
    {
        public virtual string Tel { get; set; }

        public virtual string SystemId { get; set; }
        public virtual string Password { get; set; }
        public virtual string CallBackPhone { get; set; }

        public virtual string ServiceType => "0";
    }
}