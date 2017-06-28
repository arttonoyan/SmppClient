using System;
using Artnix.Smpp.Client.Responses;

namespace Artnix.Smpp.Client.ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var multiMessage = new AnMultiMessage("37493112233", "Հայերեն հաղորդագրություն:")
            {
                new AnMessage("37455111111", "Հայերեն հաղորդագրություն 1:"),
                new AnMessage("37455222222", "English message.")
            };

            var client = new ConcreteClient(new MyBind());

            IAnMultiResponse response = client.SendMessages(multiMessage);
            if (response.StatusCode != StatusCode.ArtnixClient_SuccessAll)
            {
                if (response.ErrorResponse != null)
                {
                    // Check...
                    string errorDataJson = response.ErrorResponse.SubmitSmRespErrorData;
                }
            }

            client.Dispose();

            Console.ReadLine();
        }
    }
}
