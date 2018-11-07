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
	public class RegularBall : Ball, IDestroyableBall, IMoveableBall
	{
		public event Action OnMoveBegan;
		public event Action OnMoveEnded;
		public event Action OnBallDestroy;

		public void Destroy()
		{
			Debug.Log("Item is about to be destroyed");
		}

		public void Move()
		{
			Debug.Log("Item is about to move");
		}
	}
}
