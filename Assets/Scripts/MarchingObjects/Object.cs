using System.Linq;
using UnityEngine;

namespace MarchingObjects
{
	public abstract class Object : MonoBehaviour
	{
		protected const int c_ObjectByteSize = sizeof(float) * 3 + sizeof(float) * 4;

		public Color color;
	}
}