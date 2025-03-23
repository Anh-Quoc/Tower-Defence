using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController1 : MonoBehaviour
{
    bool isRunning = true;

    [SerializeField]
    Image pauseButtonImage;
    [SerializeField]
    Sprite pauseSprite;
    [SerializeField]
    Sprite playSprite;
    [SerializeField]
    public GameObject playButton;
    public string ChangeSceneName;

    // New Fields for OpenButton and HideButton
    [SerializeField]
    private GameObject openButton; // Reference to the OpenButton
    [SerializeField]
    private GameObject hideButton; // Reference to the HideButton

    [SerializeField]
    private GameObject targetObject; // The target object (e.g., the gun bar)

    void Start()
    {
        // Ensure the HideButton is initially inactive and OpenButton is active
        if (hideButton != null)
        {
            hideButton.SetActive(false);
        }
        else
        {
            Debug.LogError("HideButton is not assigned!");
        }
        targetObject.SetActive(false);

        if (openButton != null)
        {
            openButton.SetActive(true); // Make sure the OpenButton is initially active
        }
        else
        {
            Debug.LogError("OpenButton is not assigned!");
        }
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

    // This function is called when the OpenButton is pressed
    public void OnOpenButtonPressed()
    {
        // Show the gun bar
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Target Object (gun bar) is not assigned!");
        }

        // Hide the OpenButton and show the HideButton
        if (openButton != null)
        {
            openButton.SetActive(false); // Deactivate OpenButton
        }
        else
        {
            Debug.LogError("OpenButton is not assigned!");
        }

        if (hideButton != null)
        {
            hideButton.SetActive(true); // Activate HideButton
        }
        else
        {
            Debug.LogError("HideButton is not assigned!");
        }
    }

    // This function is called when the HideButton is pressed
    public void OnHideButtonPressed()
    {
        // Hide the gun bar
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Target Object (gun bar) is not assigned!");
        }

        // Hide the HideButton and show the OpenButton
        if (hideButton != null)
        {
            hideButton.SetActive(false); // Deactivate HideButton
        }
        else
        {
            Debug.LogError("HideButton is not assigned!");
        }

        if (openButton != null)
        {
            openButton.SetActive(true); // Activate OpenButton
        }
        else
        {
            Debug.LogError("OpenButton is not assigned!");
        }
    }

    public void UpdateCoin(int coin)
    {
        // Implement coin update logic here if necessary
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
