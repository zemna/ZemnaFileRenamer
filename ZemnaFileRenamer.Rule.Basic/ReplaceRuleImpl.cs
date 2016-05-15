using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZemnaFileRenamer.Model;
using GalaSoft.MvvmLight;
using System.IO;
using ZemnaFileRenamer.Rule.Basic.Properties;
using System.Drawing;
using ZemnaFileRenamer.Rule.View;

namespace ZemnaFileRenamer.Rule
{
    public class ReplaceRuleImpl : ViewModelBase, IRuleImpl
    {
        #region " Constructor "

        public ReplaceRuleImpl(ReplaceRule rule)
        {
            _Rule = rule;
        }

        #endregion

        private ReplaceRule _Rule;

        public IRule Rule
        {
            get { return _Rule; }
        }

        public Type GetSettingViewType()
        {
            return typeof(ReplaceRuleSetting);
        }

        public string Summary
        {
            get
            {
                return this.From + " -> " + this.To;
            }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.From))
                    return false;

                return true;
            }
        }

        #region " From "

        /// <summary>
        /// The <see cref="From" /> property's name.
        /// </summary>
        public const string FromPropertyName = "From";

        private string _From = string.Empty;

        /// <summary>
        /// Gets the From property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string From
        {
            get
            {
                return _From;
            }

            set
            {
                if (_From == value)
                {
                    return;
                }

                var oldValue = _From;
                _From = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(FromPropertyName);
            }
        }

        #endregion

        #region " To "

        /// <summary>
        /// The <see cref="To" /> property's name.
        /// </summary>
        public const string ToPropertyName = "To";

        private string _To = string.Empty;

        /// <summary>
        /// Gets the To property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string To
        {
            get
            {
                return _To;
            }

            set
            {
                if (_To == value)
                {
                    return;
                }

                var oldValue = _To;
                _To = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ToPropertyName);
            }
        }

        #endregion

        public IList<ZemnaFileInfo> Apply(IList<ZemnaFileInfo> srcFiles)
        {
            List<ZemnaFileInfo> result = new List<ZemnaFileInfo>();

            foreach (var file in srcFiles)
            {
                ZemnaFileInfo zfi = new ZemnaFileInfo();

                if (string.IsNullOrEmpty(this.From))
                    zfi.FileName = file.FileName;
                else
                    zfi.FileName = file.FileName.Replace(this.From, this.To);

                zfi.FullPath = Path.Combine(Path.GetDirectoryName(file.FullPath), zfi.FileName);

                result.Add(zfi);
            }

            return result;
        }


        public IRuleImpl Clone()
        {
            ReplaceRuleImpl obj = new ReplaceRuleImpl(_Rule);

            obj.From = this.From;
            obj.To = this.To;

            return obj;
        }
    }
}
