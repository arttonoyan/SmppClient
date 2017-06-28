using Inetlab.SMPP.Common;
using System;

namespace Artnix.Smpp.Client
{
    internal static class Converter
    {
        public static StatusCode ToStatusCode(CommandStatus commandStatus)
        {
            StatusCode code;
            if (!Enum.TryParse(commandStatus.ToString(), out code))
            {
                int csindex = (int)commandStatus;
                code = csindex > 4000 ? StatusCode.SmppClient_UnknownError : StatusCode.Esme_SystemError;
            }
            return code;
        }
    }
}