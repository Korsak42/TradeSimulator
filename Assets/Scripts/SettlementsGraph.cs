using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
[ShowOdinSerializedPropertiesInInspector]
public class SettlementsGraph : MonoBehaviour
{
    public int RoadDistance;
    public List<HexCell> Settlements;
    public struct SettlementsRelation
    {
        public HexCell Settlement1;
        public HexCell Settlement2;
    }

    public Dictionary<SettlementsRelation, bool> Graph = new Dictionary<SettlementsRelation, bool>();

    public void AddSettlementToList(HexCell cell)
    {
        Settlements.Add(cell);
    }

    public void CreateGraph()
    {
        for (int i = 0; i < Settlements.Count; i++)
        {
            for(int y = i+1; y < Settlements.Count; y++)
            {
                SettlementsRelation relation = new SettlementsRelation();
                relation.Settlement1 = Settlements[i];
                relation.Settlement2 = Settlements[y];
                if (Settlements[i].Coordinates.DistanceTo(Settlements[y].Coordinates) < RoadDistance)
                {
                    Graph.Add(relation, true);
                }
                else
                {
                    Graph.Add(relation, false);
                }
            }
        }
        DebugDict();
    }

    private void DebugDict()
    {
        int i = 0;
        foreach (KeyValuePair<SettlementsRelation, bool> g in Graph)
        {
            if (Graph[g.Key] == true)
                i++;
        }
        Debug.Log(i);
    }

    public SettlementsRelation GetNextConnectedSettlements()
    {
        var SR = new SettlementsRelation();
        foreach (KeyValuePair<SettlementsRelation, bool> g in Graph)
        {
            if (Graph[g.Key] == true)
            {
                Graph[g.Key] = false;
                return g.Key;
            }
        }
        return SR;
    }



}
