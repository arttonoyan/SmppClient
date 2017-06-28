using Inetlab.SMPP.PDU;
using System.Collections.Generic;

namespace Artnix.Smpp.Client.Responses
{
    sealed class AnMultiResponse : IAnMultiResponse
    {
        public IList<IAnResponse> SuccessResponses { get; private set; }
        public IAnResponse ErrorResponse { get; private set; }
        public StatusCode StatusCode
        {
            get
            {
                if (ErrorResponse == null)
                    return StatusCode.ArtnixClient_SuccessAll;

                if (SuccessResponses == null || SuccessResponses.Count == 0)
                    return StatusCode.ArtnixClient_UnSuccess;

                return StatusCode.ArtnixClient_SuccessPart;
            }
        }

        internal bool HasError
        {
            get { return ErrorResponse != null && !ErrorResponse.IsSuccessful; }
        }

        public void Add(StatusCode status)
        {
            var res = AnResponse.Bulder().StatusCode(status).ToAnResponse();
            if (status == StatusCode.OK)
                SuccessResponses.Add(res);
            else
                ErrorResponse = res;
        }

        public void Add(BindResp bindResp)
        {
            var res = AnResponse.Bulder().BindResp(bindResp).ToAnResponse();
            if (res.IsSuccessful)
                SuccessResponses.Add(res);
            else
                ErrorResponse = res;
        }

        public void Add(AnMessage message, SubmitSmResp smResp)
        {
            var builder = AnResponse.Bulder().Message(message);

            if(smResp != null)
            {
                builder.SubmitSmResp(smResp);
                var res = builder.ToAnResponse();
                if(res.IsSuccessful)
                    SuccessResponses.Add(res);
                else
                    ErrorResponse = res;
            }
            else
            {
                var res = builder.StatusCode(StatusCode.SmppClient_UnknownError).ToAnResponse();
                ErrorResponse = res;
            }
        }

        public void Add(AnMessage message, IList<SubmitSmResp> items)
        {
            if (items != null && items.Count > 1)
                Add(message, items[0]);
        }
    }
}