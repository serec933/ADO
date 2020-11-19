using System;

namespace ADO
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectedMode.Connected();
            //ConnectedMode.ConnctedPar();
            //ConnectedMode.ConnectedSTP();
            //ConnectedMode.ConnectedScalar();
            DisconettedMode.Disconnected();
            ConnectedMode.Connected();
        }
    }
}
