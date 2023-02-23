using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Windows.SettingsMenu
{
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;

        private const string EffectsVolume = "EffectsVolume";
        private const string MusicVolume = "MusicVolume";

        private void Start()
        {
            if (PlayerPrefs.HasKey(EffectsVolume))
            {
                SetEffects(PlayerPrefs.GetFloat(EffectsVolume));
                if (effectsSlider != null) effectsSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(EffectsVolume));
            }
            else
            {
                if (effectsSlider != null)
                {
                    SetEffects(effectsSlider.maxValue);
                    effectsSlider.SetValueWithoutNotify(effectsSlider.maxValue);
                }
            }
            if (PlayerPrefs.HasKey(MusicVolume))
            {
                SetMusic(PlayerPrefs.GetFloat(MusicVolume));
                if (musicSlider != null)
                {
                    musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(MusicVolume));
                }
            }
            else
            {
                if (musicSlider != null)
                {
                    SetMusic(musicSlider.maxValue);
                    musicSlider.SetValueWithoutNotify(musicSlider.maxValue);
                }
            }
        }

        public void SetMusic(float Music)
        {
            audioMixer.SetFloat(MusicVolume, Music);
            PlayerPrefs.SetFloat(MusicVolume, Music);
        }

        public void SetEffects(float Effects)
        {
            audioMixer.SetFloat(EffectsVolume, Effects);
            PlayerPrefs.SetFloat(EffectsVolume, Effects);
        }
    }
}
