using WFServer.Core;
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
    public static class UiUserChoice
    {
        /*
         * <ui_user_choice>
<choice choice_from="lobby_pvp_game_room" choice_id="join_quickplay_session" choice_result="1" />
</ui_user_choice>
         */

        [Query(IqType.Get, "ui_user_choice")]
        public static void UiUserChoiceSerializer(Client client, Iq iq)
        {
            //TODO
        }
    }
}
