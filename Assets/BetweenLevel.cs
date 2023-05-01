using TMPro;
using UnityEngine;

public class BetweenLevel : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelDescription;

    public TextMeshProUGUI statsTitle;
    public TextMeshProUGUI successes;
    public TextMeshProUGUI failures;


    void Awake()
    {
        var manager = GameObject.FindGameObjectWithTag(LevelsManager.Tag).GetComponent<LevelsManager>();
        levelText.text = LevelsManager.names[manager.currentLevel - 1];
        levelDescription.text = LevelsManager.texts[manager.currentLevel - 1];

        if (manager.currentLevel > 1)
        {
            statsTitle.text = $"performance review for {LevelsManager.names[manager.currentLevel - 2]}";
            successes.text = $"Successes: {manager.Successes}";
            failures.text = $"Failures: {manager.Failures}";
        }
        else
        {
            statsTitle.text = "";
            successes.text = "";
            failures.text = "";
        }
    }

    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
