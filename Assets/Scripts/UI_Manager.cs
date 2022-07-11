using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject scrollableList;
    [SerializeField] GameObject settingMenu;
    [SerializeField] Image soundImage;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite soundOff;
    // Start is called before the first frame update
    void Start()
    {
        scrollableList.SetActive(false);
        settingMenu.SetActive(false);
        soundImage.sprite = soundOn;
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OpenMenu()
    {
        settingMenu.SetActive(true);
    }
    public void Resume()
    {
        settingMenu.SetActive(false);
        scrollableList.SetActive(false);
    }
    public void OpenList()
    {
        scrollableList.SetActive(true);
    }
    public void CloseList()
    {
        scrollableList.SetActive(false);
    }
    public void SoundToggle()
    {
        if (soundImage.sprite == soundOn) soundImage.sprite = soundOff;
        else soundImage.sprite = soundOn;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
