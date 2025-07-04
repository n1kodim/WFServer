using WFServer.Core;
using WFServer.Game.Clans;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.Missions;
using WFServer.Xmpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class NotifyExpiredItems
    {
        [Query(IqType.Get, "notify_expired_items")]
        public static void NotifyExpiredItemsSerializer(Client client, Iq iq)
        {
            if (client.Profile == null)
                throw new InvalidOperationException();

            //TODO
        }
    }
}
