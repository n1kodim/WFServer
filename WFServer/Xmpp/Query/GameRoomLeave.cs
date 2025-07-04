using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.Items;
using WFServer.Xmpp;
using System;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class GameRoomLeave
    {
        [Query(IqType.Get, "gameroom_leave")]
        public static void GameRoomLeaveSerializer(Client client, Iq iq)
        {
            if (client.Profile == null)
                throw new InvalidOperationException();

            if (client.Profile.RoomPlayer == null)
                return;

            client.Profile.Room?.LeftPlayer(client);

            iq.SetQuery(Xml.Element("gameroom_leave"));
            client.QueryResult(iq);
        }
    }
}
