using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class PlayerBulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject playerBulletPrefab;

        public IObjectPool<Projectile> PlayerBullets;

        private void Awake()
        {
            PlayerBullets = new ObjectPool<Projectile>(CreatePlayerBullet, GetPlayerBullet, ReleasePlayerBullet);
        }

        private Projectile CreatePlayerBullet()
        {
            Projectile bullet = Instantiate(playerBulletPrefab, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
            bullet.SetPool(PlayerBullets);
            return bullet;
        }

        private void GetPlayerBullet(Projectile bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void ReleasePlayerBullet(Projectile bullet)
        {
            bullet.gameObject.SetActive(false);
        }

    }
}
