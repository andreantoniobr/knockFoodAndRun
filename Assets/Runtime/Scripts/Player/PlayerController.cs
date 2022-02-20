using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private float forwardSpeedZ = 10f;
    [SerializeField] private float horizontalSpeedX = 3f;
    [SerializeField] private float maxDistanceX = 3f;

    private bool isDeath = false;

    private bool isRunning = false;
    public bool IsRunning => isRunning;

    private bool isProcessingInput = true;

    private Vector3 initialPosition;
    private float targetPositionX;

    private float MaxLeftDistance => initialPosition.x - maxDistanceX;
    private float MaxRightDistance => initialPosition.x + maxDistanceX;

    public event Action<bool> OnRunEvent;

    private void Awake()
    {
        initialPosition = transform.position;
        GetPlayerInputControllerComponent();
        PlayerCollisionController.OnDeathEvent += OnDeath;
        PlayerCollisionController.OnFinishEvent += OnFinish;
    }

    private void OnDestroy()
    {
        PlayerCollisionController.OnDeathEvent -= OnDeath;
        PlayerCollisionController.OnFinishEvent -= OnFinish;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        if (isProcessingInput && !isDeath)
        {
            ProcessPlayerKeybordInput();
        }
#endif
        if (isProcessingInput && !isDeath)
        {
            ProcessPlayerInput();
        }        

        if (isRunning && !isDeath)
        {
            ProcessPlayerMovement();
        }
    }

    private void OnValidate()
    {
        GetPlayerInputControllerComponent();
    }

    private void ProcessPlayerInput()
    {
        /*
        if (playerInputController.IsTouching && !isRunning)
        {
            OnRun();            
        }

        if (playerInputController.IsTouching && isRunning)
        {
            targetPositionX = transform.position.x + playerInputController.TouchedPositionX;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, MaxLeftDistance, MaxRightDistance);*/

        //return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeedX);

        /*
        if (Input.touchCount > 0)
        {
            if (!isRunning)
            {
                OnRun();
            }

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                targetPositionX = touch.deltaPosition.x * horizontalSpeedX * Time.deltaTime;                
                //transform.Translate(targetPositionX, 0, 0);
            }
        }     */

        if (Input.touchCount > 0)
        {
            if (!isRunning)
            {
                OnRun();
            }

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    targetPositionX = (touch.deltaPosition.x / Screen.width ) * horizontalSpeedX * Time.fixedDeltaTime / touch.deltaTime;
                    break;
                case TouchPhase.Stationary:
                    targetPositionX = 0;
                    break;

                default:
                    targetPositionX = 0;
                    break;
            }
        }
    }

    private void ProcessPlayerKeybordInput()
    {
        bool keyDownLeft = Input.GetKey(KeyCode.A);
        bool keyDownRight = Input.GetKey(KeyCode.D);

        if (keyDownLeft || keyDownRight && !isRunning)
        {
            OnRun();
        }

        if (keyDownLeft)
        {
            targetPositionX = -0.5f * horizontalSpeedX * Time.fixedDeltaTime;
        } else if (keyDownRight)
        {
            targetPositionX = 0.5f * horizontalSpeedX * Time.fixedDeltaTime;
        }
        else
        {
            targetPositionX = 0;
        }

        /*MOUSE*/
        /*
        if (Input.GetMouseButton(0))
        {
            if (!isRunning)
            {
                OnRun();
            }

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPositionX = objPosition.x;
        }*/

        //targetPositionX = Mathf.Clamp(targetPositionX, MaxLeftDistance, MaxRightDistance);
    }

    private void OnRun()
    {
        isRunning = true;
        OnRunEvent?.Invoke(true);
    }

    private void OnStop()
    {
        isRunning = false;
        isProcessingInput = false;
        OnRunEvent?.Invoke(false);
    }

    private void ProcessPlayerMovement()
    {
        Vector3 currentPosition = transform.position;
        
        currentPosition.x += ProcessHorizontalMovement();
        currentPosition.x = Mathf.Clamp(currentPosition.x, MaxLeftDistance, MaxRightDistance);
        
        currentPosition.z += forwardSpeedZ * Time.fixedDeltaTime;
        transform.position = currentPosition;
    }

    private float ProcessHorizontalMovement()
    {
        //return Mathf.Lerp(transform.position.x, targetPositionX, horizontalSpeedX * Time.deltaTime);
        //argetPositionX = Mathf.Clamp(targetPositionX, MaxLeftDistance, MaxRightDistance);
        return targetPositionX * horizontalSpeedX * Time.fixedDeltaTime;       
    }

    private void GetPlayerInputControllerComponent()
    {
        if (!playerInputController)
        {
            playerInputController = GetComponent<PlayerInputController>();
        }
    }

    private void OnDeath(Vector3 position)
    {
        isDeath = true;
        Destroy(gameObject);
    }

    private void OnFinish()
    {
        StartCoroutine(OnfinishCoroutine());
    }

    private IEnumerator OnfinishCoroutine()
    {
        yield return new WaitForSeconds(1f);
        OnStop();
    }
}
