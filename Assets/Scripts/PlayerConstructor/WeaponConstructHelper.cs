using UnityEngine;

namespace Game.PlayerConstructor
{
    public class WeaponConstructHelper : MonoBehaviour
    {
        [SerializeField] private Transform secondWeaponTransform;
        [SerializeField] private Transform secondStandTransform;
        [SerializeField] private Transform spawnProjectileTransform;

        public Transform SpawnProjectileTransform { get => spawnProjectileTransform; }
        public Transform SecondStandTransform { get => secondStandTransform; }
        public Transform SecondWeaponTransform { get => secondWeaponTransform; }

        public void DestroyUseless()
        {
            Destroy(secondStandTransform.gameObject);
            Destroy(secondWeaponTransform.gameObject);
            Destroy(spawnProjectileTransform.gameObject);
            Destroy(GetComponent<WeaponConstructHelper>());
        }
    }
}
