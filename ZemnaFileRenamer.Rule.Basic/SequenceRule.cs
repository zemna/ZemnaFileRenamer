using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZemnaFileRenamer.Rule.Basic.Properties;

namespace ZemnaFileRenamer.Rule
{
    public class SequenceRule : IRule
    {
        public string RuleName
        {
            get { return "Sequence Rule"; }
        }

        public Bitmap Icon
        {
            get { return Resources.rule_icon_sequence; }
        }

        public IRuleImpl CreateObject()
        {
            return new SequenceRuleImpl(this);
        }
    }
}
