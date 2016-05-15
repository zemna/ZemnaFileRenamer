using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZemnaFileRenamer.Rule.Basic.Properties;

namespace ZemnaFileRenamer.Rule
{
    public class SetNameRule : IRule
    {
        public string RuleName
        {
            get { return "Set Name Rule"; }
        }

        public Bitmap Icon
        {
            get { return Resources.rule_icon_setname; }
        }

        public IRuleImpl CreateObject()
        {
            return new SetNameRuleImpl(this);
        }
    }
}
