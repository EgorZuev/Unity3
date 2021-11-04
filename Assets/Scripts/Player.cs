using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _jumpForce = 400f;

        private static string Speed = "Speed";

        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private bool _isFacingRight;
        private float _move;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
            }

            if (Input.GetKey(KeyCode.A))
            {
                _move = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _move = 1;
            }
            else
            {
                _move = 0;
            }

            _animator.SetFloat(Speed, Mathf.Abs(_move));

            _rigidbody2D.velocity = new Vector2(_move * _maxSpeed, _rigidbody2D.velocity.y);

            if (_move > 0 && _isFacingRight)
            {
                Flip();
            }
            else if (_move < 0 && !_isFacingRight)
            {
                Flip();
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
