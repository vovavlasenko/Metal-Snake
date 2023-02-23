using UnityEngine;
using UI.Base;
using GameStates;
using Services.WindowsService;
using UnityEngine.UI;
using GameStates.States;
using StaticData.Windows;
using Services.PlayerData;
using TMPro;

namespace UI.Windows.MainMenu
{
    public class UIMainMenuTopPlayerInfo : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI premiumMoneyText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image expImage;
        [SerializeField] private Button settingsButton;

        private IWindowsService windowsService;
        private PlayerStaticData playerStaticData;

        public void Construct(IWindowsService windowsService, PlayerStaticData playerStaticData)
        {
            this.windowsService = windowsService;
            this.playerStaticData = playerStaticData;
            settingsButton.onClick.AddListener(OpenSettings);

            expImage.fillAmount = this.playerStaticData.PlayerLevelData.GetCurrentRatio();
            moneyText.text = this.playerStaticData.PlayerMoneyData.MoneyCount.ToString();
            premiumMoneyText.text = this.playerStaticData.PlayerPremiumMoneyData.MoneyCount.ToString();
            levelText.text = this.playerStaticData.PlayerLevelData.CurrentLevel.ToString();

            this.playerStaticData.PlayerMoneyData.MoneyChangedEvent += RefreshMoneyText;
            this.playerStaticData.PlayerPremiumMoneyData.MoneyChangedEvent += RefreshPremiumMoneyText;
            this.playerStaticData.PlayerLevelData.ChangeExpEvent += RefreshExpCount;
            this.playerStaticData.PlayerLevelData.NewLevelEvent += RefreshLevel;
        }

        private void OpenSettings()
        {
            windowsService.Open(WindowsID.MainMenuSettings);
        }

        private void RefreshMoneyText(int newMoney)
        {
            moneyText.text = newMoney.ToString();
        }

        private void RefreshPremiumMoneyText(int newPremiumMoney)
        {
            premiumMoneyText.text = newPremiumMoney.ToString();
        }

        private void RefreshLevel(int newLevel)
        {
            levelText.text = newLevel.ToString();
        }

        private void RefreshExpCount(float expRatio)
        {
            expImage.fillAmount = expRatio;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            settingsButton.onClick.RemoveListener(OpenSettings);
        }

        private void OnDestroy()
        {
            this.playerStaticData.PlayerMoneyData.MoneyChangedEvent -= RefreshMoneyText;
            this.playerStaticData.PlayerPremiumMoneyData.MoneyChangedEvent -= RefreshPremiumMoneyText;
            this.playerStaticData.PlayerLevelData.ChangeExpEvent -= RefreshExpCount;
            this.playerStaticData.PlayerLevelData.NewLevelEvent -= RefreshLevel;
        }
    }
}
