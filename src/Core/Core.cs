

using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Kavet
{
    public class Game
    {
        protected GameWindow window;
        protected int width;
        protected int height;
        protected string title;
    }

    public Game(int Width, int Height, string Title)
        {
            this.width = Width;
            this.height = Height;
            this.title = Title;

            var settings = new NativeWindowSettings
            {
                ClientSize = new Vector2i(800, 600),
                Title = this.title
            };

            this.window = new GameWindow(GameWindowSettings.Default, settings);

            
        }
}