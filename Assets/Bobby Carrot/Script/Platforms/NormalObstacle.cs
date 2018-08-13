using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class NormalObstacle : Platform
	{
		public new Name name { get; private set; }

		public enum Name
		{
			NORMAL, SKY, WATER
		}


		public static new NormalObstacle DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = Instantiate(R.asset.prefab.normalObstacle, wPos, Quaternion.identity);
			obj.ID = ID;
			obj.spriteRenderer.sprite = R.asset.myTile.platforms[ID].sprite;

			if (71 <= ID && ID <= 76)
			{
				obj.name = Name.SKY;
				if (ID == 72) obj.animator.runtimeAnimatorController = R.asset.anim.star;
			}
			else if (ID == 85 || ID == 86 || ID == 91)
			{
				obj.name = Name.WATER;
				if (ID == 86) obj.animator.runtimeAnimatorController = R.asset.anim.water;
				else if (ID == 91) obj.animator.runtimeAnimatorController = R.asset.anim.waterFall;
			}
			else
			{
				obj.name = Name.NORMAL;
				if (249 <= ID && ID <= 254) obj.spriteRenderer.sortingLayerName = R.MIDDLE_LAYER;
			}

			return obj;
		}


		// Platform.Serialize(object): byte[]


		public static new NormalObstacle DeSerialize(byte[] data)
		{
			int ID; Vector3 wPos;
			DeSerialize(data, out ID, out wPos);
			return DeSerialize(ID, wPos, false);
		}


		public override bool CanEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return true;
			switch (name)
			{
				case Name.NORMAL: return false;
				case Name.SKY: return mover is MobileCloud;
				case Name.WATER: return false;
			}

			return false;
		}
	}
}
