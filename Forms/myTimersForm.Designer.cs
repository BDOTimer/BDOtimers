namespace BDOtimers
{
    partial class myTimersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myTimersForm));
            this.panelManager = new System.Windows.Forms.Panel();
            this.labelNameProgram = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonMin = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelModel = new System.Windows.Forms.Panel();
            this.buttonOn = new System.Windows.Forms.Button();
            this.panelCT = new System.Windows.Forms.Panel();
            this.richTextBoxInput = new System.Windows.Forms.RichTextBox();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.buttonDebugClose = new System.Windows.Forms.Button();
            this.panelManager.SuspendLayout();
            this.panelModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelManager
            // 
            this.panelManager.BackColor = System.Drawing.Color.LightGray;
            this.panelManager.Controls.Add(this.labelNameProgram);
            this.panelManager.Controls.Add(this.buttonHelp);
            this.panelManager.Controls.Add(this.buttonMin);
            this.panelManager.Controls.Add(this.buttonAdd);
            this.panelManager.Location = new System.Drawing.Point(3, 3);
            this.panelManager.Name = "panelManager";
            this.panelManager.Size = new System.Drawing.Size(207, 29);
            this.panelManager.TabIndex = 0;
            this.panelManager.Paint += new System.Windows.Forms.PaintEventHandler(this.panelManager_Paint);
            // 
            // labelNameProgram
            // 
            this.labelNameProgram.AutoSize = true;
            this.labelNameProgram.BackColor = System.Drawing.Color.Transparent;
            this.labelNameProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Bold);
            this.labelNameProgram.ForeColor = System.Drawing.Color.Yellow;
            this.labelNameProgram.Location = new System.Drawing.Point(38, 8);
            this.labelNameProgram.Name = "labelNameProgram";
            this.labelNameProgram.Size = new System.Drawing.Size(76, 12);
            this.labelNameProgram.TabIndex = 3;
            this.labelNameProgram.Text = "BDOtimers-...";
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackColor = System.Drawing.SystemColors.Control;
            this.buttonHelp.Location = new System.Drawing.Point(152, 3);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(17, 23);
            this.buttonHelp.TabIndex = 2;
            this.buttonHelp.Text = "?";
            this.buttonHelp.UseVisualStyleBackColor = false;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // buttonMin
            // 
            this.buttonMin.BackColor = System.Drawing.SystemColors.Control;
            this.buttonMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMin.Location = new System.Drawing.Point(3, 3);
            this.buttonMin.Name = "buttonMin";
            this.buttonMin.Size = new System.Drawing.Size(29, 23);
            this.buttonMin.TabIndex = 1;
            this.buttonMin.Text = "_";
            this.buttonMin.UseVisualStyleBackColor = false;
            this.buttonMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonMin_KeyDown);
            this.buttonMin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonMin_MouseDown);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.Location = new System.Drawing.Point(175, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(29, 23);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // panelModel
            // 
            this.panelModel.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelModel.Controls.Add(this.buttonOn);
            this.panelModel.Controls.Add(this.panelCT);
            this.panelModel.Controls.Add(this.richTextBoxInput);
            this.panelModel.Location = new System.Drawing.Point(3, 35);
            this.panelModel.Name = "panelModel";
            this.panelModel.Size = new System.Drawing.Size(207, 29);
            this.panelModel.TabIndex = 1;
            // 
            // buttonOn
            // 
            this.buttonOn.BackColor = System.Drawing.SystemColors.Control;
            this.buttonOn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOn.Location = new System.Drawing.Point(175, 3);
            this.buttonOn.Name = "buttonOn";
            this.buttonOn.Size = new System.Drawing.Size(29, 23);
            this.buttonOn.TabIndex = 1;
            this.buttonOn.Text = "O";
            this.buttonOn.UseVisualStyleBackColor = false;
            // 
            // panelCT
            // 
            this.panelCT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panelCT.Location = new System.Drawing.Point(173, 1);
            this.panelCT.Name = "panelCT";
            this.panelCT.Size = new System.Drawing.Size(33, 27);
            this.panelCT.TabIndex = 2;
            // 
            // richTextBoxInput
            // 
            this.richTextBoxInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.richTextBoxInput.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxInput.MaxLength = 15;
            this.richTextBoxInput.Multiline = false;
            this.richTextBoxInput.Name = "richTextBoxInput";
            this.richTextBoxInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBoxInput.Size = new System.Drawing.Size(140, 23);
            this.richTextBoxInput.TabIndex = 0;
            this.richTextBoxInput.Text = "";
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBoxDebug.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDebug.ForeColor = System.Drawing.Color.Lime;
            this.textBoxDebug.Location = new System.Drawing.Point(3, 144);
            this.textBoxDebug.Multiline = true;
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.Size = new System.Drawing.Size(207, 104);
            this.textBoxDebug.TabIndex = 2;
            // 
            // buttonDebugClose
            // 
            this.buttonDebugClose.BackColor = System.Drawing.Color.LightCoral;
            this.buttonDebugClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDebugClose.ForeColor = System.Drawing.Color.Firebrick;
            this.buttonDebugClose.Location = new System.Drawing.Point(183, 233);
            this.buttonDebugClose.Name = "buttonDebugClose";
            this.buttonDebugClose.Size = new System.Drawing.Size(21, 15);
            this.buttonDebugClose.TabIndex = 3;
            this.buttonDebugClose.UseVisualStyleBackColor = false;
            this.buttonDebugClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonDebugClose_MouseUp);
            // 
            // myTimersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(213, 260);
            this.Controls.Add(this.buttonDebugClose);
            this.Controls.Add(this.panelModel);
            this.Controls.Add(this.panelManager);
            this.Controls.Add(this.textBoxDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "myTimersForm";
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.myTimersForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelManager.ResumeLayout(false);
            this.panelManager.PerformLayout();
            this.panelModel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelManager;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonOn;
        private System.Windows.Forms.RichTextBox richTextBoxInput;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Button buttonDebugClose;
        public System.Windows.Forms.Panel panelModel;
        public System.Windows.Forms.Label labelNameProgram;
        public System.Windows.Forms.Panel panelCT;
        public System.Windows.Forms.Button buttonMin;
        public System.Windows.Forms.TextBox textBoxDebug;
    }
}

