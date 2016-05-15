using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using ZemnaFileRenamer.Model;
using ZemnaFileRenamer.Rule;

namespace ZemnaFileRenamer.Message
{
    public class PreviewMessage : MessageBase
    {
        public object ViewModel { get; set; }
    }
}
