using UnityEngine;


namespace Script.Terrains
{
	public class MowerPark : Platform
	{
		public bool hasMower;

		[SerializeField]
		private GameObject mower;


		public void Init(Name name, bool hasMower=true)
		{
			Init(name);
			this.hasMower = hasMower;
		}
	}
}
