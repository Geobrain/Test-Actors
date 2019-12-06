using Common;

public class StarterActorsThreaded : StarterBase
{
    
    protected override void Setup()
    {
        base.Setup();
        Add<ProcessorMoveBezier_Threaded>();
    }
    
}