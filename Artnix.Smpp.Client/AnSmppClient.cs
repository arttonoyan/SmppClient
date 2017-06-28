using Artnix.Smpp.Client.Binds;
using Artnix.Smpp.Client.Responses;
using Inetlab.SMPP;
using Inetlab.SMPP.Builders;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;
using System;
using System.Collections.Generic;
using Artnix.Smpp.Client.Extensions;

namespace Artnix.Smpp.Client
{
    public class AnSmppClient : IDisposable
    {
        public AnSmppClient(string hostName, int port, IAnBind anBind)
        {
            this.hostName = hostName;
            this.port = port;
            this.anBind = anBind;
            client = new SmppClient();
        }

        public readonly string hostName;
        public readonly int port;

        private readonly SmppClient client;
        private IAnBind anBind;

        private bool disposed;

        public IAnMultiResponse SendMessages(AnMultiMessage multiMessage)
        {
            if (anBind == null)
                throw new Exception("No Connection.");


            var multiResponse = new AnMultiResponse();
            client.Connect(hostName, port);
            if (client.Status == ConnectionStatus.Open)
            {
                var pdu = anBind.NewBind();
                BindResp bindResponce = client.Bind(pdu.SystemId, pdu.Password, ConnectionMode.Transceiver);
                if (client.Status == ConnectionStatus.Bound)
                {
                    var builder = ForSubmit(multiMessage);

                    IList<SubmitSmResp> clientResponses = client.Submit(builder);
                    multiResponse.Add(multiMessage, clientResponses);

                    foreach (string phone in multiMessage.GetAdministrators())
                    {
                        builder.To(phone);
                        clientResponses = client.Submit(builder);
                        multiResponse.Add(multiMessage, clientResponses);
                    }

                    foreach (AnMessage message in multiMessage)
                    {
                        clientResponses = client.Submit(ForSubmit(message));
                        multiResponse.Add(message, clientResponses);
                    }

                    return multiResponse;
                }
                multiResponse.Add(bindResponce);
            }
            else
            {
                multiResponse.Add(StatusCode.SmppClient_NoConnection);
            }

            return multiResponse;
        }

        private ISubmitSmBuilder ForSubmit(AnMessage message)
        {
            return SMS.ForSubmit()
                            .ServiceType(anBind.ServiceType)
                            .Text(message.text)
                            .From(anBind.Tel)
                            .To(message.phoneNumber)
                            .Coding(message.DataCoding)
                            .DeliveryReceipt();
        }

        //public Task<List<AnMultiResp>> SendMessagesAsync(AnMultiMessage message)
        //{
        //    return Task.Factory.StartNew(() => SendMessages(message));
        //}

        public void Dispose()
        {
            OnDispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                anBind = null;
                client.Dispose();
            }
            disposed = true;
        }
    }
}