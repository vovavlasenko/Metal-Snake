using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Services.Pause;

namespace Game
{
	public class Projectile : BaseProjectile
	{
		private Tween tween;
        private IObjectPool<Projectile> pool;
        private bool isReleased;

        private void OnEnable()
        {
            isReleased = false;
        }

        public void SetPool(IObjectPool<Projectile> poolToSet)
        {
            pool = poolToSet;
        }

        public override void SetSpeed(float speed)
        {
            tween = transform.DOMove(transform.position + transform.up * speed * ConstantVariables.BULLET_TIME_TO_DESTROY, ConstantVariables.BULLET_TIME_TO_DESTROY)
            .SetEase(Ease.Linear)
			.OnComplete(DestroyBullet);
		}

        protected override void DestroyBullet()
        {
			tween.Kill();
            if (!isReleased)
            {
                pool.Release(this);
                isReleased = true;
            }
        }

        public override void PauseOn()
        {
			tween.Pause();
        }

        public override void PauseOff()
        {
			tween.Play();
		}
    }
}
