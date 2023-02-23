using GameStates;
using Services.WindowsService;
using System.Collections.Generic;
using UI.Base;
using Game.PlayerConstructor;
using UnityEngine;
using UnityEngine.UI;
using GameStates.States;
using Services.PlayerData;
using Services.Sound;
using StaticData.Windows;

namespace UI.Windows.Garage
{
    public class UIGarageWindow : BaseWindow
    {
        [SerializeField] private Button backButton;
        [SerializeField] private List<Button> categoriesButton;
        [SerializeField] private UIDetailsInfo uiDetailsInfo;
        [SerializeField] private Button colorButton;

        private IWindowsService windowsService;
        private PlayerStaticData playerStaticData;
        private PlayerPartsData playerPartsData;
        private GarageLights garageLightsScript;
        private ButtonSounds buttonSoundsScript;
        private int currentCategoryIndex = 0;

        private void Awake()
        {
            garageLightsScript = FindObjectOfType<GarageLights>();
            buttonSoundsScript = FindObjectOfType<ButtonSounds>();
        }

        public void Construct(IWindowsService windowsService, PlayerStaticData playerStaticData, PlayerPartsData playerPartsData)
        {
            this.playerPartsData = playerPartsData;
            this.windowsService = windowsService;
            this.playerStaticData = playerStaticData;
            backButton.onClick.AddListener(OpenMainMenu);
            UIGarageConstructCar.instance.StartConstruct(playerPartsData.GetCurrentUISprites());
            UIGarageConstructCar.instance.OpenImages();
            UIGarageConstructCar.instance.ChangeColor(playerPartsData.CurrentBody.GetCurrentColor());
            uiDetailsInfo.onDetailChanged += playerPartsData.SetNewCurrentDetail;
            ShowCategory(0);
        }

        public void ShowCategory(int newCategoryIndex)
        {
            categoriesButton[currentCategoryIndex].interactable = true;
            currentCategoryIndex = newCategoryIndex;
            categoriesButton[newCategoryIndex].interactable = false;
            DetailType type = (DetailType)newCategoryIndex;
            uiDetailsInfo.ShowAllDetails(playerPartsData.PlayerDetails[newCategoryIndex],
                type, playerPartsData.CurrentDetailsIndexes[newCategoryIndex]);
        }

        public void ShowColorsCategory()
        {
            categoriesButton[currentCategoryIndex].interactable = true;
            currentCategoryIndex = 7;
            categoriesButton[currentCategoryIndex].interactable = false;
            Body currentBody = playerPartsData.CurrentBody;
            uiDetailsInfo.ShowColors(currentBody.Colors, currentBody.CurrentColorIndex);
        }

        private void OpenMainMenu()
        {
            buttonSoundsScript.OnClick();
            garageLightsScript.LightSwitcher();
            windowsService.Open(WindowsID.MainMenu);
            playerPartsData.SaveCurrentParts();
            CleanUp();
            UIGarageConstructCar.instance.CloseImages();
            Destroy(gameObject);
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            backButton.onClick.RemoveListener(OpenMainMenu);
            uiDetailsInfo.onDetailChanged -= playerPartsData.SetNewCurrentDetail;
            Destroy(gameObject);
        }
    }
}
