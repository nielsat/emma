using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Models
{
    public class TreeNode
    {
        public string id { get; set; } // required
        public string parent { get; set; } // required
        public string text { get; set; } // node text
        public string icon { get; set; } // string for custom
        public TreeNodeState state { get; set; }
        public string li_attr { get; set; }  // attributes for the generated LI node
        public TreeNodeAttr a_attr { get; set; }
        public TreeNode()
        {
            state = new TreeNodeState();
        }
    }

    public class TreeNodeState
    {
        public bool opened { get; set; }  // is the node open
        public bool disabled { get; set; }  // is the node disabled
        public bool selected { get; set; }  // is the node selected
    }

    public class TreeNodeAttr {
        public string title { get; set; }
        public string @class { get;set;}
        public string id { get; set; }
    }
}