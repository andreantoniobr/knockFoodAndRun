using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerController.OnRunEvent += OnRun;
    }

    private void OnDestroy()
    {
        playerController.OnRunEvent -= OnRun;
    }

    private void OnRun(bool isRunning)
    {
        if (animator && playerController)
        {
            animator.SetBool(PlayerAnimatorConstants.IsRunning, isRunning);
        }
    }


    /*
    private void Update()
    {
        SetAnimatorRun();
    }*/
}
