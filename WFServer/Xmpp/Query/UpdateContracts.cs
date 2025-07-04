using WFServer.Core;
using WFServer.Xmpp;
using System;

namespace WFServer.Xmpp.Query
{
    public static class UpdateContracts
    {
        [Query(IqType.Get, "update_contracts")]
        public static void UpdateContractsSerializer(Client client, Iq iq)
        {
            if (!client.IsDedicated)
                throw new InvalidOperationException();

            //TODO


        }
    }
}
