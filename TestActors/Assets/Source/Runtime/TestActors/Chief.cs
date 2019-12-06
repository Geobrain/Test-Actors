using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Common;
using Pixeye.Actors;
using UnityEngine;

namespace Common
{
	
	public class Chief : Actor
	{


		protected override void Setup()
		{
			entity.Set(global::Tag.TypeChief);

		}

	}

}


