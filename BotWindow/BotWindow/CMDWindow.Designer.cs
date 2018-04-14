namespace BotWindow
{
    partial class CMDWindow
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label_Elapsed = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label_Customer = new System.Windows.Forms.Label();
            this.label_Boosted = new System.Windows.Forms.Label();
            this.label_Gamecount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(14, 91);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(867, 260);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label_Elapsed
            // 
            this.label_Elapsed.AutoSize = true;
            this.label_Elapsed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Elapsed.Location = new System.Drawing.Point(13, 72);
            this.label_Elapsed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Elapsed.Name = "label_Elapsed";
            this.label_Elapsed.Size = new System.Drawing.Size(131, 16);
            this.label_Elapsed.TabIndex = 1;
            this.label_Elapsed.Text = "Running: 0 seconds";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 357);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(867, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label_Customer
            // 
            this.label_Customer.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Customer.Location = new System.Drawing.Point(430, 22);
            this.label_Customer.Name = "label_Customer";
            this.label_Customer.Size = new System.Drawing.Size(451, 33);
            this.label_Customer.TabIndex = 3;
            this.label_Customer.Text = "Customer: XXXXXXX";
            this.label_Customer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Boosted
            // 
            this.label_Boosted.AutoSize = true;
            this.label_Boosted.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Boosted.Location = new System.Drawing.Point(13, 49);
            this.label_Boosted.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Boosted.Name = "label_Boosted";
            this.label_Boosted.Size = new System.Drawing.Size(129, 16);
            this.label_Boosted.TabIndex = 4;
            this.label_Boosted.Text = "Boosted: 0 seconds";
            // 
            // label_Gamecount
            // 
            this.label_Gamecount.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Gamecount.Location = new System.Drawing.Point(430, 55);
            this.label_Gamecount.Name = "label_Gamecount";
            this.label_Gamecount.Size = new System.Drawing.Size(451, 33);
            this.label_Gamecount.TabIndex = 5;
            this.label_Gamecount.Text = "Games boosting: 0";
            this.label_Gamecount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CMDWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 384);
            this.Controls.Add(this.label_Gamecount);
            this.Controls.Add(this.label_Boosted);
            this.Controls.Add(this.label_Customer);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label_Elapsed);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "CMDWindow";
            this.ShowIcon = false;
            this.Text = "Boost Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CMDWindow_FormClosing);
            this.Load += new System.EventHandler(this.CMDWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label_Elapsed;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label_Customer;
        private System.Windows.Forms.Label label_Boosted;
        private System.Windows.Forms.Label label_Gamecount;
    }
}