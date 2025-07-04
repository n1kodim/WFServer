﻿using WFServer.Core;
using WFServer.Game.Items;
using WFServer.Xmpp;
using System;
using System.Linq;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class ConsumeItem
    {
        [Query(IqType.Get, "consume_item")]
        public static void ConsumeItemSerializer(Client client, Iq iq)
        {
            if (!client.IsDedicated)
                throw new InvalidOperationException();

            var q = iq.Query;

            var profile_id = ulong.Parse(q.GetAttribute("profile_id"));
            var item_profile_id = ulong.Parse(q.GetAttribute("item_profile_id"));

            Client target = null;
            lock (Server.Clients)
            {
                target = Server.Clients
                    .FirstOrDefault(x => x.ProfileId == profile_id);
            }

            if (target == null)
                return;

            var item = target.Profile.Items
                .FirstOrDefault(x => x.Id == item_profile_id);

            if (item == null || item.Type != ItemType.Consumable)
                throw new QueryException(1);

            if (item.Type == ItemType.Consumable)
            {
                item.ConsumeItem(1);
            }

            client.QueryResult(iq);
        }
    }
}
