using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FeatureCellSwitcher : MonoBehaviour
{
    public HexCell HexCell;
    public FeatureCell Cell;

    public void SetFeatureCells()
    {
        ActivateCells();
        Cell.PlaceTextures(HexCell.TerrainType);
        DisableFeaturesOnRoads(HexCell.GetRoads());
        //Cell.DisplacePlaces();
        Cell.SetPriority();
    }

    private void ActivateCells()
    {
        Cell.ActivatePlaces();
    }

    public void DisableFeaturesOnRoads(bool[] roads)
    {
        for (int i = 0; i < Cell.Places.Count; i++)
        {
            for (int j = 0; j < roads.Length; j++)
            {
                if (roads[j])
                {
                    Cell.DeactivatePlace(i);
                }
            }
        }
        if (HexCell.HasRoads)
        {
            Cell.DeactivatePlace(7);
        }
    }

    [Button]
    public void SetPriority()
    {
        Cell.SetPriority();
    }
}
