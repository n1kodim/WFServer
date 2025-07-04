using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Game.GameRooms;
using WFServer.Game.Items;
using WFServer.Game.Missions;
using WFServer.Xmpp;
using System;
using System.Linq;
using System.Xml;

namespace WFServer.Xmpp.Query
{
    public static class ConvertCardsToLeftover
    {
        //<convert_cards_to_leftover card_id='1425'/>
        [Query(IqType.Get, "convert_cards_to_leftover")]
        public static void ConvertCardsToLeftoverSerializer(Client client, Iq iq)
        {
            if (client.Profile == null)
                throw new InvalidOperationException();

            var q = iq.Query;

            ulong card_id = ulong.Parse(q.GetAttribute("card_id"));

            var card = client.Profile.Items.FirstOrDefault(x => x.Id == card_id);

            if (card != null && card.Quantity > 0)
            {
                var item = client.Profile.GiveItem("leftover_card", ItemType.Consumable, quantity: card.Quantity);
                q.Attr("leftover_cards_new_count", item.Quantity);

                card.Quantity = 0;
                card.Update();
            }

            client.QueryResult(iq);
        }
    }
}
