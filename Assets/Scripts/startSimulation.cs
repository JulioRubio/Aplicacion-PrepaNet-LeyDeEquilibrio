using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;

public class startSimulation : MonoBehaviour, IPointerDownHandler{
    public bool simulatorFlag = false;
    public GameObject Platform;
    public GameObject[] gameObjectList;
    public List<GameObject> spawnedObjects;
    public TextMeshProUGUI simulatorText;
    public GameObject winText;
    public Text textWin;
    public string rightEq;

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
        rightEq = "";
        simulatorText = FindObjectOfType<TextMeshProUGUI> ();
        Levels levels;
        string pathLevels = Application.streamingAssetsPath + "/levels.json";
        string contents = File.ReadAllText(pathLevels);
        levels = JsonUtility.FromJson<Levels>( "{\"levels\":" + contents + "}");

        LevelContents levelContents;
        string pathLevelContents = Application.streamingAssetsPath + "/level_contents.json";
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
                            Debug.Log("MASS");
                            Debug.Log(GameObject.Find(LC.content).GetComponent<Rigidbody2D>().mass * 100);
                            var mass = GameObject.Find(LC.content).GetComponent<Rigidbody2D>().mass * 100;
                            rightEq += "(" + LC.position + ")" + "(" + mass + ")" + "+";
                            var size = gameObjectList.Length;
                            for (int j = 5; j < size; j++)
                            {
                                gameObjectList[j].SetActive(false);

                            }
                        }
                        Debug.Log(LC.canvas_flag);
                        Debug.Log(LC.content);
                        Debug.Log(LC.hidden_mass_flag);
                        Debug.Log(LC.position);
                     }
                 }    
            }
        }
        foreach (string o in objects)
        {
            if (gameOs.Contains(o))
            {
                Debug.Log("Hello");
                Debug.Log(o);
            }
            else
            {
                GameObject.Find(o).SetActive(false);
            }
        }
        rightEq = rightEq.Remove(rightEq.Length - 1, 1); 
    }

    public void OnPointerDown(PointerEventData eventData){
        string leftEq = "";
        if(!simulatorFlag){
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 360, min = -360 };
            var size = gameObjectList.Length;
            for(int i = 0; i < 5; i++){
                gameObjectList[i].SetActive(false);

            }
            var result = 0;
            foreach (var obj in spawnedObjects)
            {
                var rb = obj.GetComponent<Rigidbody2D>();
                var mass = rb.mass;
                var pos = obj.transform.position.x;
                if (pos < 0){
                    leftEq += "(" + Math.Abs(pos) + ")" + "(" + mass + ")" + "+";
                }
                leftEq = leftEq.Remove(leftEq.Length - 1, 1); 
                result += (pos * mass * 100);

            }
            if (result == 0){

                Debug.Log("Ganaste");
                textWin.text = "Ganaste\n" + leftEq + "=" + rightEq;
            }
            else{
                Debug.Log("No seas menso");
                textWin.text = "Intenta de nuevo";
            }
            simulatorText.SetText("Detener Simulador");
            simulatorFlag = true;
            winText.SetActive(true);
            
        }
        else{
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 0, min = 0 };
            var size = gameObjectList.Length;
            for (int i = 0; i < 5; i++)
            {
                gameObjectList[i].SetActive(true);
            }
            foreach (var obj in spawnedObjects)
            {
                Destroy(obj);
            }
            simulatorText.SetText("Iniciar Simulador");
            simulatorFlag = false;
            winText.SetActive(false);
        }
    }

    public void objectCreated(GameObject spawned){
        spawnedObjects.Add(spawned);
    }


}

