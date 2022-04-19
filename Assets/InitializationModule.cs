using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]

public class InitializationModule : SerializedMonoBehaviour
{
    public static InitializationModule instance;

    public List<ISettlement> Settlements = new();
    public List<IMarket> Markets = new();
    public List<IStrat> Strats = new();
    public List<IProducer> Producers = new();
    public List<IServiceman> Servicemen = new();
    public List<INoble> Nobles = new();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SettlementInit();
        StratInit();
        MarketInit();
        ProducerInit();
        ServicemanInit();
        NobleInit();
    }

    private void StratInit()
    {
        foreach (IStrat strat in Strats)
        {
            strat.GlobalInit();
        }
    }

    private void SettlementInit()
    {
        foreach(ISettlement settlement in Settlements)
        {
            settlement.GlobalInit();
        }
    }

    private void MarketInit()
    {
        foreach (IMarket market in Markets)
        {
            market.GlobalInit();
        }
    }

    private void ProducerInit()
    {
        foreach(IProducer producer in Producers)
        {
            producer.ProducerGlobalInit();
        }
    }

    private void ServicemanInit()
    {
        foreach (IServiceman serviceman in Servicemen)
        {

        }
    }

    private void NobleInit()
    {
        foreach (INoble noble in Nobles)
        {

        }
    }

    public void SubscribeStrat(Strat strat)
    {
        Strats.Add(strat);
    }
}
