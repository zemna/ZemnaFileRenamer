using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZemnaFileRenamer.Rule;

namespace ZemnaFileRenamer.Message
{
    public class RuleSettingFocusMessage : MessageBase
    {
        public IRule Rule { get; set; }
    }
}
