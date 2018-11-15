using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Collections;

namespace UILibrary.PushPanel.Design
{
    /* 作者：Starts_2000
     * 日期：2010-08-10
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */
    internal class PushPanelDesigner : PanelDesigner
    {
        private DesignerActionListCollection _actionLists;

        public PushPanelDesigner()
            : base()
        {
        }

        public override bool CanParent(Control control)
        {
            if (control != null)
            {
                return control is PushPanelItem;
            }
            return false;
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = base.ActionLists;
                    _actionLists.Add(new PushPanelActionList(this));
                }
                return _actionLists;
            }
        }

        protected override void PostFilterProperties(IDictionary properties)
        {
            base.PreFilterAttributes(properties);
            properties.Remove("AccessibilityObject");
            properties.Remove("AccessibleDefaultActionDescription");
            properties.Remove("AccessibleDescription");
            properties.Remove("AccessibleName");
            properties.Remove("AccessibleRole");
            properties.Remove("AntiAliasing");
            properties.Remove("AutoScroll");
            properties.Remove("AutoScrollMargin");
            properties.Remove("AutoScrollMinSize");
        }
    }

    internal class PushPanelActionList : DesignerActionList
    {
        private PushPanelDesigner _designer;

        public PushPanelActionList(PushPanelDesigner designer)
            : base(designer.Component)
        {
            _designer = designer;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            items.Add(new DesignerActionMethodItem(
                this,
                "InvokeItemDialog",
                "编辑项...",
                true));
            return items;
        }

        public void InvokeItemDialog()
        {
            EditorServiceContext.EditValue(_designer, base.Component, "Items");
        }
    }
}
