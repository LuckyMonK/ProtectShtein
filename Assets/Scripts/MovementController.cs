using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //private CharacterController controller;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    private Quaternion qTo;
    [SerializeField] private LeanJoystick jstick;
    private void Start()
    {
        jstick = FindObjectOfType<LeanJoystick>();
        rb = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void ProcessMovement() {
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = new Vector3(jstick.ScaledValue.x, 0, jstick.ScaledValue.y);
        if (move != Vector3.zero)
            qTo = Quaternion.LookRotation(move);

        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * speed * 3f);
        rb.velocity = move * speed + new Vector3(0, -10f, 0);
        
        //controller.Move(move * Time.deltaTime * speed);
    }
}
