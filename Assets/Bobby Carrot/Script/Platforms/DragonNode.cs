using UnityEngine;
using BobbyCarrot.Util;
using BobbyCarrot.Movers;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	/// <summary>
	/// Head Node has Animation
	/// </summary>
	public class DragonNode : Platform
	{
		public new Name name { get; private set; }

		[System.NonSerialized]
		public Dragon dragon;

		public static readonly int FIRE = Animator.StringToHash("fire");

		[SerializeField] private DragonNodeName_Sprite_Dict sprites;

		public enum Name
		{
			HEAD, BODY, TAIL
		}


		public static DragonNode New(Name name, Vector3 wPos, bool use = true)
		{
			var node = Instantiate(R.asset.prefab.dragonNode, wPos, Quaternion.identity);
			node.name = name;
			if (use) node.Use();
			return node;
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[name];
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall ||
			(
				name == Name.TAIL && (mover is Walker || mover is GrassMower)
			);


		public override async Task OnEnter(Mover mover)
		{
			if (name != Name.TAIL || (!(mover is Walker) && !(mover is GrassMower))) return;

			if (mover is Walker) await ((Walker)mover).GotoIdle((Walker.RelaxState)Walker.dirToIdle[mover.direction]);
			mover.isLock = true;
			await dragon.Fire();
			mover.isLock = false;
		}
	}
}
