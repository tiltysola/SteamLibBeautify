using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using Zebone.His;

namespace SteamLibBeautify
{
    public partial class Client : Form
    {
        string appdata = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData) + "\\SteamLibBeautify";
        public Client()
        {
            InitializeComponent();
            if (false == System.IO.Directory.Exists(appdata))
            {
                System.IO.Directory.CreateDirectory(appdata);
            }
            System.IO.File.Copy("./libraryroot.css", appdata + "/libraryroot.css", true);
            if (!System.IO.File.Exists(appdata + "/properties.ini"))
            {
                FileStream fs = new FileStream(appdata + "/properties.ini", FileMode.Create, FileAccess.Write);
                fs.SetLength(0);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write("");
                sw.Flush();
                sw.Close();
            }
            // 读配置项
            if (System.IO.File.Exists(appdata + "/properties.ini"))
            {
                steam_dir.Text = OperateIniFile.ReadIniData("properties", "steam_dir", "", appdata + "/properties.ini");
                if (OperateIniFile.ReadIniData("properties", "img_enable", "true", appdata + "/properties.ini") == "true")
                {
                    img_enable.Checked = true;
                    img_dir.Text = OperateIniFile.ReadIniData("properties", "img_src", "", appdata + "/properties.ini");
                }
                else
                {
                    img_enable.Checked = false;
                }
                if (OperateIniFile.ReadIniData("properties", "font_enable", "false", appdata + "/properties.ini") == "true")
                {
                    font_enable.Checked = true;
                    font_dir.Text = OperateIniFile.ReadIniData("properties", "font_src", "", appdata + "/properties.ini");
                }
                else
                {
                    font_enable.Checked = false;
                }
                if (OperateIniFile.ReadIniData("properties", "service_register", "true", appdata + "/properties.ini") == "true")
                {
                    reg_serivce.Checked = true;
                }
                if (OperateIniFile.ReadIniData("properties", "hide_mainlib", "false", appdata + "/properties.ini") == "true")
                {
                    hide_mainlib.Checked = true;
                }
            }
        }

        private void Save_setting_Click(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName("SteamService").ToList().Count == 0 &&
                System.Diagnostics.Process.GetProcessesByName("SteamService.exe").ToList().Count == 0)
            {
                MessageBox.Show("Steam没有正在运行，或者SteamService服务出现异常。");
                return;
            }
            if (steam_dir.Text == "")
            {
                MessageBox.Show("你似乎没有选择Steam的根目录。");
                return;
            }
            if (img_enable.Checked == true)
            {
                if (img_dir.Text == "")
                {
                    MessageBox.Show("你启用了修改图片功能，但是好像你并没有上传图片。");
                    return;
                }
            }
            if (font_enable.Checked == true)
            {
                if (font_dir.Text == "")
                {
                    MessageBox.Show("你启用了修改字体功能，但是好像你并没有上传字体。");
                    return;
                }
            }
            // 判断libraryroot.css是否存在
            string path = steam_dir.Text + "\\steamui\\css\\libraryroot.css";
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show("你选择的Steam目录有误，或者您没有启用Steam Beta Update测试版客户端。");
                return;
            }
            // 写配置项
            OperateIniFile.WriteIniData("properties", "steam_dir", steam_dir.Text, appdata + "/properties.ini");
            if (img_enable.Checked == true)
            {
                OperateIniFile.WriteIniData("properties", "img_enable", "true", appdata + "/properties.ini");
                OperateIniFile.WriteIniData("properties", "img_src", img_dir.Text, appdata + "/properties.ini");
            }
            else
            {
                OperateIniFile.WriteIniData("properties", "img_enable", "false", appdata + "/properties.ini");
            }
            if (font_enable.Checked == true)
            {
                OperateIniFile.WriteIniData("properties", "font_enable", "true", appdata + "/properties.ini");
                OperateIniFile.WriteIniData("properties", "font_src", font_dir.Text, appdata + "/properties.ini");
            }
            else
            {
                OperateIniFile.WriteIniData("properties", "font_enable", "false", appdata + "/properties.ini");
            }
            if (reg_serivce.Checked == true)
            {
                OperateIniFile.WriteIniData("properties", "service_register", "true", appdata + "/properties.ini");
            }
            else
            {
                OperateIniFile.WriteIniData("properties", "service_register", "false", appdata + "/properties.ini");
            }
            if (hide_mainlib.Checked == true)
            {
                OperateIniFile.WriteIniData("properties", "hide_mainlib", "true", appdata + "/properties.ini");
            }
            else
            {
                OperateIniFile.WriteIniData("properties", "hide_mainlib", "false", appdata + "/properties.ini");
            }
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
                if (img_enable.Checked == true)
                {
                    sw.Write("body{background-image:url(bg.png)!important;background-repeat:no-repeat!important;background-size:100% 100%!important}");
                }
                if (font_enable.Checked == true)
                {
                    sw.Write("@font-face{font-family:Youmu;font-style:normal;font-weight:400;font-display:swap;src:url(font.ttf)}");
                }
                if (hide_mainlib.Checked == true)
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
                string css_source = GetMD5HashFromFile(steam_dir.Text + "\\steamui\\css\\libraryroot.css");
                string css = GetMD5HashFromFile(appdata + "/libraryroot_new.css");
                if (css != css_source)
                    System.IO.File.Copy(appdata + "/libraryroot_new.css", steam_dir.Text + "\\steamui\\css\\libraryroot.css", true);
                if (img_enable.Checked == true)
                {
                    string img_source = GetMD5HashFromFile(steam_dir.Text + "\\steamui\\css\\bg.png");
                    string img = GetMD5HashFromFile(img_dir.Text);
                    if (img != img_source)
                        System.IO.File.Copy(img_dir.Text, steam_dir.Text + "\\steamui\\css\\bg.png", true);
                }
                if (font_enable.Checked == true)
                {
                    string font_source = GetMD5HashFromFile(steam_dir.Text + "\\steamui\\css\\font.ttf");
                    string font = GetMD5HashFromFile(font_dir.Text);
                    if (font != font_source)
                        System.IO.File.Copy(font_dir.Text, steam_dir.Text + "\\steamui\\css\\font.ttf", true);
                }
                // 服务
                string serviceFilePath = $"{Application.StartupPath}\\SteamLibBeautifyService.exe";
                string serviceName = "Steam Library Beautify";
                if (reg_serivce.Checked == true)
                {
                    try
                    {
                        if (this.IsServiceExisted(serviceName)) this.UninstallService(serviceName);
                        this.InstallService(serviceFilePath);
                        if (this.IsServiceExisted(serviceName)) this.ServiceStart(serviceName);
                        Console.WriteLine("Service Installed");
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.ToString());
                    }
                }
                else
                {
                    try
                    {
                        if (this.IsServiceExisted(serviceName)) this.ServiceStop(serviceName);
                        if (this.IsServiceExisted(serviceName))
                        {
                            this.ServiceStop(serviceName);
                            this.UninstallService(serviceFilePath);
                        }
                        Console.WriteLine("Service Uninstalled");
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.ToString());
                    }
                }
                MessageBox.Show("保存成功！");
            }
            catch (Exception ee)
            {
                MessageBox.Show("出错了！！！");
                Console.WriteLine(ee.ToString());
            }
        }

        private void Select_steam_dir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            if (path.ShowDialog() == DialogResult.OK)
            {
                string path2 = path.SelectedPath.Trim() + "\\steamui\\css\\libraryroot.css";
                if (!System.IO.File.Exists(path2))
                {
                    MessageBox.Show("你选择的Steam目录有误，或者您没有启用Steam Beta Update测试版客户端。");
                    return;
                }
                if (path.SelectedPath.Trim() != "")
                    steam_dir.Text = path.SelectedPath.Trim();
            }
        }

        private void Select_img_src_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img_dir.Text = openFileDialog1.FileName;
            }
        }

        private void Select_font_src_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                font_dir.Text = openFileDialog1.FileName;
            }
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
        //判断服务是否存在
        private bool IsServiceExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        //安装服务
        private void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                System.Collections.IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
        }

        //卸载服务
        private void UninstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                installer.Uninstall(null);
            }
        }
        //启动服务
        private void ServiceStart(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
        }

        //停止服务
        private void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }
    }
}
