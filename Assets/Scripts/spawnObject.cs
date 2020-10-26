using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //se encarga de posicionar el objeto creado en el punto de x correspondiente a algun sprite spawner y una y de 0.7
        //de esta forma el objeto no se mueve de lugar y se puede asegurar un balance
        FixedJoint2D fixedJoint2D = GetComponent<FixedJoint2D>();
        Vector2 newPos = new Vector2(Mathf.Round(transform.localPosition.x), 0.7F);
        fixedJoint2D.connectedAnchor = newPos;

        fixedJoint2D.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
