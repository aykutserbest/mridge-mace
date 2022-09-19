using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewCharacterController : MonoBehaviour
{
    private PlayerInput _playerInput; 
    private CharacterController _controller;
    private Animator _animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float mobilePlayerSpeed = 0.01f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");


    private Touch _touch;

    private void Start()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();
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

        // joystick - touch

        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 moveJoystick = new Vector3(input.x, 0, input.y);
        _controller.Move(moveJoystick * Time.deltaTime * playerSpeed);
        
        transform.position = new Vector3(transform.position.x , 0.53f, transform.position.z);
        
        if (moveJoystick != Vector3.zero)
        {
            _animator.SetBool(IsRunning, true);
            gameObject.transform.forward = moveJoystick;
        }
        else
        {
            _animator.SetBool(IsRunning,false);
        }
         
         
        
        /*
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                Vector3 moveMobile = new Vector3(
                    transform.position.x + _touch.deltaPosition.x + mobilePlayerSpeed,
                    transform.position.y,
                    transform.position.z + _touch.position.y + mobilePlayerSpeed
                );
                _controller.Move(moveMobile * Time.deltaTime * mobilePlayerSpeed);
            }
        }*/
            
        
        //w-a-s-d
            /*
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
        _controller.Move(playerVelocity * Time.deltaTime);*/
    }
}
