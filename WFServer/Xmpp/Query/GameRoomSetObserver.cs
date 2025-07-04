﻿using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.Missions;
using WFServer.Xmpp;
using System;
using System.Linq;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class GameRoomSetObserver
    {
        //<gameroom_set_observer target_id="5" is_observer="1" />

        [Query(IqType.Get, "gameroom_set_observer")]
        public static void GameRoomUpdatePvP(Client client, Iq iq)
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

            var target = rCore.Players.FirstOrDefault(x => x.ProfileId == ulong.Parse(iq.Query.GetAttribute("target_id")));
            if (target != null)
                target.Profile.RoomPlayer.Observer = Convert.ToBoolean(int.Parse(iq.Query.GetAttribute("is_observer")));

            iq.SetQuery(Xml.Element("gameroom_set_observer").Child(room.Serialize().Child(rCore.Serialize())));
            client.QueryResult(iq);
        }
    }
}
