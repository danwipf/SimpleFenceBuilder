using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFenceBuilder;

    [ExecuteInEditMode]
    public class SFB_PivotChanger : MonoBehaviour {
    public bool AlwaysChangePivot = false;
	public PivotTransform piv_struct = new PivotTransform();
    public void Update(){
        piv_struct = piv_struct.SFB_Setup(AlwaysChangePivot);    
        }
    }


