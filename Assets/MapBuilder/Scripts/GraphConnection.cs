using System;

[Serializable]
public class GraphConnection
{
    public HexCell OriginCell;
    public HexCell TargetCell;

    public int Weight;

    public GraphConnection()
    {

    }

    public GraphConnection(HexCell origin, HexCell target, int weight)
    {
        OriginCell = origin;
        TargetCell = target;
        Weight = weight;
    }
}
