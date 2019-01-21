using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFenceBuilder
{
		[System.Serializable]public struct PivotTransform
		{	
			//[Pivot]
			public Transform Pivot_Transform;
			
			public bool ChangePivot;
			public bool ReImport;
			[Range(-1,1f)]public float OffsetX,OffsetY,OffsetZ;
			public float OuterOffset;

			//Internal
			[HideInInspector]public Vector3 PivotOffset;
			[HideInInspector]public Mesh mesh;
			[HideInInspector]public Vector3[] vertices;
			[HideInInspector]public Vector3[] normals;
			[HideInInspector]public Vector4[] tangents;
			[HideInInspector]public Vector3[] OriginalVertices;
			[HideInInspector]public Quaternion OriginalRotation;
			[HideInInspector]public Transform Pivot_Transform_INT;

		}
}
