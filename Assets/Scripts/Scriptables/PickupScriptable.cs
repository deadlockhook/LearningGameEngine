using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickups", menuName = "Scriptables interactables")]
public class PickupScriptable : ScriptableObject
{
    public string sometehing = "";
    public void DrawDebugInfo()
    {
        Debug.Log("Log " + sometehing);
    }
}
