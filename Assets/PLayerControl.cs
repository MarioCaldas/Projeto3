﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    Vector3 camForward, move, moveInput;
    float forwarAmount;
    float turnAmount;

    public Transform Axe;


    private Vector3 moveVelocity;
    public float moveSpeed;


    private Camera mainCamera;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetupAnimator();
        //cam = Camera.main.transform;

        mainCamera = FindObjectOfType<Camera>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
        MovementTopDown();
        UpdateAnimator();
        ConvertMoveInput();

        // FARMAR!!!
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Farming");
        }

    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwarAmount = localMove.z;
    }

    void MovementTopDown()
    {
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            moveInput = transform.TransformDirection(moveInput);

            moveVelocity = moveInput * moveSpeed;         

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLenght;
           
            if (groundPlane.Raycast(cameraRay, out rayLenght))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
                //Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

                transform.LookAt(new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
                //transform.TransformDirection(moveVelocity);
            }
        }

    }
   
    void UpdateAnimator()
    {
        if (animator.GetBool("Gun") == true)
        {
            animator.SetBool("Sword", false);
            animator.SetFloat("Forward", forwarAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        }

        if (animator.GetBool("Sword") == true)
        {
            animator.SetBool("Gun", false);
            animator.SetFloat("ForwardS", forwarAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("TurnS", turnAmount, 0.1f, Time.deltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            animator.SetBool("Sword", true);
            animator.SetBool("Gun", false);
            Axe.gameObject.SetActive(true);
            
        }
        if (Input.GetKeyDown("2"))
        {
            animator.SetBool("Gun", true);
            animator.SetBool("Sword", false);
            Axe.gameObject.SetActive(false);
        }

    }

    void SetupAnimator()
    {
        animator = GetComponent<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != animator)
            {
                animator.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }

}
