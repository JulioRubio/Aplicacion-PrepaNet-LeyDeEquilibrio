using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
using System;
using System.Linq;
public class ReiniciarSimulador : MonoBehaviour

{
    public GameObject[] gameObjectList;
    public List<GameObject> spawnedObjects;
    public GameObject Platform;
    public void OnPointerDown(PointerEventData eventData)
    {
        var hinge = Platform.GetComponent<HingeJoint2D>();
        hinge.limits = new JointAngleLimits2D() { max = 0, min = 0 };
        var size = gameObjectList.Length;

        foreach (var obj in spawnedObjects) {
                Destroy(obj);
        }
    }


   
    }
