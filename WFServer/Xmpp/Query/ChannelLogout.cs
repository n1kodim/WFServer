using WFServer.Core;
using WFServer.Xmpp;
using System;

namespace WFServer.Xmpp.Query
{
    public static class ChannelLogout
    {
        [Query(IqType.Get, "channel_logout")]
        public static void ChannelLogoutSerializer(Client client, Iq iq)
        {
            //TODO
            client.IqResult(iq);
        }
    }
}
