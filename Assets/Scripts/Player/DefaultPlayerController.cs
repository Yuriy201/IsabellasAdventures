using UnityEngine;

namespace Player
{
    public class DefaultPlayerController : PlayerController
    {
        protected override void Walk()
        {
            _speed = _inputHandler.Sprinting ? _sprintSpeed : _walkSpeed;

            _rb.velocity = new Vector2(_inputHandler.Directon.x * _speed, _rb.velocity.y);
            _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
        }

        protected override void FaceRotation()
        {
            if (_rb.velocity.x < 0 && _canRotate == false)
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
            _animator.SetTrigger("Jump");
        }
        private void ResetJumpTrigger()
        {
            _animator.ResetTrigger("Jump");
        }
        protected override void Fire()
        {
            if (_canShoot)
            {
                GameObject newBullet = ObjectPool.Instance.GetObject(_bulletPrefab, _shootPoint.transform);
                newBullet.transform.rotation = _shootPoint.rotation;
                newBullet.GetComponent<Bullet>().ApplyVelocity();

                ObjectPool.Instance.ReternObject(newBullet, 2f);

                _animator.SetTrigger("Shoot");
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

                    _animator.SetTrigger("Shoot");
                    StartCoroutine(ReloadFire());
                }
            }
        }
    }
}