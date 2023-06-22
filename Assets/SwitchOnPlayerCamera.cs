using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnPlayerCamera : MonoBehaviour
{
    public GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCamera.activeSelf == false)
        {
            playerCamera.SetActive(true);
        }
    }
}
