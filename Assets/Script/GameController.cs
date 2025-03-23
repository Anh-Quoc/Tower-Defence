using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    
    bool isRunning = true;
   
    
    [SerializeField]
    Image pauseButtonImage; // Reference to the Image component of the button
    [SerializeField]
    Sprite pauseSprite; // Sprite for the "Pause" state
    [SerializeField]
    Sprite playSprite;  // Sprite for the "Play" state
    [SerializeField]
    private GameObject targetObject; // The GameObject you want to enable/disable
    [SerializeField]
    public GameObject playButton;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPauseButton()
    {
        if (isRunning)
            PauseGame();
        else
            ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isRunning = false;
       

        if (pauseButtonImage != null)
        {
            pauseButtonImage.sprite = playSprite; // Switch to "Play" sprite
            Debug.Log("Pause sprite updated successfully.");
        }
        else
        {
            Debug.LogError("pauseButtonImage is not assigned!");
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isRunning = true;
        
        pauseButtonImage.sprite = pauseSprite; 
    }

    
    public void OpenAndCloseGunBar()
    {
        
            bool isActive = targetObject.activeSelf; 
            targetObject.SetActive(!isActive); 
        
    }



    public void UpdateCoin(int coin)
    {
        
    }

    public void StartGame()
    {

        bool isActive = playButton.activeSelf;
        playButton.SetActive(!isActive);

    }



}
