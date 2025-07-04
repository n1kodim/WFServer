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
    public static class PlayerLeaveBan
    {
        /*
<player_leave_ban>
<profile profile_id="19" game_room_type="pvp_public" game_mode="tdm" />
</player_leave_ban>
         */

        [Query(IqType.Get, "player_leave_ban")]
        public static void PlayerLeaveBanSerializer(Client client, Iq iq)
        {
            if (!client.IsDedicated)
                throw new InvalidOperationException();

            var q = iq.Query;
            //TODO
        }
    }
}

