using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kavet.Core
{
    class Camera
    {
        // getting world position
        public Vector3 Position;
        public Vector3 Front;
        public Vector3 Up;

        // the movement settings
        public float Speed;
        private float yaw = -90f;
        private float pitch = 0f;
        private float sensitivity = 0.1f;

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        public void ProcessInput(KeyboardState keyboard, float deltaTime)
        {
            float velocity = Speed * deltaTime;

            if (keyboard.IsKeyDown(Keys.W))
                Position += Front * velocity;
            if (keyboard.IsKeyDown(Keys.S))
                Position -= Front * velocity;
            if (keyboard.IsKeyDown(Keys.A))
                Position -= Vector3.Normalize(Vector3.Cross(Front, Up)) * velocity;
            if (keyboard.IsKeyDown(Keys.D))
                Position += Vector3.Normalize(Vector3.Cross(Front, Up)) * velocity;
        }

        public void ProcessMouse(float deltaX, float deltaY)
        {
            yaw += deltaX * sensitivity;
            pitch -= deltaY * sensitivity;

            //clamping the camera
            pitch = Math.Clamp(pitch, -89f, 89f);

            //finding the x, y, z in that order, ensuring that the length is always 1
            Front = Vector3.Normalize(new Vector3(
                MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch)),
                MathF.Sin(MathHelper.DegreesToRadians(pitch)),
                MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch))
             ));
        }

        public Camera(Vector3 startPosition)
        {
            Position = startPosition;
            Front = new Vector3(0, 0, -1);
            Up = new Vector3(0, 1, 0);
            Speed = 2.5f;
        }
    }
}
