using UnityEngine;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class Dragon : Platform
	{
		public static Dragon instance { get; private set; }


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}


		public async Task Fire(NormalObstacle dragonHead)
		{
			throw new System.NotImplementedException();
		}


		public static new Dragon DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}
	}
}
