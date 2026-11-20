namespace ChudoObuvApp
{
    partial class FormProducts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProducts));
            FlowPanelProducts = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // FlowPanelProducts
            // 
            FlowPanelProducts.AutoScroll = true;
            FlowPanelProducts.Dock = DockStyle.Fill;
            FlowPanelProducts.FlowDirection = FlowDirection.TopDown;
            FlowPanelProducts.Location = new Point(0, 0);
            FlowPanelProducts.Name = "FlowPanelProducts";
            FlowPanelProducts.Size = new Size(914, 537);
            FlowPanelProducts.TabIndex = 1;
            FlowPanelProducts.WrapContents = false;
            // 
            // FormProducts
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(914, 537);
            Controls.Add(FlowPanelProducts);
            Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormProducts";
            Text = "Чудо Обувь — Каталог товаров";
            WindowState = FormWindowState.Maximized;
            Load += FormProducts_Load;
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel FlowPanelProducts;
    }
}