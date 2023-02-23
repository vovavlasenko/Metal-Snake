using UnityEngine;

namespace UI.Windows.MainMenu
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private PlayerHints allHints;
        [SerializeField] private Transform hintParentTransform;

        public void ChooseRandomHint(int playerLevel)
        {
            int count = allHints.LevelHints.Count;
            while (true)
            {
                int index = Random.Range(0, count);
                if (allHints.LevelHints[index].MinLevel <= playerLevel)
                {
                    Instantiate(allHints.LevelHints[index].Hint, hintParentTransform);
                    break;
                }
            }
        }
    }
}
