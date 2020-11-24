using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class startSimulation : MonoBehaviour, IPointerDownHandler{
    public bool simulatorFlag = false;
    public GameObject Platform;
    public GameObject[] gameObjectList;
    public List<GameObject> spawnedObjects;
    public TextMeshProUGUI simulatorText;
    public GameObject spawn;
    private GameObject defaultObject;
    private List<GameObject> objectsCreated = new List<GameObject>();
    private List<Vector2> objectsPosicion = new List<Vector2>();
    public Text FDerText, DDerText, FIzqText, MIzqText, DIzqText;
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
        //string pathLevels = Application.streamingAssetsPath + "/levels.json";
        //path en android
        string pathLevels = Path.Combine(Application.persistentDataPath,"levels.json");

        string contents = File.ReadAllText(pathLevels);
        levels = JsonUtility.FromJson<Levels>( "{\"levels\":" + contents + "}");
        
        LevelContents levelContents;


        //path en windows
        //string pathLevelContents = Application.streamingAssetsPath + "/level_contents.json";
        //path en android
        string pathLevelContents = Path.Combine(Application.persistentDataPath, "level_contents.json");


        contents = File.ReadAllText(pathLevelContents);
        levelContents = JsonUtility.FromJson<LevelContents>( "{\"level_contents\":" + contents + "}");
        string difficulty = PlayerPrefs.GetString("difficulty");
        int levelNumber = PlayerPrefs.GetInt("levelNumber");
        string[] gameOs = new string[8];
        string[] objects = {"TX Village Props Barrel", "TX Village Props Barrel (1)", "TX Village Props Barrel (2)", "TX Village Props Barrel (3)",
                            "TX Village Props Barrel (4)", "TX Village Props Barrel (5)", "TX Village Props Barrel (6)"};
        int i = 0;  
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
                            newSpawnObject = GameObject.Find(LC.content);
                            
                            newSpawnObject.GetComponent<types>().ChangeSprite();
                            Vector2 newPos = new Vector2(gameObjectList[LC.position+4].transform.position.x, gameObjectList[LC.position + 4].transform.position.y - 0.6f);
                            var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.Euler(0, 0, 0));
                            newObj.transform.parent = GameObject.Find("Platform").transform;
                            objectsCreated.Add(newObj);
                            objectsPosicion.Add(newPos);
                            
                        }
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
                defaultObject = GameObject.Find(o);
            }
            else
            {
                GameObject.Find(o).SetActive(false);
            }
        }
        defaultObject.GetComponent<types>().ChangeSprite();
    }

    public void OnPointerDown(PointerEventData eventData){
        if(!simulatorFlag){
            double staticObjectsAcumlativeDistance = 0.0;
            double staticObjectsForce = 0.0;
            double dynamicObjectsAcumlativeDistance = 0.0;
            double staticObjectsAcumlativeMass = 0.0;
            double dynamicObjectsAcumlativeMass = 0.0;
            double dynamicObjectsForce = 0.0;
            foreach(var obj in objectsCreated)
            {
                double distance = Math.Round(obj.transform.localPosition.x);
                double mass = Math.Round(obj.GetComponent<Rigidbody2D>().mass,2);

                staticObjectsAcumlativeMass += mass;
                staticObjectsAcumlativeDistance += distance;
                staticObjectsForce += distance * mass;
            }
            //print("Total distance static " + staticObjectsAcumlativeDistance);
            //print("Total mass static " + staticObjectsAcumlativeMass);
            //print("Total force static " + staticObjectsForce);
            removeNull();
            foreach (var obj in spawnedObjects) {
                double distance = Math.Round(obj.transform.localPosition.x);
                double mass = Math.Round(obj.GetComponent<Rigidbody2D>().mass,2);
                dynamicObjectsAcumlativeMass += mass;
                dynamicObjectsAcumlativeDistance += distance;
                dynamicObjectsForce += (-1 * distance) * mass;
            }
            dynamicObjectsAcumlativeDistance *= -1;
            //print("Total mass dynamic " + dynamicObjectsAcumlativeMass);
            //print("Total distance dynamic " + dynamicObjectsAcumlativeDistance);
            //print("Total force dynamic " + dynamicObjectsForce);
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 360, min = -360 };
            //print(dynamicObjects);
            for(int i = 0; i < 5; i++){
                gameObjectList[i].SetActive(false);

            }
            simulatorText.SetText("Detener Simulador");
            simulatorFlag = true;
            //Pato: Si las fuerzas son iguales manda a la pantalla de Win
            print("Total force static " + staticObjectsForce);
            if(dynamicObjectsForce > staticObjectsForce - 0.001 && dynamicObjectsForce < staticObjectsForce + 0.001){
                print("WINNER");
                SceneManager.LoadScene("Win");
            }
            dynamicObjectsForce *= 100 * 9.8;
            dynamicObjectsAcumlativeMass *= 100 * 9.8;
            staticObjectsForce *= 100 * 9.8;

            FIzqText.text += dynamicObjectsForce + " N m";
            MIzqText.text += dynamicObjectsAcumlativeMass + " N";
            DIzqText.text += dynamicObjectsAcumlativeDistance + " m";
            FDerText.text += staticObjectsForce + " N m";
            DDerText.text += staticObjectsAcumlativeDistance + " m";
       
        }
        
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void objectCreated(GameObject spawned){
        spawnedObjects.Add(spawned);
    }

    public void removeNull(){
        spawnedObjects.RemoveAll(item => item == null);
    }
}

