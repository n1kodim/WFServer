using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.Missions;
using WFServer.Xmpp;
using System;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class ClassPresence
    {
        [Query(IqType.Get, "class_presence")]
        public static void ClassPresenceSerializer(Client client, Iq iq)
        {
            if (!client.IsDedicated)
                throw new InvalidOperationException();

            var q = iq.Query;

            var rCore = client.Dedicated.Room?.GetExtension<GameRoomCore>();

            //TODO
            //<class_presence session_id="4">
            //<profile id="5" total_playtime="147.47496">
            //<presence class_id="3" value="84" />
            //</profile>
            //</class_presence>

            iq.SetQuery(Xml.Element("class_presence"));
            client.QueryResult(iq);
        }
    }
}
