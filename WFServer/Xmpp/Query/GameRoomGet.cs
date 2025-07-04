﻿using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Xmpp;
using System;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class GameRoomGet
    {
        [Query(IqType.Get, "gameroom_get")]
        public static void GameRoomGetSerializer(Client client, Iq iq)
        {
            //left="0" token="8748"
            XmlElement gameroom_get = Xml.Element("gameroom_get")
                .Attr("left", "0")
                .Attr("token", "0");

            lock (client.Channel.Rooms)
            {
                foreach (var room in client.Channel.Rooms)
                {
                    if (room.Disposed)
                        continue;

                    if (/*room.GetExtension<GameRoomCustomParams>().GetCurrentRestriction("join_in_the_process") == "0" || */room.Type != RoomType.PvP_Public && room.Type != RoomType.PvP_ClanWar /*&& room.Type != RoomType.PvP_Autostart*/)
                        continue;

                    gameroom_get.Child(room.Serialize(true));
                }
            }

            iq.SetQuery(gameroom_get);
            client.QueryResult(iq);
        }
    }
}
