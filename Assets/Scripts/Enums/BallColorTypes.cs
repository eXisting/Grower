
using UnityEngine;

namespace Enums
{
	public enum BallColor
	{
		Purple,
		Pink,
		Blue,
		Red,
		Orange
	}

	public partial class ColorGenerator
	{
		static public Color32 Get(BallColor _color)
		{
			switch (_color)
			{
				case BallColor.Purple:
					return new Color32(175,0,155,255);
				case BallColor.Pink:
					return new Color32(255,0,150,255);
				case BallColor.Blue:
					return new Color32(0,0,255,255);
				case BallColor.Red:
					return new Color32(255,0,0,255);
				case BallColor.Orange:
					return new Color32(255,190,0,255);
				default:
					return new Color32();
			}
		}
	}
}
