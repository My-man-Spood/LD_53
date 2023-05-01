using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Asseter : MonoBehaviour
{
    public static Shapes[] shapeArray = new Shapes[] { Shapes.Circle, Shapes.Square, Shapes.Triangle, Shapes.Star, Shapes.Hexagon, Shapes.Octagon };
    public static Colors[] colorArray = new Colors[] { Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow, Colors.Purple, Colors.Orange, Colors.Cyan };

    public List<ShapeId> shapeIds;
    public List<Sprite> dotSprites;

    public List<Sprite> countryIds;
    public List<Sprite> stampIds;

    private IEnumerable<Shapes> levelShapes;
    private IEnumerable<Colors> levelColors;

    public static Asseter Instance;

    void Awake()
    {
        Instance = this;

        levelShapes = ChooseRandomElements(shapeArray, 6);
        levelColors = ChooseRandomElements(colorArray, 7);
    }

    public Sprite GetShape(int shapeIndex)
    {
        var shape = levelShapes.ElementAt(shapeIndex - 1);
        return shapeIds.First(x => x.Shape == shape).sprite;
    }

    public Color GetColor(int colorIndex)
    {
        var color = levelColors.ElementAt(colorIndex - 1);
        return ColorsColor(color);
    }

    public Sprite GetCountry(int countryId)
    {
        if (countryId == 0)
        {
            return null;
        }

        return this.countryIds[countryId - 1];
    }

    public Sprite GetStamp(int stampId)
    {
        if (stampId == 0)
        {
            return null;
        }

        return this.stampIds[stampId - 1];
    }

    public Sprite GetDots(int dots)
    {
        return dotSprites[dots - 1];
    }

    public static UnityEngine.Color ColorsColor(Colors color)
    {
        return color switch
        {
            Colors.Red => UnityEngine.Color.red,
            Colors.Green => UnityEngine.Color.green,
            Colors.Blue => UnityEngine.Color.blue,
            Colors.Yellow => UnityEngine.Color.yellow,
            Colors.Purple => new UnityEngine.Color(0.5f, 0, 0.5f),
            Colors.Orange => new UnityEngine.Color(1, 0.5f, 0),
            Colors.Cyan => UnityEngine.Color.cyan,
            _ => UnityEngine.Color.white,
        };
    }

    public static T[] ChooseRandomElements<T>(T[] array, int numElements)
    {
        if (numElements > array.Length)
        {
            throw new ArgumentException("numElements cannot be greater than the length of the array.");
        }

        T[] chosenElements = new T[numElements];
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < numElements; i++)
        {
            int randIndex;

            do
            {
                randIndex = UnityEngine.Random.Range(0, array.Length);
            } while (usedIndices.Contains(randIndex));

            chosenElements[i] = array[randIndex];
            usedIndices.Add(randIndex);
        }

        return chosenElements;
    }
}

public enum Shapes
{
    Circle,
    Square,
    Triangle,
    Star,
    Hexagon,
    Octagon,
}

public enum Colors
{
    Red,
    Green,
    Blue,
    Yellow,
    Purple,
    Orange,
    Cyan,
}

[System.Serializable]
public class ShapeId
{
    public Shapes Shape;
    public Sprite sprite;
}
