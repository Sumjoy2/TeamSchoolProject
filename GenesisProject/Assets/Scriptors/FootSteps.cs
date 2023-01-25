using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource footstepsSound;
    float horizontal;
    float vertical;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if ( horizontal !=0 || vertical != 0)
        {
            footstepsSound.enabled = true;
            //Debug.Log("Sonic\n");
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }


}
