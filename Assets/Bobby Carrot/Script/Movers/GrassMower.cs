using UnityEngine;
using System.IO;


namespace BobbyCarrot.Movers
{
	public class GrassMower : Mover
	{
		public static readonly int DIR_X = Animator.StringToHash("dirX"),
			DIR_Y = Animator.StringToHash("dirY");

		public bool receiveInput = true;

		public static GrassMower instance { get; private set; }


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}
		}


		private void Start()
		{
			transform.parent = Board.instance.moverAnchor;
		}


		private void Update()
		{
			if (receiveInput && direction == Vector3Int.zero) direction = CommonUtil.GetInputDirection();
			if (direction != Vector3Int.zero)
			{
				// platform.CanExit() & platform.CanEnter() ? If True:
				// platform.OnExit()

				animator.SetInteger(DIR_X, direction.x);
				animator.SetInteger(DIR_Y, direction.y);

				// move pos and check array
				// platform.OnEnter()
			}
		}


		/// <summary>
		/// Test
		/// </summary>
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				direction = Vector3Int.zero;
				animator.SetInteger(DIR_X, 0);
				animator.SetInteger(DIR_Y, 0);
			}
		}


		public static byte[] Serialize(object _obj)
		{
			var obj = (GrassMower)_obj;
			BinaryWriter[] w = null;
			var s = Serialize(obj, w);
			s.MoveNext(); s.MoveNext(); s.MoveNext();

			return s.Current;
		}


		public static GrassMower DeSerialize(byte[] data)
		{
			BinaryReader[] r = null;
			var d = DeSerialize(data, R.asset.prefab.grassMower, r);
			d.MoveNext(); d.MoveNext();

			return d.Current;
		}
	}
}
