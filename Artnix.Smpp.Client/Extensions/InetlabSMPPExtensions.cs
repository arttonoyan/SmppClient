using Inetlab.SMPP.PDU;
using Artnix.Smpp.Client.Binds;

namespace Artnix.Smpp.Client.Extensions
{
    public static class InetlabSMPPExtensions
    {
        public static Bind NewBind(this IAnBind bind)
        {
            return new Bind
            {
                SystemId = bind.SystemId,
                Password = bind.Password
            };
        }
    }
}