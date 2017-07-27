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
            Backend.Window.KeyDown += Window_KeyDown;
            Backend.Window.KeyUp += Window_KeyUp;
            Backend.Window.Mouse.ButtonDown += Mouse_ButtonDown;
            Backend.Window.Mouse.ButtonUp += Mouse_ButtonUp;
        }

        private void Mouse_ButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                Bullet bullet = new Bullet();
                bullet.BulletPos = PlayerPos;
                Vector2 relative = MouseCoords - PlayerPos;
                relative.Normalize();
                bullet.BulletVelocity = relative * 800;
                Bullets.Add(bullet);
            }
            else if (e.Button == MouseButton.Right)
            {
                Foe foe = new Foe();
                foe.FoePos = MouseCoords;
                foe.FoeHP = 100;
                Foes.Add(foe);
            }
        }

        public List<Bullet> Bullets = new List<Bullet>();
        public bool PressW, PressS, PressA, PressD;

        public List<Foe> Foes = new List<Foe>();

        private void Window_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                PressS = false;

            }
            if (e.Key == Key.W)
            {
                PressW = false;
            }
            if (e.Key == Key.A)
            {
                PressA = false;
            }
            if (e.Key == Key.D)
            {
                PressD = false;
            }
        }

        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                PressS = true;

            }
            if (e.Key == Key.W)
            {
                PressW = true;
            }
            if (e.Key == Key.A)
            {
                PressA = true;
            }
            if (e.Key == Key.D)
            {
                PressD = true;
            }
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
        public Vector2 PlayerPos = Vector2.Zero;
        /// <summary>
        /// Update logic here.
        /// </summary>
        /// <param name="delta"></param>
        public void Tick(double delta)
        {
            if (PressW)
            {
                PlayerPos.Y -= (float)delta * 150;
            }
            if (PressS)
            {
                PlayerPos.Y += (float)delta * 150;
            }
            if (PressA)
            {
                PlayerPos.X -= (float)delta * 150;

            }
            if (PressD)
            {
                PlayerPos.X += (float)delta * 150;
            }
            foreach (Bullet bullet in Bullets)
            {
                bullet.BulletPos += bullet.BulletVelocity * (float)delta;
            }
            foreach (Foe foe in Foes)
            {
                Vector2 relative = PlayerPos - foe.FoePos;
                relative.Normalize();
                foe.FoePos += relative * (float)delta * 150;
            }
            for (int j = Bullets.Count - 1; j >= 0; j--)
            {
                Bullet bullet = Bullets[j];
                for (int i = Foes.Count - 1; i >= 0; i--)
                {
                    Foe foe = Foes[i];
                    if ((bullet.BulletPos - foe.FoePos).Length < 20)
                    {
                        foe.FoeHP -= 20;
                        if (foe.FoeHP <= 0)
                        {
                            Foes.RemoveAt(i);
                           
                        }
                        Bullets.RemoveAt(j);
                        break;
                    }
                }
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
            Matrix4 model = Matrix4.CreateScale(20, 20, 1) * Matrix4.CreateTranslation(PlayerPos.X - 10, PlayerPos.Y - 10, 0);
            GL.UniformMatrix4(2, false, ref model);
            // Render it
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(Backend.VBO_Box);
            GL.BindTexture(TextureTarget.Texture2D, Backend.Tex_Red_X);
            model = Matrix4.CreateScale(4, 4, 1) * Matrix4.CreateTranslation( MouseCoords.X - 2, MouseCoords.Y - 2, 0);
            GL.UniformMatrix4(2, false, ref model);
            // Render it

            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            foreach (Bullet bullet in Bullets)
            {
                GL.BindVertexArray(Backend.VBO_Box);
                GL.BindTexture(TextureTarget.Texture2D, Backend.Tex_Red_X);
                model = Matrix4.CreateScale(4, 4, 1) * Matrix4.CreateTranslation(bullet.BulletPos.X - 2, bullet.BulletPos.Y - 2, 0);
                GL.UniformMatrix4(2, false, ref model);
                // Render it
                GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
            }
            foreach (Foe foe in Foes)
            {
                GL.BindVertexArray(Backend.VBO_Box);
                GL.BindTexture(TextureTarget.Texture2D, Backend.Tex_Red_X);
                model = Matrix4.CreateScale(20, 20, 1) * Matrix4.CreateTranslation(foe.FoePos.X - 10, foe.FoePos.Y - 10, 0);
                GL.UniformMatrix4(2, false, ref model);
                // Render it
                GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
            }
        }
    }
}
