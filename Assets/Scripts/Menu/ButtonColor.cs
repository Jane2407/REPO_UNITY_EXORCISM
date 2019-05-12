using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header ("Color change")]
    public Sprite greenImg;
    public Sprite pinkImg;

    [Header ("Audio")]
    public AudioClip buttonSound;
    AudioSource source;
    float vol = 1;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = pinkImg;
        source.PlayOneShot(buttonSound, vol);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = greenImg;
    }
}
