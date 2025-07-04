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
    public static class ClanInfoSync
    {
        [Query(IqType.Get, "clan_info_sync")]
        public static void ClanInfoSyncSerializer(Client client, Iq iq)
        {
            if (client.Profile == null)
                throw new InvalidOperationException();

            Clan.ClanInfo(client.Profile.ClanId);
        }
    }
}
