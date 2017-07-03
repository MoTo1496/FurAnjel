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

        /// <summary>
        /// Update logic here.
        /// </summary>
        /// <param name="delta"></param>
        public void Tick(double delta)
        {
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
            Matrix4 model = Matrix4.CreateScale(50, 100, 1) * Matrix4.CreateTranslation(25, MouseCoords.Y - 25, 0);
            GL.UniformMatrix4(2, false, ref model);

            // Render it
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }
    }
}
