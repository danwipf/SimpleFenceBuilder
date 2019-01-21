using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFenceBuilder;

namespace SimpleFenceBuilder{   

    public static class SFB_PivotAdjustment
    {
        public static PivotTransform SFB_PivotUpdate(this PivotTransform pt){
            
            //reposition
            for(int i = 0; i< pt.vertices.Length; i++){
                pt.vertices[i] = pt.OriginalVertices[i] + pt.PivotOffset;
            }
            //rotation
            Quaternion PivotRotation = Quaternion.Inverse(pt.OriginalRotation);
            for( int i = 0; i < pt.vertices.Length; i++ )
                {
                    pt.vertices[i] = PivotRotation * pt.vertices[i];
                    pt.normals[i] = PivotRotation * pt.normals[i];
    
                    Vector3 tangentDir = PivotRotation * pt.tangents[i];
                    pt.tangents[i] = new Vector4( tangentDir.x, tangentDir.y, tangentDir.z, pt.tangents[i].w );
                }

            //recalculate and apply

            pt.mesh.vertices = pt.vertices;
            pt.mesh.normals = pt.normals;
            pt.mesh.tangents = pt.tangents;

            pt.mesh.RecalculateBounds();
            if(pt.Pivot_Transform.GetComponent<Collider>() != null){
            SFB_RecalculateColliderBounds(pt.mesh.bounds,pt);}
            return pt;
        }
        public static PivotTransform SFB_PivotReset(this PivotTransform pt){
            
            if(pt.Pivot_Transform_INT != pt.Pivot_Transform){
                pt.mesh = pt.Pivot_Transform.GetComponent<MeshFilter>().sharedMesh;
        
                pt.OriginalVertices = pt.mesh.vertices;
                pt.vertices = pt.mesh.vertices;
                pt.normals = pt.mesh.normals;
                pt.tangents = pt.mesh.tangents;

                pt.OriginalRotation = pt.Pivot_Transform.localRotation;
                Vector3 OriginaPosition = pt.Pivot_Transform.position;
                pt.Pivot_Transform.position = Vector3.zero;

                //center pivot to model
                pt.PivotOffset = pt.Pivot_Transform.position - pt.Pivot_Transform.GetComponent<Renderer>().bounds.center;
                pt.SFB_PivotUpdate();
                pt.Pivot_Transform.position = OriginaPosition;
                pt.OriginalVertices = pt.mesh.vertices;
                pt.Pivot_Transform_INT = pt.Pivot_Transform;
            }
            return pt;
        }
        public static PivotTransform SFB_CalcMinMaxOffset(this PivotTransform pt){
            Vector3 pvo3 = new Vector3();
            pt.OuterOffset = Mathf.Clamp(pt.OuterOffset,1,pt.OuterOffset);
            pvo3.x = Mathf.Lerp(pt.mesh.bounds.size.x * pt.OuterOffset,-pt.mesh.bounds.size.x * pt.OuterOffset,(pt.OffsetX + 1) / 2) / 2;
            pvo3.y = Mathf.Lerp(pt.mesh.bounds.size.y * pt.OuterOffset,-pt.mesh.bounds.size.y * pt.OuterOffset,(pt.OffsetY + 1) / 2) / 2;
            pvo3.z = Mathf.Lerp(pt.mesh.bounds.size.z * pt.OuterOffset,-pt.mesh.bounds.size.z * pt.OuterOffset,(pt.OffsetZ + 1) / 2) / 2;
            pt.PivotOffset = pvo3;
            return pt; 
        }
        static void SFB_RecalculateColliderBounds(Bounds b,PivotTransform pt){
            Collider col = pt.Pivot_Transform.GetComponent<Collider>();
            if(col.GetType() == typeof(BoxCollider)){
                pt.Pivot_Transform.GetComponent<BoxCollider>().center = b.center;
            }
            if(col.GetType() == typeof(SphereCollider)){
                pt.Pivot_Transform.GetComponent<SphereCollider>().center = b.center;
            }
            if(col.GetType() == typeof(CapsuleCollider)){
                pt.Pivot_Transform.GetComponent<CapsuleCollider>().center = b.center;
            }
        }
    }
}
