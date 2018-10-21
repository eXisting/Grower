using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Spheres.Core
{

	[Serializable]
	public struct Size : IComponentData
	{
		public float Value;
	}

	public class SizeComponent : ComponentDataWrapper<Size> { }

}
