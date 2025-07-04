using WFServer.Core;
using WFServer.Game.Enums;
using WFServer.Xmpp;
using System;
using System.Xml;

namespace WFServer.Game.GameRooms
{
	public class GameRoomMaster : GameRoomExtension
	{
		public Client Client { get; private set; }
		public int Revision { get; set; }

		public GameRoomMaster(Client master)
        {
			Set(master);
		}

		public void Set(Client master)
        {
			Client = master;
		}

		public override XmlElement Serialize()
		{
			return Xml.Element("room_master")
				.Attr("master", Client.Profile.Id)
				.Attr("revision", Revision);
		}
	}
}
