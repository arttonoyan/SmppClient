using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artnix.Smpp.Client
{
    public interface IAnSmppClient : IDisposable
    {
        List<AnSubmitMultiResp> SendMessages(AnMultiMessage multiMessage);
        Task<List<AnSubmitMultiResp>> SendMessagesAsync(AnMultiMessage multiMessage);
    }
}