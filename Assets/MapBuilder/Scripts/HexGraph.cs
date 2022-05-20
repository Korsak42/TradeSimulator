using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Sirenix.OdinInspector;

public class HexGraph : MonoBehaviour
{
    public List<GraphConnection> Connections;

    List<HexCell> unvisited = new List<HexCell>();

    public void Create(HexCell[] cells)
    {
        foreach (HexCell cell in cells)
        {
            foreach (HexCell neighbourCell in cell.GetNeighbors())
            {
                if (neighbourCell == null) continue;
                if (!Connections.Where(c => c.OriginCell == neighbourCell && c.TargetCell == cell).Any())
                {
                    var newConnection = new GraphConnection(cell, neighbourCell,
                        cell.Weight + neighbourCell.Weight);
                    Connections.Add(newConnection);
                }
            }
        }
    }

    [Button]
    public List<HexCell> CreatePath(HexCell[] cells, HexCell start, HexCell end)
    {
        List<HexCell> pathList = new List<HexCell>();

        if (start == null || end == null)
        {
            throw new ArgumentNullException();
        }
        if (start == end)
        {
            pathList.Add(start);
            return pathList;
        }
        Dictionary<HexCell, HexCell> previous = new Dictionary<HexCell, HexCell>();

        Dictionary<HexCell, int> vertices = new Dictionary<HexCell, int>();

        foreach (HexCell cell in cells)
        {
            //vertices.Add(cell, start.Coordinates.DistanceTo(cell.Coordinates));
            unvisited.Add(cell);

            vertices.Add(cell, int.MaxValue);
        }

        vertices[start] = 0;

        while(unvisited.Count != 0)
        {
            unvisited = unvisited.OrderBy(cell => vertices[cell]).ToList();
            HexCell current = unvisited[0];
            unvisited.Remove(current);
            if(current == end)
            {
                while(previous.ContainsKey(current))
                {
                    pathList.Add(current);
                    current = previous[current];
                }
                pathList.Add(current);
                return pathList;
            }
            for (int i = 0; i < current.GetNeighbors().Count(); i++)
            {
                if (current.GetNeighbor((HexDirection)i) != null)
                {
                    HexCell neighbor = current.GetNeighbor((HexDirection)i);
                    int length = current.Weight + neighbor.Weight +
                        neighbor.Coordinates.DistanceTo(end.Coordinates);
                    int alt = vertices[current] + length;

                    if (alt < vertices[neighbor])
                    {
                        vertices[neighbor] = alt;
                        previous[neighbor] = current;
                    }
                }
            }
        }

        List<HexCell> calculated = new List<HexCell>();
        int totalLength = 0;
        for (int i = 0; i < pathList.Count; i++)
        {
            HexCell cell = pathList[i];
            for(int j = 0; j < cell.GetNeighbors().Count(); j++)
            {
                HexCell neighbour = cell.GetNeighbor((HexDirection)j);
                if (pathList.Contains(neighbour) && !calculated.Contains(neighbour))
                {
                    totalLength += cell.Weight + neighbour.Weight +
                    neighbour.Coordinates.DistanceTo(end.Coordinates);
                }
            }
            calculated.Add(cell);
        }
        return calculated;
    }


    GraphConnection VisitToVertice(HexCell cell, int totalWeight)
    {
        List<GraphConnection> connections = GetAllConnections(cell);
        foreach (GraphConnection c in connections)
        {

            c.Weight += totalWeight;
        }
        GraphConnection connection = GetMinimalConnectionByWeight(connections);
        return connection;
    }


    List<GraphConnection> GetAllConnections(HexCell cell)
    {
        return Connections.Where(c => c.OriginCell == cell || c.TargetCell == cell).ToList();
    }

    GraphConnection GetMinimalConnectionByWeight(List<GraphConnection> graphConnections)
    {
        var comparisonValue = int.MaxValue;
        var returnConnection = new GraphConnection();
        foreach (GraphConnection connection in graphConnections)
        {
            if (connection.Weight < comparisonValue)
            {
                comparisonValue = connection.Weight;
                returnConnection = connection;
            }
        }
        return returnConnection;
    }

    HexCell GetTargetCellFromConnection(GraphConnection connection)
    {
        return connection.TargetCell;
    }

    public List<HexCell> FindPath(HexCell originCell, HexCell targetCell)
    {
        List<HexCell> pathList = new List<HexCell>();
        if (originCell == null || targetCell == null)
        {
            throw new ArgumentNullException();
        }
        if (originCell = targetCell)
        {
            pathList.Add(originCell);
            return pathList;
        }

        List<HexCell> unvisited = new List<HexCell>();

        Dictionary<HexCell, HexCell> previous = new Dictionary<HexCell, HexCell>();

        Dictionary<HexCell, int> distances = new Dictionary<HexCell, int>();

        while (unvisited.Count != 0)
        {
            unvisited = unvisited.OrderBy(cell => distances[cell]).ToList();
        }

        HexCell current = unvisited[0];

        unvisited.Remove(current);
        if(current == targetCell)
        {
            while (previous.ContainsKey(current))
            {
                pathList.Add(current);

                current = previous[current];
            }

            pathList.Add(current);
            return pathList;
        }

        for(int i = 0; i < current.GetNeighbors().Count(); i++)
        {
            HexCell neighbor = current.GetNeighbor((HexDirection)i);

            int weight = current.Weight + neighbor.Weight;

            int alt = distances[current] + weight;

            if(alt < distances[neighbor])
            {
                distances[neighbor] = alt;
                previous[neighbor] = current;
            }
        }

        return pathList;
    }

}
