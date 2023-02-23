using UnityEngine;
using System.Collections;
using Services.Pause;

namespace Game
{
    public class Chainsaw : BaseWeapon, IHealth, IPause
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private MainEnemy mainEnemy;
        [SerializeField] private Collider2D weaponCollider;
        private IHealth health = null;
        private PauseManager pauseManager;

        private void OnDestroy()
        {
            if (pauseManager != null)
            {
                pauseManager.Unregister(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(ConstantVariables.TAG_CARRIAGE))
            {
                health = collision.gameObject.GetComponent<IHealth>();
                StartCoroutine(DealDamage());
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(ConstantVariables.TAG_CARRIAGE))
            {
                StopCoroutine(DealDamage());
                health = null;
            }
        }

        private IEnumerator DealDamage()
        {
            while (health != null)
            {
                health.TakeDamage(weaponData.Damage);
                yield return new WaitForSeconds(weaponData.TimeBeetwenFire);
            }
        }

        public void TakeDamage(int damage)
        {
            mainEnemy.TakeDamage(damage);
        }

        public void PauseOn()
        {
            weaponCollider.enabled = false;
        }

        public void PauseOff()
        {
            weaponCollider.enabled = true;
        }

        public override void SetPauseManager(PauseManager pauseManager)
        {
            this.pauseManager = pauseManager;
            pauseManager.Register(this);
        }
    }
}
