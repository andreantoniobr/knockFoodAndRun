using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private bool isTouching;
    public bool IsTouching => isTouching;

    [SerializeField] private Vector3 touchedPos;


    private float touchedPositionX;
    public float TouchedPositionX => touchedPositionX;



    public void Update() 
    {
        if (Input.touchCount > 0)
        {
            isTouching = true;
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                
                // get the touch position from the screen touch to world point
                touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                touchedPositionX = touchedPos.x;
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                //transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
            }
        }
        else
        {
            isTouching = false;
        }
    }
}
