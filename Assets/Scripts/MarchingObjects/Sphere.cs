using System.Linq;
using UnityEngine;

namespace MarchingObjects
{
	public class Sphere : Object
	{
		public static readonly int SphereByteSize = c_ObjectByteSize + sizeof(float);
			
		public float size;

		public MarchingSphere ToMarchingSphere()
		{
			return new MarchingSphere
			{
				Position = transform.position,
				Size = size,
				Color = color
			};
		}
	}

	public struct MarchingSphere
	{
		public Vector3 Position;
		public float Size;
		public Color Color;
	}
}