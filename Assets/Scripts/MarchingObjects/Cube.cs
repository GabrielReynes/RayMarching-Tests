using UnityEngine;

namespace MarchingObjects
{
	public class Cube : Object
	{
		public static readonly int CubeByteSize = c_ObjectByteSize + sizeof(float) * 3;
			
		public Vector3 size;

		public MarchingCube ToMarchingCube()
		{
			return new MarchingCube
			{
				Position = transform.position,
				Size = size,
				Color = color
			};
		}
	}

	public struct MarchingCube
	{
		public Vector3 Position;
		public Vector3 Size;
		public Color Color;
	}
}