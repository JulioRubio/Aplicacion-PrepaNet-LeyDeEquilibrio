using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class startSimulation : MonoBehaviour, IPointerDownHandler{
    bool simulatorFlag = false;
    public GameObject Platform;

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
     var hinge = Platform.GetComponent<HingeJoint2D>();
     hinge.limits = new JointAngleLimits2D() { max = 360, min = -360 };
    }
}
