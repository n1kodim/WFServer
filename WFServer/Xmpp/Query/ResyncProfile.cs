using WFServer.Core;
using WFServer.Game.Clans;
using WFServer.Game.Enums;
using WFServer.Game.Notifications;
using WFServer.Xmpp;
using System;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class ResyncProfile
    {
        [Query(IqType.Get, "resync_profile")]
        public static void Serializer(Client client, Iq iq)
        {
            if (client.Profile == null)
                throw new InvalidOperationException();

            iq.SetQuery(client.Profile.ResyncProfie());
            client.QueryResult(iq);
        }
    }
}