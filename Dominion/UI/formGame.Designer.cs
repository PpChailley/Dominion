using System.ComponentModel;
using System.Windows.Forms;

namespace gbd.Dominion.UI
{
    partial class formGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.panelPlayerHand = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBuyZone = new System.Windows.Forms.FlowLayoutPanel();
            this.panelGameZone = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelDeckInfoZone = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelOpponentDeckZone = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelGameInfoZone = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // panelPlayerHand
            // 
            this.panelPlayerHand.Location = new System.Drawing.Point(12, 397);
            this.panelPlayerHand.Name = "panelPlayerHand";
            this.panelPlayerHand.Size = new System.Drawing.Size(861, 201);
            this.panelPlayerHand.TabIndex = 0;
            // 
            // panelBuyZone
            // 
            this.panelBuyZone.Location = new System.Drawing.Point(12, 12);
            this.panelBuyZone.Name = "panelBuyZone";
            this.panelBuyZone.Size = new System.Drawing.Size(1090, 212);
            this.panelBuyZone.TabIndex = 1;
            // 
            // panelGameZone
            // 
            this.panelGameZone.Location = new System.Drawing.Point(231, 230);
            this.panelGameZone.Name = "panelGameZone";
            this.panelGameZone.Size = new System.Drawing.Size(642, 161);
            this.panelGameZone.TabIndex = 2;
            // 
            // PanelDeckInfoZone
            // 
            this.PanelDeckInfoZone.Location = new System.Drawing.Point(879, 423);
            this.PanelDeckInfoZone.Name = "PanelDeckInfoZone";
            this.PanelDeckInfoZone.Size = new System.Drawing.Size(222, 174);
            this.PanelDeckInfoZone.TabIndex = 3;
            // 
            // PanelOpponentDeckZone
            // 
            this.PanelOpponentDeckZone.Location = new System.Drawing.Point(879, 230);
            this.PanelOpponentDeckZone.Name = "PanelOpponentDeckZone";
            this.PanelOpponentDeckZone.Size = new System.Drawing.Size(222, 187);
            this.PanelOpponentDeckZone.TabIndex = 4;
            // 
            // PanelGameInfoZone
            // 
            this.PanelGameInfoZone.Location = new System.Drawing.Point(12, 230);
            this.PanelGameInfoZone.Name = "PanelGameInfoZone";
            this.PanelGameInfoZone.Size = new System.Drawing.Size(207, 160);
            this.PanelGameInfoZone.TabIndex = 5;
            // 
            // formGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 610);
            this.Controls.Add(this.PanelGameInfoZone);
            this.Controls.Add(this.PanelOpponentDeckZone);
            this.Controls.Add(this.PanelDeckInfoZone);
            this.Controls.Add(this.panelGameZone);
            this.Controls.Add(this.panelBuyZone);
            this.Controls.Add(this.panelPlayerHand);
            this.Name = "formGame";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel panelPlayerHand;
        private FlowLayoutPanel panelBuyZone;
        private FlowLayoutPanel panelGameZone;
        private FlowLayoutPanel PanelDeckInfoZone;
        private FlowLayoutPanel PanelOpponentDeckZone;
        private FlowLayoutPanel PanelGameInfoZone;
    }
}

