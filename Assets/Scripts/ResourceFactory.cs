using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public  class ResourceFactory
{
    private static Dictionary<EnumResource.ResourceName, Resource> _cache = new();
    public static Resource CreateResource(EnumResource.ResourceName resourceName)
    {
        switch (resourceName)
        {
            case EnumResource.ResourceName.Food:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    FoodResource foodResource = new FoodResource();
                    _cache.Add(resourceName, foodResource);
                    return foodResource;
                }
            case EnumResource.ResourceName.Tools:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    ToolResource toolResource = new ToolResource();
                    _cache.Add(resourceName, toolResource);
                    return toolResource;
                }

            case EnumResource.ResourceName.Leavings:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    ToolResource leavingsResource = new ToolResource();
                    _cache.Add(resourceName, leavingsResource);
                    return leavingsResource;
                }
            default:
                throw new ArgumentException($"Resource {resourceName} not implemented in factory");
        }
    }
}
