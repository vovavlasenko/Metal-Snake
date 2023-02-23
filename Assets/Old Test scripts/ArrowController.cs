using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Game.HUD
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private Image arrowImage;
        [SerializeField] private float dissolveDelay;
        [SerializeField] private float dissolveDecrement;
        private float arrowAlpha = 0;
        private Color defaultColor;
        private bool dissolvingNow;

        private void Awake()
        {
            defaultColor = arrowImage.color;
        }

        public void ActiveArrow()
        {
            if (dissolvingNow)
            {
                dissolvingNow = false;
                StopCoroutine(DissolveArrow());
            }
            arrowAlpha = 0.7f;
            ChangeArrowAlpha();
        }

        public void ChangeArrowPosAndRot(Vector2 arrowPosition, Quaternion arrowRotation)
        {
            transform.position = arrowPosition;
            transform.rotation = arrowRotation;
        }

        public IEnumerator DissolveArrow()
        {
            dissolvingNow = true;
            while (arrowAlpha > 0 && dissolvingNow)
            {
                arrowAlpha -= dissolveDecrement;
                ChangeArrowAlpha();
                yield return new WaitForSeconds(dissolveDelay);
            }
        }

        private void ChangeArrowAlpha()
        {
            defaultColor.a = arrowAlpha;
            arrowImage.color = defaultColor;
        }
    }
}
