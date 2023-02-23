using GameStates;
using Services.WindowsService;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using GameStates.States;
using StaticData.Windows;
using Services.PlayerData;
using Services.Sound;
using TMPro;

namespace UI.Windows.MainMenu
{
        public class UIMainMenuWindow : BaseWindow
        {
            [SerializeField] private Button playButton;
            [SerializeField] private Button garageButton;
            [SerializeField] private Button shopButton;
            [SerializeField] private LoadingScreen loadingScreen;    
            

            private IGameStateMachine gameStateMachine;
            private IWindowsService windowsService;
            private PlayerStaticData playerStaticData;
            private GarageLights garageLightsScript;
            private ButtonSounds buttonSoundsScript;

            private void Awake()
            {
                garageLightsScript = FindObjectOfType<GarageLights>();
                buttonSoundsScript = FindObjectOfType<ButtonSounds>();
        }

            public void Construct(IGameStateMachine gameStateMachine, IWindowsService windowsService, PlayerStaticData playerStaticData)
            {
                this.gameStateMachine = gameStateMachine;
                this.windowsService = windowsService;
                this.playerStaticData = playerStaticData;
                playButton.onClick.AddListener(Play);
                garageButton.onClick.AddListener(OpenGarage);
                shopButton.onClick.AddListener(OpenShop);
            }

            private void OpenShop()
            {
                windowsService.Open(WindowsID.Shop);
                buttonSoundsScript.OnClick();
                CleanUp();
            }

            private void OpenGarage()
            {
                windowsService.Open(WindowsID.Garage);
                garageLightsScript.LightSwitcher();
                buttonSoundsScript.OnClick();
                CleanUp();
           
        }

            private void Play()
            {
                LoadingScreen screen = Instantiate(loadingScreen, transform.parent);
                screen.ChooseRandomHint(playerStaticData.PlayerLevelData.CurrentLevel);
                gameStateMachine.Enter<LoadGameLevelState, string>(ConstantVariables.GameSceneName);
                buttonSoundsScript.OnClick();
                CleanUp();
            }

            protected override void CleanUp()
            {
                base.CleanUp();
                playButton.onClick.RemoveListener(Play);
                garageButton.onClick.RemoveListener(OpenGarage);
                shopButton.onClick.RemoveListener(OpenShop);
                Destroy(gameObject);
            }

        }
    
}