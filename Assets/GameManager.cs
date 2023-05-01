using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dotsIndicator;
    public GameObject scale;
    public GameObject bookButton;

    private LevelsManager levelsManager;

    void Awake()
    {
        levelsManager = GameObject.FindGameObjectWithTag(LevelsManager.Tag).GetComponent<LevelsManager>();
    }

    void Start()
    {
        var flags = LevelsManager.flags[levelsManager.currentLevel - 1];

        if (flags.BasicWeight)
        {
            scale.SetActive(true);
        }
        else
        {
            scale.SetActive(false);
        }


        if (flags.CountryStamps)
        {
            bookButton.SetActive(true);
        }
        else
        {
            bookButton.SetActive(false);
        }

        if (flags.WeightClasses)
        {
            dotsIndicator.SetActive(true);
        }
        else
        {
            dotsIndicator.SetActive(false);
        }
    }


}
