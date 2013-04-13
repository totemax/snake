namespace Snake
{
    partial class Snake
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
            this.canvasSnake = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.canvasSnake)).BeginInit();
            this.SuspendLayout();
            // 
            // canvasSnake
            // 
            this.canvasSnake.BackColor = System.Drawing.Color.LightGreen;
            this.canvasSnake.Location = new System.Drawing.Point(12, 43);
            this.canvasSnake.Name = "canvasSnake";
            this.canvasSnake.Size = new System.Drawing.Size(520, 390);
            this.canvasSnake.TabIndex = 0;
            this.canvasSnake.TabStop = false;
            this.canvasSnake.Paint += new System.Windows.Forms.PaintEventHandler(this.canvasSnake_Paint);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Snake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 445);
            this.Controls.Add(this.canvasSnake);
            this.Name = "Snake";
            this.Text = "Snake";
            this.Load += new System.EventHandler(this.Snake_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Snake_KeyDown_1);
            ((System.ComponentModel.ISupportInitialize)(this.canvasSnake)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox canvasSnake;
        private System.Windows.Forms.Timer timer1;
    }
}