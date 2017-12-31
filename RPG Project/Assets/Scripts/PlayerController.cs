﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    Camera cam;
    PlayerMotor motor;

    public Interactable focus;
    public LayerMask movementMask;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,100,movementMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
                // move player to hit location

            // stop focusing object
            }

        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
                
            }

        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();
             
            focus = newFocus;
            motor.FollowTarget(newFocus);

        }
       
        newFocus.OnFocused(transform);
       

    }

    void RemoveFocus()
    {
        if(focus !=null)

        focus.OnDeFocused();
        focus = null;
        motor.StopFollowTarget();
    }

   
}
