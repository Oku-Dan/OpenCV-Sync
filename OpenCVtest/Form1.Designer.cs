namespace OpenCVtest
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.startstop = new System.Windows.Forms.Button();
            this.isSync = new System.Windows.Forms.CheckBox();
            this.distance = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.distance)).BeginInit();
            this.SuspendLayout();
            // 
            // startstop
            // 
            this.startstop.Location = new System.Drawing.Point(13, 13);
            this.startstop.Name = "startstop";
            this.startstop.Size = new System.Drawing.Size(75, 23);
            this.startstop.TabIndex = 0;
            this.startstop.Text = "開始";
            this.startstop.UseVisualStyleBackColor = true;
            this.startstop.Click += new System.EventHandler(this.startstop_Click);
            // 
            // isSync
            // 
            this.isSync.AutoSize = true;
            this.isSync.Checked = true;
            this.isSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isSync.Location = new System.Drawing.Point(95, 13);
            this.isSync.Name = "isSync";
            this.isSync.Size = new System.Drawing.Size(89, 19);
            this.isSync.TabIndex = 1;
            this.isSync.Text = "同期実行";
            this.isSync.UseVisualStyleBackColor = true;
            // 
            // distance
            // 
            this.distance.Location = new System.Drawing.Point(13, 43);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(120, 22);
            this.distance.TabIndex = 2;
            this.distance.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.distance.ValueChanged += new System.EventHandler(this.distance_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.distance);
            this.Controls.Add(this.isSync);
            this.Controls.Add(this.startstop);
            this.Name = "MainForm";
            this.Text = "OpenCVtest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.distance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startstop;
        private System.Windows.Forms.CheckBox isSync;
        private System.Windows.Forms.NumericUpDown distance;
    }
}

