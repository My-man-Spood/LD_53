using System.Collections.Generic;
using System.Linq;
using EasyDragAndDrop.Core;
using UnityEngine;
using UnityEngine.UI;

public class Envelope : MonoBehaviour
{
    public int shape;
    public int color;
    public int dots;

    public int country;
    public int stamp;
    public float weight;

    public bool isValid;
    public bool isBig;
    public LabelUi labelUi;

    public Image countryImage;
    public Image stampImage;

    public GameObject smallEnvelope;
    public GameObject bigEnvelope;
    public EnvelopeGenerator envelopeGenerator;
    private LevelsManager levelsManager;
    private FeatureFlags flags;

    void Awake()
    {
        levelsManager = GameObject.FindGameObjectWithTag(LevelsManager.Tag).GetComponent<LevelsManager>();
        flags = LevelsManager.flags[levelsManager.currentLevel - 1];
        envelopeGenerator = GameObject.FindGameObjectWithTag(EnvelopeGenerator.Tag).GetComponent<EnvelopeGenerator>();
    }

    void Start()
    {
        Generate();
    }

    [ContextMenu("Generate")]
    void Generate()
    {
        SetEnvelopeSize();
        GenerateValid();

        if (Random.Range(1, 5) == 1)
        {
            GenerateInvalid();
        }

        labelUi.Init(shape, color, dots);

        if (!flags.CountryStamps)
        {
            countryImage.sprite = null;
            countryImage.color = Color.clear;

            stampImage.sprite = null;
            stampImage.color = Color.clear;
        }
        else
        {
            countryImage.sprite = Asseter.Instance.GetCountry(country);
            stampImage.sprite = Asseter.Instance.GetStamp(stamp);
        }
    }

    void SetEnvelopeSize()
    {
        if (flags.BasicWeight)
        {
            isBig = Random.Range(1, 3) == 1;
        }
        else
        {
            isBig = false;
        }

        if (isBig)
        {
            smallEnvelope.SetActive(false);
            bigEnvelope.SetActive(true);
        }
        else
        {
            smallEnvelope.SetActive(true);
            bigEnvelope.SetActive(false);
        }
    }

    void GenerateValid()
    {
        isValid = true;
        SetValidShapeAndColor();
        SetValidDots();

        SetValidStamp();
        SetValidWeight();
    }

    void SetValidDots()
    {
        if (flags.WeightClasses)
        {
            if (Random.Range(1, 4) == 1)
            {
                dots = Random.Range(1, 4);
            }
            else
            {
                dots = 0;
            }
        }
    }

    private void SetValidShapeAndColor()
    {
        shape = Random.Range(1, 4);
        color = Random.Range(1, 5);
    }

    void SetValidWeight()
    {
        if (!isBig)
        {
            weight = Random.Range(25f, 37f);
        }
        else
        {
            if (flags.BasicWeight && !flags.WeightClasses)
            {
                weight = Random.Range(25f, 500f);
            }
            else if (flags.BasicWeight && flags.WeightClasses)
            {
                weight = dots switch
                {
                    1 => Random.Range(25f, 125f), // 50 - 125
                    2 => Random.Range(25f, 250f), // 125 - 250
                    3 => Random.Range(25f, 500f), // 250 - 500
                    _ => Random.Range(25f, 48f),
                };
            }
            else
            {
                weight = Random.Range(25f, 48f);
            }
        }
    }

    void SetValidStamp()
    {
        if (flags.CountryStamps)
        {
            country = Random.Range(1, 6);
            var validStamps = envelopeGenerator.GetValidStamp(country);
            stamp = validStamps.ElementAt(Random.Range(0, validStamps.Count()));
        }
        else
        {
            country = 0;
            stamp = 0;
        }
    }

    void GenerateInvalid()
    {
        // 1 shape
        // 2 color
        // 3 stamp
        // 4 weight

        var possibleWrongThings = new List<int>() { 1, 2 };

        if (flags.CountryStamps)
        {
            possibleWrongThings.Add(3);
        }

        if (flags.BasicWeight || flags.WeightClasses)
        {
            possibleWrongThings.Add(4);
        }

        // make a list of things that can be wrong
        // pick a random number of things that are wrong
        // pick a random thing that is wrong and remove it from the list
        var extraWrong = Random.Range(1, 6);

        var thingToBeWrongIndex = Random.Range(0, possibleWrongThings.Count());
        var thingToBeWrong = possibleWrongThings.ElementAt(thingToBeWrongIndex);
        possibleWrongThings.RemoveAt(thingToBeWrongIndex);

        GenerateInvalidThing(thingToBeWrong);

        // 2/5 chance to have another thing be wrong
        if (extraWrong >= 4)
        {
            thingToBeWrongIndex = Random.Range(0, possibleWrongThings.Count());
            thingToBeWrong = possibleWrongThings.ElementAt(thingToBeWrongIndex);
            possibleWrongThings.RemoveAt(thingToBeWrongIndex);
            GenerateInvalidThing(thingToBeWrong);
        }

        isValid = false;
    }

    void GenerateInvalidThing(int whatIsInvalid = 1)
    {
        if (whatIsInvalid == 1) // shape
        {
            shape = Random.Range(4, 6);
        }
        else if (whatIsInvalid == 2) // color
        {
            color = Random.Range(5, 8);
        }
        else if (whatIsInvalid == 3) // stamp
        {
            SetInvalidStamp();
        }
        else if (whatIsInvalid == 4) // weight
        {
            SetInvalidWeight();
        }
    }

    private void SetInvalidStamp()
    {
        if (country == 5) // kebak(5) peut pas avoir un stamp invalide so change le pays at random
        {
            country = Random.Range(1, 5);
        }

        // randomize le id de la stamp jusqua temps que ça soit pas valide pour le country
        var validStamps = envelopeGenerator.GetValidStamp(country);
        var thisStamp = validStamps.ElementAt(0);
        while (validStamps.Contains(thisStamp))
        {
            thisStamp = Random.Range(1, 29);
        }

        stamp = thisStamp;
    }

    private void SetInvalidWeight()
    {
        if (!isBig)
        {
            weight = Random.Range(50f, 1000f);
        }
        else
        {
            if (!flags.WeightClasses)
            {
                weight = Random.Range(500f, 1000f);
            }
            else
            {
                weight = dots switch
                {
                    1 => Random.Range(125f, 1000f), // 50 - 125
                    2 => Random.Range(250f, 1000f), // 125 - 250
                    3 => Random.Range(500f, 1000f), // 250 - 500
                    _ => Random.Range(50f, 1000f),
                };
            }
        }
    }

    public void OnBeginDrag(DragObj2D eventData)
    {
        var scale = GameObject.FindGameObjectWithTag("Scale")?.GetComponent<Scale>();
        if (scale != null)
        {
            scale.Reset();
        }
    }
}
