using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ShooterController : MonoBehaviour
{
    // Camera that allows the player to aim
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;

    // Player input from the StarterAssetsInputs script
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            // Enable the aim virtual camera
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            // Disable the aim virtual camera
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }
}
