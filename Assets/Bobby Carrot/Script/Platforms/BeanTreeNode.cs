using System.Threading.Tasks;
using BobbyCarrot.Movers;
using UnityEngine;
using BobbyCarrot.Util;


namespace BobbyCarrot.Platforms
{
	public class BeanTreeNode : Platform
	{
		public new Name name { get; private set; } = Name.GARDEN;

		[System.NonSerialized]
		public BeanTree beanTree;

		[SerializeField] private BeanTreeNodeName_Sprite_Dict sprites;

		public enum Name
		{
			GARDEN, ROOT, BODY, HEAD
		}


		public static BeanTreeNode New(Name name, Vector3 wPos, bool use = true)
		{
			var node = Instantiate(R.asset.prefab.beanTreeNode, wPos, Quaternion.identity);
			node.name = name;
			if (use) node.Use();
			return node;
		}


		private void Start()
		{
			spriteRenderer.sprite = sprites[name];
		}


		public override bool CanEnter(Mover mover) =>
			mover is Flyer || mover is FireBall || mover is Walker;


		public override async Task OnEnter(Mover mover)
		{
			if (name != Name.GARDEN || !(mover is Walker)) return;
			if (Item.count[Item.Name.BEAN] == 0)
			{
				Item.BlinkItem(Item.Name.BEAN, mover.transform, 6f);
				return;
			}

			var walker = (Walker)mover;
			await walker.GotoIdle((Walker.RelaxState)Walker.dirToIdle[walker.direction]);
			walker.receiveInput = false;
			await beanTree.Grow();
			walker.receiveInput = true;
		}
	}
}
