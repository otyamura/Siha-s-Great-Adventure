using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dieSiha : MonoBehaviour
{
    public string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (TimerScript.timeEndFlag)
        {
            SceneManager.LoadScene(sceneName);
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

   
}
