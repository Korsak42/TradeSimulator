using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Noble : Strat, INoble
{
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
}
