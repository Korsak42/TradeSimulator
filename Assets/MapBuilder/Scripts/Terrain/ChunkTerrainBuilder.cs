using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunkTerrainBuilder : MonoBehaviour
{
    public List<ChunkScenarios> ChunkScenarios;
    
    public ChunkScenarios GetScenario(int scenarioIndex)
    {
        if (scenarioIndex > ChunkScenarios.Count - 1)
            return null;
        else
            return ChunkScenarios[scenarioIndex];
    }

    public void CreateRandomTerrain(List<HexCell> cells)
    {
        foreach(HexCell cell in cells)
        {
            var values = Enum.GetValues(typeof(EnumTerrain));
            cell.TerrainType = (EnumTerrain)UnityEngine.Random.Range(0, values.Length - 1);
        }
    }

    public void CreateChunkTerrain(List<HexCell> cells)
    {
        int i = 0;
        if (GetScenario(i) != null)
        {
            var scenario = GetScenario(i);
            for (int c = 0; c < cells.Count; c++)
            {
                var r = (int)UnityEngine.Random.Range(1, 99);
                SetTerrainType(scenario, cells[c], r);
            }
        }
    }

    public void SetTerrainType(ChunkScenarios scenario, HexCell cell, int randomValue)
    {
        foreach (KeyValuePair<EnumTerrain, int> pair in scenario.terrains)
        {
            if (randomValue < scenario.terrains[pair.Key])
                cell.TerrainType = pair.Key;
        }
    }
}
