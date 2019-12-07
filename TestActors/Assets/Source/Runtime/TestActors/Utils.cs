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

        float abs_inphase = math.abs(a.x - b.x);
        float abs_quadrature = math.abs(a.y - b.y);
        
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
        angles = math.atan2(sinyCosp, cosyCosp);
        return angles * 180f;
    }
    
    
    // Случайная точка на окружности указанного радиуса
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 PosToRadius(this float2 p0, float currentRadius)
    {
        float rad = math.radians(Rand.rnd.NextFloat(0, 360));
        math.sincos(rad, out var s, out var c);
        float p1x = currentRadius * c + p0.x;
        float p1y = currentRadius * s + p0.y;
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
        var alpha = math.radians(Rand.rnd.NextBool() ? 90 : 270);
        math.sincos(alpha, out var s, out var c);
        return new float2(trPx.x + rx * c - ry * s, trPx.y + rx * s + ry * c);
    } 
    
    // B(t) = (1-t)2P0 + 2(1-t)tP1 + t2P2
    // t - меняется от 0 до 1 (это расстояние меняется от 0 до конечной точки)
    // p0 - стартовая точка
    // p1 - боковая точка отклонения дуги
    // p2 - конечная точка
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 CalculateBesierPos(this float t, float2 p0, float2 p1, float2 p2)
    {
        return (1 - t)*(1 - t) * p0 + 2 * (1 - t) * t * p1 + t*t * p2;
    }
    
}
    

public static partial class UtilsMono
{
	// Случайная точка на окружности указанного радиуса
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 PosToRadiusMONO(this float2 p0, float currentRadius)
	{
		float rad = math.radians(Random.Range(0, 360));
		math.sincos(rad, out var s, out var c);
		float p1x = currentRadius * c + p0.x;
		float p1y = currentRadius * s + p0.y;
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
		var alpha = math.radians(Random.Range(0f, 1f) < 0.5f ? 90 : 270);
		math.sincos(alpha, out var s, out var c);
		return new float2(trPx.x + rx * c - ry * s, trPx.y + rx * s + ry * c);
	} 
	
	// B(t) = (1-t)2P0 + 2(1-t)tP1 + t2P2
	// t - меняется от 0 до 1 (это расстояние меняется от 0 до конечной точки)
	// p0 - стартовая точка
	// p1 - боковая точка отклонения дуги
	// p2 - конечная точка
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float2 CalculateBesierPosMONO(this float t, float2 p0, float2 p1, float2 p2)
	{
		return (1 - t)*(1 - t) * p0 + 2 * (1 - t) * t * p1 + t*t * p2;
	}

}


