using Pixeye.Actors;

sealed partial class Model
{
    public static void Point(in ent entity)
    {
        ref var cObject = ref entity.Set<ComponentObject>();
        cObject.SetCache(entity);
    }


    
}

