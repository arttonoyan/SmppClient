using Artnix.Smpp.Client.Binds;

namespace Artnix.Smpp.Client.ConsoleTestApp
{
    class MyBind : AnBind
    {
        public override string SystemId => "login";

        public override string Password => "My Password";

        public override string Tel => "My Serve";

        public override string CallBackPhone => "37493555555";
    }
}
