using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Xmpp;
using System;

namespace WFServer.Xmpp.Query
{
    public static class SetPlayerStatus
    {
        [Query(IqType.Get, "player_status")]
        public static void PlayerStatusSerializer(Client client, Iq iq)
        {
            PlayerStatus prev_status = (PlayerStatus)int.Parse(iq.Query.GetAttribute("prev_status"));
            PlayerStatus new_status = (PlayerStatus)int.Parse(iq.Query.GetAttribute("new_status"));

            client.Presence = new_status;

            if (client.Profile != null)
            {
                var room = client.Profile.RoomPlayer?.Room;
                if (room != null)
                {
                    //TODO для быстрой игры
#if !DEBUG
                    if (new_status.HasFlag(PlayerStatus.Away) && !new_status.HasFlag(PlayerStatus.InGame) /* && (room.Type == RoomType.PvP_Public || room.Type == RoomType.PvP_Autostart || room.Type == RoomType.PvP_Rating)*/)
                        //&& room.Master.Client != client)
                    {
                        room.KickPlayer(client, RoomPlayerRemoveReason.KickTimeout);
                    }
#endif
                    client.Profile.Room?.GetExtension<GameRoomCore>()?.Update();
                }
            }


            iq.SetQuery(null);
            client.QueryResult(iq);
        }
    }
}
