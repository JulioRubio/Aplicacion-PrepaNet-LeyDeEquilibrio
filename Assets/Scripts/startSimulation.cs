using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
using System;
using System.Linq;

public class startSimulation : MonoBehaviour, IPointerDownHandler{
    public bool simulatorFlag = false;
    public GameObject Platform;
    public GameObject[] gameObjectList;
    public List<GameObject> spawnedObjects;
    public TextMeshProUGUI simulatorText;
    public startSimulation simulator;
    public GameObject spawn;


    /*
    click listener, cuando el boton el presionado mueve los limites de angulos del hinge joint son cambiados a -360 y 360 permitiendo rotacion de ambos lados
    inicialmente esta puesto como minimo de 0 y máximo de 0
    TODO 
        Cuando el simulador pasa de estado activo -> inactivo
            mover los limites a min 0 y max 0 y repoisicionar plataform de forma horizonal.
            volver a mostrar los spawner sprites
        Cuando el simulador pasa de estado inactivo -> activo
            remover los spawner sprites no utilizados
    */

    void Start () {
        Levels levels;
        //path en windows
        string pathLevels = Application.streamingAssetsPath + "/levels.json";

        //path en android
        //string pathLevels = Path.Combine(Application.persistentDataPath,"levels.json");

        string contents = File.ReadAllText(pathLevels);
        levels = JsonUtility.FromJson<Levels>( "{\"levels\":" + contents + "}");
        
        LevelContents levelContents;
        //path en windows
        string pathLevelContents = Application.streamingAssetsPath + "/level_contents.json";
        //path en android
        //string pathLevelContents = Path.Combine(Application.persistentDataPath, "level_contents.json");
        contents = File.ReadAllText(pathLevelContents);
        levelContents = JsonUtility.FromJson<LevelContents>( "{\"level_contents\":" + contents + "}");
        string difficulty = PlayerPrefs.GetString("difficulty");
        int levelNumber = PlayerPrefs.GetInt("levelNumber");
        string[] gameOs = new string[8];
        string[] objects = {"TX Village Props Barrel", "TX Village Props Barrel (1)", "TX Village Props Barrel (2)", "TX Village Props Barrel (3)",
                            "TX Village Props Barrel (4)", "TX Village Props Barrel (5)", "TX Village Props Barrel (6)"};
        int i = 0;
        Debug.Log(difficulty);
        Debug.Log(levelNumber);
        
        foreach (Level level in levels.levels)
        {
            if(level.difficulty.Equals(difficulty) && level.level == levelNumber)
            {
                 foreach (LevelContent LC in levelContents.level_contents)
                 {
                     if(level.id == LC.level_id){
                        if(LC.canvas_flag == 1)
                        {
                            gameOs[i] = LC.content;
                            i++;
                        }
                        else{
                            // Objetos que deben estar sobre la regla al principio
                            //LC.position
                            //LC.content
                            GameObject newSpawnObject;
                            if (LC.position == 1)
                            {
                                newSpawnObject = GameObject.Find(LC.content);
                                newSpawnObject.GetComponent<types>().ChangeSprite();
                                Vector2 newPos = new Vector2(1.0f, 1.5f );
                                var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.Euler(0, 0, 0));
                                
                            } else if (LC.position == 2)
                            {
                                newSpawnObject = GameObject.Find(LC.content);
                                newSpawnObject.GetComponent<types>().ChangeSprite();
                                Vector2 newPos = new Vector2(2.0f, 1.5f );
                                var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.Euler(0, 0, 0));
                                

                            }
                            else if (LC.position == 3)
                            {
                                newSpawnObject = GameObject.Find(LC.content);
                                newSpawnObject.GetComponent<types>().ChangeSprite();
                                Vector2 newPos = new Vector2(3.0f, 1.5f );
                                var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.Euler(0, 0, 0));
                                

                            }
                            else if (LC.position == 4)
                            {
                                newSpawnObject = GameObject.Find(LC.content);
                                newSpawnObject.GetComponent<types>().ChangeSprite();
                                Vector2 newPos = new Vector2(4.0f, 1.5f );
                                var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.Euler(0, 0, 0));
                               

                            }
                            else if (LC.position == 5)
                            {
                                newSpawnObject = GameObject.Find(LC.content);
                                newSpawnObject.GetComponent<types>().ChangeSprite();
                                Vector2 newPos = new Vector2(5.0f, 1.5f );
                                var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.Euler(0, 0, 0));
                                
                            }
                            

                        }
                        Debug.Log(">>>>>>");
                        Debug.Log(LC.canvas_flag);
                        Debug.Log(LC.content);
                        Debug.Log(LC.hidden_mass_flag);
                        Debug.Log(LC.position);
                        Debug.Log("<<<<<<");
                    }
                 }
                var size = gameObjectList.Length;
                for (int j = 5; j < size; j++)
                {
                    gameObjectList[j].SetActive(false);

                }
            }
            
                            
        }

        foreach (string o in objects)
        {
            if (gameOs.Contains(o))
            {

            }
            else
            {
                GameObject.Find(o).SetActive(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        if(!simulatorFlag){
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 360, min = -360 };

            for(int i = 0; i < 5; i++){
                gameObjectList[i].SetActive(false);

            }
            simulatorText.SetText("Detener Simulador");
            simulatorFlag = true;
            
        }
        else{
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 0, min = 0 };
            var size = gameObjectList.Length;
            removeNull();
            for (int i = 0; i < 5; i++){
                gameObjectList[i].SetActive(true);
                foreach (var obj in spawnedObjects) {
                    if(Math.Round(gameObjectList[i].transform.localPosition.x) == Math.Round(obj.transform.localPosition.x)){
                        print("yes");
                        //Destroy(obj);
                        gameObjectList[i].SetActive(false);
                    }
                }
            }
            simulatorText.SetText("Iniciar Simulador");
            simulatorFlag = false;
        }
    }

    public void objectCreated(GameObject spawned){
        spawnedObjects.Add(spawned);
    }

    public void removeNull(){
        spawnedObjects.RemoveAll(item => item == null);
    }
}

