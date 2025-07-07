using UnityEngine;
using UnityEditor;

public class TetrominoAligner : EditorWindow
{
    [MenuItem("Tools/Align Tetromino Children")]
    public static void ShowWindow()
    {
        GetWindow<TetrominoAligner>("Tetromino Aligner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Align selected Tetromino's children to integer local positions", EditorStyles.boldLabel);

        if (GUILayout.Button("Align Selected Tetromino"))
        {
            AlignSelectedTetromino();
        }
    }

    private void AlignSelectedTetromino()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        Transform tetromino = Selection.activeGameObject.transform;
        Undo.RecordObject(tetromino, "Align Tetromino Children");

        foreach (Transform child in tetromino)
        {
            Vector3 localPos = child.localPosition;
            localPos.x = Mathf.Round(localPos.x);
            localPos.y = Mathf.Round(localPos.y);
            localPos.z = 0;
            child.localPosition = localPos;
            EditorUtility.SetDirty(child);
        }

        Debug.Log("Aligned all children of " + tetromino.name + " to integer local positions.");
    }
}