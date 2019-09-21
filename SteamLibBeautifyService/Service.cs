using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Zebone.His;

namespace SteamLibBeautifyService
{
    public partial class Service : ServiceBase
    {
        System.Timers.Timer _timer = new System.Timers.Timer();  //计时器
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Interval = 3000;
            _timer.AutoReset = true;              
            _timer.Enabled = true;
            _timer.Start();
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
        }

        protected override void OnStop()
        {
            _timer.Stop();
        }

        private static string GetMD5HashFromFile(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return "";
            }

            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (System.Diagnostics.Process.GetProcessesByName("SteamService").ToList().Count == 0 &&
                    System.Diagnostics.Process.GetProcessesByName("SteamService.exe").ToList().Count == 0)
                {
                    return;
                }
                string appdata = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData) + "\\SteamLibBeautify";
                if (false == System.IO.Directory.Exists(appdata))
                {
                    System.IO.Directory.CreateDirectory(appdata);
                }
                string steam_dir = "", img_dir = "", font_dir = "";
                bool img_enable = true, font_enable = false, reg_service = true, hide_mainlib = false;
                // 读配置项
                if (System.IO.File.Exists(appdata + "/properties.ini"))
                {
                    steam_dir = OperateIniFile.ReadIniData("properties", "steam_dir", "", appdata + "/properties.ini");
                    if (OperateIniFile.ReadIniData("properties", "img_enable", "true", appdata + "/properties.ini") == "true")
                    {
                        img_enable = true;
                        img_dir = OperateIniFile.ReadIniData("properties", "img_src", "", appdata + "/properties.ini");
                    }
                    else
                    {
                        img_enable = false;
                    }
                    if (OperateIniFile.ReadIniData("properties", "font_enable", "false", appdata + "/properties.ini") == "true")
                    {
                        font_enable = true;
                        font_dir = OperateIniFile.ReadIniData("properties", "font_src", "", appdata + "/properties.ini");
                    }
                    else
                    {
                        font_enable = false;
                    }
                    if (OperateIniFile.ReadIniData("properties", "service_register", "true", appdata + "/properties.ini") == "true")
                    {
                        reg_service = true;
                    }
                    if (OperateIniFile.ReadIniData("properties", "hide_mainlib", "false", appdata + "/properties.ini") == "true")
                    {
                        hide_mainlib = true;
                    }
                    // 服务开始执行
                    if (steam_dir == "")
                    {
                        return;
                    }
                    if (img_enable == true)
                    {
                        if (img_dir == "")
                        {
                            return;
                        }
                    }
                    if (font_enable == true)
                    {
                        if (font_dir == "")
                        {
                            return;
                        }
                    }
                    // 判断libraryroot.css是否存在
                    string path = steam_dir + "\\steamui\\css\\libraryroot.css";
                    if (!System.IO.File.Exists(path))
                    {
                        return;
                    }
                    // 生成CSS文件
                    try
                    {

                        // 生成CSS文件
                        FileStream fs = new FileStream(appdata + "/libraryroot_new.css", FileMode.Create, FileAccess.Write);
                        FileStream source = new FileStream(appdata + "/libraryroot.css", FileMode.Open);
                        fs.SetLength(0);
                        StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                        StreamReader sr = new StreamReader(source);
                        string line;
                        sw.Write("*{font-family:Youmu!important;background:0!important;border:none!important}");
                        if (img_enable == true)
                        {
                            sw.Write("body{background-image:url(bg.png)!important;background-repeat:no-repeat!important;background-size:100% 100%!important}");
                        }
                        if (font_enable == true)
                        {
                            sw.Write("@font-face{font-family:Youmu;font-style:normal;font-weight:400;font-display:swap;src:url(font.ttf)}");
                        }
                        if (hide_mainlib == true)
                        {
                            sw.Write(".smartscrollcontainer_Container_3VQUe{display:none!important}");
                        }
                        while ((line = sr.ReadLine()) != null)
                        {
                            sw.Write(line + "\n");
                        }
                        sw.Flush();
                        sw.Close();
                        sr.Close();
                        // 检验MD5文件
                        string css_source = GetMD5HashFromFile(steam_dir + "\\steamui\\css\\libraryroot.css");
                        string css = GetMD5HashFromFile(appdata + "/libraryroot_new.css");
                        if (css != css_source)
                            System.IO.File.Copy(appdata + "/libraryroot_new.css", steam_dir + "\\steamui\\css\\libraryroot.css", true);
                        string img_source = GetMD5HashFromFile(steam_dir + "\\steamui\\css\\bg.png");
                        string img = GetMD5HashFromFile(img_dir);
                        if (img != img_source)
                            System.IO.File.Copy(img_dir, steam_dir + "\\steamui\\css\\bg.png", true);
                        string font_source = GetMD5HashFromFile(steam_dir + "\\steamui\\css\\font.ttf");
                        string font = GetMD5HashFromFile(font_dir);
                        if (font != font_source)
                            System.IO.File.Copy(font_dir, steam_dir + "\\steamui\\css\\font.ttf", true);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
    }
}
