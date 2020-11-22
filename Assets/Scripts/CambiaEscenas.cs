using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class CambiaEscenas : MonoBehaviour
{
	public void ChangeScene(string scene)
	{
		Scene myScene = SceneManager.GetActiveScene();
		if (myScene.name == "Dificultad")
        {
			PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("difficulty", EventSystem.current.currentSelectedGameObject.name);
        }
		else if (myScene.name == "Niveles")
		{
			int levelNumber = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
			PlayerPrefs.SetInt("levelNumber", levelNumber);
		}
		SceneManager.LoadScene(scene);
		if (scene == "Salir")
			Application.Quit();
	}
}
