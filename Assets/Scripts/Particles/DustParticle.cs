using UnityEngine;

public class DustParticle : MonoBehaviour
{ 
    private CarDriver carDriverScript;
    private MainPlayer mainPlayer;
    private Rigidbody2D rb;
    private ParticleSystem dustParticle;    
    private ParticleSystem.EmissionModule em;
    private int emitNumber;
    private float speedRate;
    private float maxSpeed;
    private float mass;

    private void Awake()
    {
        dustParticle = GetComponent<ParticleSystem>();
        em = dustParticle.emission;
        mainPlayer = FindObjectOfType<MainPlayer>();

        if (transform.parent.CompareTag("Carriage"))
        {
            rb = mainPlayer.GetComponent<Rigidbody2D>();
            carDriverScript = mainPlayer.GetComponent<CarDriver>();
        }

        else
        {
            rb = GetComponentInParent<Rigidbody2D>();
            carDriverScript = GetComponentInParent<CarDriver>();
        }
    }
    void Start()
    {
        mass = rb.mass;
        maxSpeed = carDriverScript.GetMaxSpeed();
    }

    void Update()
    {
        speedRate = carDriverScript.GetSpeed() / maxSpeed;
        emitNumber = (int)(mass * speedRate);
        em.rateOverDistance = emitNumber;
    }
}
