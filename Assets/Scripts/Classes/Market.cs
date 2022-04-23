using UnityEngine;

public class Market : MonoBehaviour, IMarket
{
    [SerializeField]
    public Warehouse Warehouse;
    private void Awake()
    {
        SubscribeMarket();
    }
    public void GlobalInit()
    {
        Warehouse.TestInit();
    }

    public void SubscribeMarket()
    {
        GlobalData.instance.SubscribeMarket(this);
    }

    public float CalculatePrice(Resource resource, double needsAmount, double warehouseAmount)
    {
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(needsAmount / warehouseAmount * resourceList.GetDefaultPrice(resource.Name));
        return returnValue;
    }

    public float CalculatePrice(Resource resource, double amount)
    {
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(amount / Warehouse.GetAmount(resource) * Mathf.Pow(resourceList.GetDefaultPrice(resource.Name), 2));
        return returnValue;
    }

    public float CalculateSellPrice(Resource resource, double amount)
    {
        Debug.Log("Calculating Sell Price");
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(amount / Warehouse.GetAmount(resource) * Mathf.Pow(resourceList.GetDefaultPrice(resource.Name), 2)) * 0.95f ;
        return returnValue;
    }
    public float CalculateBuyPrice(Resource resource, double amount)
    {
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(amount / Warehouse.GetAmount(resource) * Mathf.Pow(resourceList.GetDefaultPrice(resource.Name), 2)) * 1.05f;
        return returnValue;
    }
}
