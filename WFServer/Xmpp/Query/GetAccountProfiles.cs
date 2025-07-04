﻿using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Xmpp;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class GetAccountProfiles
    {
        [Query(IqType.Get, "get_account_profiles")]
        public static void GetAccountProfilesSerializer(Client client, Iq iq)
        {
            var version = iq.Query.GetAttribute("version");
            var user_id = iq.Query.GetAttribute("user_id");

#if !DEBUG
            if (version != Config.Settings.GameVersion)
                throw new QueryException(1);
#endif

            if (user_id != iq.From.Node)
                throw new QueryException(1);

            XmlElement get_account_profiles = Xml.Element("get_account_profiles");
            if (client.Profile != null)
            {
                get_account_profiles.Child(Xml.Element("profile")
                   .Attr("id", client.ProfileId)
                   .Attr("nickname", client.Profile.Nickname));

                client.Presence = PlayerStatus.Online;
                //TODO
                //PlayerStatusManager.SetPlayerStatus(profile.Id, iq.From.ToString(), PlayerStatus.Online);
            }

            iq.SetQuery(get_account_profiles);
            client.QueryResult(iq);
        }
    }
}
