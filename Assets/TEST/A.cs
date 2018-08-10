using UnityEngine;
using UnityEngine.SceneManagement;


public class A : MonoBehaviour
{
	static A()
	{
		//C.a = 1256;
	}


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			SceneManager.LoadScene("Hehe");
	}
}

