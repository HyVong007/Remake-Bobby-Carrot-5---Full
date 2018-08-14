using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class NormalGround : Platform
	{
		public new Name name { get; private set; }

		public enum Name
		{
			NORMAL, START, END
		}


		public static new NormalGround DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = New(ID, wPos, R.asset.prefab.normalGround);
			if (ID == 149) obj.name = Name.START;
			else if (ID == 150) obj.name = Name.END;
			else obj.name = Name.NORMAL;

			if (use) obj.Use();
			return obj;
		}

		// Platform.Serialize

		public static new NormalGround DeSerialize(byte[] data)
		{
			int ID; Vector3 wPos;
			DeSerialize(data, out ID, out wPos);
			return DeSerialize(ID, wPos, false);
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override void OnEnter(Mover mover)
		{
			if (name != Name.END || !(mover is Walker)) return;

			// Check Carrot or Easter Egg count
		}
	}
}
