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
    public static class MissionLoad
    {
        [Query(IqType.Result, "mission_load")]
        public static void MissionLoadSerializer(Client client, Iq iq)
        {
            var q = iq.Query;
            var result = q.GetAttribute("load_result");

            var room = client.Dedicated.Room;

            if (room == null)
                throw new InvalidOperationException();
            var rSession = room.GetExtension<GameRoomSession>();

            switch (result)
            {
                case "success":
                    {
                        Log.Info("[GameRoom] Mission load 'success' (room_id: {0}, session_id: {1})", room.Id, rSession.Id);

                        rSession.Status = SessionStatus.PreGame;
                    }
                    break;
                case "failed":
                    {
                        room?.EndSession();
                        room?.MissionLoad();

                        Log.Info("[GameRoom] Mission load 'failed' (room_id: {0}, session_id: {1})", room.Id, rSession.Id);
                    }
                    break;
            }
        }
    }
}
