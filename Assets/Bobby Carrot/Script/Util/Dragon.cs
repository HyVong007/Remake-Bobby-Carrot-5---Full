using UnityEngine;
using System.Collections.Generic;
using BobbyCarrot.Platforms;
using System.Threading.Tasks;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Util
{
	public class Dragon : MonoBehaviour
	{
		public readonly List<DragonNode> nodes = new List<DragonNode>();

		private static Dragon lastDragon;


		public static DragonNode DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			DragonNode node = null;
			switch (ID)
			{
				case 215:
					node = DragonNode.New(DragonNode.Name.HEAD, wPos, use);
					if (use)
					{
						lastDragon = Instantiate(R.asset.prefab.dragon, wPos, Quaternion.identity);
						lastDragon.transform.parent = Board.instance.platformAnchor;
					}
					else lastDragon = null;
					break;

				case 216:
					node = DragonNode.New(DragonNode.Name.BODY, wPos, use);
					break;

				case 217:
					node = DragonNode.New(DragonNode.Name.TAIL, wPos, use);
					break;
			}

			lastDragon?.Use(node);
			return node;
		}


		private void Use(DragonNode node)
		{
			nodes.Add(node);
			node.dragon = this;
			node.transform.parent = transform;
		}


		public async Task Fire()
		{
			var head = nodes[0];
			var headSprite = head.spriteRenderer.sprite;
			head.animator.enabled = true;
			head.animator.SetTrigger(DragonNode.FIRE);
			var fireBall = FireBall.instance;
			if (!fireBall)
			{
				fireBall = Instantiate(R.asset.prefab.fireBall);
				fireBall.transform.parent = Board.instance.moverAnchor;
			}

			fireBall.transform.position = head.transform.position;
			fireBall.direction = Vector3Int.left;
			fireBall.gameObject.SetActive(true);

			while (fireBall.gameObject.activeSelf) await Task.Delay(1);
			await CameraController.instance.Focus(nodes[2].transform.position, 0.1f);
			head.animator.enabled = false;
			head.spriteRenderer.sprite = headSprite;
		}
	}
}
