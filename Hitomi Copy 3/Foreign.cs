using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    static class Util
    {
        public static WebClient PlainWebClient()
        {
            WebClient wc = new WebClient();
            wc.Headers["Accept-Encoding"] = "application/x-gzip";
            wc.Encoding = Encoding.UTF8;
            return wc;
        }

        /// <summary>
        /// Catch exception and give options to the task.
        /// </summary>
        /// <param name="task">A task to start and forget.</param>
        /// <param name="detach">Start the task in different thread.</param>
        /// <param name="messageBox">Show messagebox when exception occurs.</param>
        public async static void Catch(this Task task, bool detach = true, bool messageBox = false)
        {
            try { await (detach ? Task.Run(() => task) : task).ConfigureAwait(false); }
            catch (Exception e)
            {
                string log = $"[Task Exception] {e.Message}\r\n{e.Source}\r\n{e.StackTrace}";
                LogEssential.Instance.PushLog(() => log);
                if (messageBox) MessageBox.Show(
                    "프로그램 내부에서 예외처리되지 않은 오류가 발생했습니다. " +
                    $"오류가 계속된다면 개발자에게 문의하십시오.\n{e.Source}\n" +
                    $"StackTrace: {e.StackTrace}");
            }
        }

        public static T Send<T>(this Control control, Func<T> func)
            => control.InvokeRequired ? (T)control.Invoke(func) : func();

        public static void Post(this Control control, Action action)
        {
            if (control.InvokeRequired) control.BeginInvoke(action);
            else action();
        }
    }

    class RightClickCloser
    {
        Form Form;
        public bool Enabled = true;

        public RightClickCloser(Form form)
        {
            Form = form;
            MonitorRightClick(form);
        }

        public void MonitorRightClick(Control control)
        {
            control.MouseUp += OnRButtonUp;
            control.ControlAdded += RescanControl;
            control.Controls.OfType<Control>().ToList().ForEach(x => {
                MonitorRightClick(x);
            });
        }

        private void RescanControl(object sender, ControlEventArgs e)
        {
            MonitorRightClick(e.Control);
        }

        private void OnRButtonUp(object sender, MouseEventArgs e)
        {
            if (Enabled && e.Button == MouseButtons.Right)
                Form.Close();
        }
    }

    public class LazyPicturePopup
    {
        Lazy<PicturePopup> Popup;
        public enum PopupType { Follow, Corner };

        public LazyPicturePopup(
            PictureBox pictureBox, Size size,
            PopupType type = PopupType.Follow)
        {
            Popup = new Lazy<PicturePopup>(() => {
                var popup = new PicturePopup(pictureBox.Image);
                pictureBox.MouseMove += type == PopupType.Follow
                    ? (MouseEventHandler)popup.FollowInScreen
                    : popup.OccupyOpposite;
                pictureBox.MouseLeave += delegate { popup.Hide(); };
                pictureBox.Disposed += delegate { popup.Dispose(); };
                popup.Size = FitToScreen(pictureBox, size);
                return popup;
            });

            pictureBox.MouseEnter += PictureBox_MouseEnter;
        }

        private static Size FitToScreen(PictureBox pictureBox, Size size)
        {
            Rectangle avail = Screen.FromControl(pictureBox).WorkingArea;
            float xZoom = (float)avail.Width / size.Width;
            float yZoom = (float)avail.Height / size.Height;
            float zoom = Math.Min(Math.Min(xZoom, yZoom), 1);
            Size popupSize = new Size((int)(size.Width * zoom), (int)(size.Height * zoom));
            return popupSize;
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
            => Popup.Value.Show();
    }

    public class PicturePopup : Form
    {
        public PicturePopup(Image image)
        {
            BackgroundImage = image;

            BackgroundImageLayout = ImageLayout.Zoom;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            BringToFront();
        }

        protected override bool ShowWithoutActivation => true;

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST) m.Result = (IntPtr)HTTRANSPARENT;
            else base.WndProc(ref m);
        }

        public void FollowInScreen(object sender, MouseEventArgs e)
            => FollowInScreen(Cursor.Position);

        public void FollowInScreen(Point cursor)
        {
            Rectangle scr = Screen.FromPoint(cursor).WorkingArea;
            int formRight = cursor.X + Width;
            int formBottom = cursor.Y + Height;

            Point newPos = cursor;
            newPos.X += formRight < scr.Right ? 15 : -Width - 15;
            newPos.Y += formBottom < scr.Bottom ? 0 : -Height;

            Location = newPos;
        }

        public void OccupyOpposite(object sender, MouseEventArgs e)
            => OccupyOpposite(Cursor.Position);

        public void OccupyOpposite(Point cursor)
        {
            Rectangle scr = Screen.FromPoint(cursor).WorkingArea;
            Location = new Point() {
                X = cursor.X < (scr.Left + scr.Right) / 2 ? scr.Right - Width : 0,
                Y = cursor.Y < (scr.Top + scr.Bottom) / 2 ? scr.Bottom - Height : 0
            };
        }
    }
}
