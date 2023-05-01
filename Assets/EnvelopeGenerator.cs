using System.Collections.Generic;
using UnityEngine;

public class EnvelopeGenerator : MonoBehaviour
{
    public static string Tag = "EnvelopeGenerator";
    public Transform Canvas;
    public GameObject EnvelopePrefab;

    public int success = 0;
    public int failure = 0;
    private float currentTimer = 0f;


    private LevelsManager levelsManager;

    void Awake()
    {
        levelsManager = GameObject.FindGameObjectWithTag(LevelsManager.Tag).GetComponent<LevelsManager>();
    }

    void Start()
    {
        Generate();
    }

    void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer > LevelsManager.levelTimes[levelsManager.currentLevel - 1])
        {
            levelsManager.Successes = success;
            levelsManager.Failures = failure;

            levelsManager.NextLevel();
        }
    }

    public Envelope Generate()
    {
        var obj = Instantiate(EnvelopePrefab, transform.position, Quaternion.identity, Canvas);
        var env = obj.GetComponent<Envelope>();

        return env;
    }

    public void Failure()
    {
        failure++;
        if (failure > 3)
        {
            levelsManager.RestartFromScratch();
        }

        Generate();
    }

    public void Success()
    {
        success++;
        Generate();
    }

    public IEnumerable<int> GetValidStamp(int country)
    {
        return country switch
        {
            1 => new int[] { 1, 6, 12 },
            2 => new int[] { 2, 10, 13, 17, 18, 22, 26 },
            3 => new int[] { 3, 7, 11, 16, 24, 25 },
            4 => new int[] { 4, 5, 9, 14, 21, 27, 28 },
            _ => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 },
        };
    }
}
