using Common;

public class StarterActorsJobs2 : StarterBase
{
    
    protected override void Setup()
    {
        
        base.Setup();
        Add<ProcessorMoveBezier_job2>();
    }
}