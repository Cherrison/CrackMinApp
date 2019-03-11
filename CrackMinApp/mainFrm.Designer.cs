namespace CrackMinApp
{
    partial class mainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            this.btuExc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtxInfo = new System.Windows.Forms.RichTextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.proBar = new System.Windows.Forms.ProgressBar();
            this.lblSta = new System.Windows.Forms.Label();
            this.tables = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btuExc
            // 
            this.btuExc.Location = new System.Drawing.Point(688, 253);
            this.btuExc.Margin = new System.Windows.Forms.Padding(4);
            this.btuExc.Name = "btuExc";
            this.btuExc.Size = new System.Drawing.Size(122, 48);
            this.btuExc.TabIndex = 0;
            this.btuExc.Text = "开始执行";
            this.btuExc.UseVisualStyleBackColor = true;
            this.btuExc.Click += new System.EventHandler(this.btuExc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择执行文件:";
            // 
            // rtxInfo
            // 
            this.rtxInfo.Location = new System.Drawing.Point(126, 156);
            this.rtxInfo.Name = "rtxInfo";
            this.rtxInfo.ReadOnly = true;
            this.rtxInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtxInfo.Size = new System.Drawing.Size(519, 278);
            this.rtxInfo.TabIndex = 3;
            this.rtxInfo.Text = "";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(16, 156);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(104, 20);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "执行信息:";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("幼圆", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.Location = new System.Drawing.Point(192, 86);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(649, 19);
            this.lblTip.TabIndex = 5;
            this.lblTip.Text = "请将文件放到  D:\\CrackMinApp\\wxapkg目录下 然后选择要反编译的文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("幼圆", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(429, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "生成的文件在与文件目录相同的文件夹下";
            // 
            // proBar
            // 
            this.proBar.Location = new System.Drawing.Point(651, 426);
            this.proBar.Name = "proBar";
            this.proBar.Size = new System.Drawing.Size(189, 23);
            this.proBar.Step = 20;
            this.proBar.TabIndex = 7;
            // 
            // lblSta
            // 
            this.lblSta.AutoSize = true;
            this.lblSta.Font = new System.Drawing.Font("幼圆", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSta.Location = new System.Drawing.Point(684, 370);
            this.lblSta.Name = "lblSta";
            this.lblSta.Size = new System.Drawing.Size(114, 19);
            this.lblSta.TabIndex = 8;
            this.lblSta.Text = "请开始执行";
            // 
            // tables
            // 
            this.tables.FormattingEnabled = true;
            this.tables.Location = new System.Drawing.Point(196, 37);
            this.tables.Name = "tables";
            this.tables.Size = new System.Drawing.Size(470, 28);
            this.tables.TabIndex = 9;
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(853, 461);
            this.Controls.Add(this.tables);
            this.Controls.Add(this.lblSta);
            this.Controls.Add(this.proBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.rtxInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btuExc);
            this.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "mainFrm";
            this.Text = "一键获取小程序源代码";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btuExc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxInfo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar proBar;
        private System.Windows.Forms.Label lblSta;
        private System.Windows.Forms.ComboBox tables;
    }
}

