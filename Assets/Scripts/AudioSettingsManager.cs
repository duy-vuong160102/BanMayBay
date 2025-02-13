using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;        // Slider điều chỉnh âm lượng âm nền
    public Slider effectsVolumeSlider;     // Slider điều chỉnh âm lượng âm thanh hiệu ứng

    public AudioSource backgroundMusic;    // Âm nền
    public AudioSource soundEffects;       // Hiệu ứng âm thanh
    private static AudioSettingsManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Không hủy khi đổi Scene
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có một AudioManager
        }
    }

    void Start()
    {
        // Lấy giá trị âm lượng từ PlayerPrefs
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float savedEffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);

        // Gán giá trị cho slider và âm lượng
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = savedMusicVolume;
            if (backgroundMusic != null)
            {
                backgroundMusic.volume = savedMusicVolume;
            }
        }

        if (effectsVolumeSlider != null)
        {
            effectsVolumeSlider.value = savedEffectsVolume;
            if (soundEffects != null)
            {
                soundEffects.volume = savedEffectsVolume;
            }
        }

        // Gắn sự kiện thay đổi âm lượng
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (effectsVolumeSlider != null)
        {
            effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume); // Lưu giá trị
    }

    public void SetEffectsVolume(float volume)
    {
        if (soundEffects != null)
        {
            soundEffects.volume = volume;
        }
        PlayerPrefs.SetFloat("EffectsVolume", volume); // Lưu giá trị
    }
}
