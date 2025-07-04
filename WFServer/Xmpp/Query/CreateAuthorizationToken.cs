using WFServer.Core;
using WFServer.Xmpp;
using System;

namespace WFServer.Xmpp.Query
{
    public static class CreateAuthorizationToken
    {
        [Query(IqType.Get, "create_authorization_token")]
        public static void Serializer(Client client, Iq iq)
        {

        }
    }
}

