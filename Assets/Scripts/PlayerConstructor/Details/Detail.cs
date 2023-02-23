using UnityEngine;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "New Detail", menuName = "Details/Detail")]
    public class Detail : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string detailGameName;
        [SerializeField] private string description;
        [SerializeField] private DetailType type;
        [SerializeField] private int price;
        [SerializeField] private CurrencyType currencyType;
        [SerializeField] private int minPlayerLevel;
        [SerializeField] private Sprite garageSprite;
        [SerializeField] private Sprite gameDetailSprite;

        public int ID { get => id; }
        public string Name { get => detailGameName; }
        public string Description { get => description; }
        public DetailType Type { get => type; }
        public int Price { get => price; }
        public CurrencyType Currency { get => currencyType; }
        public int MinPlayerLevel { get => minPlayerLevel; }
        public Sprite GarageDetailSprite { get => garageSprite; }
        public Sprite GameDetailSprite { get => gameDetailSprite; }

        public virtual string GetDetailCharacteristics()
        {
            return string.Empty;
        }
    }
}
