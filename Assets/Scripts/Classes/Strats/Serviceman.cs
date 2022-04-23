
public class Serviceman : Strat, IServiceman
{
    private void Awake()
    {
        GlobalLinkStrat();
        SubscribeServiceman();
    }

    private void SubscribeServiceman()
    {
        GlobalData.instance.SubsribeServiceman(this);
    }

    public virtual void ServiceWork()
    {

    }
}
