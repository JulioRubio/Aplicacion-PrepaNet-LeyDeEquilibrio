using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawnController : MonoBehaviour
{

    public GameObject spawn;
    public GameObject originalSprite;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount == 1) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(0).phase == TouchPhase.Began) {

                var newObj = GameObject.Instantiate(spawn, touchPos, Quaternion.identity);
                newObj.AddComponent<spawnObject>();
                newObj.transform.parent = GameObject.Find("Platform").transform;
                Destroy(originalSprite);
            }
        }
    }

}
