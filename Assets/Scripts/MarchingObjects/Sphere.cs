using System.Drawing;
using System.Linq;
using UnityEngine;
using Color = UnityEngine.Color;

namespace MarchingObjects
{
	public class Sphere : Object
	{
		public static readonly int SphereByteSize = c_ObjectByteSize + sizeof(float);

		[Range(1f, 10f)] public float size;
		
		public MarchingSphere ToMarchingSphere()
		{
			return new MarchingSphere
			{
				LocalToWorld = transform.worldToLocalMatrix,
				Size = size,
				Color = color
			};
		}
	}

	public struct MarchingSphere
	{
		public Matrix4x4 LocalToWorld;
		public float Size;
		public Color Color;
	}
}