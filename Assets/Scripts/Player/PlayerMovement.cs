using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;
    [SerializeField] private Rigidbody2D theRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = InputManager.instance.moveInput;

        theRB.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }
}
