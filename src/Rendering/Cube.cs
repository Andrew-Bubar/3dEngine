using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kavet.Rendering
{
    class Cube
    {
        int vao;
        int vbo;
        int shaderProgram;

        public Vector3 position;
        Color color;

        public Cube(Vector3 position, Color color)
        {

            // applying constructor
            this.position = position;
            this.color = color;

            // the visual values
        float[] vert =
        {
            // x      y      z      r          g          b         u     v
            // Front face
            -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,

            // Back face
            -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,
            0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,

            // Left face
            -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,

            // Right face
            0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 0.0f,
            0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,

            // Top face
            -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,
            0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 0.0f,
            0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,

            // Bottom face
            -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,
            0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  1.0f, 0.0f,
            0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  1.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,  0.0f, 0.0f,
        };

            // setting up the two vertext array / buffer
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vert.Length * sizeof(float), vert, BufferUsageHint.StaticDraw);

            // applying the array and buffer
            // position - 3 floats, starts at byte 0
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // color - 3 floats, starts after position (3 floats in)
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // uv - 2 floats, starts after position and color (6 floats in)
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            // the shader code
            string vertexShaderSource = @"
            #version 330 core
            layout (location = 0) in vec3 aPos;
            layout (location = 1) in vec3 aColor;
            layout (location = 2) in vec2 aUV;

            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;

            out vec3 vertexColor;
            out vec2 uv;

            void main()
            {
                gl_Position = projection * view * model * vec4(aPos, 1.0);
                vertexColor = aColor;
                uv = aUV;
            }";

            string fragmentShaderSource = @"
            #version 330 core
            in vec3 vertexColor;
            in vec2 uv;

            uniform sampler2D tex;
            uniform bool useTexture;

            out vec4 FragColor;

            void main()
            {
                if (useTexture)
                    FragColor = texture(tex, uv);
                else
                    FragColor = vec4(vertexColor, 1.0);
            }";

            // compiling the shaders into something usable
            int vertS = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertS, vertexShaderSource);
            GL.CompileShader(vertS);

            int fragS = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragS, fragmentShaderSource);
            GL.CompileShader(fragS);

            // getting a shader program that can be run
            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertS);
            GL.AttachShader(shaderProgram, fragS);
            GL.LinkProgram(shaderProgram);
        }

        public void Draw(Matrix4 view, Matrix4 projection, Texture text = null)
        {
            //getting the program
            GL.UseProgram(shaderProgram);

            // adding the position to the vertex shader
            Matrix4 model = Matrix4.CreateTranslation(position);
            int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
            GL.UniformMatrix4(modelLocation, false, ref model);

            // adding the projection to the vertex shader
            int viewLocation = GL.GetUniformLocation(shaderProgram, "view");
            GL.UniformMatrix4(viewLocation, false, ref view);

            int projLocation = GL.GetUniformLocation(shaderProgram, "projection");
            GL.UniformMatrix4(projLocation, false, ref projection);

            int useTextureLocation = GL.GetUniformLocation(shaderProgram, "useTexture"); //pulling the varrible out of the shader code

            if (text != null)
            {
                text.Use(); //applying the texture
                GL.Uniform1(useTextureLocation, 1);
            } 
            else
            {
                GL.Uniform1(useTextureLocation, 0);
            }

            //drawing the new 3d model
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }
    }
}
