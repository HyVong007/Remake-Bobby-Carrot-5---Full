using System.IO;
using UnityEngine;
using System.Collections.Generic;


namespace BobbyCarrot.Movers
{
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public abstract class Mover : MonoBehaviour
	{
		//[System.NonSerialized]
		public Vector3Int direction;

		public float speed;

		public SpriteRenderer spriteRenderer => _spriteRenderer;

		public Animator animator => _animator;

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Animator _animator;


		public static Mover DeSerialize(int ID, Vector3 wPos, bool use = true)
		{
			Mover mover = null;
			if (ID == 236)
				mover = LotusLeaf.DeSerialize(ID, wPos, use);

			else if (224 <= ID && ID <= 226)
				mover = MobileCloud.DeSerialize(ID, wPos, use);

			return mover;
		}


		protected static IEnumerator<byte[]> Serialize<T>(T obj, BinaryWriter[] writer) where T : Mover
		{
			using (MemoryStream m = new MemoryStream())
			using (BinaryWriter w = new BinaryWriter(m))
			{
				var pos = obj.transform.position;
				w.Write(pos.x); w.Write(pos.y); w.Write(pos.z);

				var dir = obj.direction;
				w.Write(dir.x); w.Write(dir.y);
				w.Write(obj.speed);
				writer = new BinaryWriter[] { w };
				yield return null;
				yield return m.ToArray();
			}
		}


		protected static IEnumerator<T> DeSerialize<T>(byte[] data, T prefab, BinaryReader[] reader) where T : Mover
		{
			var obj = Instantiate(prefab);
			using (MemoryStream m = new MemoryStream(data))
			using (BinaryReader r = new BinaryReader(m))
			{
				obj.transform.position = new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
				obj.direction = new Vector3Int(r.ReadInt32(), r.ReadInt32(), 0);
				obj.speed = r.ReadSingle();
				reader = new BinaryReader[] { r };
				yield return obj;
			}
		}
	}
}
