//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
float hash (float2 n)
{
    //le hash permet de generer un nombre aleatoire a partir de n 
    return frac(sin(dot(n, float2(123.456789, 987.654321))) * 54321.9876 );
}

float noise(float2 p)
{
    float2 i = floor(p);
    float2 u = smoothstep(0.0, 1.0, frac(p));
    float a = hash(i + float2(0,0));
    float b = hash(i + float2(1,0));
    float c = hash(i + float2(0,1));
    float d = hash(i + float2(1,1));
    float r = lerp(lerp(a, b, u.x),lerp(c, d, u.x), u.y);
    return r * r;
}

void Brownie_noise_float(float2 p, int octaves, out float value)
{
    //honnetement je ne sais pas exactement ce que fait cette fonction mais j'aime bien son resultat
        value = 0.0;
    float amplitude = 0.5;
    float e = 3.0;
    for (int i = 0; i < octaves; ++ i)
    {
        value += amplitude * noise(p); 
        p = p * e; 
        amplitude *= 0.5; 
        e *= 0.95;
    }
}
#endif //MYHLSLINCLUDE_INCLUDED
