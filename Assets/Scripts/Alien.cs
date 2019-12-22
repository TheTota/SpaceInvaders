using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        Debug.Log("TODO: Here play an animation for the death of da boi");
    }
}
