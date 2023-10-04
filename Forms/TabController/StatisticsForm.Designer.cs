namespace TawheedBasitPvtLtd.Forms.TabController
{
    partial class StatisticsForm
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
            this.ACCTabCNT = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.ACCTabCNT.SuspendLayout();
            this.SuspendLayout();
            // 
            // ACCTabCNT
            // 
            this.ACCTabCNT.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.ACCTabCNT.Controls.Add(this.tabPage1);
            this.ACCTabCNT.Controls.Add(this.TabPage2);
            this.ACCTabCNT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ACCTabCNT.ItemSize = new System.Drawing.Size(120, 40);
            this.ACCTabCNT.Location = new System.Drawing.Point(3, 0);
            this.ACCTabCNT.Name = "ACCTabCNT";
            this.ACCTabCNT.SelectedIndex = 0;
            this.ACCTabCNT.Size = new System.Drawing.Size(777, 620);
            this.ACCTabCNT.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.ACCTabCNT.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.ACCTabCNT.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.ACCTabCNT.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.ACCTabCNT.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.ACCTabCNT.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.ACCTabCNT.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.ACCTabCNT.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.ACCTabCNT.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.ACCTabCNT.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.ACCTabCNT.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.ACCTabCNT.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.ACCTabCNT.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.ACCTabCNT.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.ACCTabCNT.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.ACCTabCNT.TabButtonSize = new System.Drawing.Size(120, 40);
            this.ACCTabCNT.TabButtonTextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ACCTabCNT.TabIndex = 1;
            this.ACCTabCNT.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.ACCTabCNT.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.VerticalRight;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(649, 612);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.White;
            this.TabPage2.Location = new System.Drawing.Point(4, 4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(649, 612);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "tabPage2";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 620);
            this.Controls.Add(this.ACCTabCNT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StatisticsForm";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Text = "StatisticsForm";
            this.ACCTabCNT.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl ACCTabCNT;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage TabPage2;
    }
}