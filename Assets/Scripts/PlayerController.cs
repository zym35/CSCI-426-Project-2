using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform beam;
    public float horizontalSpeed, beamFallingSpeed, beamControlSpeed;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveWheel();
    }

    private void MoveWheel()
    {
        var input = Input.GetAxis("Horizontal");
        _rb.AddForce(Vector2.right * input * horizontalSpeed * Time.deltaTime);
    }
}
