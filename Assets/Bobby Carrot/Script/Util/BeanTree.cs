using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using BobbyCarrot.Platforms;


namespace BobbyCarrot.Util
{
	public class BeanTree : MonoBehaviour
	{
		public int height { get; private set; } = 1;

		public readonly List<BeanTreeNode> nodes = new List<BeanTreeNode>();

		[SerializeField] private Sprite littleTree;


		public static BeanTreeNode DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			if (ID != 238) return null;
			var garden = BeanTreeNode.New(BeanTreeNode.Name.GARDEN, wPos, use);
			if (!use) return garden;

			var beanTree = Instantiate(R.asset.prefab.beanTree, wPos, Quaternion.identity);
			beanTree.transform.parent = Board.instance.platformAnchor;
			garden.beanTree = beanTree;
			garden.transform.parent = beanTree.transform;
			beanTree.nodes.Add(garden);
			return garden;
		}


		private void Awake()
		{
			var array = Level.instance.topArray;
			var pos = transform.position.WorldToArray();

			do
			{
				pos += Vector3Int.up;
				++height;
			} while (array[pos.x][pos.y] != 206);

			growDelayMiliSec = Mathf.CeilToInt(Mathf.Lerp(1f, 0f, growSpeed) * 1000);
		}


		[Range(0.05f, 1f)]
		[SerializeField] private float growSpeed = 0.05f;
		private int growDelayMiliSec;

		public async Task Grow()
		{
			--Item.count[Item.Name.BEAN];
			var garden = nodes[0];
			garden.spriteRenderer.sprite = littleTree;
			await Task.Delay(growDelayMiliSec);

			nodes.Clear();
			Destroy(garden);
			var pos = transform.position;
			Use(BeanTreeNode.New(BeanTreeNode.Name.ROOT, pos));
			var head = BeanTreeNode.New(BeanTreeNode.Name.HEAD, pos += Vector3.up, false);
			int h = 2;
			while (h++ < height)
			{
				Use(BeanTreeNode.New(BeanTreeNode.Name.BODY, pos));
				head.transform.position = (pos += Vector3.up);
				await Task.Delay(growDelayMiliSec);
			}

			head.Use();
			Use(head);
		}


		private void Use(BeanTreeNode node)
		{
			node.beanTree = this;
			node.transform.parent = transform;
			nodes.Add(node);
		}
	}
}
