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




public static partial class UtilsColl
{


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Radius(in float width, in float height) =>
    	Mathf.Sqrt(Mathf.Pow(width * 0.5f, 2f) + Mathf.Pow(height * 0.5f, 2f));
	
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Asin(in float height, in float radius) => Mathf.Asin(height / (2 * radius));
	
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Radian(in float angle) => angle * Mathf.Deg2Rad;
	
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 PosA(in this float2 posCenter, in float radius, in float аSin, in float radian) => 
	    new float2(posCenter.x + Mathf.Cos(radian + Mathf.PI + аSin) * radius,posCenter.y + Mathf.Sin(radian + Mathf.PI + аSin) * radius);
	
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 PosB(in this float2 posCenter, in float radius, in float аSin, in float radian) => 
	    new float2(posCenter.x + Mathf.Cos(radian + Mathf.PI - аSin) * radius,posCenter.y + Mathf.Sin(radian + Mathf.PI - аSin) * radius);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 PosC(in this float2 posCenter, in float radius, in float аSin, in float radian) => 
	    new float2(posCenter.x + Mathf.Cos(radian + аSin) * radius,posCenter.y + Mathf.Sin(radian + аSin) * radius);
	
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 PosD(in this float2 posCenter, in float radius, in float аSin, in float radian) => 
	    new float2(posCenter.x + Mathf.Cos(radian - аSin) * radius,posCenter.y + Mathf.Sin(radian - аSin) * radius);
    

	
	
}

