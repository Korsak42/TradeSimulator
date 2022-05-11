using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceFactory
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

                    LeavingsResource leavingsResource = new LeavingsResource();
                    _cache.Add(resourceName, leavingsResource);
                    return leavingsResource;
                }
            case EnumResource.ResourceName.Beer:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    BeerResource beerResource = new BeerResource();
                    _cache.Add(resourceName, beerResource);
                    return beerResource;
                }
            case EnumResource.ResourceName.BuildingsMaterial:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    BuildingMaterialsResource buildingMaterialsResource = new BuildingMaterialsResource();
                    _cache.Add(resourceName, buildingMaterialsResource);
                    return buildingMaterialsResource;
                }

            case EnumResource.ResourceName.RawMaterial:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    RawMaterialsResource rawMaterialsResource = new RawMaterialsResource();
                    _cache.Add(resourceName, rawMaterialsResource);
                    return rawMaterialsResource;
                }

            case EnumResource.ResourceName.Wine:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    WineResource wineResource = new WineResource();
                    _cache.Add(resourceName, wineResource);
                    return wineResource;
                }
            case EnumResource.ResourceName.Weapon:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    WeaponResource weaponResource = new WeaponResource();
                    _cache.Add(resourceName, weaponResource);
                    return weaponResource;
                }
            case EnumResource.ResourceName.Cloth:
                {
                    if (_cache.TryGetValue(resourceName, out Resource resource))
                        return resource;

                    ClothResource clothResource = new ClothResource();
                    _cache.Add(resourceName, clothResource);
                    return clothResource;
                }
            default:
                throw new ArgumentException($"Resource {resourceName} not implemented in factory");
        }
    }
}
