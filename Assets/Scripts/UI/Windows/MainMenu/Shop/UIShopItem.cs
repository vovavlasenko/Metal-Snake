using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI.Windows.Shop
{
    public class UIShopItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI minLevelText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image currencyImage;
        [SerializeField] private Image detailImage;
        [SerializeField] private Button button;

        private int detailIndex = 0;
        private UIShopCategory uiShopCategory;

        public void Construct(Sprite detailImage, int detailMinLevel, int detailPrice, CurrencyType currency)
        {
            this.detailImage.sprite = detailImage;
            minLevelText.text = detailMinLevel.ToString();
            priceText.text = detailPrice.ToString();
            if (currency == CurrencyType.Coins)
            {
                currencyImage.sprite = CurrencyImages.CoinsSprite;
            }
            else
            {
                currencyImage.sprite = CurrencyImages.MicrochipSprite;
            }
        }

        public void SetCategory(UIShopCategory uiShopCategory)
        {
            this.uiShopCategory = uiShopCategory;
            button.onClick.AddListener(DetailButtonClick);
        }

        public void SetNewDetailIndex(int newDetailIndex)
        {
            detailIndex = newDetailIndex;
        }

        public void CleanUp()
        {
            button.onClick.RemoveListener(DetailButtonClick);
        }

        public void DetailButtonClick()
        {
            uiShopCategory.ShowDetailInfo(detailIndex);
        }
    }
}
