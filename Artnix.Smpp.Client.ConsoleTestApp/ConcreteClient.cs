using Artnix.Smpp.Client.Binds;

namespace Artnix.Smpp.Client.ConsoleTestApp
{
    class ConcreteClient : AnSmppClient
    {
        public ConcreteClient(IAnBind bind)
            : base("1.1.100.10", 2775, bind)
        { }
    }
}