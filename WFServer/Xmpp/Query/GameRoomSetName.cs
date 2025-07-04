using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.Items;
using WFServer.Xmpp;
using System;
using System.Linq;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class GameRoomSetName
    {
        [Query(IqType.Get, "gameroom_setname")]
        public static void GameRoomSetNameSerializer(Client client, Iq iq)
        {
            if (client.Profile == null || client.Profile.Room == null)
                throw new QueryException(1);

            var q = iq.Query;

            var roomPlayer = client.Profile.RoomPlayer;
            var room = client.Profile.RoomPlayer.Room;

            var rCore = room.GetExtension<GameRoomCore>();
            var rMaster = room.GetExtension<GameRoomMaster>();

            if (rCore == null || rMaster == null || rMaster.Client != client)
                throw new QueryException(1);

            room.SetRoomName(q.GetAttribute("room_name"));

            iq.Query.Child(room.Serialize().Child(rCore.Serialize()));
            client.QueryResult(iq);
        }
    }
}
