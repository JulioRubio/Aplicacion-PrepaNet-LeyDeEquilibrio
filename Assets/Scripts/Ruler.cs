using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : MonoBehaviour{
    public GameObject rulerSprite;
    bool rulerFlag = false;
    // Update is called once per frame
    void Update(){
         if (Input.touchCount == 1) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(0).phase == TouchPhase.Began) {
                if(rulerFlag){
                    Color tmp = rulerSprite.GetComponent<SpriteRenderer>().color;
                    tmp.a = 0f;
                    rulerSprite.GetComponent<SpriteRenderer>().color = tmp;
                    rulerFlag = false;
                }else{
                    Color tmp = rulerSprite.GetComponent<SpriteRenderer>().color;
                    tmp.a = 0.6f;
                    rulerSprite.GetComponent<SpriteRenderer>().color = tmp;
                    rulerFlag = true;
                }
            }
        }
    }
}
