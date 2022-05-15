using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip placeCardsSFX;
    [Range(0f, 2f)]
    [SerializeField] float placeCardsSFXVolume;

    [SerializeField] AudioClip cardAttackedSFX;
    [Range(0f, 2f)]
    [SerializeField] float cardAttackedSFXVolume;

    [SerializeField] AudioClip hoverOverUISFX;
    [Range(0f, 2f)]
    [SerializeField] float hoverOverUISFXVolume;

    [SerializeField] AudioClip flipCardSFX;
    [Range(0f, 2f)]
    [SerializeField] float flipCardSFXVolume;

    [SerializeField] AudioClip cardPoofSFX;
    [Range(0f, 2f)]
    [SerializeField] float cardPoofSFXVolume;

    [SerializeField] AudioClip gameOverWinSFX;
    [Range(0f, 2f)]
    [SerializeField] float gameOverWinSFXVolume;

    [SerializeField] AudioClip gameOverLoseSFX;
    [Range(0f, 2f)]
    [SerializeField] float gameOverLoseSFXVolume;

    [SerializeField] AudioClip gameOverTieSFX;
    [Range(0f, 2f)]
    [SerializeField] float gameOverTieSFXVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlaceCardsSFX()
    {
        audioSource.PlayOneShot(placeCardsSFX, placeCardsSFXVolume);
    }
    public void PlayCardAttackedSFX()
    {
        audioSource.PlayOneShot(cardAttackedSFX, cardAttackedSFXVolume);
    }
    public void PlayHoverOverUISFX()
    {
        audioSource.PlayOneShot(hoverOverUISFX, hoverOverUISFXVolume);
    }
    public void PlayFlipCardSFX()
    {
        audioSource.PlayOneShot(flipCardSFX,flipCardSFXVolume);
    }
    public void PlaycardPoofSFX()
    {
        audioSource.PlayOneShot(cardPoofSFX, cardPoofSFXVolume);
    }
    public void PlayGameOverWinSFX()
    {
        audioSource.PlayOneShot(gameOverWinSFX, gameOverWinSFXVolume);
    }
    public void PlayGameOverLoseSFX()
    {
        audioSource.PlayOneShot(gameOverLoseSFX, gameOverLoseSFXVolume);
    }
    public void PlayGameOverTieSFX()
    {
        audioSource.PlayOneShot(gameOverTieSFX, gameOverTieSFXVolume);
    }
}
