﻿using UnityEngine;

namespace Player
{
    public class DefaultPlayerController : PlayerController
    {
        [SerializeField] private AnimationClip sprintClip;
        [SerializeField] private AnimationClip idleClip; 

        private bool _isSprinting;

        protected override void Walk()
        {
            _speed = _inputHandler.Sprinting ? _sprintSpeed : _walkSpeed;

            _rb.velocity = new Vector2(_inputHandler.Directon.x * _speed, _rb.velocity.y);
            _animator.SetFloat(SpeedFloatHash, Mathf.Abs(_rb.velocity.x));

            if (Mathf.Abs(_rb.velocity.x) == _sprintSpeed && !_isSprinting)
            {
                _animator.Play(sprintClip.name);
                _isSprinting = true;
            }
            else if ((Mathf.Abs(_rb.velocity.x) != _sprintSpeed && _isSprinting) || _rb.velocity.x == 0)
            {
                _animator.SetFloat(SpeedFloatHash, 0);
                _isSprinting = false;

               
                if (_rb.velocity.x == 0)
                {
                    _animator.Play(idleClip.name);
                }
            }
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

        private void RotateFace(Quaternion degress)
        {
            transform.localRotation = degress;
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
                GameObject newBullet = ObjectPool.Instance.GetObject(_bulletPrefab, _shootPoint.transform);
                newBullet.transform.rotation = _shootPoint.rotation;
                newBullet.GetComponent<Bullet>().ApplyVelocity();

                ObjectPool.Instance.ReternObject(newBullet, 2f);

                _animator.SetTrigger(ShootTriggerHash);
                StartCoroutine(ReloadFire());
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
