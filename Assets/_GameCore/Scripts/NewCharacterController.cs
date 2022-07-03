using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterController : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            _animator.SetBool(IsRunning, true);
            gameObject.transform.forward = move;
        }
        else
        {
            _animator.SetBool(IsRunning,false);
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }
}
