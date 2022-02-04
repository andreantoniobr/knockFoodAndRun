using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (animator && playerController)
        {
            animator.SetBool(PlayerAnimatorConstants.IsRunning, playerController.IsRunning);
        }
    }
}
