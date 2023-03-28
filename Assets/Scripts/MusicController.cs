using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private const string MUSIC_VOLUME_KEY = "musicVolume";

    void Start()
    {
        // Kaydedilen müzik sesi değerini yükler veya varsayılan değer olarak 0.5'i atar
        float savedVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.5f);
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;
        // Slider'da değer değiştiğinde ses seviyesini günceller
        volumeSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
    }

    void SetMusicVolume()
    {
        // Slider'dan alınan değeri kaydeder ve ses seviyesini ayarlar
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        audioSource.volume = volume;
    }
}