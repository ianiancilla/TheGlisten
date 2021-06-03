using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<SingletonBGM>().gameObject.GetComponent<AudioSource>();
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

}
