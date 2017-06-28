using System;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;
using Newtonsoft.Json;
using Artnix.Smpp.Client.Builders;

namespace Artnix.Smpp.Client.Responses
{
    sealed class AnResponse : IAnResponse
    {
        private AnResponse() { }

        public static IAnResponseBulder Bulder()
        {
            return new AnResponseBulder();
        }

        public AnMessage message { get; private set; }

        private StatusCode statusCode;
        public StatusCode StatusCode { get; private set; }

        public string SubmitSmRespErrorData { get; private set; }

        public bool IsSuccessful => statusCode == Client.StatusCode.OK;

        private sealed class AnResponseBulder : IAnResponseBulder
        {
            public AnResponseBulder()
            {
                response = new AnResponse();
            }

            private readonly AnResponse response;

            public IAnResponseBulder Message(AnMessage message)
            {
                response.message = message;
                return this;
            }

            public IAnResponseBulder StatusCode(StatusCode statusCode)
            {
                response.statusCode = statusCode;
                return this;
            }

            public IAnResponseBulder CommandStatus(CommandStatus commandStatus)
            {
                response.statusCode = Converter.ToStatusCode(commandStatus);
                return this;
            }

            public IAnResponseBulder SubmitSmResp(SubmitSmResp submitSmResp)
            {
                try
                {
                    response.statusCode = Converter.ToStatusCode(submitSmResp.Status);
                    if (!response.IsSuccessful)
                        response.SubmitSmRespErrorData = JsonConvert.SerializeObject(submitSmResp);
                }
                catch (Exception ex)
                {
                }
                return this;
            }

            public IAnResponseBulder BindResp(BindResp bindResp)
            {
                try
                {
                    response.statusCode = Converter.ToStatusCode(bindResp.Status);
                    if (!response.IsSuccessful)
                        response.SubmitSmRespErrorData = JsonConvert.SerializeObject(bindResp);
                }
                catch (Exception ex)
                {

                }
                return this;
            }

            public IAnResponse ToAnResponse()
            {
                return response;
            }
        }
    }
}