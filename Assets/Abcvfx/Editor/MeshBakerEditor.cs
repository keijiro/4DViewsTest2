using UnityEditor;

namespace Abcvfx.Editor {

[CanEditMultipleObjects]
[CustomEditor(typeof(MeshBaker))]
class MeshBakerEditor : UnityEditor.Editor
{
    SerializedProperty _meshFilter;
    SerializedProperty _texture;
    SerializedProperty _vertexCount;

    void OnEnable()
    {
        _meshFilter = serializedObject.FindProperty("_meshFilter");
        _texture = serializedObject.FindProperty("_texture");
        _vertexCount = serializedObject.FindProperty("_vertexCount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_meshFilter);
        EditorGUILayout.PropertyField(_texture);
        EditorGUILayout.PropertyField(_vertexCount);
        serializedObject.ApplyModifiedProperties();
    }
}

}
