/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Hitomi_Copy_3
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public void PushString(string str)
        {
            if (textBox1.InvokeRequired)
            {
                Invoke(new Action<string>(PushString), new object[] { str }); return;
            }
            textBox1.SuspendLayout();
            textBox1.AppendText(str + "\r\n");
            textBox1.ResumeLayout();
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                LogEssential.Instance.PushLog(() => "Don't do that. I'm serious.");
            }
        }

        private void Log_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText("log.log", textBox1.Text);
            MessageBox.Show("Save complete!", "Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void save_log()
        {
            File.WriteAllText("log.log", textBox1.Text);
        }
    }

    public class LogEssential
    {
        private static readonly Lazy<LogEssential> instance = new Lazy<LogEssential>(() => new LogEssential());
        public static LogEssential Instance => instance.Value;
        Log my_log;

        public void Initialize()
        {
            if (HitomiSetting.Instance.GetModel().UsingLog)
            {
                my_log = new Log();
                my_log.Show();
                my_log.PushString($"Hitomi Copy {UpdateManager.Version}");
                my_log.PushString("Copyright (C) 2018. Hitomi Parser Developers");
                my_log.PushString("E -Mail: koromo.software@gmail.com");
                my_log.PushString("Source-code : https://github.com/dc-koromo/hitomi-downloader-2");
                my_log.PushString("");
                PushLog(() => "Hello!");
                PushLog(() => "Current Setting");
                PushLog(HitomiSetting.Instance.GetModel());
            }
        }

        public void SaveLog()
        {
            my_log.save_log();
        }

        public void PushLog(Func<string> str)
        {
            if (HitomiSetting.Instance.GetModel().UsingLog)
            {
                CultureInfo en = new CultureInfo("en-US");
                my_log.PushString($"[{DateTime.Now.ToString(en)}] {str()}");
            }
        }

        public void PushLog(object obj)
        {
            if (HitomiSetting.Instance.GetModel().UsingLog)
            {
                PushLog(() => obj.ToString());
                my_log.PushString(SerializeObject(obj));
            }
        }

        public static string SerializeObject(object toSerialize)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, toSerialize);
                    return textWriter.ToString();
                }
            }
            catch
            {
                try
                {
                    return JsonConvert.SerializeObject(toSerialize, Formatting.Indented);
                }
                catch (Exception e)
                {
                    Instance.PushLog(() => $"[Error] {e.Message}");
                    return toSerialize.ToString();
                }
            }
        }
    }
}
