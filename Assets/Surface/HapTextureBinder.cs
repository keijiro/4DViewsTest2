using UnityEngine;

[ExecuteInEditMode]
sealed class HapTextureBinder : MonoBehaviour
{
    [SerializeField] Klak.Hap.HapPlayer _hapPlayer = null;
    [SerializeField] string _propertyName = "_MainTex";

    MaterialPropertyBlock _block;

    void LateUpdate()
    {
        if (_block == null)
            _block = new MaterialPropertyBlock();

        var renderer = GetComponent<Renderer>();
        renderer.GetPropertyBlock(_block);

        if (_hapPlayer != null && _hapPlayer.texture != null)
            _block.SetTexture(_propertyName, _hapPlayer.texture);

        renderer.SetPropertyBlock(_block);
    }
}
