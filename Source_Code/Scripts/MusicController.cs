using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider volumeSlider;
    public AudioSource SFX;
    public Slider SFXSlider;


    void Start()
    {
        // Appliquer la valeur initiale
        SFX.volume = SFXSlider.value;
        musicSource.volume = volumeSlider.value;

        // Quand le slider change, on met à jour le volume
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SFXSlider.onValueChanged.AddListener(SetVolumeSFX);

    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetVolumeSFX(float volume)
    {
        SFX.volume = volume;
    }
}
