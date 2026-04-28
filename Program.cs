using _3dEngine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

//script wide vars
Cube cube = null;


var settings = new NativeWindowSettings
{
    ClientSize = new Vector2i(800, 600),
    Title = "RetroFPS Test"
};

using var window = new GameWindow(GameWindowSettings.Default, settings);

window.UpdateFrame += args =>
{
    if (window.KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
    {
        window.Close();
    }
};

window.Load += () =>
{
    cube = new Cube(new Vector3(0, 0, 0), new Vector3(1, 1, 0));
};


window.RenderFrame += args =>
{

    GL.ClearColor(0.3f, 0.0f, 0.6f, 1.0f);
    GL.Clear(ClearBufferMask.ColorBufferBit);

    cube.Draw();

    window.SwapBuffers();
};


window.Run();
