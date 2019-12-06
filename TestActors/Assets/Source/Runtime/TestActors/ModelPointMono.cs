using System.Runtime.CompilerServices;
using Common;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModelPointMono : MonoBehaviour
{

	private float sidePosMultMin = 0.9f;
	private float sidePosMultMax = 1.1f;

	private float2x3 posToMove;

	private float distanceFull;
	private float velocityToOneSecond;
	private float observedDistance;


	private Vector2 posEnd;
	private float timeToFinish;

	private Transform tr;

	Obj obj;


	void Start()
	{
		tr = transform;

		posEnd = new Vector2(Random.Range(0f + 5f, 640f - 5f), Random.Range(0f + 5f, 1280 - 5f));
		timeToFinish = Random.Range(600f, 600f);


		var position1 = tr.position;
		posToMove = new float2x3
		{
			c0 = new float2(position1.x, position1.y),
			c1 = new float2(posEnd.x, posEnd.y),
			c2 = default
		};

		distanceFull = posToMove.c0.AlphaBetaMagnitude(posToMove.c1);
		var sidePosMult = Random.Range(sidePosMultMin, sidePosMultMax);
		var nPosSide = posToMove.c0.CenterSidePosMONO(posToMove.c1).PosToRadiusMONO(distanceFull * 0.25f * sidePosMult);
		velocityToOneSecond = distanceFull / timeToFinish;

		posToMove.c2 = nPosSide;
		observedDistance = 0;


		obj = new Obj();
		obj.entity = default;

		var position = position1;
		var localScale = tr.localScale;
		obj.properties = new float2x4
		{
			c0 = new float2(position.x, position.y),
			c1 = new float2(localScale.x, localScale.y)
		};

		obj.rotation = tr.rotation;


		obj.properties.c2 = new float2(-1, -1);


		var posAndSize = new float2x2
		{
			c0 = new float2(position1.x, position1.y),
			c1 = obj.collBox.posAndSize.c1
		};
		obj.collBox = obj.entity.NewCollBox(posAndSize, new float2(10f, 10f), obj.rotation.ToEulerAnglesZ());


		obj.typeCollider = TypeCollider.Box;
		obj.collBox = default;
		obj.collCircle = default;
		obj.collLine = default;
		obj.collPoint = default;
	}


	void Update()
	{
		// расчет новой точки
		var velocityToOneFrame = velocityToOneSecond * Time.deltaTime;
		observedDistance += velocityToOneFrame;
		var t = observedDistance / distanceFull;
		if (t > 1f) t = 1f;
		var newPos = t.CalculateBesierPosMONO(posToMove.c0, posToMove.c2, posToMove.c1);

		// Обновление своего коллайдера
		obj.properties.c0 = newPos;
		var posAndSize = new float2x2
		{
			c0 = newPos,
			c1 = obj.collBox.posAndSize.c1
		};
		obj.collBox = obj.entity.NewCollBox(posAndSize, new float2(10f, 10f), obj.rotation.ToEulerAnglesZ());

		// перемещение на новую позицию
		tr.position = new Vector3(newPos.x, newPos.y);

#if UNITY_EDITOR
		DebugDrowBox(obj.collBox, Color.blue, Time.deltaTime);
#endif
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void DebugDrowBox(in Box box, in Color colorDebug, in float deltaIn)
	{
		//Debug.Log($" 22222 box.vertex.c0 = {box.vertex.c0} || name2 = {box.vertex.c1}");
		Debug.DrawLine((Vector2) box.vertex.c0, (Vector2) box.vertex.c1, colorDebug, Time.deltaTime);
		Debug.DrawLine((Vector2) box.vertex.c1, (Vector2) box.vertex.c2, colorDebug, Time.deltaTime);
		Debug.DrawLine((Vector2) box.vertex.c2, (Vector2) box.vertex.c3, colorDebug, Time.deltaTime);
		Debug.DrawLine((Vector2) box.vertex.c3, (Vector2) box.vertex.c0, colorDebug, Time.deltaTime);
	}
}
    
