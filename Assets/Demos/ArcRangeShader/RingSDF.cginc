void RingSDF_float(in float2 p, in float2 n, in float r, in float th, out float d)
{
    p.x = abs(p.x);
    p = mul(float2x2(n.x, n.y, -n.y, n.x), p);
    d = max(abs(length(p) - r) - th * 0.5, length(float2(p.x, max(0.0, abs(r - p.y) - th * 0.5))) * sign(p.x));
}
