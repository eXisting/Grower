using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.Interfaces
{
	public interface IMoveableBall
	{
		void Move(Vector3 _target);

		event Action OnMovementStart;
	}
}
