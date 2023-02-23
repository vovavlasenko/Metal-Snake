using UnityEngine;
using Services.Pause;
using System;

namespace Game
{
	public class Weapon : BaseWeapon, IPause
	{
		[SerializeField] private Transform pointSpawnProjectiles;
		[SerializeField] private WeaponData weaponData;
		[SerializeField] private Transform parentTransform;
		[SerializeField] private Animator anim;
		public event Action onTargetDestroy;
		private Transform target;
		private PauseManager pauseManager;
		private EnemyBulletPool enemyBulletPoolScript;
		private bool isActive = false;
		private float timer = 0;
		private bool eventWasInvoke = false;
	

        private void Awake()
        {
			enemyBulletPoolScript = FindObjectOfType<EnemyBulletPool>();
        }

        public void SetWeaponData(WeaponData newWeaponData)
        {
			weaponData = newWeaponData;
		}

		public override void SetPauseManager(PauseManager pauseManager)
		{
			this.pauseManager = pauseManager;
			pauseManager.Register(this);
		}

		public void SetTarget(Transform target)
		{
			this.target = target;
			eventWasInvoke = false;
			if (!isActive)
			{
				isActive = true;
			}
		}



		public float GetMaxRange()
		{
			return weaponData.MaxRange;
		}

		private void Update()
		{
			if (isActive)
			{
				if (target == null)
				{
					if (!eventWasInvoke)
                    {
						onTargetDestroy?.Invoke();
						eventWasInvoke = true;
					}

					if (IsNeedToRotate(parentTransform.up))
					{						
						RotateToTarget(parentTransform.up);
					}
					else
					{
						if (timer <= 0)
						{
							isActive = false;
						}
					}
				}
				else
				{
					Vector2 aimDirection = target.position - transform.position;
					if (aimDirection.magnitude < weaponData.MaxRange)
					{
						if (IsNeedToRotate(aimDirection))
						{
							RotateToTarget(aimDirection);
						}
						else
						{
							if (aimDirection.magnitude < weaponData.MaxRange)
							{
								if (timer <= 0)
								{
									Attack();
									timer = weaponData.TimeBeetwenFire;
								}
							}
						}
					}
					else
					{
						if (IsNeedToRotate(parentTransform.up))
						{
							RotateToTarget(parentTransform.up);
						}
					}
				}

				if (timer <= weaponData.TimeBeetwenFire && timer > 0)
				{
					timer -= Time.deltaTime;
				}
			}
		}

		private bool IsNeedToRotate(Vector2 secondValue)
		{
			float angle = Vector2.Angle(transform.up, secondValue);
			return angle > weaponData.RotationError;
		}

		private void Attack() 
		{
			//BaseProjectile p = Instantiate(weaponData.Projectile, pointSpawnProjectiles.position, pointSpawnProjectiles.rotation);
			Projectile p;
            p = enemyBulletPoolScript.EnemyBullets.Get();     
            p.gameObject.transform.position = pointSpawnProjectiles.position;
			p.gameObject.transform.rotation = pointSpawnProjectiles.rotation;
            p.InitBase(pauseManager, weaponData.Damage);
            p.SetSpeed(weaponData.SpeedProjectile);



            anim.Play("Shooting", 0, 0.0f);
		}

		private void RotateToTarget(Vector2 aimDirection)
		{
			Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * weaponData.RotationSpeed);
		}

		private void OnDestroy()
		{
			if (pauseManager != null)
			{
				pauseManager.Unregister(this);
			}
		}

		public void PauseOn()
		{
			this.enabled = false;
		}

		public void PauseOff()
		{
			this.enabled = true;
		}
	}
}
