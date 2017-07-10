using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace FurAnjel
{
    /// <summary>
    /// The primary coding area for your game.
    /// </summary>
    public class YourGame
    {
        /// <summary>
        /// The backend internal game system.
        /// </summary>
        public GameInternal Backend;

        /// <summary>
        /// Load anything we need here.
        /// </summary>
        public void Load()
        {
            BallMove = new Vector2(Backend.Window.Width/ 2 - 25, Backend.Window.Height / 2 - 25);
            Random random = new Random();
            BallVelocity = new Vector2((float)random.NextDouble() * 300 - 150, (float)random.NextDouble() * 300 - 150);
            // TODO: Load anything you need!

            // listen to mouse movement
            Backend.Window.MouseMove += Window_MouseMove;
        }

        /// <summary>
        /// Listens to mouse movement.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Event data.</param>
        private void Window_MouseMove(object sender, MouseMoveEventArgs e)
        {
            MouseCoords = new Vector2(e.X, e.Y);
        }

        /// <summary>
        /// Stores current mouse coordinates.
        /// </summary>
        public Vector2 MouseCoords = Vector2.Zero;
        public Vector2 BallMove = Vector2.Zero;
        public Vector2 BallVelocity = Vector2.Zero;
        public float PaddleMove = 0;

        /// <summary>
        /// Update logic here.
        /// </summary>
        /// <param name="delta"></param>
        public void Tick(double delta)
        {
            BallMove += BallVelocity * (float)delta;
            if (BallMove.Y < 0)
            {
                BallMove.Y = 0;
                BallVelocity.Y = -BallVelocity.Y;
            }
            if (BallMove.Y > Backend.Window.Height - 50)
            {
                BallMove.Y = Backend.Window.Height - 50;
                BallVelocity.Y = -BallVelocity.Y;

            }
            if (BallMove.Y > PaddleMove)
            {
                PaddleMove += (float)delta * 50;

            }
            else
            {
                PaddleMove -= (float)delta * 50;

            }
            if(BallMove.X < 75 && BallMove.X > 0 && BallMove.Y < MouseCoords.Y + 75 && BallMove.Y > MouseCoords.Y - 75)
            {
                BallMove.X = 75;
                BallVelocity.X = -BallVelocity.X;

            }
            if(BallMove.X < Backend.Window.Width && BallMove.X > Backend.Window.Width - 125 && BallMove.Y < PaddleMove + 75 && BallMove.Y > PaddleMove - 75)
            {
               BallMove.X = Backend.Window.Width - 125;
                BallVelocity.X = -BallVelocity.X;
            }

            // TODO: Move objects around.
        }

        /// <summary>
        /// Rendering logic here.
        /// </summary>
        public void Render()
        {
            // TODO: Render things to screen.

            // Configure the projection
            Matrix4 projection = Matrix4.CreateOrthographicOffCenter(0, Backend.Window.Width, Backend.Window.Height, 0, -1, 1);
            GL.UniformMatrix4(1, false, ref projection);

            // Configure a renderable
            GL.BindVertexArray(Backend.VBO_Box);
            GL.BindTexture(TextureTarget.Texture2D, Backend.Tex_Red_X);
            Matrix4 model = Matrix4.CreateScale(50, 100, 1) * Matrix4.CreateTranslation(25, MouseCoords.Y - 50, 0);
            GL.UniformMatrix4(2, false, ref model);

            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(Backend.VBO_Box);
            GL.BindTexture(TextureTarget.Texture2D, Backend.Tex_Red_X);
             model = Matrix4.CreateScale(50, 100, 1) * Matrix4.CreateTranslation( Backend.Window.Width - 75, PaddleMove - 50, 0);
            GL.UniformMatrix4(2, false, ref model);

            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(Backend.VBO_Box);
            GL.BindTexture(TextureTarget.Texture2D, Backend.Tex_Red_X);
            model = Matrix4.CreateScale(50, 50, 1) * Matrix4.CreateTranslation(BallMove.X, BallMove.Y, 0);
            GL.UniformMatrix4(2, false, ref model);


            // Render it
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }
    }
}
