
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common.Input;
using StbImageSharp;

namespace Kavet.Rendering
{
    class Texture{

        int textureID = 0;

        public Texture(string path){

            textureID = GL.GenTexture(); // creating a blank texture
            GL.BindTexture(TextureTarget.Texture2D, textureID); //making it a 2D texture

            StbImage.stbi_set_flip_vertically_on_load(1); // flipping cause openTK auto-flips it

            using var stream = File.OpenRead(path); //reading the file
            ImageResult img = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha); //making the image a var to be used

            GL.TexImage2D( //making the openGL image out of the file reading
                TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 
                img.Width, img.Height, 0, PixelFormat.Rgba, 
                PixelType.UnsignedByte, img.Data
            );

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Use()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }
    }
}