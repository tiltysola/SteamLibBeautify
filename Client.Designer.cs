namespace SteamLibBeautify
{
    partial class Client
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.steam_dir = new System.Windows.Forms.TextBox();
            this.select_steam_dir = new System.Windows.Forms.Button();
            this.select_img_src = new System.Windows.Forms.Button();
            this.img_dir = new System.Windows.Forms.TextBox();
            this.select_font_src = new System.Windows.Forms.Button();
            this.font_dir = new System.Windows.Forms.TextBox();
            this.img_enable = new System.Windows.Forms.CheckBox();
            this.font_enable = new System.Windows.Forms.CheckBox();
            this.reg_serivce = new System.Windows.Forms.CheckBox();
            this.hide_mainlib = new System.Windows.Forms.CheckBox();
            this.save_setting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // steam_dir
            // 
            this.steam_dir.Location = new System.Drawing.Point(25, 26);
            this.steam_dir.Name = "steam_dir";
            this.steam_dir.ReadOnly = true;
            this.steam_dir.Size = new System.Drawing.Size(330, 21);
            this.steam_dir.TabIndex = 0;
            // 
            // select_steam_dir
            // 
            this.select_steam_dir.Location = new System.Drawing.Point(361, 24);
            this.select_steam_dir.Name = "select_steam_dir";
            this.select_steam_dir.Size = new System.Drawing.Size(100, 23);
            this.select_steam_dir.TabIndex = 1;
            this.select_steam_dir.Text = "选择Steam目录";
            this.select_steam_dir.UseVisualStyleBackColor = true;
            this.select_steam_dir.Click += new System.EventHandler(this.Select_steam_dir_Click);
            // 
            // select_img_src
            // 
            this.select_img_src.Location = new System.Drawing.Point(361, 53);
            this.select_img_src.Name = "select_img_src";
            this.select_img_src.Size = new System.Drawing.Size(100, 23);
            this.select_img_src.TabIndex = 3;
            this.select_img_src.Text = "选择图片文件";
            this.select_img_src.UseVisualStyleBackColor = true;
            this.select_img_src.Click += new System.EventHandler(this.Select_img_src_Click);
            // 
            // img_dir
            // 
            this.img_dir.Location = new System.Drawing.Point(25, 55);
            this.img_dir.Name = "img_dir";
            this.img_dir.ReadOnly = true;
            this.img_dir.Size = new System.Drawing.Size(330, 21);
            this.img_dir.TabIndex = 2;
            // 
            // select_font_src
            // 
            this.select_font_src.Location = new System.Drawing.Point(361, 82);
            this.select_font_src.Name = "select_font_src";
            this.select_font_src.Size = new System.Drawing.Size(100, 23);
            this.select_font_src.TabIndex = 5;
            this.select_font_src.Text = "选择字体文件";
            this.select_font_src.UseVisualStyleBackColor = true;
            this.select_font_src.Click += new System.EventHandler(this.Select_font_src_Click);
            // 
            // font_dir
            // 
            this.font_dir.Location = new System.Drawing.Point(25, 84);
            this.font_dir.Name = "font_dir";
            this.font_dir.ReadOnly = true;
            this.font_dir.Size = new System.Drawing.Size(330, 21);
            this.font_dir.TabIndex = 4;
            // 
            // img_enable
            // 
            this.img_enable.AutoSize = true;
            this.img_enable.Checked = true;
            this.img_enable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.img_enable.Enabled = false;
            this.img_enable.Location = new System.Drawing.Point(467, 57);
            this.img_enable.Name = "img_enable";
            this.img_enable.Size = new System.Drawing.Size(48, 16);
            this.img_enable.TabIndex = 6;
            this.img_enable.Text = "启用";
            this.img_enable.UseVisualStyleBackColor = true;
            // 
            // font_enable
            // 
            this.font_enable.AutoSize = true;
            this.font_enable.Location = new System.Drawing.Point(467, 86);
            this.font_enable.Name = "font_enable";
            this.font_enable.Size = new System.Drawing.Size(48, 16);
            this.font_enable.TabIndex = 7;
            this.font_enable.Text = "启用";
            this.font_enable.UseVisualStyleBackColor = true;
            // 
            // reg_serivce
            // 
            this.reg_serivce.AutoSize = true;
            this.reg_serivce.Checked = true;
            this.reg_serivce.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reg_serivce.Location = new System.Drawing.Point(25, 124);
            this.reg_serivce.Name = "reg_serivce";
            this.reg_serivce.Size = new System.Drawing.Size(318, 16);
            this.reg_serivce.TabIndex = 8;
            this.reg_serivce.Text = "注册Windows服务，防止libraryroot.css被Steam修改。";
            this.reg_serivce.UseVisualStyleBackColor = true;
            // 
            // hide_mainlib
            // 
            this.hide_mainlib.AutoSize = true;
            this.hide_mainlib.Location = new System.Drawing.Point(25, 146);
            this.hide_mainlib.Name = "hide_mainlib";
            this.hide_mainlib.Size = new System.Drawing.Size(156, 16);
            this.hide_mainlib.TabIndex = 9;
            this.hide_mainlib.Text = "隐藏新UI右边的库浏览。";
            this.hide_mainlib.UseVisualStyleBackColor = true;
            // 
            // save_setting
            // 
            this.save_setting.Location = new System.Drawing.Point(25, 168);
            this.save_setting.Name = "save_setting";
            this.save_setting.Size = new System.Drawing.Size(490, 23);
            this.save_setting.TabIndex = 10;
            this.save_setting.Text = "保存设置";
            this.save_setting.UseVisualStyleBackColor = true;
            this.save_setting.Click += new System.EventHandler(this.Save_setting_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 212);
            this.Controls.Add(this.save_setting);
            this.Controls.Add(this.hide_mainlib);
            this.Controls.Add(this.reg_serivce);
            this.Controls.Add(this.font_enable);
            this.Controls.Add(this.img_enable);
            this.Controls.Add(this.select_font_src);
            this.Controls.Add(this.font_dir);
            this.Controls.Add(this.select_img_src);
            this.Controls.Add(this.img_dir);
            this.Controls.Add(this.select_steam_dir);
            this.Controls.Add(this.steam_dir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Client";
            this.Text = "Steam库美化工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox steam_dir;
        private System.Windows.Forms.Button select_steam_dir;
        private System.Windows.Forms.Button select_img_src;
        private System.Windows.Forms.TextBox img_dir;
        private System.Windows.Forms.Button select_font_src;
        private System.Windows.Forms.TextBox font_dir;
        private System.Windows.Forms.CheckBox img_enable;
        private System.Windows.Forms.CheckBox font_enable;
        private System.Windows.Forms.CheckBox reg_serivce;
        private System.Windows.Forms.CheckBox hide_mainlib;
        private System.Windows.Forms.Button save_setting;
    }
}

