using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace _3dEngine
{
    class Cube
    {
        int vao;
        int vbo;
        int shaderProgram;

        public Vector3 position;
        Vector3 color;

        public Cube(Vector3 position, Vector3 color)
        {

            // applying constructor
            this.position = position;
            this.color = color;

            // the visual values
            float[] vert =
            {
                // Front face
                -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,

                // Back face
                -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,
                -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,
                -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,

                // Left face
                -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,
                -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,

                // Right face
                 0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,

                // Top face
                -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                 0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f,  0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f,  0.5f, -0.5f,  color.X, color.Y, color.Z,

                // Bottom face
                -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,
                 0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f, -0.5f,  0.5f,  color.X, color.Y, color.Z,
                -0.5f, -0.5f, -0.5f,  color.X, color.Y, color.Z,
            };

            // setting up the two vertext array / buffer
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vert.Length * sizeof(float), vert, BufferUsageHint.StaticDraw);

            // applying the array and buffer
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // the shader code
            string vertexShaderSource = @"
            #version 330 core
            layout (location = 0) in vec3 aPos;
            layout (location = 1) in vec3 aColor;

            uniform mat4 model;

            out vec3 vertexColor;

            void main()
            {
                gl_Position = model * vec4(aPos, 1.0);
                vertexColor = aColor;
            }";

            string fragmentShaderSource = @"
            #version 330 core
            in vec3 vertexColor;
            out vec4 FragColor;
            void main()
            {
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

        public void Draw()
        {
            //getting the program
            GL.UseProgram(shaderProgram);

            // adding the position to the vertex shader
            Matrix4 model = Matrix4.CreateTranslation(position);
            int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
            GL.UniformMatrix4(modelLocation, false, ref model);

            //drawing the new 3d model
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }
    }
}
