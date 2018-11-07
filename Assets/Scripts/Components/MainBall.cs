using Base.AbstractClasses;
using Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Components
{
	public class MainBall : Ball, IDestroyableBall
	{
		public event Action OnBallDestroy;

		public void Destroy()
		{
			Debug.Log("Game is over");
		}
	}
}
