//  Project : ecs
// Contacts : Pix - ask@pixeye.games

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using UnityEngine;

//namespace Pixeye.Framework
//{
	public class Tag : ITag
	{
		
		[TagField(categoryName = "None")] public const int None = 0;
		
		#region GameStateGlobal 5 40
		
		//[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_MountObj = 8;

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_OnScene = 9;
		
		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_NotSolve = 10;

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_HitEquationToPlayer = 11;
		//[TagField(categoryName = "GameState")] public const int GameState_EndHitToPlayer = 12;
		
		

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_RestartAttack = 12;

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_DecisionWrong = 13;
		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_DecisionRight = 14;


		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_Solve = 15;

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_HitToEnemy = 16;

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_GameOver = 17;
		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_GameComplete = 18;


		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_Map_CaretOn = 19;

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_Map_LevelUpOn = 20;
		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_Map_OnMapMini = 21;
		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_Map_OnMap = 22;

		[TagField(categoryName = "GameStateGlobal")] public const int StateGlobal_EquationMoveToPlayerOn = 23;



		#endregion
		
		#region StateLocal 41 60
		
		[TagField(categoryName = "StateLocal")] public const int StateLocal_CompleteInstall_AllMountModel = 41;
		[TagField(categoryName = "StateLocal")] public const int StateLocal_CompleteInstallTile = 42;

		#endregion
		
		#region GroupEntity 61 80
		

		[TagField(categoryName = "GroupEntity")] public const int group_GridSectors = 61;
		[TagField(categoryName = "GroupEntity")] public const int group_TileAll = 62;
		[TagField(categoryName = "GroupEntity")] public const int group_TileBorderGuardList = 63;
		[TagField(categoryName = "GroupEntity")] public const int group_TileUnderBorderGuard = 64;
		
		
		//[TagField(categoryName = "Debug")] public const int StateInactive = 10;

		#endregion
		
		#region Marker 100 199
		
		//[TagField(categoryName = "Marker")] public const int Marker_ParentChildBro = 119;
		[TagField(categoryName = "Marker")] public const int Marker_OnAnimPhys = 120;
		//[TagField(categoryName = "Marker")] public const int Marker_OnMove = 121;

		/* Marker_UpdateCore - центральная точка между процессорами трансформа и остальными -
		 если в процессоре трансформа добавлен маркер - процессоры коллайдера и спатиала портишена 
		 - ориентируются на этот маркер и вносят изменения относительно ядра*/
		//[TagField(categoryName = "Marker")] public const int Marker_UpdateCore = 122;
		
		
		//[TagField(categoryName = "Marker")] public const int Marker_UpdatePosition = 123;
		//[TagField(categoryName = "Marker")] public const int Marker_UpdatePosition = 124;
		//[TagField(categoryName = "Marker")] public const int Marker_UpdateSize = 125;
		
		[TagField(categoryName = "Marker")] public const int Marker_UpdateObj = 126;
		//[TagField(categoryName = "Marker")] public const int Marker_Move = 127;
		
		//[TagField(categoryName = "Marker")] public const int Marker_UpdateCollider= 126;
		//[TagField(categoryName = "Marker")] public const int Marker_UpdateSpatialPartition= 127;
		
		
		//[TagField(categoryName = "Marker")] public const int Marker_OutOfBehaviour = 130;
		
		
		#endregion
		
		#region AI 400 599
		[TagField(categoryName = "AI")] public const int AI_Starter = 400;
		
		[TagField(categoryName = "AI")] public const int AI_OffLowBehavior = 401;
		
		
		
		[TagField(categoryName = "AI")] public const int AI_Chief_OnWorkers = 402;

		//[TagField(categoryName = "AI")] public const int AI_InOnButton = 402;

		[TagField(categoryName = "AI")] public const int AI_AttackOnPlayer = 460;
		[TagField(categoryName = "AI")] public const int AI_AttackOnEnemy = 461;

		[TagField(categoryName = "AI")] public const int AI_Move = 462;
		
		[TagField(categoryName = "AI")] public const int AI_Wander = 520;
		
		[TagField(categoryName = "AI")] public const int AI_PreparingAttack0 = 530;
		[TagField(categoryName = "AI")] public const int AI_PreparingAttack1 = 531;
		[TagField(categoryName = "AI")] public const int AI_Attack = 535;
		[TagField(categoryName = "AI")] public const int AI_Hit = 536;

		[TagField(categoryName = "AI")] public const int AI_Teleport = 541;
		
		
		[TagField(categoryName = "AI")] public const int AI_Check = 555;
		[TagField(categoryName = "AI")] public const int AI_Action = 556;
		
		//[TagField(categoryName = "AI")] public const int AI_Calculations = 560;
		[TagField(categoryName = "AI")] public const int AI_Populations = 561;
		
		
		[TagField(categoryName = "AI")] public const int AI_Death = 599;

		
		[TagField(categoryName = "AI")] public const int AI_Test = 570;
		
		[TagField(categoryName = "AI")] public const int AI_Map_levelUP = 571;
		
		[TagField(categoryName = "AI")] public const int AI_CreateWorker = 572;
		
		
		//[TagField(categoryName = "AI")] public const int AI_Worker_Create_0 = 573;
		[TagField(categoryName = "AI")] public const int AI_Worker_OnActive = 574;
		[TagField(categoryName = "AI")] public const int AI_Worker_OnProtuberance = 575;
		[TagField(categoryName = "AI")] public const int AI_Worker_DellAllCreate = 576;
		[TagField(categoryName = "AI")] public const int AI_Map_levelToStartPos = 577;
		
		#endregion
		
		#region ActionWagon 600 699
		
		[TagField(categoryName = "ActionWagon")]
		public const int ActionWagonGeneral = 600;
		
		
		
		#endregion


		#region Behaviors 700 799

		[TagField(categoryName = "Behaviors")] public const int ComponentActionCreate = 700;

		//[TagField(categoryName = "Behaviors")] public const int ComponentActionTransitionCamera = 701;
		[TagField(categoryName = "Behaviors")] public const int ComponentActionMove = 702;


		#endregion

		#region Debug 800 999

		[TagField(categoryName = "Debug")] public const int DebugOn = 800;
		[TagField(categoryName = "Debug")] public const int DebugMarker = 801;
		//[TagField(categoryName = "Debug")] public const int DebugOff = 1;

		//[TagField(categoryName = "Debug")] public const int StateActive = 5;
		//[TagField(categoryName = "Debug")] public const int StateInactive = 10;

		#endregion

		#region TypeEntity 1000 1899

		[TagField(categoryName = "TypeEntity")] public const int TypePlayer = 1000;

		[TagField(categoryName = "TypeEntity")] public const int TypeEnemy = 1001;

		[TagField(categoryName = "TypeEntity")] public const int TypeEquation = 1002;

		[TagField(categoryName = "TypeEntity")] public const int TypeFreeNumber = 1003;
 
		[TagField(categoryName = "TypeEntity")]public const int TypeExtendedCreatorTile = 1004;

		[TagField(categoryName = "TypeEntity")]public const int TypeTile = 1010;
		[TagField(categoryName = "TypeEntity")]public const int TypeProtuberance = 1011;
		//[TagField(categoryName = "TypeEntity")] public const int TypeProtuberance = 1100;

		[TagField(categoryName = "TypeEntity")]public const int TypeCamera = 1100;
		
		
		//[TagField(categoryName = "TypeEntity")]public const int TypeChiefGlobal = 1101;
		//[TagField(categoryName = "TypeEntity")]public const int TypeChiefLocal = 1102;
		[TagField(categoryName = "TypeEntity")]public const int TypeMountModel = 1103;
	
		[TagField(categoryName = "TypeEntity")]public const int TypeChief = 1600;
		[TagField(categoryName = "TypeEntity")]public const int TypeWorker = 1601;

		[TagField(categoryName = "TypeEntity")]public const int TypeActorLevelWorldMap = 1700;

		[TagField(categoryName = "TypeEntity")]public const int Type_MapTile = 1701;

		//[TagField(categoryName = "TypeEntity")] public const int TypeTileWorldMapUnique = 1702;
		[TagField(categoryName = "TypeEntity")]public const int TypeTileWorldMapTopLine = 1703;

		[TagField(categoryName = "TypeEntity")]public const int TypeTileWorldMapBottomLine = 1704;

		[TagField(categoryName = "TypeEntity")]public const int TypeTileBottomLower = 1705;


		[TagField(categoryName = "TypeEntity")] public const int TypeCheckColl = 1500;
		//[TagField(categoryName = "TypeEntity")] public const int TypeMap_ActorCreatorMountains = 1802;

		[TagField(categoryName = "TypeEntity")] public const int TypeGridZone = 1505;
		
		#endregion

		#region Tile 1900 1999

		//[TagField(categoryName = "Tile")] public const int Tile_Off = 1900;
		[TagField(categoryName = "Tile")] public const int Tile_On = 1901;

		[TagField(categoryName = "Tile")] public const int Tile_Protuberance_Pos_Up = 1902;
		[TagField(categoryName = "Tile")] public const int Tile_Protuberance_Pos_Down = 1903;


		[TagField(categoryName = "Tile")] public const int Tile_Level = 1905;
		[TagField(categoryName = "Tile")] public const int Tile_Level_AboveBorderGuardList = 1906;
		//[TagField(categoryName = "Tile")] public const int Tile_Level_BorderGuardList = 1907;
		[TagField(categoryName = "Tile")] public const int Tile_Level_UnderBorderGuardList = 1908;

		[TagField(categoryName = "Tile")] public const int Tile_Level_On = 1909;
		//[TagField(categoryName = "Tile")] public const int Tile_Level_Off = 1910;

		[TagField(categoryName = "Tile")] public const int Tile_Level_NumberPair = 1911;
		//[TagField(categoryName = "Tile")] public const int Tile_Level_onZonePlayer = 1912;
		[TagField(categoryName = "Tile")] public const int Tile_Level_Busy = 1913;
		[TagField(categoryName = "Tile")] public const int Tile_Level_UpColumn = 1914;


		#endregion

		#region StateEntity 2000 2999

		[TagField(categoryName = "StateEntity")]
		public const int ActorOn = 2000;

		[TagField(categoryName = "StateEntity")]
		public const int ActorPauseOn = 2010;

		//[TagField(categoryName = "StateEntity")] public const int alwaysVisible  = 2015;

		//[TagField(categoryName = "StateEntity")] public const int ActorSpecialActionOn = 2011;
		//[TagField(categoryName = "ActorState")] public const int actorMonoState1Off = 2010;


		[TagField(categoryName = "StateEntity")]
		public const int ActorAttackOn = 2200;

		//[TagField(categoryName = "StateEntity")] public const int ActorAnimPhysicalOn = 2300;

		#endregion

		#region GroupFilter 3000 3999

		[TagField(categoryName = "GroupFilter")]
		public const int groupInitOut = 3000;

		[TagField(categoryName = "GroupFilter")]
		public const int groupGetOutAll = 3001;

		[TagField(categoryName = "GroupFilter")]
		public const int groupFirstEntity = 3002;

		[TagField(categoryName = "GroupFilter")]
		public const int groupAverageEntity = 3003;

		[TagField(categoryName = "GroupFilter")]
		public const int groupLastEntity = 3004;

		[TagField(categoryName = "GroupFilter")]
		public const int groupsOtherEnter = 3010;

		[TagField(categoryName = "GroupFilter")]
		public const int groupsReleaseOn = 3020;

		[TagField(categoryName = "GroupFilter")]
		public const int Rigidbody2D = 3025;

		//[TagField(categoryName = "GroupFilter")] public const int TagStop = 3030;

		[TagField(categoryName = "GroupFilter")]
		public const int entRelease = 3035;



		#endregion

		#region Motion 4000 4999

		[TagField(categoryName = "Motion")] public const int ImpulseOn = 4000;
		[TagField(categoryName = "Motion")] public const int TeleportOn = 4010;


		#endregion

		#region GameMaster 5000 5999

		//[TagField(categoryName = "GameMaster")] public const int sceneOn = 5000;


		#endregion



		//разные под каждую игру

		#region Temporary 10000 10999

		[TagField(categoryName = "Temporary")] public const int TileTouchProtuberance = 10000;
		//[TagField(categoryName = "Temporary")] public const int EquationPublication = 10001;


		[TagField(categoryName = "Temporary")] public const int Test = 15000;
		[TagField(categoryName = "Temporary")] public const int Test2 = 15001;

		#endregion



		//..todo прикрутить проверку на наличие одинаковых чисел в классе таг.


	}

//}

public static class HelperTag
{
		
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static object GetFieldId(string fieldName)
	{
		object value = default;
		Type type = typeof(Tag);
		FieldInfo info = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
		value = info.GetValue(null);
        
		return value;
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static List<string> GetMembersString()
	{
		// Get the Type and MemberInfo.
		Type t = Type.GetType("Tag");
		if (t == default)
		{
			t = Type.GetType("Pixeye.Framework.Tag"); 
			if (t == default) Debug.LogError ("Не найден класс Tag в проекте");
		} 
		MemberInfo[] memberArray = t.GetMembers();
  
		// Get and display the type that declares the member.
		//Debug.Log($"memberArray.Length = {memberArray.Length} || t.FullName = {t.FullName}");

		List<string> memberString = new List<string>();
		foreach (var member in memberArray) 
		{
			if (member.DeclaringType.ToString() == t.FullName)
			{
				memberString.Add(member.Name);
			}
		}
		return memberString;
	}
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string TagToName(in this int tag)
	{
		List<string> listTagName = HelperTag.GetMembersString();
		
		for (int i = 1; i < listTagName.Count; i++)
		{
			int tagId =  (int) HelperTag.GetFieldId(listTagName[i]);
			
			if (tag == tagId)
			{
				return listTagName[i];
			}
		}

		return "Таг не найден";
	}
	
		
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void CheckDuplicateID()
	{
		List<string> listTagName = HelperTag.GetMembersString();
		List<int> listTagID = new List<int>();
		
		for (int i = 1; i < listTagName.Count; i++)
		{
			int tagId =  (int) HelperTag.GetFieldId(listTagName[i]);
			listTagID.Add(tagId);
		}

		var listDuplicateID = listTagID.GroupBy(x => x).Where (x => x.Count () != 1).Select (x => x.Key).ToList();
		
		if (listDuplicateID.Count() != 0)
		{
			var listDuplicateName = new List<string>();
			for (int i = 1; i < listTagName.Count; i++)
			{
				int tagId =  (int) HelperTag.GetFieldId(listTagName[i]);
				
				if (listDuplicateID.Any(t => t == tagId)) listDuplicateName.Add(listTagName[i]);

			}
			
			foreach (var v in listDuplicateName)
			{
				Debug.LogError ($"TAG дубликат: {v} = {(int) HelperTag.GetFieldId(v)}");
			}
			
		}

	}
}

