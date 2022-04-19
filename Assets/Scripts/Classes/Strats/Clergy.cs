
public class Clergy : Serviceman
{
    public override void ServiceWork(double amountConsumpted, double amountNeeded)
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
