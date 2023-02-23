using GameStates;
using Services.WindowsService;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using GameStates.States;
using Services.PlayerData;
using Services.Sound;
using StaticData.Windows;
using UnityEngine.SceneManagement;

namespace UI.Windows.SettingsMenu
{
    public class UISettingsWindow : BaseWindow
    {
        [SerializeField] private Button backButton;       

        private IWindowsService windowsService;
        private PlayerStaticData playerStaticData;

        private void Awake()
        {
            
        }

        public void Construct(IWindowsService windowsService, PlayerStaticData playerStaticData)
        {
            this.windowsService = windowsService;
            this.playerStaticData = playerStaticData;
            backButton.onClick.AddListener(CloseSettings);
        }

        private void CloseSettings()
        {
            var inGameAudioScript = FindObjectOfType<AudioManager>();

            if (inGameAudioScript != null)
            {
                inGameAudioScript.PlayButtonSound();
            }

            else
            {
                var mainMenuAudioScript = FindObjectOfType<ButtonSounds>();
                mainMenuAudioScript.OnClick();
                
            }

            CleanUp();
            Destroy(gameObject);
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            backButton.onClick.RemoveListener(CloseSettings);
            Destroy(gameObject);
        }
    }
}
