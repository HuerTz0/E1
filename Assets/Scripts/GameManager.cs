using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject audioPanel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    
    [SerializeField] AudioMixer mixer;
    [SerializeField] string exposedMusicParamName;
    [SerializeField] string exposedSFXParamName;
    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void ToggleAudio()
    {
        audioPanel.SetActive(!audioPanel.activeSelf);
    }
    public void setMusicVolume()
    {
        float volumeInDB = Mathf.Log10(Mathf.Max(musicSlider.value, 0.0001f)) * 20;
        mixer.SetFloat(exposedMusicParamName, volumeInDB);
    }
    public void setSFXVolume()
    {
        float volumeInDB = Mathf.Log10(Mathf.Max(sfxSlider.value, 0.0001f)) * 20;
        mixer.SetFloat(exposedSFXParamName, volumeInDB);
    }
}
