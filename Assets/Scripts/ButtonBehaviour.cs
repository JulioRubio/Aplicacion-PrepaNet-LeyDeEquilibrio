using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    
   public void onButtonPress(){
        string buttonName = EventSystem.current.currentSelectedGameObject.name; 
        int levelNumber =  Int32.Parse(buttonName[buttonName.Length - 1].ToString());
        string difficulty = buttonName.Substring(0, buttonName.Length - 1);

        PlayerPrefs.SetString("difficulty", difficulty);
        PlayerPrefs.SetInt("levelNumber", levelNumber);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
   }
}
