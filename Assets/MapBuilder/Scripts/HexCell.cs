using UnityEngine;
using UnityEngine.UI;

public class HexCell : MonoBehaviour
{
    public EnumTerrain TerrainType;
    public Canvas Canvas;
    public TMPro.TMP_Text Text;
    public HexCoordinates Coordinates;

    [SerializeField]
    HexCell[] neighbors;

    public Color Color;
    public RawImage TerrainFeature;

    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;

    }


}
