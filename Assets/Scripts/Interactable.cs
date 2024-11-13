using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractionType { Door, Button, Pickup}

    public InteractionType type;

    [SerializeField] private PickupScriptable pickupsScriptable;
    public void Activate()
    {
        Debug.Log(this.name + " was activated");

    }

    public void OnInteraction()
    {
        if (pickupsScriptable)
        pickupsScriptable.DrawDebugInfo();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
