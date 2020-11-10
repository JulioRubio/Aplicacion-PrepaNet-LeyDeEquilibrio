using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CambiaEscenas : MonoBehaviour
{
	public void ChangeScene(string scene)
	{
		SceneManager.LoadScene(scene);
		if (scene == "Salir")
			Application.Quit();
	}
}
