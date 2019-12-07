using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;


public enum TypeCollider { NoCollider, Box, Circle, Line, Point}

public struct Box
{
	public bool Exist
    {
    	[MethodImpl(MethodImplOptions.AggressiveInlining)]
    	get => posAndSize.c1.x != 0f && posAndSize.c1.y != 0f ;
    }
	
	//Координаты центра - box.c0
	
	//             			box.c1.x
	// vertex.c1-----------------------------------------------vertex.c2
	//        |             									|        
	//        |    			box.c0								| 	box.c1.y
	//        |             									|
	// vertex.c0-------------------------------------------------vertex.c3
	        
	
	public float2x2 posAndSize;
	public float2x4 vertex;
	
	public ent entity;
}




public static partial class UtilsCollBox
{

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Box NewBox(this in ent entity, in float2 pos, in float2 newBoxSize, float eulerAngleZ = 0)
	{
		Box box = new Box {entity = entity, posAndSize = new float2x2 {c0 = pos, c1 = newBoxSize}};
		return box.Box(box.posAndSize.c1, eulerAngleZ);
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Box NewCollBox(this in ent entity, in float2x2 posAndSize, in float2 newBoxSize, float eulerAngleZ)
	{
		Box box = new Box();
		box.entity = entity;
		box.posAndSize = posAndSize;
		return box.Box(newBoxSize, eulerAngleZ);
	}
	
	
	// возвращает новый бокс, который всегда вписан в outBox
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Box NewBoxInBox(this in Box outBox, in ent entity, in float2 pos, in float2 newBoxSize, float eulerAngleZ = 0)
	{
		var newPos = new float2();
		var inBoxSizeHalf = newBoxSize * 0.5f;
		newPos.x = math.clamp(pos.x, outBox.vertex.c0.x + inBoxSizeHalf.x, outBox.vertex.c2.x - inBoxSizeHalf.x);
		newPos.y = math.clamp(pos.y, outBox.vertex.c0.y + inBoxSizeHalf.y, outBox.vertex.c2.y - inBoxSizeHalf.y);
		return entity.NewBox(newPos, newBoxSize, eulerAngleZ);
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ref Box Box(this ref Box box, in float2 newBoxSize, float eulerAngleZ)
	{
		float radius = Radius(newBoxSize.x, newBoxSize.y);
		float аSin = Asin(newBoxSize.y, radius);
		float radian = Radian(eulerAngleZ);
		box.vertex = new float2x4
		{
			c0 = box.posAndSize.c0.PosA(radius, аSin, radian),
			c1 = box.posAndSize.c0.PosB(radius, аSin, radian),
			c2 = box.posAndSize.c0.PosC(radius, аSin, radian),
			c3 = box.posAndSize.c0.PosD(radius, аSin, radian)
		};
		
#if UNITY_EDITOR
		//if (debugOnShow != DebugOnShow.None) box.DebugBufferBox(debugOnShow, color);
#endif
		
		return ref box;
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Radius(in float width, in float height) => math.sqrt((width*0.5f)*(width*0.5f) + (height*0.5f)*(height*0.5f));

	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Asin(in float height, in float radius) => math.asin(height / (2*radius));
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Radian(in float angle) => math.radians(angle);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 PosA(in this float2 posCenter, in float radius, in float аSin, in float radian)
	{
		math.sincos(radian + math.PI + аSin, out var s, out var c);
		return new float2(posCenter.x + c*radius,posCenter.y + s*radius);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 PosB(in this float2 posCenter, in float radius, in float аSin, in float radian)
	{
		math.sincos(radian + math.PI - аSin, out var s, out var c);
		return new float2(posCenter.x + c*radius,posCenter.y + s*radius);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 PosC(in this float2 posCenter, in float radius, in float аSin, in float radian)
	{
		math.sincos(radian + аSin, out var s, out var c);
		return new float2(posCenter.x + c*radius,posCenter.y + s*radius);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 PosD(in this float2 posCenter, in float radius, in float аSin, in float radian)
	{
		math.sincos(radian - аSin, out var s, out var c);
		return new float2(posCenter.x + c*radius,posCenter.y + s*radius);
	}
		

}
