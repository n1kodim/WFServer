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
    public static class GameRoomSetPrivateStatus
    {
        //<gameroom_setprivatestatus private='1'/>

        [Query(IqType.Get, "gameroom_setprivatestatus")]
        public static void GameRoomSetInfo(Client client, Iq iq)
        {
            if (client.Profile == null || client.Profile.Room == null)
                throw new QueryException(1);

            var q = iq.Query;

            var roomPlayer = client.Profile.RoomPlayer;
            var room = client.Profile.RoomPlayer.Room;

            var rCore = room.GetExtension<GameRoomCore>();
            var rMaster = room.GetExtension<GameRoomMaster>();
            //var rSession = room.GetExtension<GameRoomSession>();

            if (rCore == null || rMaster == null || rMaster.Client != client)
                throw new QueryException(1);

            if (!room.Type.ToString().Contains("PvE"))
                throw new QueryException(1);

            rCore.Private = q.GetAttribute("private") == "1" ? true : false;

            iq.SetQuery(Xml.Element("gameroom_setprivatestatus").Child(room.Serialize(false).Child(rCore.Serialize())));
            client.QueryResult(iq);
        }
    }
}
