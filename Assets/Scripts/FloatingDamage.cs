using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingDamage : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Text textDamage;
    public Text TextDamage
    {
        get { return textDamage; }
        set { textDamage = value; }
    }

    [SerializeField]
    private float slideStep;

    [SerializeField]
    private float slideWaitTime;

    [SerializeField]
    [Range(0.1f, 0.9f)]
    private float normalizedFadeStarts;

    [SerializeField]
    private float slideTotalTime;

    private float timeFadeStarts;

    #endregion

    #region Methods

    private void Awake()
    {
        this.textDamage.rectTransform.anchoredPosition =
            this.textDamage.transform.parent.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(GameObject.Find("Avatar").transform.position);
    }


    public void ShowMe()
    {
        this.gameObject.SetActive(true);
        StartCoroutine("SlideTop");
        this.timeFadeStarts = this.slideTotalTime * this.normalizedFadeStarts;
        Invoke("FadeOut", this.timeFadeStarts);
    }


    private void FadeOut()
    {
        this.textDamage.CrossFadeAlpha(0, this.slideTotalTime - this.timeFadeStarts, false);
        Invoke("Respaw", this.slideTotalTime);
    }

    private void Respaw()
    {
        StopCoroutine("SlideTop");
        this.gameObject.SetActive(false);
        this.textDamage.rectTransform.anchoredPosition = Vector2.zero;
        this.textDamage.CrossFadeAlpha(1, 0, true);
    }

    private IEnumerator SlideTop()
    {
        while (true)
        {
            this.textDamage.rectTransform.anchoredPosition = new Vector2(
                this.textDamage.rectTransform.anchoredPosition.x,
                this.textDamage.rectTransform.anchoredPosition.y + this.slideStep);

            yield return new WaitForSeconds(this.slideWaitTime);
        }
    }

    #endregion
}
