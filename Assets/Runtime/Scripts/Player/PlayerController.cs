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

    private bool isRunning = false;
    public bool IsRunning => isRunning;

    private Vector3 initialPosition;
    private float targetPositionX;

    private float MaxLeftDistance => initialPosition.x - maxDistanceX;
    private float MaxRightDistance => initialPosition.x + maxDistanceX;

    private void Awake()
    {
        initialPosition = transform.position;
        GetPlayerInputControllerComponent();
    }    

    private void Update()
    {
        ProcessPlayerKeybordInput();

        if (isRunning)
        {
            ProcessPlayerMovement();
        }       
    }

    private void ProcessPlayerKeybordInput()
    {
        bool keyDownLeft = Input.GetKey(KeyCode.A);
        bool keyDownRight = Input.GetKey(KeyCode.D);

        if (keyDownLeft || keyDownRight && !isRunning)
        {
            isRunning = true;
        }

        if (keyDownLeft)
        {
            targetPositionX = transform.position.x - 2f;
        }

        if (keyDownRight)
        {
            targetPositionX = transform.position.x + 2f;
        }

        if (Input.GetMouseButton(0))
        {
            if (!isRunning)
            {
                isRunning = true;
            }

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPositionX = objPosition.x;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, MaxLeftDistance, MaxRightDistance);
    }

    private void OnValidate()
    {
        GetPlayerInputControllerComponent();
    }

    private void ProcessPlayerMovement()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = ProcessHorizontalMovement();
        currentPosition.z += forwardSpeedZ * Time.deltaTime;
        transform.position = currentPosition;
    }

    private float ProcessHorizontalMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeedX);
    }

    private void GetPlayerInputControllerComponent()
    {
        if (!playerInputController)
        {
            playerInputController = GetComponent<PlayerInputController>();
        }
    }
}
