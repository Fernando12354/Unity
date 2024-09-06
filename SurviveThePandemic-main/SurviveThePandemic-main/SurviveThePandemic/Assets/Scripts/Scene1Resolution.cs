using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(3840, 2160, FullScreenMode.FullScreenWindow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
