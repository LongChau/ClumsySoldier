using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProjectPanel : EditorWindow {

    [MenuItem("Window/Project Panel")]
    static void ShowProjectPanel()
    {
        GetWindow<ProjectPanel>(false, "Project Panel");
    }    

    void FindIngameElements()
    {
    }

    private void OnEnable()
    {
        FindIngameElements();
    }

    private void OnHierarchyChange()
    {
        FindIngameElements();
    }

    private void OnGUI()
    {
        EditorGUIUtility.labelWidth = 60;

        EditorGUILayout.Separator();
        //ShowCanvasGroups();
        //ShowHeaderFooter();

        if (GUILayout.Button("Reset"))
        {
            //ResetUIToDefault();
        }
    }

    //private void ShowHeaderFooter()
    //{
    //    if (header != null)
    //    {
    //        EditorGUI.BeginChangeCheck();
    //        bool b = EditorGUILayout.Toggle("Header", header.activeSelf);
    //        if (EditorGUI.EndChangeCheck())
    //        {
    //            Undo.RecordObject(header, "Show/Hide Header");
    //            header.SetActive(b);
    //        }
    //    }

    //    if (footer != null)
    //    {
    //        EditorGUI.BeginChangeCheck();
    //        bool b = EditorGUILayout.Toggle("Footer", footer.activeSelf);
    //        if (EditorGUI.EndChangeCheck())
    //        {
    //            Undo.RecordObject(header, "Show/Hide Header");
    //            footer.SetActive(b);
    //        }
    //    }

    //    if (mailContent != null)
    //    {
    //        EditorGUI.BeginChangeCheck();
    //        bool b = EditorGUILayout.Toggle("Mail", mailContent.activeSelf);
    //        if (EditorGUI.EndChangeCheck())
    //        {
    //            Undo.RecordObject(header, "Show/Hide Mail Content");
    //            mailContent.SetActive(b);
    //        }
    //    }
    //}

    //private void ShowCanvasGroups()
    //{
    //    if (canvasGroups != null)
    //    {
    //        //EditorGUI.indentLevel++;
    //        EditorGUILayout.BeginHorizontal();
    //        EditorGUI.BeginChangeCheck();
    //        selectedCanvas = EditorGUILayout.Popup("Group", selectedCanvas, canvasGroupNames);
    //        if (EditorGUI.EndChangeCheck())
    //        {
    //            Undo.RecordObject(canvasGroups[selectedCanvas], "Switch Canvas");
    //            for (int i = 0; i < canvasGroups.Length; i++)
    //            {
    //                if (i == selectedCanvas)
    //                {
    //                    canvasGroups[i].interactable = true;
    //                    canvasGroups[i].alpha = 1;
    //                    canvasGroups[i].gameObject.SetActive(true);
    //                }
    //                else
    //                {
    //                    canvasGroups[i].interactable = false;
    //                    canvasGroups[i].alpha = 0;
    //                    canvasGroups[i].gameObject.SetActive(false);
    //                }
    //            }
    //        }
    //        EditorGUILayout.EndHorizontal();
    //        //EditorGUI.indentLevel--;
    //    }
    //}

    //void ResetUIToDefault()
    //{
    //    if (canvasGroups != null)
    //    {
    //        selectedCanvas = 0;
    //        Undo.RecordObject(canvasGroups[selectedCanvas], "Reset Canvas");
    //        for (int i = 0; i < canvasGroups.Length; i++)
    //        {
    //            if (i == selectedCanvas)
    //            {
    //                canvasGroups[i].interactable = true;
    //                canvasGroups[i].alpha = 1;
    //            }
    //            else
    //            {
    //                canvasGroups[i].interactable = false;
    //                canvasGroups[i].alpha = 0;
    //            }

    //            canvasGroups[i].gameObject.SetActive(true);
    //        }
    //    }

    //    if (header != null)
    //    {
    //        Undo.RecordObject(header, "Reset Header");
    //        header.SetActive(false);
    //    }

    //    if (footer != null)
    //    {
    //        Undo.RecordObject(footer, "Reset Footer");
    //        footer.SetActive(false);
    //    }

    //    if (mailContent != null)
    //    {
    //        Undo.RecordObject(footer, "Reset Mail Content");
    //        mailContent.SetActive(false);
    //    }

    //    if (map != null)
    //    {
    //        Undo.RecordObject(map, "Reset map");
    //        map.gameObject.SetActive(false);
    //    }
    //}
}
