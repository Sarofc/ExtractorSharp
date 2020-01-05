namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x02000006 RID: 6
	//[global::System.ComponentModel.Composition.ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
	//[global::System.ComponentModel.Composition.Export(typeof(global::ExtractorSharp.Component.ESDialog))]
	public partial class MainDialog : global::ExtractorSharp.Component.ESDialog
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002B3F File Offset: 0x00000D3F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B60 File Offset: 0x00000D60
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.fileList = new global::ExtractorSharp.Component.ESListBox<string>();
			this.serverList = new global::System.Windows.Forms.ComboBox();
			this.directoryList = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.keywordBox = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.bar = new global::System.Windows.Forms.ProgressBar();
			this.tipLabel = new global::System.Windows.Forms.Label();
			this.addListItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.saveAsItem = new global::System.Windows.Forms.ToolStripMenuItem();
			base.SuspendLayout();
			this.fileList.Location = new global::System.Drawing.Point(25, 86);
			this.fileList.Name = "fileList";
			this.fileList.Size = new global::System.Drawing.Size(560, 340);
			this.fileList.TabIndex = 0;
			this.fileList.CanClear = false;
			this.fileList.CanDelete = false;
			this.serverList.FormattingEnabled = true;
			this.serverList.Location = new global::System.Drawing.Point(120, 18);
			this.serverList.Size = new global::System.Drawing.Size(121, 20);
			this.serverList.TabIndex = 3;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(320, 21);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(29, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = base.Language["Directory"];
			this.directoryList.FormattingEnabled = true;
			this.directoryList.Location = new global::System.Drawing.Point(400, 18);
			this.directoryList.Size = new global::System.Drawing.Size(121, 20);
			this.directoryList.TabIndex = 3;
			global::System.Windows.Forms.ComboBox.ObjectCollection items = this.directoryList.Items;
			object[] items2 = this.directories;
			items.AddRange(items2);
			this.directoryList.SelectedIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(23, 21);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(100, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = base.Language["SelectServer"];
			this.keywordBox.Location = new global::System.Drawing.Point(120, 59);
			this.keywordBox.Name = "textBox2";
			this.keywordBox.Size = new global::System.Drawing.Size(400, 21);
			this.keywordBox.TabIndex = 7;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(23, 62);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(29, 12);
			this.label2.TabIndex = 8;
			this.label2.Text = base.Language["Search"];
			this.bar.Location = new global::System.Drawing.Point(25, 469);
			this.bar.Name = "progressBar1";
			this.bar.Size = new global::System.Drawing.Size(581, 23);
			this.bar.TabIndex = 9;
			this.bar.Minimum = 0;
			this.bar.Maximum = 100;
			this.bar.Value = 0;
			this.tipLabel.AutoSize = true;
			this.tipLabel.Location = new global::System.Drawing.Point(23, 439);
			this.tipLabel.Name = "tipLabel";
			this.tipLabel.Size = new global::System.Drawing.Size(0, 12);
			this.tipLabel.TabIndex = 10;
			global::System.Windows.Forms.ContextMenuStrip contextMenuStrip = this.fileList.ContextMenuStrip;
			contextMenuStrip.Items.Add(this.addListItem);
			contextMenuStrip.Items.Add(this.saveAsItem);
			this.addListItem.Text = base.Language["AddList"];
			this.saveAsItem.Text = base.Language["SaveAs"];
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(638, 509);
			base.Controls.Add(this.tipLabel);
			base.Controls.Add(this.bar);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.keywordBox);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.serverList);
			base.Controls.Add(this.fileList);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.directoryList);
			base.Name = "fileDownload";
			this.Text = base.Language["FileDownload"];
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400000C RID: 12
		private global::ExtractorSharp.Plugin.Download.DirectoryInfo[] directories = new global::ExtractorSharp.Plugin.Download.DirectoryInfo[]
		{
			new global::ExtractorSharp.Plugin.Download.DirectoryInfo
			{
				Name = "ImagePacks2",
				Prefix = "sprite",
				Suffix = ".NPK"
			},
			new global::ExtractorSharp.Plugin.Download.DirectoryInfo
			{
				Name = "SoundPacks",
				Prefix = "sounds",
				Suffix = ".npk"
			}
		};

		// Token: 0x0400000E RID: 14
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.ComboBox serverList;

		// Token: 0x04000010 RID: 16
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000011 RID: 17
		private global::System.Windows.Forms.TextBox keywordBox;

		// Token: 0x04000012 RID: 18
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000013 RID: 19
		private global::ExtractorSharp.Component.ESListBox<string> fileList;

		// Token: 0x04000014 RID: 20
		private global::System.Windows.Forms.ProgressBar bar;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.Label tipLabel;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.ToolStripMenuItem addListItem;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.ToolStripMenuItem saveAsItem;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.ComboBox directoryList;
	}
}
