namespace Artnix.Smpp.Client.Responses
{
    public interface IAnResponse
    {
        AnMessage message { get; }
        string SubmitSmRespErrorData { get; }
        StatusCode StatusCode { get; }
        bool IsSuccessful { get; }
    }
}