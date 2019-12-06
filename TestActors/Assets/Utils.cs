using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;
using Rand = Common.Rand;
using Random = UnityEngine.Random;


public static class Utils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float AlphaBetaMagnitude(in this float2 a, in float2 b)
    {
        //sqrt(I^2 + Q^2) ~= alpha * max(|I|, |Q|) + beta * min(|I|, |Q|)
        //α =1 and β = .0 25  - оптимально для всех случаев
        //http://dspguru.com/dsp/tricks/magnitude-estimator/
        //http://www.claysturner.com/dsp/FastMagnitude.pdf
        
        float alpha = 1f;
        float beta = 0.25f;

        float abs_inphase = Math.Abs(a.x - b.x);
        float abs_quadrature = Math.Abs(a.y - b.y);
        
        return abs_inphase > abs_quadrature ?
              alpha * abs_inphase + beta * abs_quadrature
            : alpha * abs_quadrature + beta * abs_inphase;
    }


    // z-axis rotation
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToEulerAnglesZ(in this quaternion q)
    {
        float angles;
        float sinyCosp = +2.0f * (q.value.w * q.value.z + q.value.x * q.value.y);
        float cosyCosp = +1.0f - 2.0f * (q.value.y * q.value.y + q.value.z * q.value.z);
        angles = Mathf.Atan2(sinyCosp, cosyCosp); 
        return angles * 180f;
    }
    
    
    // Случайная точка на окружности указанного радиуса
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 PosToRadius(this float2 p0, float currentRadius)
    {
        float rad = Rand.rnd.NextFloat(0, 360) * Mathf.Deg2Rad;
        float p1x = currentRadius * Mathf.Cos(rad) + p0.x;
        float p1y = currentRadius * Mathf.Sin(rad) + p0.y;
        return new float2(p1x, p1y);
    }
    
    // В центре пути выбирается одна из боковых окружностей радиусом в полпути. На окружности берется боковая точка
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 CenterSidePos(this float2 startPos, float2 endPos)
    {
        //находим точку середины пути 
        float2 trPx = new float2((startPos.x + endPos.x) * 0.5f, (startPos.y + endPos.y) * 0.5f);

        //находим центр боковой окружности с радиусом половины пути между startPos-endPos.
        //Т.е. это точка сверху или снизу(случайно) строго от центра пути. На расстоянии в полпути от пути
        var rx = endPos.x - trPx.x;
        var ry = endPos.y - trPx.y;
        var alpha = Rand.rnd.NextBool() ? 90 * Mathf.Deg2Rad : 270 * Mathf.Deg2Rad;
        return new float2(trPx.x + rx * Mathf.Cos(alpha) - ry * Mathf.Sin(alpha), trPx.y + rx * Mathf.Sin(alpha) + ry * Mathf.Cos(alpha));
    } 
    
    // B(t) = (1-t)2P0 + 2(1-t)tP1 + t2P2
    // t - меняется от 0 до 1 (это расстояние меняется от 0 до конечной точки)
    // p0 - стартовая точка
    // p1 - боковая точка отклонения дуги
    // p2 - конечная точка
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 CalculateBesierPos(this float t, float2 p0, float2 p1, float2 p2)
    {
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
    }
    
}
    

public static partial class UtilsMono
{
	// Случайная точка на окружности указанного радиуса
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 PosToRadiusMONO(this float2 p0, float currentRadius)
	{
		float rad = Random.Range(0, 360) * Mathf.Deg2Rad;
		float p1x = currentRadius * Mathf.Cos(rad) + p0.x;
		float p1y = currentRadius * Mathf.Sin(rad) + p0.y;
		return new float2(p1x, p1y);
	}
	
	// В центре пути выбирается одна из боковых окружностей радиусом в полпути. На окружности берется боковая точка
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 CenterSidePosMONO(this float2 startPos, float2 endPos)
	{
		//находим точку середины пути 
		float2 trPx = new float2((startPos.x + endPos.x) * 0.5f, (startPos.y + endPos.y) * 0.5f);

		//находим центр боковой окружности с радиусом половины пути между startPos-endPos.
		//Т.е. это точка сверху или снизу(случайно) строго от центра пути. На расстоянии в полпути от пути
		var rx = endPos.x - trPx.x;
		var ry = endPos.y - trPx.y;
		var alpha = Random.Range(0f, 1f) < 0.5f ? 90 * Mathf.Deg2Rad : 270 * Mathf.Deg2Rad;
		return new float2(trPx.x + rx * Mathf.Cos(alpha) - ry * Mathf.Sin(alpha), trPx.y + rx * Mathf.Sin(alpha) + ry * Mathf.Cos(alpha));
	} 
	
	// B(t) = (1-t)2P0 + 2(1-t)tP1 + t2P2
	// t - меняется от 0 до 1 (это расстояние меняется от 0 до конечной точки)
	// p0 - стартовая точка
	// p1 - боковая точка отклонения дуги
	// p2 - конечная точка
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 CalculateBesierPosMONO(this float t, float2 p0, float2 p1, float2 p2)
	{
		return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
	}

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
		newPos.x = Mathf.Clamp(pos.x, outBox.vertex.c0.x + inBoxSizeHalf.x, outBox.vertex.c2.x - inBoxSizeHalf.x);
		newPos.y = Mathf.Clamp(pos.y, outBox.vertex.c0.y + inBoxSizeHalf.y, outBox.vertex.c2.y - inBoxSizeHalf.y);
		return entity.NewBox(newPos, newBoxSize, eulerAngleZ);
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ref Box Box(this ref Box box, in float2 newBoxSize, float eulerAngleZ)
	{
		var radius = UtilsColl.Radius(newBoxSize.x, newBoxSize.y);
		var аSin = UtilsColl.Asin(newBoxSize.y, radius);
		var radian = UtilsColl.Radian(eulerAngleZ);
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
	


}


