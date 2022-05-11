using System.Linq;
public class Serviceman : Strat, IServiceman
{
    public float Salary;
    private void Awake()
    {
        SubscribeServiceman();
    }

    private void SubscribeServiceman()
    {
        GlobalData.instance.SubsribeServiceman(this);
    }

    public virtual void SetSalary()
    {

    }

    public virtual void ServiceWork()
    {

    }

    public virtual void GetSalary()
    {
        var noble = Settlement.GetStrats().FirstOrDefault(x => x.StratType == EnumStrats.Noble) as Noble;
        noble.PaySalary(this, Salary * GetPopulation());

    }
}
