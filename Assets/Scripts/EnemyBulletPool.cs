using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class EnemyBulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject enemyBulletPrefab;

        public IObjectPool<Projectile> EnemyBullets;


        private void Awake()
        {
            EnemyBullets = new ObjectPool<Projectile>(CreateEnemyBullet, GetEnemyBullet, ReleaseEnemyBullet);
        }
 
        private Projectile CreateEnemyBullet()
        {
            Projectile enemyBullet = Instantiate(enemyBulletPrefab, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
            enemyBullet.SetPool(EnemyBullets);
            return enemyBullet;
        }

        public void GetEnemyBullet(Projectile bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        public void ReleaseEnemyBullet(Projectile bullet)
        {
            bullet.gameObject.SetActive(false);
        }

    }
}
