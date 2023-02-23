using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource defaultSource;
    [SerializeField] private AudioClip collisionPropsSound;
    [SerializeField] private AudioClip collisionCarsSound;
    [SerializeField] private AudioClip explosionLightSound;
    [SerializeField] private AudioClip explosionHeavySound;
    [SerializeField] private AudioClip honkSound;
    [SerializeField] private AudioClip shotMain_101_Sound;
    [SerializeField] private AudioClip shotMain_201_Sound;
    [SerializeField] private AudioClip shotMain_301_Sound;
    [SerializeField] private AudioClip shotSecondary_101_release;
    [SerializeField] private AudioClip shotSecondary_101_hit;
    [SerializeField] private AudioClip bearMG_shot;
    [SerializeField] private AudioClip hyenaRifle_shot;
    [SerializeField] private AudioClip levelCompletedSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip buttonPressedSound;


    


    public enum Sound 
    { PropsCollision, CarsCollision, LightExplosion, HeavyExplosion, Honk, ShotMain_101, ShotMain_201, ShotMain_301, 
        Secondary101_release, Secondary101_hit, BearMGShot, HyenaRifleShot, LevelCompleted, GameOver, ButtonPressed
    };

    /// <summary>
    /// Необходимый звук будет проигрываться через AudioSource того объекта, который издает этот звук. 
    /// Таким образом, если враг столкнется с препятствием за пределами игрового экрана (а значит и AudioListener'a), этот звук мы не услышим 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="source"></param>
    public void PlaySound(Sound s, AudioSource source)
    {
        switch(s)
        {
            case Sound.CarsCollision: 
               source.PlayOneShot(collisionCarsSound);
               break;
            case Sound.PropsCollision: 
                source.PlayOneShot(collisionPropsSound);
                break;
            case Sound.LightExplosion: 
                source.PlayOneShot(explosionLightSound);
                break;
            case Sound.HeavyExplosion: 
                source.PlayOneShot(explosionHeavySound);
                break;
            case Sound.Honk:
                source.PlayOneShot(honkSound);
                break;
            case Sound.ShotMain_101:
                source.PlayOneShot(shotMain_101_Sound);
                break;
            case Sound.ShotMain_201:
                source.PlayOneShot(shotMain_201_Sound);
                break;
            case Sound.ShotMain_301:
                source.PlayOneShot(shotMain_301_Sound);
                break;
            case Sound.Secondary101_release:
                source.PlayOneShot(shotSecondary_101_release);
                break;
            case Sound.Secondary101_hit:
                source.PlayOneShot(shotSecondary_101_hit);
                break;
            case Sound.BearMGShot:
                source.PlayOneShot(bearMG_shot);
                break;
            case Sound.HyenaRifleShot:
                source.PlayOneShot(hyenaRifle_shot);
                break;
            case Sound.LevelCompleted:
                source.PlayOneShot(levelCompletedSound);
                break;
            case Sound.GameOver:
                source.PlayOneShot(gameOverSound);
                break;
            //case Sound.ButtonPressed:
            //    source.PlayOneShot(buttonPressedSound);
            //    break;
            default:
                Debug.LogWarning(s + " not found");
                break;
        }
    }

    public void PlayButtonSound()
    {
        defaultSource.PlayOneShot(buttonPressedSound);
    }

}
