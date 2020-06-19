float FloorPatternLine(float2 uv, float2 dir, float repeat)
{
    float p = repeat * dot(uv, dir);
    return 1 - abs(frac(p) - 0.5) / fwidth(p);
}

void FloorPattern_float(float2 uv, float time, out float output)
{
    const float angle = 3.14159265359 / 3;
    const float sin_a = sin(angle);
    const float cos_a = sin(angle);
    const float rep = 20;

    float c = FloorPatternLine(uv, float2(0, 1), rep * sin_a);
    c = max(c, FloorPatternLine(uv, float2(cos_a, sin_a), rep));
    c = max(c, FloorPatternLine(uv, float2(cos_a, -sin_a), rep));

    float dist = length(uv - 0.5) * 2;
    c *= max(0.1, sin(-time * 3 + dist * 2)); // wave
    c *= max(0, 1 - dist); // falloff

    output = c;
}
