using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DemandsList", order = 6)]
public class DemandsList : SerializedScriptableObject
{
    [NonSerialized, OdinSerialize, ShowInInspector]
    public Dictionary<EnumStrats, List<EnumResource.ResourceName>> Demands;
    [Button]
    public List<Resource> GetDemands(EnumStrats stratType)
    {
        List<EnumResource.ResourceName> resourceNames = Demands[stratType];
        List<Resource> resources = new();
        foreach (EnumResource.ResourceName name in resourceNames)
        {
            resources.Add(ResourceFactory.CreateResource(name));
        }
        return resources;
    }
}
