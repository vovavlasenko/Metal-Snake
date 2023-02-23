using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private GameObject explosionLightPrefab;
    [SerializeField] private GameObject explosionHeavyPrefab;

    /// <summary>
    /// ����� ������
    /// </summary>
    /// <param name="rocketPosition"></param>
    public void LightExplosion(Vector3 rocketPosition)
    {
        GameObject explosion;
        explosion = Instantiate(explosionLightPrefab, rocketPosition, Quaternion.identity); 
        Destroy(explosion, 2);
    }

    /// <summary>
    /// ����� �������/������ ����������/������ ������
    /// </summary>
    /// <param name="targetPosition"></param>
    public void HeavyExplosion(Vector3 targetPosition)
    {
        GameObject explosion;
        explosion = Instantiate(explosionHeavyPrefab, targetPosition, Quaternion.identity);
        Destroy(explosion, 2);
    }
}

