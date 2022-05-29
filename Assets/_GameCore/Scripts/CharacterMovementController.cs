using System;
using UnityEngine;

namespace _GameCore.Scripts
{
   public class CharacterMovementController : MonoBehaviour
   {
      [SerializeField] private Rigidbody rb;
      [SerializeField] private float speed = 5;
      private Vector3 _input;

      private void Update()
      {
         GetInput();
         Look();
      }

      private void FixedUpdate()
      {
         Move();
      }

      void Look()
      {
         if (_input != Vector3.zero)
         {
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative,Vector3.up);

            transform.rotation = rot;
         }
      }

      void GetInput()
      {
         _input = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
      }

      void Move()
      {
         rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
      }
   
   }
}
