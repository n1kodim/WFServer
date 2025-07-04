﻿using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.Items;
using WFServer.Xmpp;
using System;
using System.Linq;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class GameRoomKick
    {
        [Query(IqType.Get, "gameroom_kick")]
        public static void GameRoomKickSerializer(Client client, Iq iq)
        {
            //TODO прислать пакет когда не в комнате
            if (client.Profile == null || client.Profile.RoomPlayer?.Room == null)
                throw new QueryException(1);

            var q = iq.Query;

            var roomPlayer = client.Profile.RoomPlayer;
            var room = client.Profile.Room;

            if (room == null)
                throw new QueryException(2);

            var rCore = room.GetExtension<GameRoomCore>();
            var rMaster = room.GetExtension<GameRoomMaster>();

            if (rCore == null || rMaster == null || rMaster.Client != client)
                throw new QueryException(1);

            lock (rCore.Players)
            {
                var target = rCore.Players.FirstOrDefault(x => x.ProfileId == ulong.Parse(iq.Query.GetAttribute("target_id")));
                if (target != null)
                    room.KickPlayer(target, RoomPlayerRemoveReason.KickMaster);
            }

            iq.SetQuery(Xml.Element("gameroom_kick"));
            client.QueryResult(iq);
        }
    }
}
