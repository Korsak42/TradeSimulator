using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RoadsSprites", order = 8)]

public class RoadsTilesData : ScriptableObject
{
    public List<Texture> Roads;

    public List<Texture> Roads1;
    public List<Texture> Roads2;
    public List<Texture> Roads3;

    public List<Sprite> Crossroads;

    public Texture GetRoad(HexDirection _in, HexDirection _out)
    {
        var difference = GetDifference(_in, _out);
        Debug.Log(difference);
        switch (difference)
        {
            case 2:
                return RotateRoad(Roads1, _out);
            case 1:
                return RotateRoad(Roads2, _out);
            case 0:
                return RotateRoad(Roads3, _out);
        }
        Debug.Log($"Something wrong with HexDirection, difference is equal {difference}");
        return null;
    }
    [Button]
    private int GetDifference(HexDirection _in, HexDirection _out)
    {
        var difference = Mathf.Abs(_in - _out);
        if (difference == 5)
            difference = 0;
        if (difference == 4)
            difference = 1;

        return difference;
    }

    public Texture RotateRoad(List<Texture> roads, HexDirection _out)
    {
        switch (_out)
        {
            case HexDirection.E:
                return roads[0];
            case HexDirection.SE:
                return roads[1];
            case HexDirection.SW:
                return roads[2];
            case HexDirection.W:
                return roads[3];
            case HexDirection.NW:
                return roads[4];
            case HexDirection.NE:
                return roads[5];
        }
        Debug.Log($"Something wrong with HexDirection on RotateRoad");
        return null;
    }


    public void GetCrossRoad(List<HexDirection> directions)
    {

    }

}
