using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    private AudioManager audioManager;
    private ExplosionController explosion;

    private void Start()
    {
        explosion = FindObjectOfType<ExplosionController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantVariables.TAG_ENEMY))
        {
            audioManager.PlaySound(AudioManager.Sound.Secondary101_hit, collision.GetComponent<AudioSource>());
            explosion.LightExplosion(transform.position);
        }
    }
}
