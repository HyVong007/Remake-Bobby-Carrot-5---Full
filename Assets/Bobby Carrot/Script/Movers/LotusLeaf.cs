using UnityEngine;
using BobbyCarrot.Platforms;
using System.IO;
using System.Threading.Tasks;


namespace BobbyCarrot.Movers
{
	public class LotusLeaf : Mover, IPlatformProcessor, IUsable
	{
		public Walker walker { get; private set; }


		public static new LotusLeaf DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			var obj = Instantiate(R.asset.prefab.lotusLeaf, wPos, Quaternion.identity);
			if (use) obj.Use();
			return obj;
		}


		public bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall || (mover is Walker && direction == Vector3Int.zero);


		public bool CanExit(Mover mover) => CanEnter(mover);


		public async Task OnExit(Mover mover) { }


		public async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			// Lotus Leaf can go ahead ?
		}


		public void Use(Vector3Int? pos = null)
		{
			if (pos == null) pos = transform.position.WorldToArray();
			var p = pos.Value;
			Platform.array[p.x][p.y].Push(this);
			transform.parent = Board.instance.moverAnchor;
		}
	}
}
