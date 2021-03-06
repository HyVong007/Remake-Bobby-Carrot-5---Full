﻿using BobbyCarrot.Movers;
using System.Threading.Tasks;
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

		public static NormalGround startPoint { get; private set; }

		public static NormalGround endPoint { get; private set; }


		static NormalGround()
		{
			System.Action f = () =>
			  {
				  if (Carrot.countDown == 0 && EasterEgg.countDown == 0)
					  endPoint.animator.enabled = true;
			  };

			Carrot.onRemoveCarrot += f;
			EasterEgg.onAddEgg += f;
		}


		public static new NormalGround DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = New(ID, wPos, R.asset.prefab.normalGround);
			if (ID == 149)
			{
				obj.name = Name.START;
				startPoint = obj;
			}
			else if (ID == 150)
			{
				obj.name = Name.END;
				endPoint = obj;
			}
			else obj.name = Name.NORMAL;

			if (use) obj.Use();
			return obj;
		}


		public override bool CanEnter(Mover mover) =>
			!(mover is LotusLeaf) && !(mover is MobileCloud);


		public override async Task OnEnter(Mover mover)
		{
			if (name != Name.END || !(mover is Walker)) return;

			if (Carrot.countDown == 0 && EasterEgg.countDown == 0)
			{
				// Bạn thắng rồi !
			}
		}
	}
}
