using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElements : MonoBehaviour, IPointerEnterHandler {

	public AudioClip highlightSound;
	private AudioSource audioSource;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (audioSource == null)
			audioSource = GetComponent<AudioSource> ();
		if (audioSource == null)
			audioSource = gameObject.AddComponent<AudioSource> ();

		audioSource.PlayOneShot (highlightSound, .4f);
	}
}
