using UnityEngine;
using UnityEngine.UI;
using Game.PlayerConstructor;
using System.Collections.Generic;

namespace UI.Windows.Garage
{
    public class UIGarageConstructCar : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> constructImages;
        [SerializeField] private SpriteRenderer backWheelsImage;
        [SerializeField] private SpriteRenderer coloringBodyImage;
        [SerializeField] private GameObject imagesGO;

        private const int SecondWeaponStandIndex = 7;

        public static UIGarageConstructCar instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void OpenImages()
        {
            imagesGO.SetActive(true);
        }

        public void CloseImages()
        {
            imagesGO.SetActive(false);
        }

        public void StartConstruct(List<Sprite> sprites)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                constructImages[i].sprite = sprites[i];
                if (i == 3)
                {
                    backWheelsImage.sprite = sprites[i];
                }
            }
        }

        public void ChangeColor(Color newColor)
        {
            coloringBodyImage.color = newColor;
        }

        public void ChangeSecondStandSprite(Sprite secondStandSprite)
        {
            constructImages[SecondWeaponStandIndex].sprite = secondStandSprite;
        }

        public void ChangeSprite(DetailType type, Sprite detailSprite)
        {
            constructImages[(int)type].sprite = detailSprite;
            if (type == DetailType.Wheels)
            {
                backWheelsImage.sprite = detailSprite;
            }
        }
    }
}
