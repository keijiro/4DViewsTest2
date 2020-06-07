using System.Collections.Generic;
using UnityEngine;

namespace Abcvfx {

sealed class MeshToPointsConverter
{
    #region Private members

    (List<Vector3> P, List<Vector2> T, List<int> I) _tempList =
      (new List<Vector3>(), new List<Vector2>(), new List<int>());

    (ComputeBuffer P, ComputeBuffer T, ComputeBuffer I) _source;

    (RenderTexture P, RenderTexture N, RenderTexture C) _converted;

    static RenderTexture NewRenderTexture
      (int width, int height, RenderTextureFormat format)
    {
        var rt = new RenderTexture(width, height, 0, format);
        rt.enableRandomWrite = true;
        rt.Create();
        return rt;
    }

    #endregion

    #region Public accessor

    public Texture PositionMap => _converted.P;
    public Texture NormalMap   => _converted.N;
    public Texture ColorMap    => _converted.C;

    #endregion

    #region Public methods

    public MeshToPointsConverter(int vertexCount)
    {
        var w = 1024;
        var h = (vertexCount + w - 1) / w;
        _converted.P = NewRenderTexture(w, h, RenderTextureFormat.ARGBFloat);
        _converted.N = NewRenderTexture(w, h, RenderTextureFormat.ARGBHalf);
        _converted.C = NewRenderTexture(w, h, RenderTextureFormat.Default);
    }

    public void ReleaseOnDisable()
    {
        _source.P?.Dispose();
        _source.T?.Dispose();
        _source.I?.Dispose();
        _source = (null, null, null);
    }

    public void ReleaseOnDestroy()
    {
        if (_converted.P != null) Object.Destroy(_converted.P);
        if (_converted.N != null) Object.Destroy(_converted.N);
        if (_converted.C != null) Object.Destroy(_converted.C);
        _converted = (null, null, null);
    }

    public void ProcessMesh(Mesh mesh, Texture texture, ComputeShader compute)
    {
        mesh.GetIndices(_tempList.I, 0);
        mesh.GetVertices(_tempList.P);
        mesh.GetUVs(0, _tempList.T);

        var icount = _tempList.I.Count;
        var vcount = _tempList.P.Count;
        var vcount_x2 = vcount * 2;
        var vcount_x3 = vcount * 3;

        if ((_source.I?.count ?? 0) != icount ||
            (_source.P?.count ?? 0) != vcount_x3)
        {
            ReleaseOnDisable();
        }

        if (_source.I == null)
        {
            _source.I = new ComputeBuffer(icount, sizeof(int));
            _source.P = new ComputeBuffer(vcount_x3, sizeof(float));
            _source.T = new ComputeBuffer(vcount_x2, sizeof(float));
        }

        _source.I.SetData(_tempList.I);
        _source.P.SetData(_tempList.P);
        _source.T.SetData(_tempList.T);

        compute.SetInt("IndexCount", icount);
        compute.SetBuffer(0, "Indices", _source.I);
        compute.SetBuffer(0, "Positions", _source.P);
        compute.SetBuffer(0, "TexCoords", _source.T);
        compute.SetTexture(0, "BaseTexture", texture);
        compute.SetTexture(0, "PositionMap", _converted.P);
        compute.SetTexture(0, "NormalMap", _converted.N);
        compute.SetTexture(0, "ColorMap", _converted.C);
        compute.Dispatch(0, _converted.P.width / 8, _converted.P.height / 8, 1);
    }

    #endregion
}

}
