using UnityEngine;
using System.Threading.Tasks;


namespace BobbyCarrot.Platforms
{
	public class BeanTreeController : MonoBehaviour
	{
		public static BeanTreeController instance { get; private set; }


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
	}
}
