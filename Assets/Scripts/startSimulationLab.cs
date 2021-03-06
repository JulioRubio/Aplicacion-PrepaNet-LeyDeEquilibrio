﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;


public class startSimulationLab : MonoBehaviour, IPointerDownHandler{
    public bool simulatorFlag = false;
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
    public void OnPointerDown(PointerEventData eventData){
        if(!simulatorFlag){
            var hinge = Platform.GetComponent<HingeJoint2D>();
            hinge.limits = new JointAngleLimits2D() { max = 360, min = -360 };
            var size = gameObjectList.Length;
            for(int i = 0; i < size; i++){
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
            for (int i = 0; i < size; i++)
            {
                gameObjectList[i].SetActive(true);
                foreach (var obj in spawnedObjects) {
                    if(Math.Round(gameObjectList[i].transform.localPosition.x) == Math.Round(obj.transform.localPosition.x)){
                        print("yes");
                        //Destroy(obj);
                        gameObjectList[i].SetActive(false);
                    }
                }
                
            } 
            //spawnedObjects.Clear();
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

    public void changeFlag(){
        if(simulatorFlag){
            simulatorText.SetText("Iniciar Simulador");
        }
        simulatorFlag = false;
    }
}

