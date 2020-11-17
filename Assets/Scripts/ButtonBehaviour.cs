using System.Collections;
using System.Collections.Generic;
using System.IO;
using System
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    
   public void onButtonPress(){
        Levels levels;
        string pathLevels = Application.streamingAssetsPath + "/levels.json";
        string contents = File.ReadAllText(pathLevels);
        levels = JsonUtility.FromJson<Levels>(contents);

        LevelContents levelContents;
        string pathLevelContents = Application.streamingAssetsPath + "/level_contents.json";
        contents = File.ReadAllText(pathLevelContents);
        levelContents = JsonUtility.FromJson<LevelContents>(contents);

        string buttonName = EventSystem.current.currentSelectedGameObject.name; 
        int levelNumber =  Int32.Parse(buttonName[buttonName.Length - 1].ToString());
        string difficulty = buttonName.Substring(0, buttonName.Length - 1);

        string result = "";
        foreach (Level level in levels.levels)
        {
            if(level.difficulty.Equals(difficulty) && level.level == levelNumber)
            {
                 foreach (LevelContent LC in levelContents.level_contents)
                 {
                     if(level.id == LC.level_id){
                         PlayerPrefs.SetInt(LC.id + "canvas_flag", LC.canvas_flag);
                         PlayerPrefs.SetString(LC.id + "content", LC.content);
                         PlayerPrefs.SetInt(LC.id + "hidden_mass_flag", LC.hidden_mass_flag);
                         PlayerPrefs.SetInt(LC.id + "position", LC.position);
                         result = result + LC.id + ","
                     }
                 }    
            }
        }
        PlayerPrefs.SetString("ids", result)
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
   }
}
