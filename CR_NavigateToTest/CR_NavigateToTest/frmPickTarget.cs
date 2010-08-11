using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using CR_NavigateToTest;
using DevExpress.DXCore.Controls.XtraBars;
using System.ComponentModel;
using System.Drawing;

namespace CR_NavigateToTest
{
    public partial class frmPickTarget : Form
    {
        private Point _location;
        IEnumerable<LanguageElement> _elements;

        public LanguageElement SelectedElement
        {
            get
            {
                TreeNode selectedNode = trvTargets.SelectedNode;
                if (selectedNode == null)
                    return null;
                return selectedNode.Tag as LanguageElement;
            }
        }


        public frmPickTarget(IEnumerable<LanguageElement> elements) : this()
        {
            _elements = elements;
            foreach (var classElement in _elements.Select(el => el.GetClass()).Distinct())
            {
                var classNode = new TreeNode(classElement.Name) { Tag = classElement, ImageIndex = 0, SelectedImageIndex = 0 };
                trvTargets.Nodes.Add(classNode);
                foreach (var methodElement in _elements.Where(el => el.GetClass() == classElement).Select(e => e.GetMethod()).Where(m => m != null).Distinct())
                {
                    var methodNode = classNode.Nodes.Add(methodElement.Name);
                    methodNode.Tag = methodElement;
                    methodNode.ImageIndex = 1;
                    methodNode.SelectedImageIndex = 1;
                    foreach (var reference in _elements.Where(el => el.GetMethod() == methodElement))
                    {
                        var finalLoc = methodNode.Nodes.Add(reference.Parent.ToString());
                        finalLoc.Tag = reference;
                        finalLoc.ImageIndex = 2;
                        finalLoc.SelectedImageIndex = 2;
                    }
                }
            }            
        }

        public frmPickTarget()
        {
            InitializeComponent();
            var elements = new List<LanguageElement> { new Class(), new Method(), new ElementReferenceExpression(string.Empty) };
            elements.ForEach(e => imageList1.Images.Add(this.GetGlyphImage(e)));
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

        private void trvTargets_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private Image GetGlyphImage(IElement lElement)
        {
            Image image = null;
            try
            {
                image = CodeRush.Resources.SourceModelImages.Images[lElement.ImageIndex];
            }
            catch (Exception)
            {
            }
            return image;
        }

        public DialogResult ShowAt(IWin32Window owner, Point location)
        {
            _location = location;
            return this.ShowDialog(owner);
        }
        
        private void frmPickTarget_Shown(object sender, EventArgs e)
        {
            this.Location = _location;
            ResizeToContents();
        }

        private void trvTargets_SizeChanged(object sender, EventArgs e)
        {
            this.Size = trvTargets.Size;
        }

        private void trvTargets_ClientSizeChanged(object sender, EventArgs e)
        {
            this.Size = trvTargets.Size;
        }

        private void trvTargets_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ResizeToContents();
        }

        private void trvTargets_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            ResizeToContents();
        }

        private void ResizeToContents()
        {
            var visibleNodes = GetNodes(trvTargets.Nodes.Cast<TreeNode>(), n => n.Level == 0 || n.Parent.IsExpanded );
            var maxWidth = visibleNodes.Select(n => n.Bounds.Right).Max();
            var maxHeight = visibleNodes.Count() * trvTargets.ItemHeight;
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

    }
}
