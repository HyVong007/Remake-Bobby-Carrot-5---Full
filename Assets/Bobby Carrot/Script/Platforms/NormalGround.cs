using UnityEngine;


namespace BobbyCarrot.Platforms
{
	public class NormalGround : Platform
	{
		public new Name name { get; private set; }

		public enum Name
		{
			NORMAL, START, END, BEAN_TREE_GARDEN, BEAN_TREE_ROOT,
			BEAN_TREE_BODY, BEAN_TREE_HEAD
		}
	}
}
