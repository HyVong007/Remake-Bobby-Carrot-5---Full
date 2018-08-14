using UnityEngine;
using BobbyCarrot.Platforms;


namespace BobbyCarrot.Movers
{
	public class MobileCloud : Mover, IPlatformProcessor, IUsable
	{
		public PinWheel.Color color { get; private set; }

		[SerializeField] PinWheelColor_Sprite_Dict sprites;


		public static new MobileCloud DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			return null;
		}


		public bool CanExit(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public bool CanEnter(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public void OnExit(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public void OnEnter(Mover mover)
		{
			throw new System.NotImplementedException();
		}


		public void Use(Vector3Int? pos = null)
		{
			if (pos == null) pos = transform.position.WorldToArray();
			var p = pos.Value;
			Platform.array[p.x][p.y].Add(this);
		}
	}
}
