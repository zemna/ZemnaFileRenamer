using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZemnaFileRenamer.Enum;

namespace ZemnaFileRenamer.Model
{
    public class RenameModeItem
    {
        public RenameMode Mode { get; set; }

        public string DisplayName { get; set; }

        public static IList<RenameModeItem> GetRenameModeList()
        {
            List<RenameModeItem> result = new List<RenameModeItem>();

            result.Add(new RenameModeItem() { Mode = RenameMode.Move, DisplayName = "Move" });
            result.Add(new RenameModeItem() { Mode = RenameMode.Copy, DisplayName = "Copy" });

            return result;
        }
    }
}
