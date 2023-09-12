using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TMP_Text text;
    public float appearDuration, fadeDuration;
    public float appearDistance, fadeDistance;

    public void Instantiate(string message)
    {
        text.text = message;
    }

    private void Start()
    {
        canvasGroup.alpha = 1;
        
        transform.DOMoveY(transform.position.y + appearDistance, appearDuration).onComplete += delegate
        {
            canvasGroup.DOFade(0, fadeDuration);
            transform.DOMoveY(transform.position.y + fadeDistance, fadeDuration).onComplete += delegate
            {
                Destroy(gameObject);
            };
        };
    }
}
