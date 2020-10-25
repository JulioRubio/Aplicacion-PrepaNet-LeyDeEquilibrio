using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FixedJoint2D fixedJoint2D = GetComponent<FixedJoint2D>();
        fixedJoint2D.enabled = true;

        Vector2 newPos = new Vector2(Mathf.Round(fixedJoint2D.connectedAnchor.x), 0.7F);
        fixedJoint2D.connectedAnchor = newPos;
        print(fixedJoint2D.connectedAnchor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
