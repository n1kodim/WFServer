using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.GameRoomVotes;
using WFServer.Game.Items;
using WFServer.Xmpp;
using System;
using System.Linq;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class SessionJoin
    {
        [Query(IqType.Get, "session_join")]
        public static void SessionJoinSerializer(Client client, Iq iq)
        {
            if (client.Profile == null)
                throw new InvalidOperationException();

            if (client.Profile.RoomPlayer?.Room == null)
                throw new QueryException(1);

            var roomPlayer = client.Profile.RoomPlayer;
            var room = client.Profile.RoomPlayer.Room;

            var rCore = room.GetExtension<GameRoomCore>();
            var rSession = room.GetExtension<GameRoomSession>();

            if (rSession.Status != SessionStatus.InGame)
                throw new QueryException(1);

            XmlElement session_join = Xml.Element("session_join")
                .Attr("room_id", room.Id)
                .Attr("server", "wf-wfserver")
                .Attr("hostname", rSession.Dedicated.Client.IPAddress)
                .Attr("port", rSession.Dedicated.Port)
                .Attr("local", "0")
                .Attr("session_id", rSession.Id);

            /*if (room.Type.ToString().Contains("PvE"))
            {
                var rMission = room.GetExtension<GameRoomMission>(false);

                if (rMission.Mission.MissionType != "easymission" &&
                    rMission.Mission.MissionType != "normalmission" &&
                    rMission.Mission.MissionType != "hardmission")
                {
                    foreach (var target in rCore.Players)
                    {
                        var item = target.Profile.Items.FirstOrDefault(x => x.Name == "mission_access_token_04");
                        item?.ConsumeItem(1);
                    }
                }
            }*/

            room.GetExtension<GameRoomVoteStates>()?.MissionVote?.VotingStarted();

            iq.SetQuery(session_join);
            client.QueryResult(iq);
        }
    }
}
