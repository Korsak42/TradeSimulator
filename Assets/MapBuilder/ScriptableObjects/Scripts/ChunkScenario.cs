using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChunkScenario", order = 1)]

public class ChunkScenarios : SerializedScriptableObject
{
    public Dictionary<EnumTerrain, int> terrains;

    public bool HasRoad;
    public bool HasCity;

    public int GetPercentageOfTerrain(EnumTerrain terrain)
    {
        return terrains[terrain];
    }

    public List<EnumTerrain> GetTerrain()
    {
        return terrains.Keys.ToList();
    }

    public bool CheckDictionary()
    {
        int sum = 0;
        foreach (KeyValuePair<EnumTerrain, int> pair in terrains)
        {
            sum += terrains[pair.Key];
        }
        if (sum == 100)
        {
            Debug.Log($"Dictionary {this.name} settings is correct");
            return true;
        }
        else
        {
            Debug.Log($"Dictionary {this.name} settings is correct");
            return false;
        }    
    }
}
