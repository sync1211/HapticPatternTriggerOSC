namespace Haptics_Presets_OSC
{
    partial class PresetsForm
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
            label2 = new Label();
            StartButton = new Button();
            SenderPortInput = new MaskedTextBox();
            dataGridView1 = new DataGridView();
            AddPatternButton = new Button();
            IPInput = new MaskedTextBox();
            IP = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ReceiverPortInput = new MaskedTextBox();
            label1 = new Label();
            StopButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 25);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 8;
            label2.Text = "Port";
            // 
            // StartButton
            // 
            StartButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            StartButton.Location = new Point(13, 54);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(120, 23);
            StartButton.TabIndex = 9;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // SenderPortInput
            // 
            SenderPortInput.Location = new Point(65, 22);
            SenderPortInput.Name = "SenderPortInput";
            SenderPortInput.Size = new Size(96, 23);
            SenderPortInput.TabIndex = 11;
            SenderPortInput.ValidatingType = typeof(DateTime);
            SenderPortInput.TextChanged += SenderPortInput_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.ActiveCaptionText;
            dataGridView1.Location = new Point(14, 99);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ShowCellToolTips = false;
            dataGridView1.Size = new Size(757, 296);
            dataGridView1.TabIndex = 13;
            // 
            // AddPatternButton
            // 
            AddPatternButton.Location = new Point(672, 70);
            AddPatternButton.Name = "AddPatternButton";
            AddPatternButton.Size = new Size(99, 23);
            AddPatternButton.TabIndex = 14;
            AddPatternButton.Text = "Add Pattern";
            AddPatternButton.UseVisualStyleBackColor = true;
            AddPatternButton.Click += AddPatternButton_Click;
            // 
            // IPInput
            // 
            IPInput.Location = new Point(65, 51);
            IPInput.Name = "IPInput";
            IPInput.Size = new Size(96, 23);
            IPInput.TabIndex = 16;
            IPInput.ValidatingType = typeof(DateTime);
            IPInput.TextChanged += IPInput_TextChanged;
            // 
            // IP
            // 
            IP.AutoSize = true;
            IP.Location = new Point(13, 54);
            IP.Name = "IP";
            IP.Size = new Size(52, 15);
            IP.TabIndex = 15;
            IP.Text = "Target IP";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SenderPortInput);
            groupBox1.Controls.Add(IPInput);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(IP);
            groupBox1.Location = new Point(164, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(171, 89);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Sending";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ReceiverPortInput);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(StopButton);
            groupBox2.Controls.Add(StartButton);
            groupBox2.Location = new Point(14, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(144, 89);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            groupBox2.Text = "Receiving";
            // 
            // ReceiverPortInput
            // 
            ReceiverPortInput.Location = new Point(48, 22);
            ReceiverPortInput.Name = "ReceiverPortInput";
            ReceiverPortInput.Size = new Size(85, 23);
            ReceiverPortInput.TabIndex = 11;
            ReceiverPortInput.ValidatingType = typeof(DateTime);
            ReceiverPortInput.TextChanged += ServerPortInput_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 25);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 8;
            label1.Text = "Port";
            // 
            // StopButton
            // 
            StopButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            StopButton.Location = new Point(13, 54);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(120, 23);
            StopButton.TabIndex = 10;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Visible = false;
            StopButton.Click += StopButton_Click;
            // 
            // PresetsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(783, 410);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(AddPatternButton);
            Controls.Add(dataGridView1);
            Name = "PresetsForm";
            Text = "bHaptics Patterns OSC";
            FormClosing += PresetsForm_FormClosing;
            Load += PresetsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label2;
        private Button StartButton;
        private Button StopButton;
        private MaskedTextBox SenderPortInput;
        private DataGridView dataGridView1;
        private Button AddPatternButton;
        private MaskedTextBox IPInput;
        private Label IP;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private MaskedTextBox ReceiverPortInput;
        private Label label1;
    }
}
