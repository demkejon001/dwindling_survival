using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector] public Controls controls;
    [HideInInspector] public Vector2 moveInput;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new Controls();
        controls.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
}
