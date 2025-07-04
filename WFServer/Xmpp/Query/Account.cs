﻿using WFServer.Core;
using WFServer.Xmpp;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class Account
    {
        [Query(IqType.Get, "account")]
        public static void AccountSerializer(Client client, Iq iq)
        {
            //TODO проверять логин и пароль
            XmlElement account = Xml.Element("account")
                .Attr("user", client.UserId)
                .Attr("survival_lb_enabled", " ")
                .Attr("active_token", "0")
                .Attr("nickname", "");

            XmlElement masterservers = Xml.Element("masterservers");
            foreach (MasterServer channel in Server.Channels)
            {
                masterservers.Child(channel.Serialize());
            }

            account.Child(masterservers);

            iq.SetQuery(account);
            client.QueryResult(iq);
        }
    }
}
