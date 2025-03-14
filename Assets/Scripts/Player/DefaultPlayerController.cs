﻿using UnityEngine;
using System.Collections;

namespace Player
{
    public class DefaultPlayerController : PlayerController
    {
        [SerializeField] private AnimationClip sprintClip;
        [SerializeField] private AnimationClip idleClip;

        private bool _isSprinting;
        private bool _idlePlayed;
        private Coroutine _idleCoroutine;

        protected override void Walk()
        {
            _speed = _inputHandler.Sprinting ? _sprintSpeed : _walkSpeed;

            _rb.velocity = new Vector2(_inputHandler.Directon.x * _speed, _rb.velocity.y);
            _animator.SetFloat(SpeedFloatHash, Mathf.Abs(_rb.velocity.x));

            if (Mathf.Abs(_rb.velocity.x) == _sprintSpeed && !_isSprinting)
            {
                _animator.Play(sprintClip.name);
                Debug.Log("Sprint");
                _isSprinting = true;
                _idlePlayed = false; // Reset the idle flag when sprinting starts

                // Stop any ongoing idle coroutine since we are sprinting
                if (_idleCoroutine != null)
                {
                    StopCoroutine(_idleCoroutine);
                    _idleCoroutine = null;
                }
            }
            else if ((Mathf.Abs(_rb.velocity.x) != _sprintSpeed && _isSprinting) || _rb.velocity.x == 0)
            {
                _animator.SetFloat(SpeedFloatHash, 0);
                _isSprinting = false;

                if (_rb.velocity.x == 0 && !_idlePlayed)
                {
                    // Start coroutine to play idle animation for 0.3 seconds
                    if (_idleCoroutine != null)
                    {
                        StopCoroutine(_idleCoroutine);
                    }
                    _idleCoroutine = StartCoroutine(PlayIdleAnimation());
                }
            }
        }

        private IEnumerator PlayIdleAnimation()
        {
            _animator.Play(idleClip.name);
            yield return new WaitForSeconds(0.3f);

            _idlePlayed = true; // Mark that idle animation has been played

            // After 0.3 seconds, you can stop the animation or transition to another state.
            _animator.SetFloat(SpeedFloatHash, 0);
        }

        protected override void FaceRotation()
        {
            if (_rb.velocity.x < 0 && !_canRotate)
            {
                RotateFace(Quaternion.Euler(0, 180, 0));
                _canRotate = true;
            }
            else if (_rb.velocity.x > 0 && _canRotate)
            {
                RotateFace(Quaternion.Euler(0, 0, 0));
                _canRotate = false;
            }
        }

        private void RotateFace(Quaternion degrees)
        {
            transform.localRotation = degrees;
        }

        protected override void Jump()
        {
            jumpBufferTimer = _jumpBufferTime;

            if (coyoteTimer > 0f)
            {
                jumpTimer = _jumpTime;

                EnableJumpTrigger();

                coyoteTimer = -1f;
                jumpBufferTimer = -1f;

                return;
            }

            if (currentAirJumps > 0)
            {
                jumpTimer = _jumpTime;

                EnableParticles();
                currentAirJumps--;
                jumpBufferTimer = -1f;

                return;
            }

            ResetJumpTrigger();
        }

        private void EnableParticles()
        {
            _airJumpParticles.Emit(_airJumpParticlesCount);
        }

        private void EnableJumpTrigger()
        {
            _animator.SetTrigger(JumpTriggerHash);
        }

        private void ResetJumpTrigger()
        {
            _animator.ResetTrigger(JumpTriggerHash);
        }

        protected override void Fire()
        {
            if (_canShoot)
            {
                ArrowManager arrowManager = GetComponent<ArrowManager>();
                if (arrowManager.HasArrows())
                {
                    // Use an arrow
                    arrowManager.UseArrow();

                    // Create and shoot the bullet
                    GameObject newBullet = ObjectPool.Instance.GetObject(_bulletPrefab, _shootPoint.transform);
                    newBullet.transform.rotation = _shootPoint.rotation;
                    newBullet.GetComponent<Bullet>().ApplyVelocity();

                    // Play shooting animation
                    _animator.SetTrigger(ShootTriggerHash);

                    // Start reload coroutine
                    StartCoroutine(ReloadFire());
                }
                else
                {
                    Debug.Log("No arrows left!");
                }
            }
        }

        protected override void AltFire()
        {
            if (_canShoot)
            {
                if (Stats.RemoveMana(this))
                {
                    GameObject newBullet = ObjectPool.Instance.GetObject(_altBulletPrefab, _shootPoint.transform);
                    newBullet.transform.rotation = _shootPoint.rotation;
                    newBullet.GetComponent<Bullet>().ApplyVelocity();

                    ObjectPool.Instance.ReternObject(newBullet, 2f);

                    _animator.SetTrigger(ShootTriggerHash);
                    StartCoroutine(ReloadFire());
                }
            }
        }
    }
}
