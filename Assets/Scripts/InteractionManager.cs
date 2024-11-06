using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public CameraManager camManager;
    public Camera playerCam;
    public int maxRayDistance;
    public UIManager uiManager;

    [SerializeField] private GameObject target;

    private Interactable targetInteractable;

    public bool interactionPossible;
    private Vector3 hitPosition;
    private void Awake()
    {
        playerCam = camManager.playerCamera;
        uiManager = FindObjectOfType<UIManager>();
        camManager = FindObjectOfType<CameraManager>();
    }
    void Update()
    {
        interactionPossible = target != null;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, maxRayDistance))
        {
            if (hit.transform.gameObject.tag == "Interactable")
            {
                Debug.Log("Looking at " + hit.transform.gameObject.name);
                target = hit.transform.gameObject;
                targetInteractable = target.GetComponent<Interactable>();
                hitPosition = hit.point;
            }
            else
                Debug.Log("Looking at " + hit.transform.gameObject.name);
        }
        else
        {
            target = null;
            targetInteractable = null;
        }

        SetGameplayMessage();
    }

    public void Interact()
    {
        switch (targetInteractable.type)
        {
            case Interactable.InteractionType.Door:
                {
                    target.SetActive(false);
                    break;
                }
            case Interactable.InteractionType.Button:
                {
                    targetInteractable.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;
                }
            case Interactable.InteractionType.Pickup:
                {
                    targetInteractable.gameObject.transform.position = hitPosition;
                    break;
                }
            default:
                break;
        }
    }

    void SetGameplayMessage()
    {
        string message = string.Empty;

        if (target != null)
        {
            switch (targetInteractable.type)
            {
                case Interactable.InteractionType.Door:
                    {
                        message = "Press LMB to open door";
                        break;
                    }
                case Interactable.InteractionType.Button:
                    {
                        message = "Press LMB to press the button";
                        break;
                    }
                case Interactable.InteractionType.Pickup:
                    {
                        message = "Press LMB to pickup this object";
                        break;
                    }
            }
        }

        uiManager.UpdateGameplayMessage(message);
    }
}
