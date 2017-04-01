namespace OperationTickets
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridOperationTickMain = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewRoom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridCol_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCol_Task = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCol_No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCol_OperationPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCol_CreateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCol_OperationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOperationTickMain)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridOperationTickMain);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 26);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(938, 562);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridOperationTickMain
            // 
            this.gridOperationTickMain.ContextMenuStrip = this.contextMenuStrip1;
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.First.Hint = "第一条";
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.Last.Hint = "最后一条";
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.Next.Hint = "下一条";
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.NextPage.Hint = "下一页";
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.Prev.Hint = "上一条";
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.PrevPage.Hint = "上一页";
            this.gridOperationTickMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridOperationTickMain.EmbeddedNavigator.TextStringFormat = "第 {0} 条     共 {1} 条";
            this.gridOperationTickMain.Location = new System.Drawing.Point(12, 12);
            this.gridOperationTickMain.MainView = this.viewRoom;
            this.gridOperationTickMain.Name = "gridOperationTickMain";
            this.gridOperationTickMain.Size = new System.Drawing.Size(914, 538);
            this.gridOperationTickMain.TabIndex = 5;
            this.gridOperationTickMain.UseEmbeddedNavigator = true;
            this.gridOperationTickMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewRoom});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStripMenuItem,
            this.queryToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            // 
            // addStripMenuItem
            // 
            this.addStripMenuItem.Name = "addStripMenuItem";
            this.addStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addStripMenuItem.Text = "新增 (&N)";
            this.addStripMenuItem.ToolTipText = "以此模板新增操作票";
            this.addStripMenuItem.Click += new System.EventHandler(this.addStripMenuItem_Click);
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            this.queryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.queryToolStripMenuItem.Text = "查询 (&Q)";
            this.queryToolStripMenuItem.ToolTipText = "查询该条操作票";
            this.queryToolStripMenuItem.Click += new System.EventHandler(this.queryToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "导出 (&W)";
            this.exportToolStripMenuItem.ToolTipText = "导出该条操作票到Word";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "删除 (&D)";
            this.deleteToolStripMenuItem.ToolTipText = "删除选择的操作票";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // viewRoom
            // 
            this.viewRoom.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridCol_Name,
            this.gridCol_Task,
            this.gridCol_No,
            this.gridCol_OperationPerson,
            this.gridCol_CreateTime,
            this.gridCol_OperationDate});
            this.viewRoom.GridControl = this.gridOperationTickMain;
            this.viewRoom.Name = "viewRoom";
            this.viewRoom.OptionsBehavior.Editable = false;
            this.viewRoom.OptionsDetail.EnableMasterViewMode = false;
            this.viewRoom.OptionsSelection.MultiSelect = true;
            this.viewRoom.OptionsView.ShowAutoFilterRow = true;
            this.viewRoom.OptionsView.ShowFooter = true;
            this.viewRoom.OptionsView.ShowGroupPanel = false;
            this.viewRoom.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridCol_CreateTime, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // gridCol_Name
            // 
            this.gridCol_Name.Caption = "操作票名称";
            this.gridCol_Name.FieldName = "Name";
            this.gridCol_Name.Name = "gridCol_Name";
            this.gridCol_Name.OptionsColumn.AllowEdit = false;
            this.gridCol_Name.OptionsColumn.AllowFocus = false;
            this.gridCol_Name.Visible = true;
            this.gridCol_Name.VisibleIndex = 1;
            this.gridCol_Name.Width = 145;
            // 
            // gridCol_Task
            // 
            this.gridCol_Task.Caption = "操作任务";
            this.gridCol_Task.FieldName = "Task";
            this.gridCol_Task.Name = "gridCol_Task";
            this.gridCol_Task.OptionsColumn.AllowEdit = false;
            this.gridCol_Task.OptionsColumn.AllowFocus = false;
            this.gridCol_Task.Visible = true;
            this.gridCol_Task.VisibleIndex = 2;
            this.gridCol_Task.Width = 145;
            // 
            // gridCol_No
            // 
            this.gridCol_No.AppearanceCell.Options.UseTextOptions = true;
            this.gridCol_No.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridCol_No.AppearanceHeader.Options.UseTextOptions = true;
            this.gridCol_No.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridCol_No.Caption = "编号";
            this.gridCol_No.FieldName = "No";
            this.gridCol_No.Name = "gridCol_No";
            this.gridCol_No.OptionsColumn.AllowEdit = false;
            this.gridCol_No.OptionsColumn.AllowFocus = false;
            this.gridCol_No.Visible = true;
            this.gridCol_No.VisibleIndex = 0;
            this.gridCol_No.Width = 70;
            // 
            // gridCol_OperationPerson
            // 
            this.gridCol_OperationPerson.Caption = "开票人";
            this.gridCol_OperationPerson.FieldName = "User";
            this.gridCol_OperationPerson.Name = "gridCol_OperationPerson";
            this.gridCol_OperationPerson.OptionsColumn.AllowEdit = false;
            this.gridCol_OperationPerson.OptionsColumn.AllowFocus = false;
            this.gridCol_OperationPerson.Visible = true;
            this.gridCol_OperationPerson.VisibleIndex = 3;
            this.gridCol_OperationPerson.Width = 94;
            // 
            // gridCol_CreateTime
            // 
            this.gridCol_CreateTime.Caption = "创建时间";
            this.gridCol_CreateTime.FieldName = "CreateTime";
            this.gridCol_CreateTime.Name = "gridCol_CreateTime";
            this.gridCol_CreateTime.OptionsColumn.AllowEdit = false;
            this.gridCol_CreateTime.OptionsColumn.AllowFocus = false;
            this.gridCol_CreateTime.Visible = true;
            this.gridCol_CreateTime.VisibleIndex = 4;
            this.gridCol_CreateTime.Width = 145;
            // 
            // gridCol_OperationDate
            // 
            this.gridCol_OperationDate.Caption = "操作日期";
            this.gridCol_OperationDate.FieldName = "OperationDate";
            this.gridCol_OperationDate.Name = "gridCol_OperationDate";
            this.gridCol_OperationDate.OptionsColumn.AllowEdit = false;
            this.gridCol_OperationDate.OptionsColumn.AllowFocus = false;
            this.gridCol_OperationDate.Visible = true;
            this.gridCol_OperationDate.VisibleIndex = 5;
            this.gridCol_OperationDate.Width = 152;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(938, 562);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridOperationTickMain;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(918, 542);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnExport});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "新增";
            this.btnAdd.Id = 0;
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "删除";
            this.btnDelete.Id = 1;
            this.btnDelete.ImageIndex = 1;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // btnExport
            // 
            this.btnExport.Caption = "导出";
            this.btnExport.Id = 2;
            this.btnExport.ImageIndex = 9;
            this.btnExport.Name = "btnExport";
            this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(938, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 588);
            this.barDockControlBottom.Size = new System.Drawing.Size(938, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 562);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(938, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 562);
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "12.png");
            this.imageCollection.Images.SetKeyName(1, "13.png");
            this.imageCollection.Images.SetKeyName(2, "16.png");
            this.imageCollection.Images.SetKeyName(3, "(08,03).png");
            this.imageCollection.Images.SetKeyName(4, "查询导出.png");
            this.imageCollection.Images.SetKeyName(5, "导出.png");
            this.imageCollection.Images.SetKeyName(6, "导入.png");
            this.imageCollection.Images.SetKeyName(7, "Get Document.png");
            this.imageCollection.Images.SetKeyName(9, "table_excel.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 588);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "广东电网操作票";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOperationTickMain)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarButtonItem btnExport;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.XtraGrid.GridControl gridOperationTickMain;
        private DevExpress.XtraGrid.Views.Grid.GridView viewRoom;
        private DevExpress.XtraGrid.Columns.GridColumn gridCol_Name;
        private DevExpress.XtraGrid.Columns.GridColumn gridCol_Task;
        private DevExpress.XtraGrid.Columns.GridColumn gridCol_No;
        private DevExpress.XtraGrid.Columns.GridColumn gridCol_OperationPerson;
        private DevExpress.XtraGrid.Columns.GridColumn gridCol_CreateTime;
        private DevExpress.XtraGrid.Columns.GridColumn gridCol_OperationDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStripMenuItem;
    }
}