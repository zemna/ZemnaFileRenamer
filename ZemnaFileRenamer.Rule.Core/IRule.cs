using System.Drawing;

namespace ZemnaFileRenamer.Rule
{
    /// <summary>
    /// Rule Information Interface
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Rule Name
        /// </summary>
        string RuleName { get; }

        /// <summary>
        /// Rule Icon
        /// </summary>
        Bitmap Icon { get; }

        /// <summary>
        /// Create and return Rule implementation instance
        /// </summary>
        /// <returns>Rule implementation instance</returns>
        IRuleImpl CreateObject();
    }
}
