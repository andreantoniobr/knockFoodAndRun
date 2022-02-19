using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{    
    [SerializeField] private bool detectSwipeOnlyAfterRelease = false;
    [SerializeField] private float SWIPE_THRESHOLD = 20f;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    private float touchedPositionX;
    public float TouchedPositionX => touchedPositionX;
    
    private bool isTouching = false;
    public bool IsTouching => isTouching;

    private void Update()
    {
#if UNITY_EDITOR
        ProcessKeybordInput();
#endif

#if UNITY_ANDROID
        ProcessTouchMobileInput();
#endif        
    }

    private void ProcessTouchMobileInput()
    {
        /*
        if (Input.touchCount > 0)
        {
            isTouching = true;
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                touchedPositionX = touch.deltaPosition.x > 0 ? -1 : 1;
            }
        }
        else 
        {
            isTouching = false;
        }*/

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
                isTouching = true;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
                isTouching = false;
            }
        }
    }

    void checkSwipe()
    {
        //Check if Horizontal swipe
        if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                touchedPositionX = 1;
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                touchedPositionX = -1;
            }
            fingerUp = fingerDown;
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    private void ProcessKeybordInput()
    {
        bool keyDownLeft = Input.GetKey(KeyCode.A);
        bool keyDownRight = Input.GetKey(KeyCode.D);

        if (keyDownLeft || keyDownRight)
        {
            isTouching = true;            
        } 
        else
        {
            isTouching = false;
        }

        if (keyDownLeft)
        {
            touchedPositionX = -1;
        }

        if (keyDownRight)
        {
            touchedPositionX = 1;
        }
    }
}
