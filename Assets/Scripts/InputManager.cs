using TMPro;
using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // Event to pass movement direction
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();  // Event to pass movement direction
    // Event triggered when Space is pressed (for jump)
    public UnityEvent OnSpacePressed = new UnityEvent();
    // Event triggered when right mouse button is pressed (for dash)
    public UnityEvent OnDash = new UnityEvent();

    // Update is called once per frame
    void Update()
    {        
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            input += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            input += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input += Vector3.back;
        }
        OnMove?.Invoke(input);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnDash?.Invoke();
        }
    }
}
