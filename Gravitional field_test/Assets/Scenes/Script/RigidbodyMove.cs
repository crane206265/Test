using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RigidbodyMove : MonoBehaviour
{
    public GameObject rocket;
    public GameObject mass1;
    public GameObject mass2;

    public Rigidbody rocketrb;
    public Rigidbody mass1rb;
    public Rigidbody mass2rb;

    void Start()
    {
        rocketrb = rocket.GetComponent<Rigidbody>();
        mass1rb = mass1.GetComponent<Rigidbody>();
        mass2rb = mass2.GetComponent<Rigidbody>();
        rocket.transform.position = new Vector3(-10, 0, -20);
        rocketrb.velocity = new Vector3(8, 0, 0);
    }

    void Update()
    {
        float coef = 1.0f;

        Vector3 r1 = rocket.transform.position - mass1.transform.position;
        float r1sqr = r1.sqrMagnitude;
        float m1 = mass1rb.mass;
        Vector3 F1 = -coef*(m1/r1sqr)*r1;
        rocketrb.AddForce(F1, ForceMode.Acceleration);

        Vector3 r2 = rocket.transform.position - mass2.transform.position;
        float r2sqr = r2.sqrMagnitude;
        float m2 = mass2rb.mass;
        Vector3 F2 = -coef*(m2/r2sqr)*r2;
        rocketrb.AddForce(F2, ForceMode.Acceleration);

        Debug.Log(F1);
        Debug.Log(F2);
    }
}
