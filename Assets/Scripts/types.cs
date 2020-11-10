using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class types : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Rigidbody2D newMass;
    void ChangeSprite()
    {
    
        spriteRenderer.sprite = newSprite;
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

        newMass.mass = rigidbody2D.mass;
        transform.position = new Vector3(0.0f, -2.0f, 0.0f);
        newMass.AddForce(transform.up * 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        ChangeSprite();

                    }

                    break;

            }
        }
    }

}


