using System.Drawing;
using System.Linq;
using UnityEngine;
using Color = UnityEngine.Color;

namespace MarchingObjects
{
	public class Sphere : Object
	{
		public const int c_SphereByteSize = c_ObjectByteSize + sizeof(float);

		[Range(1f, 10f)] public float size;
		
		public MarchingSphere ToMarchingSphere()
		{
			return new MarchingSphere
			{
				Object = ToMarchingObject(),
				Size = size
			};
		}
	}

	public struct MarchingSphere
	{
		public MarchingObject Object;
		public float Size;
	}
}