using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public bool isCollided;

    void Start()
    {
        isCollided = false;
    }

    public void OnCollisionStay(UnityEngine.Collision collision)
    {
        isCollided = true;
    } 
    
    void Update()
    {
        if(isCollided)
        {
            Debug.Log("asdf");
        }
    }
}