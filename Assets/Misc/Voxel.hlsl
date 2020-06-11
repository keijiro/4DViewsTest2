void FaceEdge_float(float2 uv, out float alpha)
{
    float2 fw = fwidth(uv);
    float2 edge = min(uv, 1 - uv) / fw;
    alpha = saturate(1 - min(edge.x, edge.y));
}
