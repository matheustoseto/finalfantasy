using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SoundMenu : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

    private AudioSource audioSound;

    public void OnPointerEnter(PointerEventData ped)
    {
        audioSound = SoundControl.GetInstance().ExecuteEffect(TypeSound.Cursor);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        SoundControl.GetInstance().ExecuteStop(audioSound);
    }
}
