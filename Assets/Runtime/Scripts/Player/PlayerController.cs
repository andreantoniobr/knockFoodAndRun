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
        ProcessPlayerKeybordInput();

        if (isRunning && !isDeath)
        {
            ProcessPlayerMovement();
        }       
    }

    private void OnValidate()
    {
        GetPlayerInputControllerComponent();
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
                OnRun();
            }

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPositionX = objPosition.x;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, MaxLeftDistance, MaxRightDistance);
    }

    private void OnRun()
    {
        isRunning = true;
        OnRunEvent?.Invoke(isRunning);
    }

    private void OnStop()
    {
        isRunning = false;
        OnRunEvent?.Invoke(isRunning);
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

    private void OnDeath()
    {
        isDeath = true;
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
