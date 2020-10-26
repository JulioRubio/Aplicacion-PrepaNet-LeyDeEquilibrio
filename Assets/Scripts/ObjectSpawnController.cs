using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawnController : MonoBehaviour
{

    public GameObject spawn;
    public GameObject originalSprite;

    //detecta touch, si la posicion del touch coincide con algun polygon collider quitar collider y sustituir por objeto
    void Update() {
        if (Input.touchCount == 1) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(0).phase == TouchPhase.Began) {
                Destroy(originalSprite);
                Vector2 newPos = new Vector2(originalSprite.transform.position.x,originalSprite.transform.position.y);
                var newObj = GameObject.Instantiate(spawn, newPos, Quaternion.identity);
                newObj.AddComponent<spawnObject>();
                newObj.transform.parent = GameObject.Find("Platform").transform;
            }
        }
    }

}
