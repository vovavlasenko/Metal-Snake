using UnityEngine;
using Services.Pause;

public abstract class CarPause : MonoBehaviour, IPause
{
    [SerializeField] private CarDriver carDriver;
    [SerializeField] private EngineVolume engineVolume;
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private Rigidbody2D carRigidBody;
    [SerializeField] private AudioSource engineSource;
    private PauseManager pauseManager;

    protected virtual void Start()
    {
        pauseManager = RefContainer.Instance.MainPauseManager;
        pauseManager.Register(this);
        weapon.SetPauseManager(pauseManager);
        StartExtend();
    }

    private void OnDestroy()
    {
        if (pauseManager != null)
        {
            pauseManager.Unregister(this);
        }
    }

    public void PauseOff()
    {
        carRigidBody.bodyType = RigidbodyType2D.Dynamic;
        PauseOffExtend();
        carDriver.enabled = true;
        engineVolume.enabled = true;
        engineSource.Play();
    }

    public void PauseOn()
    {
        carRigidBody.bodyType = RigidbodyType2D.Static;
        PauseOnExtend();
        carDriver.enabled = false;
        engineVolume.enabled = false;
        engineSource.Pause();
    }

    protected abstract void PauseOnExtend();
    protected abstract void PauseOffExtend();
    protected abstract void StartExtend();
}
