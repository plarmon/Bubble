using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnClickFinished;

    [SerializeField] private AudioSource _popSound;

    private Button _button;
    private Image _bubbleImg;


    private void Awake()
    {
        _button = GetComponent<Button>();
        _bubbleImg = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        _button.interactable = false;
       _bubbleImg.GetComponent<Animator>().Play("BubblePop");
       _popSound.Play();
       StartCoroutine(DelayInvokeEvent());
    }

    public void OnPointerEnter(PointerEventData data)
    {
       _bubbleImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        _bubbleImg.enabled = false;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private IEnumerator DelayInvokeEvent()
    {
        yield return new WaitForSeconds(0.6f);
        _button.interactable = true;
        OnClickFinished.Invoke();
    }
}