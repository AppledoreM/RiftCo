using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, moveSpeed * Time.deltaTime, 0); 
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0); 
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0); 
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0); 
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int health = animator.GetInteger("Health") - 10;
            animator.SetInteger("Health", health) ;
            print("Health: " + health.ToString());
           
        }
    }
}
