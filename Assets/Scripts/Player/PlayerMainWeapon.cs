using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services.Pause;
using Game;

public class PlayerMainWeapon : BaseWeapon, IPause
{
	[SerializeField] private Transform pointSpawnProjectiles;
	[SerializeField] private WeaponData weaponData;
	[SerializeField] private Transform parentTransform;
	[SerializeField] private AdditionalWeapon additionalWeapon;
	private Transform target;
	private PauseManager pauseManager;
	private bool isActive = false;
	float timer = 0;

	[SerializeField] private WeaponData weapon101Data;
	[SerializeField] private WeaponData weapon201Data;
	[SerializeField] private WeaponData weapon301Data;
	[SerializeField] private AudioManager audioManager;
	[SerializeField] private AudioSource source;
    [SerializeField] private Animator anim;

	[SerializeField] private PlayerBulletPool playerBulletPoolScript;

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
		if (!isActive)
		{
			isActive = true;
		}
	}

	public Transform GetTarget()
    {
		return target;
    }

	public float GetMaxRange()
	{
		return weaponData.MaxRange;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
        Attack();

		if (isActive)
		{
			if (target == null)
			{
				additionalWeapon.doubleTap = false;

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
		Projectile p = playerBulletPoolScript.PlayerBullets.Get();
		p.gameObject.transform.position = pointSpawnProjectiles.position;
		p.gameObject.transform.rotation = pointSpawnProjectiles.rotation;
		p.InitBase(pauseManager, weaponData.Damage);
		p.SetSpeed(weaponData.SpeedProjectile);
		PlayShotSound();
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

	private void PlayShotSound()
	{
		if (weaponData == weapon101Data)
			audioManager.PlaySound(AudioManager.Sound.ShotMain_101, source);
		if (weaponData == weapon201Data)
			audioManager.PlaySound(AudioManager.Sound.ShotMain_201, source);
		if (weaponData == weapon301Data)
			audioManager.PlaySound(AudioManager.Sound.ShotMain_301, source);
	}

}
