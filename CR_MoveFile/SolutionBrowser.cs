using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using System.IO;
using System.Collections;
using DevExpress.CodeRush.StructuralParser;

namespace CR_MoveFile
{
    public partial class SolutionBrowser : Form
    {
        private Point _location;
        private const string CSharpProject = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
        private const string VBProject = "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}";
        private const string CPPProject = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";

        public string SelectedPath
        {
            get
            {
                return treeView1.SelectedNode.Tag.ToString();
            }
        }

        public string SelectedProject
        {
            get
            {
                var selectedNode = treeView1.SelectedNode;
                while (selectedNode.Level > 1)
                {
                    selectedNode = selectedNode.Parent;
                }
                return selectedNode.Text;
            }
        }
        public SolutionBrowser(string currentFile)
        {
            _currentFile = currentFile;
            InitializeComponent();
            treeView1.ImageList = imageList1;
            LoadTree();
        }
        private void LoadTree()
        {
            var selectedDir = Path.GetDirectoryName(_currentFile);
            var rootNode = new TreeNode(Path.GetFileNameWithoutExtension(CodeRush.Solution.Active.FullName)) { ImageIndex = 0, SelectedImageIndex = 0 };
            rootNode.ImageIndex = 0;
            foreach (EnvDTE.Project project in CodeRush.Solution.Active.Projects)
            {
                if (string.IsNullOrEmpty(project.FileName))
                    continue;
                var projectNode = new TreeNode(project.Name) { ImageIndex = 1, SelectedImageIndex = 1 };
                
                projectNode.ImageIndex = 1;
                rootNode.Nodes.Add(projectNode);
                var projectDir = Path.GetDirectoryName(project.FileName);
                projectNode.Tag = projectDir;
                BuildDirectoryNodes(projectDir, projectNode, project.ProjectItems);
            }
            this.treeView1.Nodes.Add(rootNode);
            this.treeView1.SelectedNode = GetNodes(this.treeView1.Nodes.OfType<TreeNode>(), n => n.Tag.ToString() == selectedDir).FirstOrDefault();
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            if (msg.Msg == WM_KEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        this.DialogResult = DialogResult.OK;
                        return true;
                    case Keys.Escape:
                        this.DialogResult = DialogResult.Cancel;
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public DialogResult ShowAt(IWin32Window owner, Point location)
        {
            _location = location;
            return this.ShowDialog(owner);
        }

        private void ResizeToContents()
        {
            var visibleNodes = GetNodes(treeView1.Nodes.Cast<TreeNode>(), n => n.Level == 0 || n.Parent.IsExpanded);
            var maxWidth = visibleNodes.Select(n => n.Bounds.Right).Max();
            var maxHeight = visibleNodes.Count() * treeView1.ItemHeight;
            this.Size = new Size(maxWidth + 10, maxHeight + 10);
        }
        public IEnumerable<TreeNode> GetNodes(IEnumerable<TreeNode> nodes, Func<TreeNode, bool> predicate)
        {
            if (nodes.Count() > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeNode cNode in GetNodes(node.Nodes.Cast<TreeNode>(), predicate))
                            yield return cNode;
                    }
                    if (predicate(node))
                        yield return node;
                }
            }
        }

        private void BuildDirectoryNodes(string basePath, TreeNode rootNode, EnvDTE.ProjectItems items)
        {
            foreach (EnvDTE.ProjectItem item in items)
            {
                string path = Path.Combine(basePath, item.Name);
                if (Directory.Exists(path))
                {
                    var dirNode = new TreeNode(item.Name) { ImageIndex = 2, SelectedImageIndex = 2, Tag = path };
                    rootNode.Nodes.Add(dirNode);
                    BuildDirectoryNodes(path, dirNode, item.ProjectItems);
                }
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ResizeToContents();
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            ResizeToContents();
        }

        private void SolutionBrowser_Shown(object sender, EventArgs e)
        {
            this.Location = _location;
            ResizeToContents();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
