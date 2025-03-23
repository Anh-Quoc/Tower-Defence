using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    
    bool isRunning = true;
   
    
    [SerializeField]
    Image pauseButtonImage;
    [SerializeField]
    Image openButtonImage;
    [SerializeField]
    Sprite pauseSprite;
    [SerializeField]
    Sprite openSprite;
    [SerializeField]
    Sprite playSprite;
    [SerializeField]
    Sprite closeGunBarSprite;
    [SerializeField]
    private GameObject targetObject; 
    [SerializeField]
    public GameObject playButton;
    public string ChangeSceneName;


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

        // Change the sprite based on the gun bar state
        if (!isActive) // If gun bar is now active (opened)
        {
            // Update the sprite to the "close" gun bar sprite (if needed)
            openButtonImage.sprite = closeGunBarSprite;
        }
        else 
        {
            
            openButtonImage.sprite = openSprite;
        }
    }



    public void UpdateCoin(int coin)
    {
        
    }

    public void StartGame()
    {

        bool isActive = playButton.activeSelf;
        playButton.SetActive(!isActive);

    }

    public void ChangeScene()
    {
        // Load the scene with the given name
        SceneManager.LoadScene(ChangeSceneName);
    }




}
