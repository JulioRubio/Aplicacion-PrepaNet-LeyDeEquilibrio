using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setNewAngle : MonoBehaviour
{
    public Transform actualTransform;
    public Transform newTransform;
    // Start is called before the first frame update
    void Start()
    {
        actualTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        actualTransform.eulerAngles = newTransform.eulerAngles;
    }
}
