using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class startSimulation : MonoBehaviour, IPointerDownHandler{
    bool simulatorFlag = false;
    public GameObject Platform;
    public GameObject[] gameObjectList;
    public List<GameObject> spawnedObjects;
    public TextMeshProUGUI simulatorText;

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
        simulatorText = FindObjectOfType<TextMeshProUGUI> ();
        Levels levels;
        string pathLevels = Application.streamingAssetsPath + "/levels.json";
        string contents = File.ReadAllText(pathLevels);
        levels = JsonUtility.FromJson<Levels>( "{\"levels\":" + contents + "}");

        LevelContents levelContents;
        string pathLevelContents = Application.streamingAssetsPath + "/level_contents.json";
        contents = File.ReadAllText(pathLevelContents);
        levelContents = JsonUtility.FromJson<LevelContents>( "{\"level_contents\":" + contents + "}");
        difficulty = PlayerPrefs.GetString("difficulty");
        levelNumber = PlayerPrefs.GetString("levelNumber");

        foreach (Level level in levels.levels)
        {
            if(level.difficulty.Equals(difficulty) && level.level == levelNumber)
            {
                 foreach (LevelContent LC in levelContents.level_contents)
                 {
                     if(level.id == LC.level_id){
                        Debug.Log(LC.canvas_flag);
                        Debug.Log(LC.content);
                        Debug.Log(LC.hidden_mass_flag);
                        Debug.Log(LC.position);
                     }
                 }    
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData){
        if(!simulatorFlag){
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 360, min = -360 };
            for(int i = 0; i < 10; i++){
                gameObjectList[i].SetActive(false);

            }
            simulatorText.SetText("Detener Simulador");
            simulatorFlag = true;
            
        }
        else{
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 0, min = 0 };
            for(int i = 0; i < 10; i++){
                gameObjectList[i].SetActive(true);
            }
            foreach (var obj in spawnedObjects) {
                Destroy(obj);
            }      
            simulatorText.SetText("Iniciar Simulador");
            simulatorFlag = false;
        }
    }

    public void objectCreated(GameObject spawned){
        spawnedObjects.Add(spawned);
    }
}
