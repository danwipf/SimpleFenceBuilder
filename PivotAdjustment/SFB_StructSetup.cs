using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;
namespace SimpleFenceBuilder{
	public static class SFB_StructSetup {
		
		public static void ReImport(this GameObject go){
			if(go.scene.IsValid()){
				string realPath = Application.dataPath;
				realPath.Remove(realPath.Length -6);
				string selectedPath = realPath + AssetDatabase.GetAssetPath(go);
				string[] fileEntries = Directory.GetFiles(selectedPath,"*",SearchOption.AllDirectories);
				for(int i = 0; i<fileEntries.Length; i++){
					string file = fileEntries[i];
					file = file.Replace("\\","/");
					file = file.Remove(0,realPath.Length);
					if(file.Contains(PrefabUtility.GetCorrespondingObjectFromSource(go).name) && !file.Contains(".meta")){
						AssetDatabase.ImportAsset("Assets"+file);
						Debug.Log(" Object was succesfully reimported at: "+"<color=#e0771a><i>"+"Assets"+file+"</i></color>");
					}
				}
			}else{
					Debug.Log("Can Only Re-Import Scene Objects.");
			}
		}
		public static PivotTransform[] SFB_ArraySetup(this PivotTransform[] pt,bool always = false){
			
			for(int i = 0; i<pt.Length; i++){
				pt[i] = pt[i].SFB_Setup();
			}
			return pt;
		}
		public static PivotTransform SFB_Setup(this PivotTransform pt, bool always = false){
			
			if(pt.Pivot_Transform != null){
				if(pt.ReImport){
					ReImport(pt.Pivot_Transform.gameObject);
					pt.ReImport = false;
				}
				if(pt.ChangePivot){
					pt = pt.SFB_CalcMinMaxOffset();
            		pt = pt.SFB_PivotUpdate();
					pt.ChangePivot = false;
				}
				if(always){
					pt = pt.SFB_CalcMinMaxOffset();
            		pt = pt.SFB_PivotUpdate();
				}
				pt = pt.SFB_PivotReset();
				}
			return pt;
		}
	}
}


	
