using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyDown_Music : MonoBehaviour
{

    public AudioClip walkSound;
    public AudioClip jumpSound;
    int count = 0;
    AudioSource audioSource;

    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetKey("right") || Input.GetKey("left")) && gameObject.GetComponent<OnKeyPress_MoveGravity>().groundFlag == true)
        {
            if (count == 10)
            {
                audioSource.PlayOneShot(walkSound);
                count = 0;
            }
            else
            {
                count++;
            }
        }
        if (Input.GetKey("space") && gameObject.GetComponent<OnKeyPress_MoveGravity>().groundFlag == true)
        {
            audioSource.PlayOneShot(jumpSound);
        }

    }
}
