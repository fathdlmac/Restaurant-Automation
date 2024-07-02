namespace Proje
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kadi = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.giris_b = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // kadi
            // 
            this.kadi.Location = new System.Drawing.Point(160, 29);
            this.kadi.Name = "kadi";
            this.kadi.Size = new System.Drawing.Size(118, 23);
            this.kadi.TabIndex = 0;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(160, 58);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(118, 23);
            this.pass.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(53, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kullanıcı Adı :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(76, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Şifre :";
            // 
            // giris_b
            // 
            this.giris_b.BackColor = System.Drawing.Color.Red;
            this.giris_b.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.giris_b.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.giris_b.Location = new System.Drawing.Point(160, 87);
            this.giris_b.Name = "giris_b";
            this.giris_b.Size = new System.Drawing.Size(75, 23);
            this.giris_b.TabIndex = 4;
            this.giris_b.Text = "Giriş";
            this.giris_b.UseVisualStyleBackColor = false;
            this.giris_b.Click += new System.EventHandler(this.giris_b_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(363, 129);
            this.Controls.Add(this.giris_b);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.kadi);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox kadi;
        private TextBox pass;
        private Label label1;
        private Label label2;
        private Button giris_b;
    }
}