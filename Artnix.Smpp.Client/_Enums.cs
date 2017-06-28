namespace Artnix.Smpp.Client
{
    public enum Language : byte
    {
        /// <summary>Default language.</summary>
        Latin = 0,
        Armenian = 1
    }

    /// <summary>ESME is External Short Messaging Entity</summary>
    public enum StatusCode
    {
        /// <summary>
        /// No Error
        /// </summary>
        OK = 0,
        /// <summary>
        /// System Error.
        /// </summary>
        Esme_SystemError = 8,
        /// <summary>
        /// SMPP Client No Connection.
        /// </summary>
        SmppClient_NoConnection = 4099,
        /// <summary>
        /// SMPP Client Unbound.
        /// </summary>
        SmppClient_UnBound = 4100,
        /// <summary>
        /// SMPP Client Unknown Error
        /// </summary>
        SmppClient_UnknownError = 8191,

        ArtnixClient_SuccessAll = 12000,
        ArtnixClient_SuccessPart = 12100,
        ArtnixClient_UnSuccess = 12200,
    }
}
