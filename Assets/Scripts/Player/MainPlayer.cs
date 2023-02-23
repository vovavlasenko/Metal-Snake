using UnityEngine;
using Game;
using System;
using Game.Player;
using UnityEngine.Rendering.Universal;

public class MainPlayer : MonoBehaviour
{
	[SerializeField] private PlayerMainWeapon weapon;
	[SerializeField] private Health health;
	[SerializeField] private CarDriver carDriver;
	[SerializeField] private Rigidbody2D playerRigidbody;
	[SerializeField] private AdditionalWeapon additionalWeapon;
	[SerializeField] private PlayerDeath playerDeath;
	[SerializeField] private AudioManager audioManager;
	[SerializeField] private AudioSource source;

	private Controller controller = new Controller();
	public Controller PlayerController { get => controller; }
	private Vector3 dir;
	private Camera cam;
	private ExplosionController explosion;
	private float collisionBumperModifier = 1;

	private float doubleTapTimer = 0.7f;
	private bool canDoubleTap;
	private GameObject oldTarget;

	public event Action PlayerDeathEvent;
	public Health PlayerHealth => health;

	// Collision System
	private const float speedAfterCollision = 1;
	private const float maxCollisionAngleForTurn = 25;
	private bool isInCollision = false;
	private float collisionTurnAmount = 0;
	private const float collisionPowerForSlow = 1;
	private const float collisionAngleForNewDirection = 30;

	private void Start()
	{
		dir = transform.up;

		//for tests
		health.Init(carDriver.GetMaxHP());

		controller.Init();
		controller.onMove += SetDir;
		controller.onTouchPos += TouchEnemy;

		cam = RefContainer.Instance.MainCamera;

		explosion = FindObjectOfType<ExplosionController>();
	}

	public void SetBumperModifier(float BumperModifier)
	{
		collisionBumperModifier *= BumperModifier;
	}

	private void SlowDown()
	{
		carDriver.SetSpeed(speedAfterCollision);
	}

	public void PlayLightExplosionSound()
	{
		audioManager.PlaySound(AudioManager.Sound.LightExplosion, source); // ������������� ���� ����������� �������
	}

	public void Die()
	{
		playerDeath.TurnOffVisiblePlayerParts();
		PlayerDeathEvent?.Invoke();
		explosion.HeavyExplosion(transform.position);
		audioManager.PlaySound(AudioManager.Sound.HeavyExplosion, source); //������������� ���� ����������� ������
		DisablePlayerShadow();
	}

	public void TouchEnemy(Vector2 posScreen)
	{
		Ray r = cam.ScreenPointToRay(posScreen);
		RaycastHit2D hit = new RaycastHit2D();
		hit = Physics2D.GetRayIntersection(r, 100);
		if (hit.collider)
		{
			Debug.Log(hit.collider.tag);
			if (hit.collider.CompareTag(ConstantVariables.TAG_ENEMY_TARGET_COLLIDER) || hit.collider.CompareTag(ConstantVariables.TAG_ENEMY))
			{
				weapon.SetTarget(hit.collider.transform);
				GameObject newTarget = hit.collider.gameObject;

				if (oldTarget == null) // ���� ���� �����������
				{
					MarkAim(newTarget); // �������� ����� ���� ��������
				}
				else if (newTarget != oldTarget) // ���� ����� ���� ���������� �� ����������
				{
					MarkNotAim(oldTarget); // ������� � ���������� ������
					MarkAim(newTarget); // �������� ����� ���� ��������
				}



				if (canDoubleTap == true && additionalWeapon.isReloaded)  // ���� ������ ������������ � ����� ������ ������� ���
				{
					additionalWeapon.doubleTap = true; // ���� ������ ��� ������� ������ 
				}

				canDoubleTap = true;
				Invoke("DoubleTapTimerOver", doubleTapTimer); // �� ��������� ����� �������, ��������� ��� �� ����� ��������� �������	
			}
		}
	}

	private void MarkAim(GameObject enemy)
	{
		enemy.GetComponent<AimSpriteController>().EnableAimSprite();
		oldTarget = enemy;
	}

	private void MarkNotAim(GameObject enemy)
	{
		enemy.GetComponent<AimSpriteController>().DisableAimSprite();
	}

	private void DisablePlayerShadow()
	{
		GetComponent<ShadowCaster2D>().enabled = false;
	}


	/// <summary>
	/// ���������� ������� �����
	/// </summary>
	private void DoubleTapTimerOver() 
    {
		canDoubleTap = false;
    }

	private void SetDir(Vector2 swipe)
	{
		dir = swipe.normalized;
	}

	private void Update()
	{
		if (isInCollision)
        {
			carDriver.SetInputs(1, collisionTurnAmount);
			return;
		}

		if (Vector2.Dot((Vector2)transform.up, dir) > 0.95f)
		{
			carDriver.SetInputs(1, 0);
			return;
		}

		float turnAmount;
		float angleToDir = Vector2.SignedAngle((Vector2)transform.up, (Vector2)dir.normalized);
		if (angleToDir > 0)
		{
			turnAmount = 1f;
		}
		else
		{
			turnAmount = -1f;
		}

		carDriver.SetInputs(1, turnAmount);

	}

	private void OnDestroy()
	{
		controller.onMove -= SetDir;
		controller.onTouchPos -= TouchEnemy;
	}

    private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag(ConstantVariables.TAG_OBSTACLE))
		{
			// ��������� ������ ���� ������������
			float collisionPower = 0;
			ContactPoint2D[] contactPoints = new ContactPoint2D[4];
			int contacsCount = other.GetContacts(contactPoints);
			for (int i = 0; i < contacsCount; i++)
            {
				collisionPower += contactPoints[i].normalImpulse;
			}

			// ������������� ���� ������������ ������ � ������������
			if (collisionPower > ConstantVariables.MinCollisionPowerForSound)
			{
				audioManager.PlaySound(AudioManager.Sound.PropsCollision, source); 
			}

			int damage = 0;
			// ��������� ��������� �� ����� ������������ � �������� ����� ����������
			Vector2 dirToHitPoint = other.GetContact(0).point - (Vector2)transform.position;
			float angle = Vector2.Angle(transform.up, dirToHitPoint);
			
			if (angle <= maxCollisionAngleForTurn)
			{
				Vector2 reflect = Vector2.Reflect(transform.up, other.GetContact(0).normal);
				if (Vector2.SignedAngle(transform.up, reflect) < 0)
				{
					collisionTurnAmount = -1.2f;
				}
				else
				{
					collisionTurnAmount = 1.2f;
				}

				if (collisionPower > collisionPowerForSlow)
				{
					isInCollision = true;
					SlowDown();
				}
				else if (collisionPower == 0)
                {
					isInCollision = true;
				}
				damage = Mathf.RoundToInt(collisionPower * ConstantVariables.CollisionHealthModifier * collisionBumperModifier);
			}
			else
            {
				damage = Mathf.RoundToInt(collisionPower * ConstantVariables.CollisionHealthModifier);
			}
			health.TakeDamage(damage);
		}
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
		if (collision.collider.CompareTag(ConstantVariables.TAG_OBSTACLE))
		{
			Vector2 normal = collision.GetContact(0).normal;
			if (Vector2.Angle(normal, dir) < 88)
            {
				isInCollision = false;
				return;
			}

			Vector2 transUp = transform.up;
			Vector2 reflect = Vector2.Reflect(transUp, normal);
			float angle = Vector2.Angle(transUp, reflect);
			SetDir(transUp);

			if (angle < collisionAngleForNewDirection)
			{
				Vector2 newDirection = transUp.normalized - Vector2.Dot(transUp, normal) * normal;
				SetDir(newDirection);
				collisionTurnAmount = 0;
				isInCollision = false;
			}
		}
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.collider.CompareTag(ConstantVariables.TAG_OBSTACLE))
		{
			isInCollision = false;
			collisionTurnAmount = 0;
		}
	}

	public void PauseOn()
    {
		controller.DisableControll();
	}

	public void PauseOff()
	{
		controller.EnableControll();
	}
}
