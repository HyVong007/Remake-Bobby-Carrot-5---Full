using UnityEngine;
using System.Threading.Tasks;
using BobbyCarrot.Movers;


namespace BobbyCarrot.Platforms
{
	public class DragonController : MonoBehaviour
	{
		public static DragonController instance { get; private set; }


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
	}
}
