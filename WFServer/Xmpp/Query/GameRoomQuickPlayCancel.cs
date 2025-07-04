using WFServer.Core;
using WFServer.Game.Enums;

namespace WFServer.Xmpp.Query
{
    public static class GameRoomQuickPlayCancel
    {
        [Query(IqType.Get, "gameroom_quickplay_cancel")]
        public static void GameRoomQuickPlayCancelSerializer(Client client, Iq iq)
        {
            client.Profile?.Room?.LeftPlayer(client, RoomPlayerRemoveReason.Left);
        }
    }
}
