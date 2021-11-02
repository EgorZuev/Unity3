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
        [SerializeField] private int _coins = 0;

        private static Animator Animator;
        private static Rigidbody2D Rigidbody2D;
        private static Collider2D Collider2D;
        private static string Speed = "Speed";

        private bool _isFacingRight;
        private float _move;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
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

            Animator.SetFloat(Speed, Mathf.Abs(_move));

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

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Coin>(out Coin coin))
            {
                _coins++;
                Destroy(coin.gameObject);
            }
        }
    }
}
