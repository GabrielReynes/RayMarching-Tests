using UnityEngine;

namespace MarchingObjects
{
	public class Cube : Object
	{
		//														   - Size              - RoundingRadius
		public static readonly int CubeByteSize = c_ObjectByteSize + sizeof(float) * 3 + sizeof(float);

		[Range(0f, 5f)] public float roundingRadius;
		public Vector3 size;
		
		public MarchingCube ToMarchingCube()
		{
			return new MarchingCube
			{
				LocalToWorld = transform.worldToLocalMatrix,
				Size = size,
				Color = color,
				RoundingRadius = roundingRadius
			};
		}
	}

	public struct MarchingCube
	{
		public Matrix4x4 LocalToWorld;
		public Vector3 Size;
		public Color Color;
		public float RoundingRadius;
	}
}