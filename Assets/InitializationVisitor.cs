using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]

public class InitializationVisitor : SerializedMonoBehaviour
{
    public static InitializationVisitor instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    private void StratInit(List<Strat> strats)
    {
        foreach (IStrat strat in strats)
        {
            strat.GlobalInit();
        }
    }

    private void SettlementInit(List<ISettlement> settlements)
    {
        foreach(ISettlement settlement in settlements)
        {
            settlement.GlobalInit();
        }
    }

    private void MarketInit(List<IMarket> markets)
    {
        foreach (IMarket market in markets)
        {
            market.GlobalInit();
        }
    }

    private void ProducerInit(List<IProducer> producers)
    {
        foreach(IProducer producer in producers)
        {
            producer.ProducerGlobalInit();
        }
    }

    private void ServicemanInit(List<IServiceman> servicemen)
    {
        foreach (IServiceman serviceman in servicemen)
        {

        }
    }

    private void NobleInit(List<INoble> nobles)
    {
        foreach (INoble noble in nobles)
        {

        }
    }
}
