using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManRectangleMovement : MonoBehaviour
{
    public float speed = 0.5f; // Units per second
    public Animator pacmanAnimatorController; // Assign in Inspector
    public AudioSource moveAudio; // Assign in Inspector

    // Hard-coded rectangle corners
    private Vector3[] corners;
    private int currentCorner = 0;
    private int nextCorner = 1;
    private float t = 0f;
    private float segmentDuration;

    // Direction triggers
    private string[] directionTriggers = { "GoRight", "GoDown", "GoLeft", "GoUp" };

    void Start()
    {
        // Set up the rectangle corners
        // Example: grid size 1 unit, rectangle 5 units wide, 3 units tall
        Vector3 start = transform.position; // Top-left
        corners = new Vector3[4];
        corners[0] = start; // Top-left
        corners[1] = start + new Vector3(5, 0, 0); // Top-right
        corners[2] = start + new Vector3(5, -4, 0); // Bottom-right
        corners[3] = start + new Vector3(0, -4, 0); // Bottom-left

        // Calculate duration for the first segment
        segmentDuration = Vector3.Distance(corners[currentCorner], corners[nextCorner]) / speed;

        // Set initial direction animation
        SetDirectionAnimation();

        // Play movement audio
        if (moveAudio != null) moveAudio.Play();
    }

    void Update()
    {
        // t += Time.deltaTime / segmentDuration;
        // // Debug.Log("Time trigger: " + t);
        // transform.position = Vector3.Lerp(corners[currentCorner], corners[nextCorner], t);
        //
        // if (t >= 1f)
        // {
        //     // Move to next segment
        //     currentCorner = nextCorner;
        //     nextCorner = (nextCorner + 1) % corners.Length;
        //     t = 0f;
        //     segmentDuration = Vector3.Distance(corners[currentCorner], corners[nextCorner]) / speed;
        //     SetDirectionAnimation();
        // }
    }

    void SetDirectionAnimation()
    {
        // Reset all triggers first (optional, but recommended)
        foreach (var trigger in directionTriggers)
            pacmanAnimatorController.ResetTrigger(trigger);

        // Set the trigger for the current direction
        Debug.Log("Setting trigger: " + directionTriggers[currentCorner]);
        pacmanAnimatorController.SetTrigger(directionTriggers[currentCorner]);
    }
}
