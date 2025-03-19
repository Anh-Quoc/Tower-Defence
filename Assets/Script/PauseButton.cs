using TMPro;
using UnityEngine;
using UnityEngine.UI; // For working with UI elements

public class PauseButton : MonoBehaviour
{
    private int totalPoints = 0;
    bool isRunning = true;
    bool isGameOver = false;
    [SerializeField]
    int TotalTime;
    [SerializeField]
    Text txtTime;
    [SerializeField]
    TextMeshProUGUI txtPause;
    [SerializeField]
    Image healthBar;
    float time = 0;
    public int HP = 100;
    
    void Start()
    {
        txtTime.text = "Time: " + TotalTime.ToString();
    }

    void Update()
    {
        if (isGameOver) return;

        time += Time.deltaTime;
        if (time >= 1)
        {
            TotalTime--;
            txtTime.text = "Time: " + TotalTime.ToString();
            time = 0;
        }

        if (TotalTime <= 0)
        {
            Time.timeScale = 0;
            isGameOver = true;
        }
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
        txtPause.text = "Resume";
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isRunning = true;
        txtPause.text = "Pause";
    }

    public void UpdatePoints(int points)
    {
        totalPoints += points;
        // Update your UI or other logic here
    }

    public void OnChangeHealth(int damage, int points)
    {
        HP -= damage;
        healthBar.fillAmount = (float)HP / 100f;
        if (HP <= 0)
        {
          
        }
    }
   
}