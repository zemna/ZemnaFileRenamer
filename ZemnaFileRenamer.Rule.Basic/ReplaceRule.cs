using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZemnaFileRenamer.Rule.Basic.Properties;

namespace ZemnaFileRenamer.Rule
{
    public class ReplaceRule : IRule
    {
        public string RuleName
        {
            get { return "Replace Rule"; }
        }

        public IRuleImpl CreateObject()
        {
            return new ReplaceRuleImpl(this);
        }

        public Bitmap Icon
        {
            get { return Resources.rule_icon_replace; }
        }
    }
}
