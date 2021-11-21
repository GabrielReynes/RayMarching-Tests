using UnityEngine;

namespace MarchingObjects
{
	public class Cube : Object
	{
		//														   - Size              - RoundingRadius
		public const int c_CubeByteSize = c_ObjectByteSize + sizeof(float) * 3 + sizeof(float);

		[Range(0f, 5f)] public float roundingRadius;
		public Vector3 size;
		
		public MarchingCube ToMarchingCube()
		{
			return new MarchingCube
			{
				Object = ToMarchingObject(),
				Size = size,
				RoundingRadius = roundingRadius
			};
		}
	}

	public struct MarchingCube
	{
		public MarchingObject Object;
		public Vector3 Size;
		public float RoundingRadius;
	}
}