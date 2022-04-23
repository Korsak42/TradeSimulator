
public class Clergy : Serviceman
{
    public override void GlobalInit()
    {
        StratType = EnumStrats.Clergy;
        base.GlobalInit();
    }
    public override void ServiceWork()
    {
        if (Happy > 1)
        {
            Settlement.ChangePiety(DataKeeper.instance.Constants.MinFloatStep, true);
        }
        else
        {
            Settlement.ChangePiety(DataKeeper.instance.Constants.MinFloatStep, false);
        }
    }
}
