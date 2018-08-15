using BobbyCarrot.Movers;
using System.Threading.Tasks;
using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class Grass : Platform
	{
		public override bool CanEnter(Mover mover) =>
			mover is GrassMower || mover is Flyer || mover is FireBall;


		public override async Task OnEnter(Mover mover)
		{
			if (mover is Flyer || mover is FireBall) return;

			// Remove this out of Platform array list
			// Anim
			Destroy(gameObject, 2f);
		}


		public static new Grass DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
