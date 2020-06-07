using UnityEditor;

namespace Abcvfx.Editor {

[CanEditMultipleObjects]
[CustomEditor(typeof(MeshBaker))]
class MeshBakerEditor : UnityEditor.Editor
{
    SerializedProperty _meshFilter;
    SerializedProperty _hapPlayer;
    SerializedProperty _vertexCount;

    void OnEnable()
    {
        _meshFilter = serializedObject.FindProperty("_meshFilter");
        _hapPlayer = serializedObject.FindProperty("_hapPlayer");
        _vertexCount = serializedObject.FindProperty("_vertexCount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_meshFilter);
        EditorGUILayout.PropertyField(_hapPlayer);
        EditorGUILayout.PropertyField(_vertexCount);
        serializedObject.ApplyModifiedProperties();
    }
}

}
