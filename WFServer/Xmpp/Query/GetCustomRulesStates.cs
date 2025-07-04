using WFServer.Core;
using WFServer.Xmpp;

namespace WFServer.Xmpp.Query
{
    public static class GetCustomRulesStates
    {

        [Query(IqType.Get, "get_custom_rules_states")]
        public static void GetCustomRulesStatesSerializer(Client client, Iq iq)
        {
            //TODO


        }
    }
}
