using UnityEngine;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class BeanTree : Platform
	{
		public static BeanTree instance { get; private set; }


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}


		public async Task PlantTree(NormalGround beanTreeGarden)
		{
			throw new System.NotImplementedException();
		}


		public static new BeanTree DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
