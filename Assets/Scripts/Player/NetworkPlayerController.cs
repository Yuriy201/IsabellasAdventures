using System.Collections;
using CustomAttributes;
using InputSystem;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(PhotonView))]
    public class NetworkPlayerController : PlayerController
    {
        private PhotonView _photonView;

        protected override void Awake()
        {
            base.Awake();
            _photonView = GetComponent<PhotonView>();
        }
        protected override void Walk()
        {
            _speed = _inputHandler.Sprinting ? _sprintSpeed : _walkSpeed;

            _rb.velocity = new Vector2(_inputHandler.Directon.x * _speed, _rb.velocity.y);
            _animator.SetFloat(SpeedFloatHash, Mathf.Abs(_rb.velocity.x));
        }

        protected override void FaceRotation()
        {
            if (_rb.velocity.x < 0 && _canRotate == false)
            {
                _photonView.RPC(nameof(RotateFace), RpcTarget.AllBuffered, Quaternion.Euler(0, 180, 0));
                _canRotate = true;
            }
            else if (_rb.velocity.x > 0 && _canRotate)
            {
                _photonView.RPC(nameof(RotateFace), RpcTarget.AllBuffered, Quaternion.Euler(0, 0, 0));
                _canRotate = false;
            }
        }

        [PunRPC]
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

                _photonView.RPC(nameof(EnableJumpTrigger), RpcTarget.All);

                coyoteTimer = -1f;
                jumpBufferTimer = -1f;

                return;
            }

            if (currentAirJumps > 0)
            {
                jumpTimer = _jumpTime;

                _photonView.RPC(nameof(EnableParticles), RpcTarget.All);
                currentAirJumps--;
                jumpBufferTimer = -1f;

                return;
            }

            _photonView.RPC(nameof(ResetJumpTrigger), RpcTarget.All);
        }
        [PunRPC]
        private void EnableParticles()
        {
            _airJumpParticles.Emit(_airJumpParticlesCount);
        }
        [PunRPC]
        private void EnableJumpTrigger()
        {
            _animator.SetTrigger(JumpTriggerHash);
        }
        [PunRPC]
        private void ResetJumpTrigger()
        {
            _animator.ResetTrigger(JumpTriggerHash);
        }
        protected override void Fire()
        {
            if (_canShoot)
            {
                _photonView.RPC(nameof(FireRPC), RpcTarget.All);
            }
        }
        [PunRPC]
        private void FireRPC()
        {
            GameObject newBullet = ObjectPool.Instance.GetObject(_bulletPrefab, _shootPoint.transform);
            newBullet.transform.rotation = _shootPoint.rotation;
            newBullet.GetComponent<Bullet>().ApplyVelocity();

            ObjectPool.Instance.ReternObject(newBullet, 2f);

            _animator.SetTrigger(ShootTriggerHash);
            StartCoroutine(ReloadFire());
        }
        protected override void AltFire()
        {
            if (_canShoot)
            {
                if (Stats.RemoveMana(this))
                {
                    _photonView.RPC(nameof(AltFireRPC), RpcTarget.All);
                }
            }
        }
        [PunRPC]
        private void AltFireRPC()
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