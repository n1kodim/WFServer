using WFServer.Core;
using WFServer.Xmpp;
using System;

namespace WFServer.Xmpp.Query
{
    public static class Example
    {
        [Query(IqType.Get, "example")]
        public static void ExampleSerializer(Client client, Iq iq)
        {

        }
    }
}
