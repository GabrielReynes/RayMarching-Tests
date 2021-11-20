using System.Linq;
using UnityEngine;

namespace MarchingObjects
{
	public abstract class Object : MonoBehaviour
	{
		//									 - LocalToWorldMatrix - Color	
		protected const int c_ObjectByteSize = sizeof(float) * 16 + sizeof(float) * 4;
		
		public Color color;
	}
}