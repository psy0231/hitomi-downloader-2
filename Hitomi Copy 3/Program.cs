using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    static class Program
    {
        static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            var name = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";

            string[] names = thisAssembly.GetManifestResourceNames();
            var resources = thisAssembly.GetManifestResourceNames().Where(s => s.EndsWith(name));
            if (resources.Count() > 0)
            {
                string resourceName = resources.First();
                using (Stream stream = thisAssembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        byte[] assembly = new byte[stream.Length];
                        stream.Read(assembly, 0, assembly.Length);
                        return Assembly.Load(assembly);
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveAssembly);
            Application.ThreadException += ApplicationThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += unhandledException;
            AppSetup();
        }

        private static void unhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("프로그램 내부에서 예외처리되지 않은 오류가 발생했습니다. 오류가 계속된다면 개발자에게 문의하십시오. " + (e.ExceptionObject as Exception).Source + "\nStackTrace: " + (e.ExceptionObject as Exception).StackTrace);
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("프로그램 내부에서 예외처리되지 않은 오류가 발생했습니다. 오류가 계속된다면 개발자에게 문의하십시오. " + (e.Exception as Exception).Source + "\nStackTrace: " + (e.Exception as Exception).StackTrace);
        }

        static void AppSetup()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
