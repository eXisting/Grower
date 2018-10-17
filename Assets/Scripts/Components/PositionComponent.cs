﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Spheres.Core
{

	[Serializable]
	public struct Position : IComponentData
	{
		public Vector3 Value;
	}

	public class PositionComponent : ComponentDataWrapper<Position> { }

}