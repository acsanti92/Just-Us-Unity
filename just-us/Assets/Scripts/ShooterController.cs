using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ShooterController : MonoBehaviour
{
    // Camera that allows the player to aim
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    // Sensitivity values for normal and aim mode
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    // Third person controller script
    private ThirdPersonController thirdPersonController;
    // Player input from the StarterAssetsInputs script
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            // Enable the aim virtual camera
            aimVirtualCamera.gameObject.SetActive(true);
            // Set the sensitivity of the third person controller
            thirdPersonController.SetSensitivity(aimSensitivity);
        }
        else
        {
            // Disable the aim virtual camera
            aimVirtualCamera.gameObject.SetActive(false);
            // Set the sensitivity of the third person controller
            thirdPersonController.SetSensitivity(normalSensitivity);
        }
    }
}
