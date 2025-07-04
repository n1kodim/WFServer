using WFServer.Core;
using WFServer.Game;
using WFServer.Xmpp;
using System;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class MissionsGetList
    {
        //private static XmlDocument missions = new XmlDocument();

        //TODO temp
        static MissionsGetList()
        {
            //missions.Load("Game/missions.xml");
            //missions.DocumentElement.Attr("hash", missions.OuterXml.GetHashCode());
        }

        [Query(IqType.Get, "missions_get_list")]
        public static void MissionsGetListSerializer(Client client, Iq iq)
        {
            //iq.SetQuery(missions.DocumentElement);
            iq.SetQuery(GameData.MissionsList);
            client.QueryResult(iq);
        }
    }
}
