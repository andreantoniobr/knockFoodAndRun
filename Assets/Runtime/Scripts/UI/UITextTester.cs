using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextTester : MonoBehaviour
{
    [SerializeField] private PlayerController playerMovementController;
    [SerializeField] private TextMeshProUGUI text;

    private void LateUpdate()
    {
        if (playerMovementController && text)
        {
            text.text = $"isRunning: {playerMovementController.IsRunning}";
        }
    }
}
