using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SetBuildNumber
{
    class Program
    {
        private static int Count = 0;

        static void Main(string[] args)
        {
#if DEBUG
            Directory.SetCurrentDirectory(@"D:\GitHub\tamisa\Tigra\Fontes\Tigra\Tigra");
#endif

            ProcessStartInfo start = new ProcessStartInfo("git", "rev-list HEAD");
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.RedirectStandardOutput = true;
            start.UseShellExecute = false;
            start.CreateNoWindow = false;

            Process proc = new Process();
            proc.StartInfo = start;
            proc.OutputDataReceived += proc_OutputDataReceived;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.WaitForExit();

            string pattern = "^ *<add +key=\"BuildNumber\" +value=\"(?<BuildNumber>.*)\" *\\/> *$";
            Regex r = new Regex(pattern);
            StringBuilder sb = new StringBuilder();
            Match m;
            string s;

            FileStream fs = File.Open("web.config", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader reader = new StreamReader(fs);

            while (false == reader.EndOfStream)
            {
                s = reader.ReadLine();

                if ((m = r.Match(s)).Success)
                {
                    Group g = m.Groups["BuildNumber"];
                    s = s.Substring(0, g.Index) + Count.ToString() + s.Substring(g.Index + g.Length);
                }

                sb.AppendLine(s);
            }
            reader.Close();
            fs.Close();

            fs = File.Open("web.config", FileMode.Open, FileAccess.Write, FileShare.Write);
            StreamWriter writer = new StreamWriter(fs);
            writer.Write(sb.ToString());
            writer.Flush();
            writer.Close();
            fs.Close();
        }

        static void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && e.Data.Length == 40)
            {
                Count++;
            }
        }
    }
}
