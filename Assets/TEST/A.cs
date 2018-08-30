using UnityEngine;
using UnityEngine.SceneManagement;


public class A : MonoBehaviour
{
	public bool loadScene2;


	private void Awake()
	{
		C.a = this;
	}


	private void Update()
	{
		if (loadScene2)
		{
			loadScene2 = false;
			SceneManager.LoadScene("2");
		}
	}
}


public class C
{
	public static A a;
}
