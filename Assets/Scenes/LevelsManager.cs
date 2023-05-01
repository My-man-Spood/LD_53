using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public static string Tag = "LevelsManager";

    public static string[] names = new string[] { "Day 1", "day 12", "day 164", "day 289" };
    public static string[] texts = new string[] {
        "Kebak is a new sovereign state and it just set up a new federal postal service. Use the label on the envelopes and mailboxes to sort the mail. Get through as many letters as you can before the end of the day. Reject anything that doesn't match.",
        "People are sending just about anything through the mail. We need to make sure that the letters are not too heavy for the postman to carry. Use the scale to weigh the letters. Anything above 50g should be in a big yellow envelope. Reject anything above 500g.",
        "We will now allow people to send mail to other countries. Use the postage book to see which stamps are valid for each country. Reject any letters that don't have a valid stamp.",
        "We need to extract a little more out of our customers so we're creating weight classes with different rates, they look like little dots on the envelopes. weight classes ALWAYS take precedence over the color of the label. Use the postage book for details."
    };

    public static FeatureFlags[] flags = new FeatureFlags[]
    {
        new FeatureFlags { BasicWeight = false, CountryStamps = false, WeightClasses = false },
        new FeatureFlags { BasicWeight = true, CountryStamps = false, WeightClasses = false },
        new FeatureFlags { BasicWeight = true, CountryStamps = true, WeightClasses = false },
        new FeatureFlags { BasicWeight = true, CountryStamps = true, WeightClasses = true },
    };

    public static float[] levelTimes = new float[] { 60, 120, 300, 420 };
    public int currentLevel = 1;

    public int Successes = 0;
    public int Failures = 0;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        SceneManager.LoadScene("BetweenLevelsScene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("BetweenLevelsScene");
    }

    public void RestartFromScratch()
    {
        currentLevel = 1;
        SceneManager.LoadScene("BetweenLevelsScene");
    }
}


public class FeatureFlags
{
    public bool BasicWeight { get; set; }
    public bool CountryStamps { get; set; }
    public bool WeightClasses { get; set; }
}