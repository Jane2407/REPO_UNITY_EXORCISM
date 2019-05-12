using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color oldColor;

    [Header("Audio")]
    public AudioClip buttonSound;
    AudioSource source;
    float vol = 1;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        oldColor = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(255, 255, 255);
        source.PlayOneShot(buttonSound, vol);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = oldColor;
    }
}
