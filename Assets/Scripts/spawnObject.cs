using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FixedJoint2D fixedJoint2D = GetComponent<FixedJoint2D>();

        print(transform.localPosition);
        Vector2 newPos = new Vector2(Mathf.Round(transform.localPosition.x), 0.7F);
        print(newPos);
        fixedJoint2D.connectedAnchor = newPos;

        fixedJoint2D.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
