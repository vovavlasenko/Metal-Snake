using UnityEngine;

public class MainEnemy : MonoBehaviour, IHealth
{
	[SerializeField] private Health health;
	[SerializeField] private CarDriver carDriver;
	[SerializeField] private AudioClip bearClip;
	private AudioManager audioManager;
	private AudioSource source;
	private ExplosionController explosion;

	private void Start()
	{
		int maxHP = carDriver.GetMaxHP();
		health.Init(maxHP);
		health.onDeath += Die;

		audioManager = FindObjectOfType<AudioManager>();
		source = GetComponent<AudioSource>();
		explosion = FindObjectOfType<ExplosionController>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		float modifier = 1;
		float collisionPower = other.contacts[0].normalImpulse;
		float damage = collisionPower * ConstantVariables.CollisionHealthModifier;
		if (other.gameObject.TryGetComponent<IPlayerHealth>(out IPlayerHealth otherHealth))
		{
			otherHealth.TakeCollisionDamage(damage);
			modifier = otherHealth.GetModifierForEnemy();
			if (collisionPower > ConstantVariables.MinCollisionPowerForSound)
            {
				audioManager.PlaySound(AudioManager.Sound.CarsCollision, source);
			}
		}
		health.TakeDamage(Mathf.RoundToInt(damage * modifier));

		if (other.gameObject.CompareTag("Obstacle"))
			audioManager.PlaySound(AudioManager.Sound.PropsCollision, source);
	}

	public void TakeDamage(int damage)
	{
		health.TakeDamage(damage);
	}

	private void Die()
	{
		if (source.clip == bearClip) // ≈сли уничтоженный враг - медведь, воспроизводим звук heavy explosion
			audioManager.PlaySound(AudioManager.Sound.HeavyExplosion, source);

		else
		{
		   audioManager.PlaySound(AudioManager.Sound.LightExplosion, source); // ѕри уничтожении другого врага, воспроизводитс€ light explosion
		}

		explosion.HeavyExplosion(transform.position);
		Destroy(gameObject);
	}
}
