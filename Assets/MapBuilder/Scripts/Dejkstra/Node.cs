using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The Node.
/// </summary>
public class Node : MonoBehaviour
{

    /// <summary>
    /// The connections (neighbors).
    /// </summary>
    [SerializeField]
    protected List<Node> m_Connections = new List<Node>();
    protected HexCell cell;

    /// <summary>
    /// Gets the connections (neighbors).
    /// </summary>
    /// <value>The connections.</value>
    public virtual List<Node> connections
    {
        get
        {
            return m_Connections;
        }
        set
        {
            connections = new List<Node>();
        }
    }

    public Node this[int index]
    {
        get
        {
            return m_Connections[index];
        }
    }
}

