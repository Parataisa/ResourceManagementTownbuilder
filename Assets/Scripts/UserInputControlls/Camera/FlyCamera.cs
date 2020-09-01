﻿using UnityEngine;
//https://gamedevacademy.org/unity-rts-camera-tutorial/
public class FlyCamera : MonoBehaviour
    {
    public float moveSpeed;
    public float zoomSpeed;

    public float minZoomDist;
    public float maxZoomDist;

    private Camera cam;

#pragma warning disable IDE0051 // Remove unused private members
    void Awake()
#pragma warning restore IDE0051 // Remove unused private members
        {
        cam = Camera.main;
        }
#pragma warning disable IDE0051 // Remove unused private members
    void Update()
#pragma warning restore IDE0051 // Remove unused private members
        {
        Move();
        Zoom();
        }

    void Move()
        {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 dir = transform.forward * zInput + transform.right * xInput;

        transform.position += dir * moveSpeed * Time.deltaTime;
        }

    void Zoom()
        {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, cam.transform.position);

        if (dist < minZoomDist && scrollInput > 0.0f)
            return;
        else if (dist > maxZoomDist && scrollInput < 0.0f)
            return;

        cam.transform.position += cam.transform.forward * scrollInput * zoomSpeed;
        }
    public void FocusOnPosition(Vector3 pos)
        {
        transform.position = pos;
        }
    }