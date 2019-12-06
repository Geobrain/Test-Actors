using Common;

public class StarterActorsJobs : StarterBase
{
    
    protected override void Setup()
    {
        
        base.Setup();
        Add<ProcessorMoveBezier_job>();
    }
    

}