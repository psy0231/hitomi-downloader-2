﻿/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
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


        bool force_close = false;
        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !force_close)
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

        private void button2_Click(object sender, EventArgs e)
        {
            HitomiSetting.Instance.GetModel().UsingLog = false;
            HitomiSetting.Instance.Save();
            force_close = true;
            MessageBox.Show("Setting.json의 UsingLog 항목을 true로 바꾸면 로그를 다시 사용할 수 있습니다.");
            Close();
        }

        List<string> cmd_stack = new List<string>();
        int stack_pointer = 0;
        bool check_tab = false;

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PushString("dc-koromo@hitomi-copy$ " + textBox2.Text);
                string cmd = textBox2.Text.Trim().Split(' ')[0];
                //

                if (cmd == "enum")
                {
                    if (textBox2.Text.Trim().Split(' ').Length == 1)
                    {
                        foreach (var f in Application.OpenForms)
                        {
                            PushString(f.GetType().Name);
                        }
                    }
                    else
                    {
                        try
                        {
                            string frm_name = textBox2.Text.Trim().Split(' ')[1];
                            Application.OpenForms[frm_name].GetType().GetFields()
                                    .ToList().ForEach(x => PushString(x.Name.PadRight(25) + $"[{x.ToString()}]"));
                        }
                        catch (Exception ex)
                        {
                            PushString(ex.Message);
                        }
                    }
                }
                else if (cmd == "get")
                {
                    string[] split = textBox2.Text.Trim().Split(' ');
                    if (split.Length >= 3)
                    {
                        string frm = split[1];
                        string var = split[2];

                        try
                        {
                            PushString(LogEssential.SerializeObject(Application.OpenForms[frm].GetType().GetField(var).GetValue(Application.OpenForms[frm])));
                        }
                        catch (Exception ex)
                        {
                            PushString(ex.Message);
                        }
                    }
                    else
                    {
                        PushString("using 'get [Form] [Variable]");
                    }
                }
                else if (cmd == "set")
                {
                    string[] split = textBox2.Text.Trim().Split(' ');
                    if (split.Length >= 4)
                    {
                        string frm = split[1];
                        string var = split[2];
                        string val = split[3];

                        try
                        {
                            Application.OpenForms[frm].GetType().GetField(var).SetValue(Application.OpenForms[frm],
                                Convert.ChangeType(val, Application.OpenForms[frm].GetType().GetField(var).GetValue(Application.OpenForms[frm]).GetType()));
                        }
                        catch (Exception ex)
                        {
                            PushString(ex.Message);
                        }
                    }
                    else
                    {
                        PushString("using 'get [Form] [Variable] [Value]");
                    }
                }

                cmd_stack.Insert(0, textBox2.Text + " ");
                textBox2.Text = "";
                stack_pointer = 0;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (stack_pointer >= cmd_stack.Count)
                    stack_pointer = cmd_stack.Count - 1;
                if (stack_pointer >= 0)
                {
                    textBox2.Text = cmd_stack[stack_pointer];
                    Application.DoEvents();
                    textBox2.Focus();
                    textBox2.SelectionStart = Math.Max(0, textBox2.Text.Length * 10);
                    textBox2.SelectionLength = 0;
                    textBox2.Focus();
                }
                stack_pointer++;
            }
            else if (e.KeyCode == Keys.Down)
            {
                textBox2.SelectionStart = Math.Max(0, textBox2.Text.Length - 1);
                textBox2.SelectionLength = 0;
            }
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
                    return JsonConvert.SerializeObject(toSerialize, Formatting.Indented, new JsonSerializerSettings
                    {
                        //ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });
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
