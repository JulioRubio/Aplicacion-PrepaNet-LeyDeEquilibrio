using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setNewAngleLab : MonoBehaviour
{
   public startSimulationLab simulator;
    public Transform actualTransform;
    public Transform newTransform;
    public GameObject objectController;
    // Start is called before the first frame update
    void Start()
    {
        simulator = GameObject.FindObjectOfType<startSimulationLab>();
        actualTransform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        simulator = GameObject.FindObjectOfType<startSimulationLab>();
        actualTransform.eulerAngles = newTransform.eulerAngles;


        if (Input.touchCount == 1 && !simulator.simulatorFlag)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(0).phase == TouchPhase.Began){
                objectController.SetActive(true);
                Destroy(this.gameObject);
                
            }
        }


       
    }
}
