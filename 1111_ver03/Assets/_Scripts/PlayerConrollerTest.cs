using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Urachacha
{
    public class PlayerConrollerTest : MonoBehaviourPun
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float gravity;

        [SerializeField] private bool isGrounded;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundMask;

        private Vector3 moveDirection;
        private float directionY;

        private CharacterController controller;
        private Animator anim;
        
        private void Start()   
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponentInChildren<Animator>();
        }

        private void Update() 
        {
            Move();
        }

        public void Move()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (moveDirection != Vector3.zero)
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            if (isGrounded)
            {
                anim.SetBool("isGrounded", true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }   
            }

            directionY -= gravity * Time.deltaTime;

            moveDirection.y = directionY;
            controller.Move(moveDirection * Time.deltaTime);
        }

        public void Idle()
        {
            moveDirection *= moveSpeed;
            anim.SetFloat("Speed", 0, -0.1f, Time.deltaTime);
        }

        public void Run()
        {
            moveDirection *= moveSpeed;
            anim.SetFloat("Speed", 1, -0.1f, Time.deltaTime);
        }

        public void Jump()
        {
            directionY = jumpSpeed;
            anim.SetTrigger("isJump");
        }
    }
}
