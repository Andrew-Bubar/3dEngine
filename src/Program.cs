using Kavet;
using Kavet.Core;
using Kavet.Rendering;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

//script wide vars
Cube cube1 = null;
Cube cube2 = null;
Camera camera = null;
Texture demoTexture = null;


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

    camera.ProcessInput(window.KeyboardState, (float)args.Time);
};

window.MouseMove += args =>
{
    camera.ProcessMouse(args.DeltaX, args.DeltaY);
};

window.Load += () =>
{
    demoTexture = new Texture("art/textures/woodTexture.png");

    cube1 = new Cube(new Vector3(-0.6f, 0, 0), Color.White);
    cube2 = new Cube(new Vector3(0.6f, 0, 0), Color.Yellow);

    camera = new Camera(new Vector3(0, 0, 3));

    // open GL settings
    window.CursorState = CursorState.Grabbed;
    GL.Enable(EnableCap.DepthTest);
};


window.RenderFrame += args =>
{

    GL.ClearColor(0.3f, 0.0f, 0.6f, 1.0f);
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

    Matrix4 view = camera.GetViewMatrix();
    Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
        MathHelper.DegreesToRadians(75f), // this is the FOV
        800f / 600f, //width / height of screen
        0.1f, // where the camera cuts as in closeness (0 will crash it)
        100f //render disntance
    );

    cube1.Draw(view, projection, demoTexture);
    cube2.Draw(view, projection);

    window.SwapBuffers();
};


window.Run();
