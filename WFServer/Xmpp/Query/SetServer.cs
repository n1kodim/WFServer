﻿using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.GameRoomVotes;
using WFServer.Game.Missions;
using WFServer.Xmpp;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class SetServer
    {
        //<setserver server=""
        //host="n1kodim"
        //port="64090"
        //node="127.0.0.1" mission_key=""
        //status="5" version="1.22400.5519.45100" mode="pvp_pve" session_id=""
        //cpu_usage="0" memory_usage="574"
        //load_average="0"
        //region_id="global"
        //master_server_resource="pvp_skilled_1"
        //build_type="--profile" />

        [Query(IqType.Get, "setserver")]
        public static void SetServerSerializer(Client client, Iq iq)
        {
            if (!client.IsDedicated)
                throw new ServerException("Is not dedicated, ip: " + client.IPAddress);

            var q = iq.Query;

            var host = q.GetAttribute("host");
            var node = q.GetAttribute("node");
            var port = int.Parse(q.GetAttribute("port"));
            var ms_resource = q.GetAttribute("master_server_resource");
            var status = Utils.ParseEnum<SessionStatus>(q.GetAttribute("status"));

            if (client.Dedicated == null)
                throw new InvalidOperationException();

            client.Dedicated.SetServer(status, ms_resource, host, node, port);

            iq.SetQuery(Xml.Element("setserver").Attr("master_node", node));
            client.QueryResult(iq);

            var room = client.Dedicated.Room;

            if (room == null)
                return;

            var rSession = client.Dedicated.Room.GetExtension<GameRoomSession>();

            rSession.Status = status;

            switch (status)
            {
                case SessionStatus.Ready:
                    {
                        //rSession.Status = SessionStatus.None;
                        //room.EndSession();
                    }
                    break;
                case SessionStatus.PreGame:
                    {
                        if (room.Type == RoomType.PvP_Rating)
                        {
                            room.StartRatingTimer();
                        }
                        else
                        {
                            rSession.Status = SessionStatus.InGame;
                        }

                        room.GetExtension<GameRoomAutoStart>()?.Stop();
                        room.StartMissionVoting();
                    }
                    break;
                case SessionStatus.InGame:
                    {
                    }
                    break;
                case SessionStatus.PostGame:
                    {
                        Utils.Delay(15).ContinueWith(task => room.EndMissionVoting());
                    }
                    break;
                case SessionStatus.EndGame:
                    {
                        room.EndSession();
                    }
                    break;
            }
        }
    }
}
