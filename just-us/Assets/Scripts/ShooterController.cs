using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    // Camera that allows the player to aim
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    // Sensitivity values for normal and aim mode
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    // Layer mask for the aim collider
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    // debug transform for the aim collider
    [SerializeField] private Transform debugTransform;
    // Bullet projectile prefab
    [SerializeField] private Transform pfBulletProjectile;
    // Spawn position for the bullet
    [SerializeField] private Transform spawnBulletPosition;

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
        // Set the mouse position to the center of the screen
        Vector3 mouseWorldPosition = Vector3.zero;
        // Allows the player to aim
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // Cast a ray from the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        // Raycast to the aim collider layer mask
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            // Enable the aim virtual camera
            aimVirtualCamera.gameObject.SetActive(true);
            // Set the sensitivity of the third person controller
            thirdPersonController.SetSensitivity(aimSensitivity);
            // Disable the rotation of the player when moving
            thirdPersonController.SetRotateOnMove(false);

            // Set the aim target
            Vector3 worldAimTarget = mouseWorldPosition;
            // Set the y position of the aim target to the player's y position
            worldAimTarget.y = transform.position.y;
            // Set the aim direction
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            // Rotate the player to face the aim target
            transform.rotation = Quaternion.LookRotation(aimDirection);
        }
        else
        {
            // Disable the aim virtual camera
            aimVirtualCamera.gameObject.SetActive(false);
            // Set the sensitivity of the third person controller
            thirdPersonController.SetSensitivity(normalSensitivity);
            // Enable the rotation of the player when moving
            thirdPersonController.SetRotateOnMove(true);
        }

        if (starterAssetsInputs.shoot)
        {
            // Get the aim direction
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            // Spawn the bullet projectile
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            // Set the shoot input to false
            starterAssetsInputs.shoot = false;
        }
    }
}
