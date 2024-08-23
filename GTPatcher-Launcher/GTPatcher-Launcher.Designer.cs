namespace GTPatcher_Launcher
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            eggTooltip = new ToolTip(components);
            versionLabel = new Label();
            manifestTooltip = new ToolTip(components);
            manifestHeader = new Label();
            manifestIdLabel = new Label();
            infoTab = new TabPage();
            label3 = new Label();
            bigAssInfoHeader = new Label();
            settingsTab = new TabPage();
            textBox1 = new TextBox();
            steamAccountUsername = new TextBox();
            browseButton = new Button();
            pathHeader = new Label();
            steamHelpBlurb = new Label();
            steamAccountUsernameHeader = new Label();
            installationsTab = new TabPage();
            installedHeader = new Label();
            openInstallPathButton = new Button();
            uninstallButton = new Button();
            playButton = new Button();
            descriptionLabel = new Label();
            descriptionHeader = new Label();
            steamBuildBox = new ComboBox();
            buildLabel = new Label();
            tabBox = new TabControl();
            infoTab.SuspendLayout();
            settingsTab.SuspendLayout();
            installationsTab.SuspendLayout();
            tabBox.SuspendLayout();
            SuspendLayout();
            // 
            // eggTooltip
            // 
            eggTooltip.IsBalloon = true;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(6, 57);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(57, 15);
            versionLabel.TabIndex = 1;
            versionLabel.Text = "beta 0.1.0";
            eggTooltip.SetToolTip(versionLabel, "Now, these points of data make a beautiful line\r\nAnd we're out of beta, we're releasing on time\r\nSo I'm GLaD I got burned, think of all the things we learned\r\nFor the people who are still alive\r\n");
            // 
            // manifestHeader
            // 
            manifestHeader.AutoSize = true;
            manifestHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            manifestHeader.Location = new Point(6, 63);
            manifestHeader.Name = "manifestHeader";
            manifestHeader.Size = new Size(56, 15);
            manifestHeader.TabIndex = 2;
            manifestHeader.Text = "Manifest";
            manifestTooltip.SetToolTip(manifestHeader, "Manifests are Steam's way to identify individual builds of games, apps, DLC, etc.");
            // 
            // manifestIdLabel
            // 
            manifestIdLabel.AutoSize = true;
            manifestIdLabel.Location = new Point(6, 78);
            manifestIdLabel.Name = "manifestIdLabel";
            manifestIdLabel.Size = new Size(178, 15);
            manifestIdLabel.TabIndex = 3;
            manifestIdLabel.Text = "No Steam manifest for this build";
            manifestTooltip.SetToolTip(manifestIdLabel, "Manifests are Steam's way to identify individual builds of games, apps, DLC, etc.");
            // 
            // infoTab
            // 
            infoTab.Controls.Add(label3);
            infoTab.Controls.Add(versionLabel);
            infoTab.Controls.Add(bigAssInfoHeader);
            infoTab.Location = new Point(4, 24);
            infoTab.Name = "infoTab";
            infoTab.Padding = new Padding(3);
            infoTab.Size = new Size(768, 401);
            infoTab.TabIndex = 4;
            infoTab.Text = "Info";
            infoTab.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(6, 338);
            label3.Name = "label3";
            label3.Size = new Size(479, 90);
            label3.TabIndex = 2;
            label3.Text = resources.GetString("label3.Text");
            // 
            // bigAssInfoHeader
            // 
            bigAssInfoHeader.AutoSize = true;
            bigAssInfoHeader.Font = new Font("Segoe UI", 30F);
            bigAssInfoHeader.Location = new Point(-4, 3);
            bigAssInfoHeader.Name = "bigAssInfoHeader";
            bigAssInfoHeader.Size = new Size(523, 54);
            bigAssInfoHeader.TabIndex = 0;
            bigAssInfoHeader.Text = "Gorilla Tag Patcher Launcher";
            // 
            // settingsTab
            // 
            settingsTab.Controls.Add(textBox1);
            settingsTab.Controls.Add(steamAccountUsername);
            settingsTab.Controls.Add(browseButton);
            settingsTab.Controls.Add(pathHeader);
            settingsTab.Controls.Add(steamHelpBlurb);
            settingsTab.Controls.Add(steamAccountUsernameHeader);
            settingsTab.Location = new Point(4, 24);
            settingsTab.Name = "settingsTab";
            settingsTab.Padding = new Padding(3);
            settingsTab.Size = new Size(768, 401);
            settingsTab.TabIndex = 3;
            settingsTab.Text = "Settings";
            settingsTab.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(87, 94);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(675, 23);
            textBox1.TabIndex = 8;
            // 
            // steamAccountUsername
            // 
            steamAccountUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            steamAccountUsername.Location = new Point(6, 21);
            steamAccountUsername.Name = "steamAccountUsername";
            steamAccountUsername.Size = new Size(756, 23);
            steamAccountUsername.TabIndex = 5;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(6, 94);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(75, 23);
            browseButton.TabIndex = 1;
            browseButton.Text = "Browse...";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // pathHeader
            // 
            pathHeader.AutoSize = true;
            pathHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            pathHeader.Location = new Point(6, 76);
            pathHeader.Name = "pathHeader";
            pathHeader.Size = new Size(96, 15);
            pathHeader.TabIndex = 7;
            pathHeader.Text = "Installation Path";
            // 
            // steamHelpBlurb
            // 
            steamHelpBlurb.AutoSize = true;
            steamHelpBlurb.Location = new Point(6, 47);
            steamHelpBlurb.Name = "steamHelpBlurb";
            steamHelpBlurb.Size = new Size(731, 15);
            steamHelpBlurb.TabIndex = 6;
            steamHelpBlurb.Text = "You will be asked for your account password (and any Steam Guard verification, if needed) upon downloading a Steam build of the game.\r\n";
            // 
            // steamAccountUsernameHeader
            // 
            steamAccountUsernameHeader.AutoSize = true;
            steamAccountUsernameHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            steamAccountUsernameHeader.Location = new Point(6, 3);
            steamAccountUsernameHeader.Name = "steamAccountUsernameHeader";
            steamAccountUsernameHeader.Size = new Size(152, 15);
            steamAccountUsernameHeader.TabIndex = 4;
            steamAccountUsernameHeader.Text = "Steam Account Username";
            // 
            // installationsTab
            // 
            installationsTab.Controls.Add(installedHeader);
            installationsTab.Controls.Add(openInstallPathButton);
            installationsTab.Controls.Add(uninstallButton);
            installationsTab.Controls.Add(playButton);
            installationsTab.Controls.Add(descriptionLabel);
            installationsTab.Controls.Add(descriptionHeader);
            installationsTab.Controls.Add(manifestIdLabel);
            installationsTab.Controls.Add(manifestHeader);
            installationsTab.Controls.Add(steamBuildBox);
            installationsTab.Controls.Add(buildLabel);
            installationsTab.Location = new Point(4, 24);
            installationsTab.Name = "installationsTab";
            installationsTab.Padding = new Padding(3);
            installationsTab.Size = new Size(768, 401);
            installationsTab.TabIndex = 1;
            installationsTab.Text = "Installations";
            installationsTab.UseVisualStyleBackColor = true;
            // 
            // installedHeader
            // 
            installedHeader.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            installedHeader.AutoSize = true;
            installedHeader.Font = new Font("Segoe UI", 9F);
            installedHeader.Location = new Point(6, 354);
            installedHeader.Name = "installedHeader";
            installedHeader.Size = new Size(126, 15);
            installedHeader.TabIndex = 8;
            installedHeader.Text = "Currently installed: Yes";
            // 
            // openInstallPathButton
            // 
            openInstallPathButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            openInstallPathButton.Location = new Point(6, 372);
            openInstallPathButton.Name = "openInstallPathButton";
            openInstallPathButton.Size = new Size(107, 23);
            openInstallPathButton.TabIndex = 7;
            openInstallPathButton.Text = "Open install path";
            openInstallPathButton.UseVisualStyleBackColor = true;
            // 
            // uninstallButton
            // 
            uninstallButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            uninstallButton.Location = new Point(119, 372);
            uninstallButton.Name = "uninstallButton";
            uninstallButton.Size = new Size(75, 23);
            uninstallButton.TabIndex = 6;
            uninstallButton.Text = "Uninstall";
            uninstallButton.UseVisualStyleBackColor = true;
            // 
            // playButton
            // 
            playButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            playButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            playButton.Location = new Point(687, 372);
            playButton.Name = "playButton";
            playButton.Size = new Size(75, 23);
            playButton.TabIndex = 2;
            playButton.Text = "Play!";
            playButton.UseVisualStyleBackColor = true;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Assertive;
            descriptionLabel.Location = new Point(6, 119);
            descriptionLabel.MaximumSize = new Size(500, 0);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(158, 15);
            descriptionLabel.TabIndex = 5;
            descriptionLabel.Text = "No description for this patch";
            // 
            // descriptionHeader
            // 
            descriptionHeader.AutoSize = true;
            descriptionHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            descriptionHeader.Location = new Point(6, 104);
            descriptionHeader.Name = "descriptionHeader";
            descriptionHeader.Size = new Size(71, 15);
            descriptionHeader.TabIndex = 4;
            descriptionHeader.Text = "Description";
            // 
            // steamBuildBox
            // 
            steamBuildBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            steamBuildBox.DropDownStyle = ComboBoxStyle.DropDownList;
            steamBuildBox.FormattingEnabled = true;
            steamBuildBox.Location = new Point(6, 21);
            steamBuildBox.Name = "steamBuildBox";
            steamBuildBox.Size = new Size(756, 23);
            steamBuildBox.TabIndex = 1;
            steamBuildBox.SelectedIndexChanged += steamBuildBox_SelectedIndexChanged;
            // 
            // buildLabel
            // 
            buildLabel.AutoSize = true;
            buildLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buildLabel.Location = new Point(6, 3);
            buildLabel.Name = "buildLabel";
            buildLabel.Size = new Size(35, 15);
            buildLabel.TabIndex = 0;
            buildLabel.Text = "Build";
            // 
            // tabBox
            // 
            tabBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabBox.Controls.Add(installationsTab);
            tabBox.Controls.Add(settingsTab);
            tabBox.Controls.Add(infoTab);
            tabBox.Location = new Point(12, 9);
            tabBox.Name = "tabBox";
            tabBox.SelectedIndex = 0;
            tabBox.Size = new Size(776, 429);
            tabBox.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabBox);
            Name = "Form1";
            Text = "GTPatcher Launcher";
            Load += Form1_Load;
            infoTab.ResumeLayout(false);
            infoTab.PerformLayout();
            settingsTab.ResumeLayout(false);
            settingsTab.PerformLayout();
            installationsTab.ResumeLayout(false);
            installationsTab.PerformLayout();
            tabBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ToolTip eggTooltip;
        private ToolTip manifestTooltip;
        private TabPage infoTab;
        private Label label3;
        private Label versionLabel;
        private Label bigAssInfoHeader;
        private TabPage settingsTab;
        private TextBox textBox1;
        private TextBox steamAccountUsername;
        private Button browseButton;
        private Label pathHeader;
        private Label steamHelpBlurb;
        private Label steamAccountUsernameHeader;
        private TabPage installationsTab;
        private Label installedHeader;
        private Button openInstallPathButton;
        private Button uninstallButton;
        private Button playButton;
        private Label descriptionLabel;
        private Label descriptionHeader;
        private Label manifestIdLabel;
        private Label manifestHeader;
        private ComboBox steamBuildBox;
        private Label buildLabel;
        private TabControl tabBox;
    }
}
