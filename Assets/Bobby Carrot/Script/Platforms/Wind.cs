using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Wind : Platform
	{
		public bool isStop { get; private set; }
		[SerializeField] Sprite windSource, windStop;


		public static new Wind DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var wind = Instantiate(R.asset.prefab.wind, wPos, Quaternion.identity);
			wind.isStop = (ID == 245);
			if (use) wind.Use();
			return wind;
		}


		public override bool CanEnter(Mover mover)
		{
			if (isStop) return !(mover is LotusLeaf) && !(mover is MobileCloud);
			if (mover is Flyer || mover is FireBall) return true;
			if (mover is Walker)
				if (Item.count[Item.Name.KITE] > 0) return true;
				else Item.BlinkItem(Item.Name.KITE, mover.transform);

			return false;
		}


		public override async Task OnEnter(Mover mover)
		{
			if (isStop || mover is Flyer || mover is FireBall) return;

			var walker = (Walker)mover;
			walker.gameObject.SetActive(false);
			var flyer = Flyer.instance;
			if (!flyer)
			{
				flyer = Instantiate(R.asset.prefab.flyer);
				flyer.transform.parent = Board.instance.moverAnchor;
			}

			flyer.transform.position = transform.position;
			flyer.direction = walker.direction;
			flyer.speed = walker.speed;
			flyer.gameObject.SetActive(true);
		}
	}
}
