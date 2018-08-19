using UnityEngine;


public class TestTool : MonoBehaviour
{
	public MyButton left, right, up, down;

	public static TestTool instance { get; private set; }


	private void Awake()
	{
		if (!instance) instance = this;
		else if (this != instance)
		{
			Destroy(gameObject);
			return;
		}

		left.DIR = Vector3Int.left; left.tool = this;
		right.DIR = Vector3Int.right; right.tool = this;
		up.DIR = Vector3Int.up; up.tool = this;
		down.DIR = Vector3Int.down; down.tool = this;
	}

	public Vector3Int direction;
}
