using System;
using System.Collections.Generic;
using ZemnaFileRenamer.Model;

namespace ZemnaFileRenamer.Rule
{
    /// <summary>
    /// Rule implementation interface
    /// </summary>
    public interface IRuleImpl
    {
        /// <summary>
        /// Rule information interface
        /// </summary>
        IRule Rule { get; }

        /// <summary>
        /// Rule is valid or not
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Get Setting View Type
        /// </summary>
        /// <returns></returns>
        Type GetSettingViewType();

        /// <summary>
        /// Apply Rule and return applied file list
        /// </summary>
        /// <param name="srcFiles">Source file list</param>
        /// <returns>Applied file list</returns>
        IList<ZemnaFileInfo> Apply(IList<ZemnaFileInfo> srcFiles);

        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns>Cloned object</returns>
        IRuleImpl Clone();

        /// <summary>
        /// Rule Summary
        /// </summary>
        string Summary { get; }
    }
}
