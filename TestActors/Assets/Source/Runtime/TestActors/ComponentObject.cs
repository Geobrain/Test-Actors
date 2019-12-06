using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;


public class ComponentObject
{
	public Transform tr;
	// размер LocalScale для обьектов с канвасом
	public RectTransform rectTransformLocalScale;
	
	public Obj obj;
	public ParentConstraint parentConstraint;

}



#region UTILITIES

public static partial class UtilsComponent
{
	    
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetCache(this ComponentObject component, in ent entity, in RectTransform rectTransformLocalScale = default)
	{
		component.tr = entity.transform;
		component.parentConstraint = component.tr.GetComponent<ParentConstraint>();

		Obj obj;
		obj.entity = entity;

		var position = component.tr.position;
		var localScale = component.tr.localScale;
		obj.properties = new float2x4
		{
			c0 = new float2(position.x, position.y),
			c1 = new float2(localScale.x, localScale.y)
		};
		
		obj.rotation = component.tr.rotation;
		
	    if (rectTransformLocalScale == default)
	    {
	        obj.properties.c2 = new float2(-1, -1);
	    }
	    else
	    {
	        component.rectTransformLocalScale = rectTransformLocalScale;
	        var rect = rectTransformLocalScale.rect;
	        obj.properties.c2 = new float2(rect.x, rect.y);
	    }

		obj.typeCollider = default;
		obj.collBox = default;
		obj.collCircle = default;
		obj.collLine = default;
		obj.collPoint = default;
        
		component.obj = obj;
	}

		
}

#endregion




#region HELPERS

static partial class component
{
	public const string Object = "ComponentObject";

	public static ref ComponentObject ComponentObject(in this ent entity) => ref StorageComponentObject.components[entity.id];
}

sealed class StorageComponentObject : Storage<ComponentObject>
{
	public override ComponentObject Create() => new ComponentObject();
	public override void Dispose(indexes disposed)
	{
		foreach (var id in disposed)
		{
			ref var component = ref components[id];
			component.tr = null;
			component.rectTransformLocalScale = null;
			component.parentConstraint = null;
		}
	}
}

#endregion


public struct Obj
{
	
	public bool Exist
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => entity != default;
	}
	
	 
	public ent entity; // 5 байт
	
	/// properties
	///	c0 = position
	/// c1 = localScale
	/// c2 = rectScale		находится минус 1 если обьект не имеет RectTransform!
	/// c4 = rotation   (transform.rotation.eulerAngles.z)
	public float2x4 properties; 
	
    public quaternion rotation;
	
	// коллайдер обьекта в гриде
	public TypeCollider typeCollider;
	public Box collBox;
	public float2 collCircle; // 8
	public float2 collLine; // 8
	public float2 collPoint; // 8
}




//public enum TypeObjLocalScale {Transform, RectTransform}

//ChildBig - ,больше ячейки грида - ищется отдельно
//public enum TypeSPChild
//{
//	ChildBig, // поиск ведется вне зон грида
//	ChildMid, // может иметь обьемный коллайдер - круглый или прямоугольный
//	ChildLine, // коллайдер в виде линии - как у меча
//	ChildPoint, // коллайдер - точка
//	None //  не регистрируется в SpatialPartition
//} 
