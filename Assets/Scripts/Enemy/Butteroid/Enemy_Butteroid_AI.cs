using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Butteroid_AI : MonoBehaviour
{
    public float speed = 3;
    public Animator animator;
    // Start is called before the first frame update
    public float roamDistance = 0.0f;
    public float roamDirectionX = 0.0f;
    public float roamDirectionY = 0.0f;
    public  double roamTriggerProbability = 0.001;

    Rigidbody2D rigidbody;

    System.Random randomGenerator;

    void Start()
    {
        animator = GetComponent<Animator>();
        randomGenerator = new System.Random();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    bool movementHandler()
    {
        animator.SetBool("IsWalking", false);
        if(roamDistance > 0.0f)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("HBlend", roamDirectionX);
            animator.SetFloat("VBlend", roamDirectionY);
            float h = roamDirectionX / (float)Math.Sqrt(roamDirectionX * roamDirectionX + roamDirectionY * roamDirectionY);
            float v = roamDirectionY / (float)Math.Sqrt(roamDirectionX * roamDirectionX + roamDirectionY * roamDirectionY);
            float moveDistance = speed * Time.deltaTime;
            moveDistance = Math.Min(moveDistance, roamDistance);

            float minX = Camera.main.ViewportToWorldPoint(Vector2.zero).x;
            float maxX = Camera.main.ViewportToWorldPoint(Vector2.one).x;
            float minY = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
            float maxY = Camera.main.ViewportToWorldPoint(Vector2.one).y;
            float roamX = h * moveDistance;
            float roamY = v * moveDistance;

            if(transform.position.x  + roamX < minX || transform.position.x + roamX > maxX || transform.position.y + roamY < minY || transform.position.y + roamY > maxY)
            {
                roamDirectionX = 0;
                roamDirectionY = 0;
                roamDistance = 0;
                return true;
            }


           transform.Translate(roamX, roamY, 0);
            roamDistance -= moveDistance;

            if (roamDistance <= 0.0f)
            {
                roamDirectionX = 0;
                roamDirectionY = 0;
                roamDistance = 0;
                return true;
            }
            return false;
        }

        return true; 
    }

    void roamHandler()
    {

        if(randomGenerator.NextDouble() <= roamTriggerProbability)
        {
            
            roamDistance = randomGenerator.Next(4, 20);
            roamDirectionX = (float)randomGenerator.NextDouble() * 2.0f - 1.0f;
            roamDirectionY = (float)randomGenerator.NextDouble() * 2.0f - 1.0f;

            if (roamDirectionX == roamDirectionY && roamDirectionX == 0) roamDistance = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision 2d Occured!");
    }
    // Update is called once per frame
    void Update()
    {
        bool finishedRoam = movementHandler();

        if (finishedRoam)
        {
            roamHandler();            
        }
        
    }
}
