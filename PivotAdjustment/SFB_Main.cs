using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFenceBuilder;

    [ExecuteInEditMode]
    public class SFB_Main : MonoBehaviour {

	public PivotTransform piv_struct = new PivotTransform();
    public void Update(){
        piv_struct = piv_struct.SFB_Setup();    
        }
    }


