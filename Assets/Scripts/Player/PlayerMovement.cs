using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RogueDescent
{
    public class PlayerMovement : MonoBehaviour
    {
        //config
        [SerializeField] private ContactFilter2D _movementFilter;
        
        //component references
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;

        private Player _player;
        //private
        private List<RaycastHit2D> _hits;
        public Vector2 FacingDirection => _facingDirection;
        private Vector2 _facingDirection;

        public Transform FacingTransform => _facingTransform;
        [SerializeField] private Transform _facingTransform;
        private void Awake()
        {
            _player = GetComponent<Player>();
            _hits = new List<RaycastHit2D>(3);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            _movement = direction;
        }

        private void FixedUpdate()
        {
           // MovePositionTick();
           var desiredVelocity = _movement * (_player.Stats.speed);
           if (_movement.sqrMagnitude > Mathf.Epsilon)
           {
               _facingDirection = _movement.normalized;
               _facingTransform.rotation = Quaternion.FromToRotation(Vector3.right, _facingDirection);
           }
           _rigidbody.velocity = desiredVelocity;
        }

        private void MovePositionTick()
        {
            var desiredMovement = _movement * (_player.Stats.speed * Time.fixedDeltaTime);
            var distance = desiredMovement.magnitude;
            var direction = desiredMovement.normalized;
            int hitCount = _rigidbody.Cast(direction, _movementFilter, _hits, distance);
            if (hitCount > 0)
            {
                for (var index = 0; index < hitCount; index++)
                {
                    var hit = _hits[index];
                    
                    //here, we just want to remove the normal from the collision
                    if (Vector2.Angle(direction, hit.normal) > 90)
                    {
                        distance = Mathf.Min(distance, hit.distance);
                        //if we're in a thing.
                        if (distance <= Mathf.Epsilon)
                        {
                            //leave
                            break;
                        }
                    }
                }
            }

            //if not zero. Epsilon is just a very small number.
            if (distance > Mathf.Epsilon)
            {
                _rigidbody.MovePosition(_rigidbody.position + direction * distance);
            }
        }

        //Jump in last faced direction this distance.
        public void Dash(Vector2 direction, float distance)
        {
            //see movePositionTick for raycast jump code.
            int hitCount = _rigidbody.Cast(direction, _movementFilter, _hits, distance);
            if (hitCount > 0)
            {
                for (var index = 0; index < hitCount; index++)
                {
                    var hit = _hits[index];

                    //here, we just want to remove the normal from the collision
                    if (Vector2.Angle(direction, hit.normal) > 90)
                    {
                        distance = Mathf.Min(distance, hit.distance);
                        //if we're in a thing.
                        if (distance <= Mathf.Epsilon)
                        {
                            //leave
                            break;
                        }
                    }
                }
            }

            //if not zero. Epsilon is just a very small number.
            if (distance > Mathf.Epsilon)
            {
                _rigidbody.MovePosition(_rigidbody.position + direction * distance);
            }
        }
    }
}
