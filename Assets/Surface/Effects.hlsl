void Contour_float(float3 position, float repeat, out float output)
{
    float t = _Time.y;
    float x = position.y;

    // Contour using derivatives
    float x2 = x * repeat + t * 3;
    float fw = fwidth(x2);
    float g = saturate(1 - abs(0.5 - frac(x2)) / fw);

    // Frequency filter
    g = lerp(g, 0.4, smoothstep(0.5, 1, fw));

    // Output
    output = g;
}
