using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StratFactory 
{
    public static BeggarsBuilder Beggars;
    public static PeasantsBuilder Peasants;
    public static ArtisansBuilder Artisans;
    public static SoldiersBuilder Soldiers;
    public static ClergyBuilder Clergy;
    public static BureaucratsBuilder Bureaucrats;
    public static NobleBuilder Noble;

    public static void FactoryInit()
    {
        GameObject go = GameObject.Find("StratBuilder");
        Beggars = go.GetComponent<BeggarsBuilder>();
        Peasants = go.GetComponent<PeasantsBuilder>();
        Artisans = go.GetComponent<ArtisansBuilder>();
        Soldiers = go.GetComponent<SoldiersBuilder>();
        Clergy = go.GetComponent<ClergyBuilder>();
        Bureaucrats = go.GetComponent<BureaucratsBuilder>();
        Noble = go.GetComponent<NobleBuilder>();
    }

    public static void CreateStrat(GameObject settlement, EnumStrats stratType)
    {
        if (Beggars == null)
            FactoryInit();
        switch(stratType)
        {
            case EnumStrats.Beggars:
                {
                    Beggars.Create(settlement);
                    break;
                }
            case EnumStrats.Peasants:
                {
                    Peasants.Create(settlement);
                    break;
                }
            case EnumStrats.Artisans:
                {
                    Artisans.Create(settlement);
                    break;
                }
            case EnumStrats.Soldiers:
                {
                    Soldiers.Create(settlement);
                    break;
                }
            case EnumStrats.Clergy:
                {
                    Clergy.Create(settlement);
                    break;
                }
            case EnumStrats.Bureaucrats:
                {
                    Bureaucrats.Create(settlement);
                    break;
                }
            case EnumStrats.Noble:
                {
                    Noble.Create(settlement);
                    break;
                }
        }
    }
}
