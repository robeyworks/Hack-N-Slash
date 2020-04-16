﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Dash (shift)
 * Movement
 * Power jetpack (space)
 * Roadhog hook (right-click)           make it not collide with the player. Search "CHANGE THIS"
 * 
 * 
 * https://www.youtube.com/watch?v=LNLVOjbrQj4
 * https://www.youtube.com/watch?v=THnivyG0Mvo
 * https://www.youtube.com/watch?v=8uD2ATIot0A
 * 
 * Maybe dust particles
*/

public class PlayerController : MonoBehaviour
{
    CharacterController player;
    public Transform cam;
    private Vector3 offset = new Vector3(0f, 7f, -5f);
    private float speed = 5f;
    private float gravity = -20f;
    private float jumpImpulse = 10f;
    private Vector3 velocity = Vector3.zero;
    private float speedY = 0;
    private float v;
    private float h;

    public GameObject spawnDashParticles;
    private float dashTimer;
    private bool invulnerable = false;

    private bool hookOut = false;
    public Rigidbody hook;
    public float hookSpeed = 20;

    //public Camera playerCam;
    //private Vector3 mousePos;

    private Vector3 intersection;
    private Vector3 diff;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        dashTimer = 4f;
    }
    
    // Update is called once per frame
    void Update()
    {
        //dash once every three seconds, for .25 seconds
        dashTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer > 3f) dashTimer = 0f;
        if (dashTimer < .25f)
        {
            GameObject trail = (GameObject)Instantiate(spawnDashParticles, transform.position, transform.rotation);
        }
    }

    // FixedUpdate is called once per physics calculation
    void FixedUpdate()
    {
        //Rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0;

        if (plane.Raycast(ray, out distance))
        {
            intersection = ray.GetPoint(distance);
            diff = intersection - transform.position;

            float angle = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 90 - angle, 0);
        }

        //mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);

        //Move
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        velocity = new Vector3(h * speed, speedY, v * speed);
        
        //float backDirection = sin(velocity) * -1f;

        //if player is on ground, they can jump and spawn particles while moving
        if (player.isGrounded)
        {
            if (Input.GetButton("Jump")) speedY = jumpImpulse;
        }

        //dash once every three seconds, for .25 seconds
        /*dashTimer += Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer > 3f) dashTimer = 0f;
        if(dashTimer < .25f)
        {
            GameObject trail = (GameObject)Instantiate(spawnDashParticles, transform.position, transform.rotation);
            velocity.x *= 5;
            velocity.z *= 5;
        }*/

        //movement for dash is done by physics calculation, not frames
        if (dashTimer < .25f)
        {
            velocity.x *= 5;
            velocity.z *= 5;
            invulnerable = true;
        }
        else invulnerable = false;

        //hook
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Pressed right click.");
            hookOut = true;
            Vector3 hookSpawn = new Vector3((diff.x / Mathf.Abs(diff.x)) * .5f, 0f, (diff.z / Mathf.Abs(diff.z)) * .5f);
            Rigidbody instantiatedProjectile = Instantiate(hook, transform.position + hookSpawn, Quaternion.identity) as Rigidbody;//CHANGE THIS    transform.position + the distance so it doesn't spawn in player
            //Vector3 aim = diff;
            //aim.Normalize();
            //instantiatedProjectile.velocity = new Vector3(aim.x * hookSpeed, 0, aim.z * hookSpeed);
            //Debug.Log(instantiatedProjectile.velocity);
            //instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, hookSpeed));

            //turn off collision with player
            //Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
            

            /*if (Physics.Raycast(ray))
            {
                Vector3 hookDirection = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
                hookDirection.Normalize();
                Rigidbody instantiatedProjectile = Instantiate(hook, transform.position, transform.localRotation) as Rigidbody;
                instantiatedProjectile.velocity = transform.TransformDirection(hookDirection * hookSpeed);//(new Vector3(hookSpeed, 0, hookSpeed) * transform.localRotation.y);
                //forward vector of character
            }*/

        }

        speedY += (gravity * Time.deltaTime);
        player.Move(velocity * Time.deltaTime);
        cam.position = transform.position + offset;
        //lastRotation = backDirection;
    }



    void OnTriggerEnter(Collider other)//trigger to collision
    {
        /*if (other.gameObject.CompareTag("Acorn"))
        {
            acorn = 2;
            other.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Pinecone"))
        {
            pointsMult = 2;
            other.gameObject.SetActive(false);//potentially change to destroy
        }*/

        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (!invulnerable)
            {
                //other.gameObject.SetActive(false);
                Debug.Log("Not invulnerable");


                player.Move(new Vector3(-velocity.x * Time.deltaTime * 15f, 0f, -velocity.z * Time.deltaTime * 15f));
            }
            else Debug.Log("Invulnerable");
        }
    }
}