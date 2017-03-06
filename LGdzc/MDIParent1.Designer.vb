<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIParent1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIParent1))
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("部门及人员管理")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("资产类别管理")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("常用字典信息管理")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("基础资料管理", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3})
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("新设备入库")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("新入库设备分配")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("查看入库与分配信息")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("入库设备管理", New System.Windows.Forms.TreeNode() {TreeNode5, TreeNode6, TreeNode7})
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("设备信息管理")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("闲置设备再分配")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("设备调拨")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("在用设备管理", New System.Windows.Forms.TreeNode() {TreeNode9, TreeNode10, TreeNode11})
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("数据备份")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("数据导入")
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("系统管理", New System.Windows.Forms.TreeNode() {TreeNode13, TreeNode14})
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.SystemManagToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasswrodToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SysLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.InitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogicCheckToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.JczlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BmToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LbToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.zcrkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZcrkToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.rkzcfpToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.llrkzcToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZcglToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExploreZCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.zcczToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.资产报废ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CaclToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TxtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintPreviewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemManagToolStripMenuItem, Me.JczlToolStripMenuItem, Me.zcrkToolStripMenuItem, Me.ZcglToolStripMenuItem, Me.zcczToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.ViewMenu, Me.HelpMenu})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(738, 25)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'SystemManagToolStripMenuItem
        '
        Me.SystemManagToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PasswrodToolStripMenuItem, Me.SysLogToolStripMenuItem, Me.ToolStripMenuItem1, Me.InitToolStripMenuItem, Me.LogicCheckToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem1})
        Me.SystemManagToolStripMenuItem.Name = "SystemManagToolStripMenuItem"
        Me.SystemManagToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.SystemManagToolStripMenuItem.Text = "系统管理"
        '
        'PasswrodToolStripMenuItem
        '
        Me.PasswrodToolStripMenuItem.Name = "PasswrodToolStripMenuItem"
        Me.PasswrodToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.PasswrodToolStripMenuItem.Text = "密码修改"
        '
        'SysLogToolStripMenuItem
        '
        Me.SysLogToolStripMenuItem.Name = "SysLogToolStripMenuItem"
        Me.SysLogToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.SysLogToolStripMenuItem.Text = "操作日志"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(145, 6)
        '
        'InitToolStripMenuItem
        '
        Me.InitToolStripMenuItem.Name = "InitToolStripMenuItem"
        Me.InitToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.InitToolStripMenuItem.Text = "初始化系统"
        '
        'LogicCheckToolStripMenuItem
        '
        Me.LogicCheckToolStripMenuItem.Name = "LogicCheckToolStripMenuItem"
        Me.LogicCheckToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.LogicCheckToolStripMenuItem.Text = "数据逻辑纠错"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(145, 6)
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(148, 22)
        Me.ExitToolStripMenuItem1.Text = "退出"
        '
        'JczlToolStripMenuItem
        '
        Me.JczlToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BmToolStripMenuItem, Me.LbToolStripMenuItem, Me.ZdToolStripMenuItem, Me.ImportToolStripMenuItem})
        Me.JczlToolStripMenuItem.Name = "JczlToolStripMenuItem"
        Me.JczlToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.JczlToolStripMenuItem.Text = "基础资料"
        '
        'BmToolStripMenuItem
        '
        Me.BmToolStripMenuItem.Name = "BmToolStripMenuItem"
        Me.BmToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.BmToolStripMenuItem.Text = "部门及人员管理"
        '
        'LbToolStripMenuItem
        '
        Me.LbToolStripMenuItem.Name = "LbToolStripMenuItem"
        Me.LbToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.LbToolStripMenuItem.Text = "资产类别管理"
        '
        'ZdToolStripMenuItem
        '
        Me.ZdToolStripMenuItem.Name = "ZdToolStripMenuItem"
        Me.ZdToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.ZdToolStripMenuItem.Text = "通用信息管理"
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.ImportToolStripMenuItem.Text = "数据导入"
        '
        'zcrkToolStripMenuItem
        '
        Me.zcrkToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZcrkToolStripMenuItem1, Me.rkzcfpToolStripMenuItem3, Me.llrkzcToolStripMenuItem3})
        Me.zcrkToolStripMenuItem.Name = "zcrkToolStripMenuItem"
        Me.zcrkToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.zcrkToolStripMenuItem.Text = "资产入库"
        '
        'ZcrkToolStripMenuItem1
        '
        Me.ZcrkToolStripMenuItem1.Name = "ZcrkToolStripMenuItem1"
        Me.ZcrkToolStripMenuItem1.Size = New System.Drawing.Size(160, 22)
        Me.ZcrkToolStripMenuItem1.Text = "资产入库"
        '
        'rkzcfpToolStripMenuItem3
        '
        Me.rkzcfpToolStripMenuItem3.Name = "rkzcfpToolStripMenuItem3"
        Me.rkzcfpToolStripMenuItem3.Size = New System.Drawing.Size(160, 22)
        Me.rkzcfpToolStripMenuItem3.Text = "新入库资产分配"
        '
        'llrkzcToolStripMenuItem3
        '
        Me.llrkzcToolStripMenuItem3.Name = "llrkzcToolStripMenuItem3"
        Me.llrkzcToolStripMenuItem3.Size = New System.Drawing.Size(160, 22)
        Me.llrkzcToolStripMenuItem3.Text = "浏览入库资产"
        '
        'ZcglToolStripMenuItem
        '
        Me.ZcglToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExploreZCToolStripMenuItem})
        Me.ZcglToolStripMenuItem.Name = "ZcglToolStripMenuItem"
        Me.ZcglToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.ZcglToolStripMenuItem.Text = "资产管理"
        '
        'ExploreZCToolStripMenuItem
        '
        Me.ExploreZCToolStripMenuItem.Name = "ExploreZCToolStripMenuItem"
        Me.ExploreZCToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ExploreZCToolStripMenuItem.Text = "资产信息浏览"
        '
        'zcczToolStripMenuItem
        '
        Me.zcczToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.资产报废ToolStripMenuItem})
        Me.zcczToolStripMenuItem.Name = "zcczToolStripMenuItem"
        Me.zcczToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.zcczToolStripMenuItem.Text = "资产处置"
        '
        '资产报废ToolStripMenuItem
        '
        Me.资产报废ToolStripMenuItem.Name = "资产报废ToolStripMenuItem"
        Me.资产报废ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.资产报废ToolStripMenuItem.Text = "资产报废"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CaclToolStripMenuItem, Me.TxtToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.ToolsToolStripMenuItem.Text = "实用工具"
        '
        'CaclToolStripMenuItem
        '
        Me.CaclToolStripMenuItem.Name = "CaclToolStripMenuItem"
        Me.CaclToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.CaclToolStripMenuItem.Text = "计算器"
        '
        'TxtToolStripMenuItem
        '
        Me.TxtToolStripMenuItem.Name = "TxtToolStripMenuItem"
        Me.TxtToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.TxtToolStripMenuItem.Text = "记事本"
        '
        'ViewMenu
        '
        Me.ViewMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarToolStripMenuItem, Me.StatusBarToolStripMenuItem})
        Me.ViewMenu.Name = "ViewMenu"
        Me.ViewMenu.Size = New System.Drawing.Size(60, 21)
        Me.ViewMenu.Text = "视图(&V)"
        '
        'ToolBarToolStripMenuItem
        '
        Me.ToolBarToolStripMenuItem.Checked = True
        Me.ToolBarToolStripMenuItem.CheckOnClick = True
        Me.ToolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolBarToolStripMenuItem.Name = "ToolBarToolStripMenuItem"
        Me.ToolBarToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.ToolBarToolStripMenuItem.Text = "工具栏(&T)"
        '
        'StatusBarToolStripMenuItem
        '
        Me.StatusBarToolStripMenuItem.Checked = True
        Me.StatusBarToolStripMenuItem.CheckOnClick = True
        Me.StatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.StatusBarToolStripMenuItem.Name = "StatusBarToolStripMenuItem"
        Me.StatusBarToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.StatusBarToolStripMenuItem.Text = "状态栏(&S)"
        '
        'HelpMenu
        '
        Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.IndexToolStripMenuItem, Me.SearchToolStripMenuItem, Me.ToolStripSeparator8, Me.AboutToolStripMenuItem})
        Me.HelpMenu.Name = "HelpMenu"
        Me.HelpMenu.Size = New System.Drawing.Size(61, 21)
        Me.HelpMenu.Text = "帮助(&H)"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ContentsToolStripMenuItem.Text = "目录(&C)"
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.Image = CType(resources.GetObject("IndexToolStripMenuItem.Image"), System.Drawing.Image)
        Me.IndexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        Me.IndexToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.IndexToolStripMenuItem.Text = "索引(&I)"
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Image = CType(resources.GetObject("SearchToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SearchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.SearchToolStripMenuItem.Text = "搜索(&S)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(163, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.AboutToolStripMenuItem.Text = "关于(&A) ..."
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 396)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(738, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(32, 17)
        Me.ToolStripStatusLabel.Text = "状态"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 56)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Size = New System.Drawing.Size(738, 340)
        Me.SplitContainer1.SplitterDistance = 160
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 9
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TreeView1.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.TreeView1.FullRowSelect = True
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.ItemHeight = 36
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "部门及人员管理"
        TreeNode1.Text = "部门及人员管理"
        TreeNode2.Name = "资产类别管理"
        TreeNode2.Text = "资产类别管理"
        TreeNode3.Name = "常用字典信息管理"
        TreeNode3.Text = "常用字典信息管理"
        TreeNode4.Name = "节点2"
        TreeNode4.Text = "基础资料管理"
        TreeNode5.Name = "新设备入库"
        TreeNode5.Text = "新设备入库"
        TreeNode6.Name = "新入库设备分配"
        TreeNode6.Text = "新入库设备分配"
        TreeNode7.Name = "查看入库与分配信息"
        TreeNode7.Text = "查看入库与分配信息"
        TreeNode8.Name = "节点4"
        TreeNode8.Text = "入库设备管理"
        TreeNode9.Name = "设备信息管理"
        TreeNode9.Text = "设备信息管理"
        TreeNode10.Name = "闲置设备再分配"
        TreeNode10.Text = "闲置设备再分配"
        TreeNode11.Name = "设备调拨"
        TreeNode11.Text = "设备调拨"
        TreeNode12.Name = "Node2"
        TreeNode12.Text = "在用设备管理"
        TreeNode13.Name = "数据备份"
        TreeNode13.Text = "数据备份"
        TreeNode14.Name = "数据导入"
        TreeNode14.Text = "数据导入"
        TreeNode15.Name = "系统管理"
        TreeNode15.Text = "系统管理"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode4, TreeNode8, TreeNode12, TreeNode15})
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.ShowRootLines = False
        Me.TreeView1.Size = New System.Drawing.Size(158, 338)
        Me.TreeView1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "dw2.ico")
        Me.ImageList1.Images.SetKeyName(1, "ok")
        Me.ImageList1.Images.SetKeyName(2, "lb.ico")
        '
        'NewToolStripButton
        '
        Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
        Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.NewToolStripButton.Name = "NewToolStripButton"
        Me.NewToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.NewToolStripButton.Text = "新建"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.OpenToolStripButton.Text = "打开"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.SaveToolStripButton.Text = "保存"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.PrintToolStripButton.Text = "打印"
        '
        'PrintPreviewToolStripButton
        '
        Me.PrintPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintPreviewToolStripButton.Image = CType(resources.GetObject("PrintPreviewToolStripButton.Image"), System.Drawing.Image)
        Me.PrintPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.PrintPreviewToolStripButton.Name = "PrintPreviewToolStripButton"
        Me.PrintPreviewToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.PrintPreviewToolStripButton.Text = "打印预览"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 31)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Black
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.HelpToolStripButton.Text = "帮助"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.ToolStripSeparator1, Me.PrintToolStripButton, Me.PrintPreviewToolStripButton, Me.ToolStripSeparator2, Me.HelpToolStripButton, Me.ToolStripButton1})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(738, 31)
        Me.ToolStrip.TabIndex = 6
        Me.ToolStrip.Text = "ToolStrip"
        '
        'MDIParent1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 418)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "MDIParent1"
        Me.Text = "长沙市电子计算站：IT设备管理系统"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ViewMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents JczlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BmToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LbToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZcglToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExploreZCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemManagToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasswrodToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SysLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CaclToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LogicCheckToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZdToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents zcrkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents zcczToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 资产报废ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZcrkToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintPreviewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents rkzcfpToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents llrkzcToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem

End Class
