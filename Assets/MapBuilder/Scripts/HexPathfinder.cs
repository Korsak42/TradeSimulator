using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPathfinder : MonoBehaviour
{
	int count = 0;
	int minimum = int.MaxValue;
	public int Count
	{
		get
		{
			return count;
		}
	}

	List<HexCell> list = new List<HexCell>();
	public List<HexCell> CreatePath(HexCell cell1, HexCell cell2)
	{
		var returnList = new List<HexCell>();
		returnList.Add(cell1);
		var distance = cell1.Coordinates.DistanceTo(cell2.Coordinates);
		var newDisance = distance;
		do
		{
			foreach (HexCell c in cell1.GetNeighbors())
			{
				if (c != null)
				{
					var newDistance = c.Coordinates.DistanceTo(cell2.Coordinates);
					if (newDistance < distance)
					{
						returnList.Add(c);
						cell1 = c;
						distance = c.Coordinates.DistanceTo(cell2.Coordinates);
						break;
					}
				}
			}
		}
		while (distance > 1);
		returnList.Add(cell2);
		return returnList;
	}

	public List<HexCell> CreatePathWithRoadsCheck(HexCell cell1, HexCell cell2)
	{
		var returnList = new List<HexCell>();
		returnList.Add(cell1);
		var distance = cell1.Coordinates.DistanceTo(cell2.Coordinates);
		var newDisance = distance;
		do
		{
			foreach (HexCell c in cell1.GetNeighbors())
			{
				if (c != null)
				{
					var newDistance = c.Coordinates.DistanceTo(cell2.Coordinates);
					if (newDistance < distance)
					{
						returnList.Add(c);
						cell1 = c;
						distance = c.Coordinates.DistanceTo(cell2.Coordinates);
						break;
					}
				}
			}
		}
		while (distance > 1);
		returnList.Add(cell2);
		returnList = SearchRoadsWithNeighbors(returnList);
		return returnList;
	}

	private List<HexCell> SearchRoadsWithNeighbors(List<HexCell> cells)
    {
		var returnList = new List<HexCell>();
		foreach (HexCell cell in cells)
        {
			if (cell.HasRoads)
			{
				returnList.Add(cell);
			}
			else
            {
				returnList.Add(SearchRoadsInNeighbors(cell));
				break;
            }
        }
		if (returnList.Count == cells.Count)
			return cells;
		else
			return returnList;
    }

	private HexCell SearchRoadsInNeighbors(HexCell cell)
    {
		var neighborsList = new List<HexCell>(cell.GetNeighbors());
		foreach(HexCell c in neighborsList)
        {
			if (c.HasRoads)
				return c;
        }
		throw new System.NotImplementedException();
    }

	public void Enqueue(HexCell cell)
	{
		count += 1;
		int priority = cell.SearchPriority;
		if (priority < minimum)
		{
			minimum = priority;
		}

		while (priority >= list.Count)
		{
			list.Add(null);
		}
		cell.NextWithSamePriority = list[priority];
		list[priority] = cell;
	}

	public HexCell Dequeue()
	{
		count -= 1;
		for (; minimum < list.Count; minimum++)
		{
			HexCell cell = list[minimum];
			if (cell != null)
			{
				list[minimum] = cell.NextWithSamePriority;
				return cell;
			}
		}
		return null;
	}

	public void Change(HexCell cell, int oldPriority)
	{
		HexCell current = list[oldPriority];
		HexCell next = current.NextWithSamePriority;
		if (current == cell)
		{
			list[oldPriority] = next;
		}
		else
		{
			while (next != cell)
			{
				current = next;
				next = current.NextWithSamePriority;
			}
			current.NextWithSamePriority = cell.NextWithSamePriority;
		}
		Enqueue(cell);
		count -= 1;
	}

	public void Clear()
	{
		list.Clear();
		count = 0;
		minimum = int.MaxValue;
	}

}