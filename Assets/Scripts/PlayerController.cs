using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform beam;
    public float horizontalSpeed, beamControlSpeed;
    public Rigidbody2D beamRb;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var input = Input.GetAxis("Horizontal");
        MoveWheel(input);
        ControlBeam(input);
    }

    private void MoveWheel(float input)
    {
        _rb.AddForce(input * horizontalSpeed * Time.deltaTime * Vector2.right, ForceMode2D.Impulse);
    }

    private void ControlBeam(float input)
    {
        beamRb.AddTorque(input * beamControlSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
