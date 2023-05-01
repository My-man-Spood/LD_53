using System.Linq;
using EasyDragAndDrop.Core;
using UnityEngine;

public class PostageSlot : MonoBehaviour
{
    public EnvelopeStack stack;

    public int Shape;
    public int Color;
    public int Dots;

    private EnvelopeGenerator generator;
    private LevelsManager levelsManager;
    private FeatureFlags flags;

    public void Awake()
    {
        levelsManager = GameObject.FindGameObjectWithTag(LevelsManager.Tag).GetComponent<LevelsManager>();
        flags = LevelsManager.flags[levelsManager.currentLevel - 1];
        generator = GameObject.FindGameObjectWithTag(EnvelopeGenerator.Tag).GetComponent<EnvelopeGenerator>();
    }

    public void OnDrop(DragObj2D eventData)
    {
        print("eventData: " + eventData.name);

        var envelope = eventData.GetComponent<Envelope>();
        stack.AddEnvelope(envelope.isBig ? 3 : 2);

        if (CheckValid(envelope))
        {
            print("CORRECT!");
            Destroy(eventData.gameObject);
            GameObject.FindGameObjectWithTag(EnvelopeGenerator.Tag).GetComponent<EnvelopeGenerator>().Success();
        }
        else
        {
            print("INCORRECT!");
            Destroy(eventData.gameObject);
            GameObject.FindGameObjectWithTag(EnvelopeGenerator.Tag).GetComponent<EnvelopeGenerator>().Failure();
        };
    }

    public bool CheckValid(Envelope envelope)
    {
        bool colorValid = true;
        bool dotsValid = true;
        bool shapeValid = true;
        bool weightValid = true;
        bool stampValid = true;

        shapeValid = envelope.shape == Shape;
        colorValid = envelope.color == Color;

        if (flags.BasicWeight)
        {
            if (envelope.isBig)
            {
                weightValid = envelope.weight < 500;
            }
            else
            {
                weightValid = envelope.weight < 50;
            }
        }

        if (flags.WeightClasses)
        {
            dotsValid = envelope.dots == Dots;
            colorValid = envelope.dots > 0 ? true : envelope.color == Color;

            if (!envelope.isBig)
            {
                weightValid = envelope.weight < 50;
            }
            else
            {
                weightValid = envelope.dots switch
                {
                    1 => envelope.weight < 125, // 50 - 125
                    2 => envelope.weight < 250, // 125 - 250
                    3 => envelope.weight < 500, // 250 - 500
                    _ => envelope.weight < 50,
                };
            }
        }

        if (flags.CountryStamps)
        {
            stampValid = generator.GetValidStamp(envelope.country).Contains(envelope.stamp);
        }

        return shapeValid && colorValid && dotsValid && stampValid && weightValid;
    }
}
