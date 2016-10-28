﻿namespace GUILayer.Forms
{
    partial class FrmSaveStack
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.availableStacksGrid = new System.Windows.Forms.DataGridView();
            this.btnSaveStack = new System.Windows.Forms.Button();
            this.txtStackDescription = new System.Windows.Forms.TextBox();
            this.lblStackDescription = new System.Windows.Forms.Label();
            this.ixStackID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.availableStacksGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::GUILayer.Properties.Resources.action_Cancel_16xLG;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(290, 607);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(154, 65);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel\r\n(Ctrl-C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 121;
            this.label3.Text = "Available Stacks:";
            // 
            // availableStacksGrid
            // 
            this.availableStacksGrid.AllowUserToAddRows = false;
            this.availableStacksGrid.AllowUserToDeleteRows = false;
            this.availableStacksGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.availableStacksGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.availableStacksGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.availableStacksGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.availableStacksGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ixStackID,
            this.ShowName,
            this.StackName});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.availableStacksGrid.DefaultCellStyle = dataGridViewCellStyle11;
            this.availableStacksGrid.Location = new System.Drawing.Point(15, 28);
            this.availableStacksGrid.MultiSelect = false;
            this.availableStacksGrid.Name = "availableStacksGrid";
            this.availableStacksGrid.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.availableStacksGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.availableStacksGrid.RowHeadersWidth = 15;
            this.availableStacksGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.availableStacksGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.availableStacksGrid.Size = new System.Drawing.Size(518, 515);
            this.availableStacksGrid.TabIndex = 120;
            this.availableStacksGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.availableStacksGrid_CellClick);
            this.availableStacksGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.availableStacksGrid_CellContentClick);
            // 
            // btnSaveStack
            // 
            this.btnSaveStack.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveStack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveStack.Image = global::GUILayer.Properties.Resources.folder_Open_16xLG;
            this.btnSaveStack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveStack.Location = new System.Drawing.Point(99, 607);
            this.btnSaveStack.Name = "btnSaveStack";
            this.btnSaveStack.Size = new System.Drawing.Size(154, 65);
            this.btnSaveStack.TabIndex = 2;
            this.btnSaveStack.Text = "Save Stack\r\n(Ctrl-S)";
            this.btnSaveStack.UseVisualStyleBackColor = true;
            this.btnSaveStack.Click += new System.EventHandler(this.btnSaveStack_Click);
            // 
            // txtStackDescription
            // 
            this.txtStackDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStackDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStackDescription.Location = new System.Drawing.Point(103, 564);
            this.txtStackDescription.MaxLength = 50;
            this.txtStackDescription.Name = "txtStackDescription";
            this.txtStackDescription.Size = new System.Drawing.Size(270, 22);
            this.txtStackDescription.TabIndex = 1;
            // 
            // lblStackDescription
            // 
            this.lblStackDescription.AutoSize = true;
            this.lblStackDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStackDescription.Location = new System.Drawing.Point(15, 567);
            this.lblStackDescription.Name = "lblStackDescription";
            this.lblStackDescription.Size = new System.Drawing.Size(87, 16);
            this.lblStackDescription.TabIndex = 126;
            this.lblStackDescription.Text = "Description";
            // 
            // ixStackID
            // 
            this.ixStackID.DataPropertyName = "ixStackID";
            this.ixStackID.HeaderText = "ID";
            this.ixStackID.Name = "ixStackID";
            this.ixStackID.ReadOnly = true;
            this.ixStackID.Visible = false;
            this.ixStackID.Width = 75;
            // 
            // ShowName
            // 
            this.ShowName.DataPropertyName = "ShowName";
            this.ShowName.HeaderText = "Show Name";
            this.ShowName.Name = "ShowName";
            this.ShowName.ReadOnly = true;
            this.ShowName.Width = 175;
            // 
            // StackName
            // 
            this.StackName.DataPropertyName = "StackName";
            this.StackName.HeaderText = "Stack Name";
            this.StackName.Name = "StackName";
            this.StackName.ReadOnly = true;
            this.StackName.Width = 327;
            // 
            // FrmSaveStack
            // 
            this.AcceptButton = this.btnSaveStack;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(549, 684);
            this.Controls.Add(this.txtStackDescription);
            this.Controls.Add(this.lblStackDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveStack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.availableStacksGrid);
            this.Location = new System.Drawing.Point(550, 150);
            this.Name = "FrmSaveStack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Stack";
            ((System.ComponentModel.ISupportInitialize)(this.availableStacksGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveStack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView availableStacksGrid;
        private System.Windows.Forms.TextBox txtStackDescription;
        private System.Windows.Forms.Label lblStackDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ixStackID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShowName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StackName;
    }
}