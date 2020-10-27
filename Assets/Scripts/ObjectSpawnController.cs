using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawnController : MonoBehaviour
{

    public GameObject spawn;
    public GameObject originalSprite;
    public startSimulation simulator;

    void Start(){
        simulator = GameObject.FindObjectOfType<startSimulation>();
    }

    //detecta touch, si la posicion del touch coincide con algun polygon collider quitar collider y sustituir por objeto
    void Update() {
        if (Input.touchCount == 1) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(0).phase == TouchPhase.Began) {
                originalSprite.SetActive(false);
                Vector3 rotationVector = new Vector3(0f,0f,originalSprite.transform.parent.rotation.z);
                Vector2 newPos = new Vector2(originalSprite.transform.position.x,originalSprite.transform.position.y-0.7f);
                var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.Euler(0, 0, rotationVector.z));
                print(newObj.transform.position);
                newObj.AddComponent<spawnObject>();
                newObj.transform.parent = GameObject.Find("Platform").transform;

                simulator.objectCreated(newObj);
            }
        }
    }

}
