

using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;

namespace Kavet.Rendering;

class Color
{
    public Vector3 val;

    public float X => val.X;
    public float Y => val.Y;
    public float Z => val.Z;

    private Color(float r, float g, float b)
    {
        val = new Vector3(r,g,b);
    }

    // preset colors
    public static readonly Color Red     = new Color(1.0f, 0.0f, 0.0f);
    public static readonly Color Green   = new Color(0.0f, 1.0f, 0.0f);
    public static readonly Color Blue    = new Color(0.0f, 0.0f, 1.0f);
    public static readonly Color White   = new Color(1.0f, 1.0f, 1.0f);
    public static readonly Color Black   = new Color(0.0f, 0.0f, 0.0f);
    public static readonly Color Yellow  = new Color(1.0f, 1.0f, 0.0f);
    public static readonly Color Purple  = new Color(0.5f, 0.0f, 0.5f);

    public static Color Custom(float r, float g, float b) => new Color(r,g,b);
}