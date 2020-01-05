namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x02000009 RID: 9
	//[global::System.ComponentModel.Composition.ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
	//[global::System.ComponentModel.Composition.Export(typeof(global::ExtractorSharp.Component.ESDialog))]
	public partial class ReplaceOtherDialog : global::ExtractorSharp.Component.ESDialog
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003536 File Offset: 0x00001736
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003558 File Offset: 0x00001758
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.serverList = new global::System.Windows.Forms.ComboBox();
			this.yesButton = new global::ExtractorSharp.Component.ESButton();
			this.cancelButton = new global::ExtractorSharp.Component.ESButton();
			this.progress = new global::System.Windows.Forms.ProgressBar();
			this.serverList.FormattingEnabled = true;
			this.serverList.Location = new global::System.Drawing.Point(33, 54);
			this.serverList.Name = "combo";
			this.serverList.Size = new global::System.Drawing.Size(216, 20);
			this.serverList.TabIndex = 0;
			this.serverList.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.progress.Location = new global::System.Drawing.Point(33, 54);
			this.progress.Name = "progress";
			this.progress.Size = new global::System.Drawing.Size(216, 23);
			this.progress.TabIndex = 1;
			this.progress.Visible = false;
			this.yesButton.Location = new global::System.Drawing.Point(33, 117);
			this.yesButton.Name = "yesButton";
			this.yesButton.Size = new global::System.Drawing.Size(75, 23);
			this.yesButton.TabIndex = 2;
			this.yesButton.Text = base.Language["OK"];
			this.yesButton.UseVisualStyleBackColor = true;
			this.cancelButton.Location = new global::System.Drawing.Point(174, 117);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new global::System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = base.Language["Cancel"];
			this.cancelButton.UseVisualStyleBackColor = true;
			base.CancelButton = this.cancelButton;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(281, 166);
			base.Name = "replaceFromServer";
			this.Text = base.Language["ReplaceFromServer"];
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.yesButton);
			base.Controls.Add(this.progress);
			base.Controls.Add(this.serverList);
		}

		// Token: 0x04000020 RID: 32
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000021 RID: 33
		private global::System.Windows.Forms.ComboBox serverList;

		// Token: 0x04000022 RID: 34
		private global::ExtractorSharp.Component.ESButton yesButton;

		// Token: 0x04000023 RID: 35
		private global::ExtractorSharp.Component.ESButton cancelButton;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.ProgressBar progress;
	}
}
