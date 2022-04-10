using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ResourseList", order = 1)]
public class ResourceList : ScriptableObject
{
    [SerializeField]
    public List<Resource> Resources;

    public float GetDefaultPrice(EnumResource.ResourceName name)
    {
        return FindResource(name).Price;
    }

    public double GetDefaultAmount(EnumResource.ResourceName name)
    {
        return FindResource(name).Amount;
    }

    public int GetDefaultPriority(EnumResource.ResourceName name)
    {
        return FindResource(name).Priority;
    }

    public Resource FindResource(EnumResource.ResourceName name)
    {
        var returnRes = Resources.Where(x => x.Name == name).FirstOrDefault();
        if (returnRes == null)
            Debug.Log("Didn't find: " + name);
        return returnRes;
    }
    public Resource DeepCopy(Resource resource)
    {
        Resource res = new Resource(resource.Name, resource.Amount, resource.Price, resource.Priority);
        return res;
    }

    public Resource DeepCopy(EnumResource.ResourceName name, double amount)
    {
        Resource res = new Resource(name, amount, GetDefaultPrice(name), GetDefaultPriority(name));
        return res;
    }

    public List<Resource> DeepCopyList()
    {
        List<Resource> returnList = new List<Resource>();
        foreach (Resource r in Resources)
        {
            returnList.Add(DeepCopy(r));
        }

        return returnList;
    }

    public Resource GetRandomResource()
    {
        int r = Random.Range(0, Resources.Count - 1);
        return Resources[r];
    }


}
