using WFServer.Core;
using WFServer.Xmpp;
using System;

namespace WFServer.Xmpp.Query
{
    public static class PeerStatusUpdate
    {
        [Query(IqType.Get, "peer_status_update")]
        public static void PeerStatusUpdateSerializer(Client client, Iq iq)
        {
            if (client.Profile == null)
                throw new InvalidOperationException();

            var q = iq.Query;

            client.PeerStatusUpdateBroadcast(q);
            client.QueryResult(iq);
        }
    }
}
