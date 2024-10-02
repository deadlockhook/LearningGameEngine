using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class anim_controller : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animController;
    public GameObject character;
    public PlayerLocomotionHandler locomotionHandler;
    void Start()
    {
        character.SetActive(true);
    }
    void Update()
    {
       animController.SetFloat("speed", locomotionHandler.playerVelocity);
    }
}
