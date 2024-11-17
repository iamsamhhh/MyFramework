using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace MyFramework{
public class ScriptableObjectEditor : EditorWindow
{
    private static ScriptableObjectEditor window;
    private List<ScriptableObject> scriptableObjectList = new List<ScriptableObject>();
    private ScriptableObject currenScriptableObject;

        [MenuItem("MyFramework/Framework/Util/ScrptableObject Editor")]
    public static void MenuClicked(){
        window = GetWindow<ScriptableObjectEditor>();
        window.titleContent = new GUIContent("ScriptableObject Editor");
    }

    private void OnGUI(){
        DrawList();
        DrawInspector();
    }

    private void OnEnable() {
        RefreshList();
    }

    private void RefreshList(){
        scriptableObjectList.Clear();
        var assetGUIDs = AssetDatabase.FindAssets ("t: ScriptableObject");
        foreach (var guid in assetGUIDs){
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if (scriptableObject)
                scriptableObjectList.Add(scriptableObject);
        }
    }

    private void DrawList(){
        GUILayout.BeginArea(
            new Rect(0, 0, window.position.width * 0.25f, window.position.height), 
            new GUIStyle("FrameBox")
        );

        foreach (var scriptableObject in scriptableObjectList){
            if(!scriptableObject)
                continue;
            if (GUILayout.Button(scriptableObject.name)){
                currenScriptableObject = scriptableObject;
            }
        }

        GUILayout.EndArea();
    }

    private void DrawInspector(){
        GUILayout.BeginArea (
            new Rect(window.position.width * .25f, y: 0, window.position.width * .75f, window.position.height), 
            new GUIStyle("FrameBox")
        );

        if(!currenScriptableObject) goto exit;

        var editor = Editor.CreateEditor(currenScriptableObject);
        editor.OnInspectorGUI();


        exit:
        GUILayout.EndArea();
    }
}
}