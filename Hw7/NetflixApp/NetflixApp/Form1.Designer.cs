namespace NetflixApp
{
	partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdGetMovieName = new System.Windows.Forms.Button();
            this.txtMovieID = new System.Windows.Forms.TextBox();
            this.cmdGetMovieReviews = new System.Windows.Forms.Button();
            this.cmdAvgRating = new System.Windows.Forms.Button();
            this.txtRatingsMovieName = new System.Windows.Forms.TextBox();
            this.cmdInsertMovie = new System.Windows.Forms.Button();
            this.txtInsertMovieName = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmdInsertReview = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRating = new System.Windows.Forms.Label();
            this.tbarRating = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdEachRating = new System.Windows.Forms.Button();
            this.cmdTopUsers = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmdTopMoviesByNumReviews = new System.Windows.Forms.Button();
            this.cmdTopMoviesByRating = new System.Windows.Forms.Button();
            this.txtTopN = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarRating)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(380, 412);
            this.listBox1.TabIndex = 0;
            // 
            // cmdConnect
            // 
            this.cmdConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConnect.Location = new System.Drawing.Point(12, 493);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(195, 31);
            this.cmdConnect.TabIndex = 1;
            this.cmdConnect.Text = "Test Connection";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdGetMovieName
            // 
            this.cmdGetMovieName.Location = new System.Drawing.Point(127, 14);
            this.cmdGetMovieName.Name = "cmdGetMovieName";
            this.cmdGetMovieName.Size = new System.Drawing.Size(178, 40);
            this.cmdGetMovieName.TabIndex = 2;
            this.cmdGetMovieName.Text = "Get Movie Name";
            this.cmdGetMovieName.UseVisualStyleBackColor = true;
            this.cmdGetMovieName.Click += new System.EventHandler(this.cmdGetMovieName_Click);
            // 
            // txtMovieID
            // 
            this.txtMovieID.Location = new System.Drawing.Point(15, 19);
            this.txtMovieID.Name = "txtMovieID";
            this.txtMovieID.Size = new System.Drawing.Size(82, 29);
            this.txtMovieID.TabIndex = 3;
            this.txtMovieID.Text = "124";
            this.txtMovieID.TextChanged += new System.EventHandler(this.txtMovieID_TextChanged);
            // 
            // cmdGetMovieReviews
            // 
            this.cmdGetMovieReviews.Location = new System.Drawing.Point(328, 14);
            this.cmdGetMovieReviews.Name = "cmdGetMovieReviews";
            this.cmdGetMovieReviews.Size = new System.Drawing.Size(178, 40);
            this.cmdGetMovieReviews.TabIndex = 4;
            this.cmdGetMovieReviews.Text = "Get Reviews";
            this.cmdGetMovieReviews.UseVisualStyleBackColor = true;
            this.cmdGetMovieReviews.Click += new System.EventHandler(this.cmdGetMovieReviews_Click);
            // 
            // cmdAvgRating
            // 
            this.cmdAvgRating.Location = new System.Drawing.Point(15, 13);
            this.cmdAvgRating.Name = "cmdAvgRating";
            this.cmdAvgRating.Size = new System.Drawing.Size(193, 40);
            this.cmdAvgRating.TabIndex = 5;
            this.cmdAvgRating.Text = "Avg Rating";
            this.cmdAvgRating.UseVisualStyleBackColor = true;
            this.cmdAvgRating.Click += new System.EventHandler(this.cmdAvgRating_Click);
            // 
            // txtRatingsMovieName
            // 
            this.txtRatingsMovieName.Location = new System.Drawing.Point(232, 47);
            this.txtRatingsMovieName.Name = "txtRatingsMovieName";
            this.txtRatingsMovieName.Size = new System.Drawing.Size(274, 29);
            this.txtRatingsMovieName.TabIndex = 7;
            this.txtRatingsMovieName.Text = "Finding Nemo";
            // 
            // cmdInsertMovie
            // 
            this.cmdInsertMovie.Location = new System.Drawing.Point(15, 12);
            this.cmdInsertMovie.Name = "cmdInsertMovie";
            this.cmdInsertMovie.Size = new System.Drawing.Size(193, 40);
            this.cmdInsertMovie.TabIndex = 8;
            this.cmdInsertMovie.Text = "Insert Movie";
            this.cmdInsertMovie.UseVisualStyleBackColor = true;
            this.cmdInsertMovie.Click += new System.EventHandler(this.cmdInsertMovie_Click);
            // 
            // txtInsertMovieName
            // 
            this.txtInsertMovieName.Location = new System.Drawing.Point(232, 19);
            this.txtInsertMovieName.Name = "txtInsertMovieName";
            this.txtInsertMovieName.Size = new System.Drawing.Size(274, 29);
            this.txtInsertMovieName.TabIndex = 9;
            this.txtInsertMovieName.Text = "When Harry Met Salley";
            this.txtInsertMovieName.TextChanged += new System.EventHandler(this.txtInsertMovieName_TextChanged);
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(12, 452);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(195, 26);
            this.txtFileName.TabIndex = 10;
            this.txtFileName.Text = "netflix.mdf";
            // 
            // cmdInsertReview
            // 
            this.cmdInsertReview.Location = new System.Drawing.Point(15, 67);
            this.cmdInsertReview.Name = "cmdInsertReview";
            this.cmdInsertReview.Size = new System.Drawing.Size(193, 40);
            this.cmdInsertReview.TabIndex = 11;
            this.cmdInsertReview.Text = "Insert Review";
            this.cmdInsertReview.UseVisualStyleBackColor = true;
            this.cmdInsertReview.Click += new System.EventHandler(this.cmdInsertReview_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Aqua;
            this.panel1.Controls.Add(this.lblRating);
            this.panel1.Controls.Add(this.tbarRating);
            this.panel1.Controls.Add(this.cmdInsertMovie);
            this.panel1.Controls.Add(this.cmdInsertReview);
            this.panel1.Controls.Add(this.txtInsertMovieName);
            this.panel1.Location = new System.Drawing.Point(414, 426);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 122);
            this.panel1.TabIndex = 12;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(456, 67);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(20, 24);
            this.lblRating.TabIndex = 13;
            this.lblRating.Text = "3";
            // 
            // tbarRating
            // 
            this.tbarRating.LargeChange = 1;
            this.tbarRating.Location = new System.Drawing.Point(232, 62);
            this.tbarRating.Maximum = 5;
            this.tbarRating.Minimum = 1;
            this.tbarRating.Name = "tbarRating";
            this.tbarRating.Size = new System.Drawing.Size(190, 45);
            this.tbarRating.TabIndex = 12;
            this.tbarRating.Value = 3;
            this.tbarRating.Scroll += new System.EventHandler(this.tbarRating_Scroll);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aqua;
            this.panel2.Controls.Add(this.cmdEachRating);
            this.panel2.Controls.Add(this.cmdAvgRating);
            this.panel2.Controls.Add(this.txtRatingsMovieName);
            this.panel2.Location = new System.Drawing.Point(414, 288);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(523, 120);
            this.panel2.TabIndex = 13;
            // 
            // cmdEachRating
            // 
            this.cmdEachRating.Location = new System.Drawing.Point(15, 68);
            this.cmdEachRating.Name = "cmdEachRating";
            this.cmdEachRating.Size = new System.Drawing.Size(193, 40);
            this.cmdEachRating.TabIndex = 8;
            this.cmdEachRating.Text = "Each Rating";
            this.cmdEachRating.UseVisualStyleBackColor = true;
            this.cmdEachRating.Click += new System.EventHandler(this.cmdEachRating_Click);
            // 
            // cmdTopUsers
            // 
            this.cmdTopUsers.Location = new System.Drawing.Point(127, 116);
            this.cmdTopUsers.Name = "cmdTopUsers";
            this.cmdTopUsers.Size = new System.Drawing.Size(379, 40);
            this.cmdTopUsers.TabIndex = 6;
            this.cmdTopUsers.Text = "Top N Users by Reviews Submitted";
            this.cmdTopUsers.UseVisualStyleBackColor = true;
            this.cmdTopUsers.Click += new System.EventHandler(this.cmdTopUsers_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Aqua;
            this.panel3.Controls.Add(this.txtMovieID);
            this.panel3.Controls.Add(this.cmdGetMovieName);
            this.panel3.Controls.Add(this.cmdGetMovieReviews);
            this.panel3.Location = new System.Drawing.Point(414, 15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(523, 67);
            this.panel3.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Aqua;
            this.panel4.Controls.Add(this.cmdTopMoviesByNumReviews);
            this.panel4.Controls.Add(this.cmdTopUsers);
            this.panel4.Controls.Add(this.cmdTopMoviesByRating);
            this.panel4.Controls.Add(this.txtTopN);
            this.panel4.Location = new System.Drawing.Point(414, 101);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(523, 169);
            this.panel4.TabIndex = 15;
            // 
            // cmdTopMoviesByNumReviews
            // 
            this.cmdTopMoviesByNumReviews.Location = new System.Drawing.Point(127, 66);
            this.cmdTopMoviesByNumReviews.Name = "cmdTopMoviesByNumReviews";
            this.cmdTopMoviesByNumReviews.Size = new System.Drawing.Size(379, 40);
            this.cmdTopMoviesByNumReviews.TabIndex = 7;
            this.cmdTopMoviesByNumReviews.Text = "Top N Movies by Num Reviews";
            this.cmdTopMoviesByNumReviews.UseVisualStyleBackColor = true;
            this.cmdTopMoviesByNumReviews.Click += new System.EventHandler(this.cmdTopMoviesByNumReviews_Click);
            // 
            // cmdTopMoviesByRating
            // 
            this.cmdTopMoviesByRating.Location = new System.Drawing.Point(127, 15);
            this.cmdTopMoviesByRating.Name = "cmdTopMoviesByRating";
            this.cmdTopMoviesByRating.Size = new System.Drawing.Size(379, 40);
            this.cmdTopMoviesByRating.TabIndex = 5;
            this.cmdTopMoviesByRating.Text = "Top N Movies by Avg Rating";
            this.cmdTopMoviesByRating.UseVisualStyleBackColor = true;
            this.cmdTopMoviesByRating.Click += new System.EventHandler(this.cmdTopMoviesByAvgRating_Click);
            // 
            // txtTopN
            // 
            this.txtTopN.Location = new System.Drawing.Point(15, 71);
            this.txtTopN.Name = "txtTopN";
            this.txtTopN.Size = new System.Drawing.Size(82, 29);
            this.txtTopN.TabIndex = 5;
            this.txtTopN.Text = "10";
            this.txtTopN.TextChanged += new System.EventHandler(this.txtTopN_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(956, 561);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.listBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarRating)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button cmdConnect;
		private System.Windows.Forms.Button cmdGetMovieName;
		private System.Windows.Forms.TextBox txtMovieID;
		private System.Windows.Forms.Button cmdGetMovieReviews;
    private System.Windows.Forms.Button cmdAvgRating;
		private System.Windows.Forms.TextBox txtRatingsMovieName;
		private System.Windows.Forms.Button cmdInsertMovie;
		private System.Windows.Forms.TextBox txtInsertMovieName;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button cmdInsertReview;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TrackBar tbarRating;
		private System.Windows.Forms.Label lblRating;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button cmdEachRating;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Button cmdTopMoviesByRating;
    private System.Windows.Forms.TextBox txtTopN;
    private System.Windows.Forms.Button cmdTopUsers;
    private System.Windows.Forms.Button cmdTopMoviesByNumReviews;
	}
}

