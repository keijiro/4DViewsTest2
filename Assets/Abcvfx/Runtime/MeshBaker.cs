using UnityEngine;

namespace Abcvfx {

//
// MeshBaker: Convert a textured mesh into a colored point cloud.
//
[ExecuteInEditMode]
public sealed class MeshBaker : MonoBehaviour
{
    #region Editable attribute

    [SerializeField] MeshFilter _meshFilter = null;
    [SerializeField] Klak.Hap.HapPlayer _hapPlayer = null;
    [SerializeField] int _vertexCount = 32768;

    #endregion

    #region Serialized resource reference

    [SerializeField, HideInInspector] ComputeShader _compute = null;

    #endregion

    #region Public properties

    public Texture PositionMap => _converter?.PositionMap;
    public Texture ColorMap    => _converter?.ColorMap;
    public Texture NormalMap   => _converter?.NormalMap;

    #endregion

    #region Private objects

    MeshToPoints _converter;

    #endregion

    #region MonoBehaviour implementation

    void OnDisable()
      => _converter?.ReleaseOnDisable();

    void OnDestroy()
      => _converter?.ReleaseOnDestroy();

    void LateUpdate()
    {
        if (_meshFilter == null || _hapPlayer == null) return;

        if (_converter == null) _converter = new MeshToPoints(_compute);

        _converter.ProcessMesh
          (_meshFilter.sharedMesh, _hapPlayer.texture, _vertexCount);
    }

    #endregion
}

}
