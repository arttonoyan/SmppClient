using Artnix.Smpp.Client.Responses;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;

namespace Artnix.Smpp.Client.Builders
{
    interface IAnResponseBulder
    {
        IAnResponseBulder Message(AnMessage message);
        IAnResponseBulder StatusCode(StatusCode statusCode);
        IAnResponseBulder CommandStatus(CommandStatus commandStatus);
        IAnResponseBulder SubmitSmResp(SubmitSmResp submitSmResp);
        IAnResponseBulder BindResp(BindResp bindResp);
        IAnResponse ToAnResponse();
    }
}