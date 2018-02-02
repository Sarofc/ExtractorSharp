﻿using System.Windows.Forms;

namespace ExtractorSharp.View.SettingPane {
    partial class MarketplacePane {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            list = new ListView();
            refreshButton = new Button();
            installButton = new Button();

            list.View = System.Windows.Forms.View.SmallIcon;
            list.SmallImageList = new ImageList();
            list.Size = new System.Drawing.Size(300, 300);

            refreshButton.Text = Language["Refresh"];
            refreshButton.Location = new System.Drawing.Point(0, 310);


            installButton.Text = Language["Install"];
            installButton.Location = new System.Drawing.Point(100, 310);


            this.Name = "Marketplace";
            this.Parent = "Plugin";
            Controls.Add(list);
            Controls.Add(refreshButton);
            Controls.Add(installButton);
        }

        #endregion

        private ListView list;

        private Button refreshButton;

        private Button installButton;
    }
}
