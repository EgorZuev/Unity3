using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _jumpForce = 400f;
        [SerializeField] private int _coins = 0;

        private static Animator Animator;
        private static Rigidbody2D Rigidbody2D;
        private static Collider2D Collider2D;

        private bool _isFacingRight;
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

            _move = CrossPlatformInputManager.GetAxis("Horizontal");

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

            if (_isJump)
            {
                Rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
                _isJump = false;
            }
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void AddCoin()
        {
            _coins++;
        }
    }
}
