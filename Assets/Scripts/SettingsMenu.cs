using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject[] canvasObject;

    public static bool activeRunner;
    public static bool activeObstacle;
    public static bool isGameActive;
    public static bool isPauseMenu;
    public static bool isSounds;

    private void Start()
    {
        activeRunner = false;
        activeObstacle = false;
        isGameActive = false;
        isPauseMenu = false;

        // Ses ayarını yükle
        if (PlayerPrefs.HasKey("isSounds"))
        {
            isSounds = PlayerPrefs.GetInt("isSounds") == 1;
            canvasObject[4].SetActive(isSounds);
            canvasObject[5].SetActive(!isSounds);
        }
        else
        {
            isSounds = true; // Varsayılan olarak ses açık
            canvasObject[4].SetActive(true);
            canvasObject[5].SetActive(false);
        }
    }

    private void Update()
    {
        if (PlayerController.fallTrue == true)
        {
            canvasObject[1].SetActive(false);
            StartCoroutine(DeactivateAfterSeconds(2f));
        }
    }

    public void StartBtnClick()
    {
        // Canvas Ayarları
        canvasObject[0].SetActive(false);
        canvasObject[1].SetActive(true);
        activeRunner = true;
        activeObstacle = true;

        // Skor ve Coin Ayarları
        isGameActive = true;
    }

    public void PauseBtnClick()
    {
        canvasObject[1].SetActive(false);
        canvasObject[3].SetActive(true);
        activeRunner = false;
        Time.timeScale = 0;
    }

    public void ContinueBtnClick()
    {
        canvasObject[1].SetActive(true);
        canvasObject[3].SetActive(false);
        activeRunner = true;
        Time.timeScale = 1;
    }

    public void MainMenuBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        isSounds = true;
    }

    public void OpenSoundsBtnClick() 
    {
        canvasObject[4].SetActive(true);
        canvasObject[5].SetActive(false);
        isSounds = true;

        // Ses ayarını kaydet
        PlayerPrefs.SetInt("isSounds", 1);
    }

    public void CloseSoundsBtnClick() 
    {
        canvasObject[4].SetActive(false);
        canvasObject[5].SetActive(true);
        isSounds = false;

        // Ses ayarını kaydet
        PlayerPrefs.SetInt("isSounds", 0);
    }

    public void TryAgainBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }

    IEnumerator DeactivateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canvasObject[2].SetActive(true);
    }
}
