using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Spheres.Core
{

	[Serializable]
	public struct Spawn : IComponentData
	{
		public float Value;
	}

	public class SpawnComponent : ComponentDataWrapper<Spawn> { }

}