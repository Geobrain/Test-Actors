using Common;

public class StarterActors : StarterBase
{
    protected override void Setup()
    {
        
        base.Setup();
        Add<ProcessorMoveBezier>();
    }
    

}