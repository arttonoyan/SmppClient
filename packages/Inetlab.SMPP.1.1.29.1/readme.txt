Inetlab.SMPP Client/Server Library

EVALUATION VERSION. Trial Period 30 days.

For production use you need to purchase a license at http://www.inetlab.com/Purchase/default.aspx

Documentation (work in progress, contributions appreciated):

Please see http://wiki.inetlab.com/ for more information on using Inetlab.SMPP library

Getting Started: 
http://wiki.inetlab.com/doku.php/smpp/client_getting_started

Sample Application: 
http://www.inetlab.com/Downloading/Inetlab.SMPP.zip


HOW TO INSTALL LICENSE FILE
====================
After purchase you will receive Inetlab.SMPP.license file per E-Mail. Add this file to the project where you have a reference on Inetlab.SMPP.dll.
Change "Build Action" of the file to "Embedded Resource". 

Another way to activate the library is to set License property for SmppClient or SmppServer instances.
_client.License = this.GetType().Assembly.GetManifestResourceStream(this.GetType(), "Inetlab.SMPP.license" );




Quick Sample
=================

SmppClient client = new SmppClient ();

client.Connect( "127.0.0.1", 7777);

if (client.Status == Inetlab.SMPP.Common. ConnectionStatus.Open)
{
      client.Bind( "username", "password" , Inetlab.SMPP.Common.ConnectionMode .Transmitter);

      if (client.Status == Inetlab.SMPP.Common. ConnectionStatus.Bound)
      {
            IList< SubmitSmResp> respList = client.Submit(
                SMS.ForSubmit().From( "8888").To("7917123456" ).Text("Test SMS")
                );

            if (respList.Count > 0 && respList[0].Status == Inetlab.SMPP.Common.CommandStatus .ESME_ROK)
            {
                 Console.WriteLine( "SMS has been sent");
                 foreach ( SubmitSmResp resp in respList)
                 {
                      Console.WriteLine( "MessageId: " + resp.MessageId);
                 }
            }

            client.UnBind();
      }

      client.Disconnect();
}



