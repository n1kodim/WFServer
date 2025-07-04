using WFServer.Core;
using WFServer.Xmpp;
using System;
using System.Collections.Generic;
using System.Text;

namespace WFServer.Xmpp.Query
{
    public static class PlayerReconnectUnban
    {
        [Query(IqType.Get, "player_reconnect_unban")]
        public static void PlayerReconnectUnbanSerializer(Client client, Iq iq)
        {
            //TODO

        }
    }
}
