using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _jumpForce = 400f;
        [SerializeField] private bool _airControl = false;

        private static Animator Animator;
        private static Rigidbody2D Rigidbody2D;
        private static Collider2D Collider2D;

        private bool _isFacingRight;
        private bool _isGrounded;
        private bool _isJump;
        private float _move;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (!_isJump)
            {
                _isJump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        private void FixedUpdate()
        {
            _isGrounded = false;


            if (Collider2D.gameObject == gameObject)
                _isGrounded = true;

            Animator.SetBool("Ground", _isGrounded);

            Animator.SetFloat("vSpeed", Rigidbody2D.velocity.y);

            Move();

            _isJump = false;
        }


        public void Move()
        {
            _move = CrossPlatformInputManager.GetAxis("Horizontal");

            if (_isGrounded || _airControl)
            {
                Animator.SetFloat("Speed", Mathf.Abs(_move));

                Rigidbody2D.velocity = new Vector2(_move * _maxSpeed, Rigidbody2D.velocity.y);

                if (_move > 0 && _isFacingRight)
                {
                    Flip();
                }
                else if (_move < 0 && !_isFacingRight)
                {
                    Flip();
                }
            }
            if (_isGrounded && _isJump && Animator.GetBool("Ground"))
            {
                _isGrounded = false;
                Animator.SetBool("Ground", false);
                Rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
            }
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
