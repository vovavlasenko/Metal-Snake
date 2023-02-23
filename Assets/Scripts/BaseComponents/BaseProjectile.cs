using UnityEngine;
using Services.Pause;

namespace Game
{
    public abstract class BaseProjectile : MonoBehaviour, IPause
    {
        private int damage;
        private PauseManager pauseManager;


        public virtual void SetRotationParams(float rotationSpeed, float rotationError)
        {

        }

        public virtual void SetTarget(Transform target)
        {

        }

        public virtual void SetSpeed(float speed)
        {

        }

        

        public void InitBase(PauseManager pauseManager, int damage)
        {
            this.damage = damage;
            this.pauseManager = pauseManager;
            pauseManager.Register(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IHealth>(out IHealth health))
            {
                health.TakeDamage(damage);
            }          

            DestroyBullet();
        }

        protected virtual void DestroyBullet()
        {
            pauseManager.Unregister(this);         
            Destroy(gameObject);          
        }

        public virtual void PauseOff()
        {
            
        }

        public virtual void PauseOn()
        {
            
        }
    }
}
