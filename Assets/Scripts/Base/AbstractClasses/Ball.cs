using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.AbstractClasses
{
	abstract public class Ball : MonoBehaviour
	{
		public float Radius { get; set; }

		public Color32 Color { get; set; }
	}
}
