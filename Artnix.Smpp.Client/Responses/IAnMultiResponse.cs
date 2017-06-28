using System.Collections.Generic;

namespace Artnix.Smpp.Client.Responses
{
    public interface IAnMultiResponse
    {
        IList<IAnResponse> SuccessResponses { get; }
        IAnResponse ErrorResponse { get; }
        StatusCode StatusCode { get; }
    }
}