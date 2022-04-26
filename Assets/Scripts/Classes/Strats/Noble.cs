using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Noble : Strat, INoble
{
    public float BureaucratsSalary;
    public float SoldiersSalary;
    private void Awake()
    {
        GlobalLinkStrat();
        SubscribeNoble();
    }
    public override void GlobalInit()
    {
        StratType = EnumStrats.Noble;
        Settlement.SetOwner(this);
        base.GlobalInit();
    }

    public void CollectTaxes(double amount)
    {
        ChangeGold(amount, true);
    }

    public void SubscribeNoble()
    {
        GlobalData.instance.SubscribeNoble(this);
    }

    public void PaySalary(Strat strat, double salary)
    {
        ChangeGold(salary, false);
        strat.ChangeGold(salary, true);
    }
}
