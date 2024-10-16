using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] CameraManager cameraManager;
    Camera playerCamera;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask cubeLayerMask;
    // [SerializeField] Vector3 
    void Start()
    {
        playerCamera = cameraManager.playerCamera;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hitDistance = 10;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, hitDistance, groundLayerMask.value))
        {
            hitDistance = hit.distance;
        }

        RaycastHit[] hits = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, hitDistance, cubeLayerMask.value);

        foreach(RaycastHit currentHit in hits)
        {
            if (currentHit.collider.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.blue;
            }
        }


    }


}
