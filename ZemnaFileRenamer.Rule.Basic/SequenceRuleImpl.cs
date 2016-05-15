using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZemnaFileRenamer.Model;
using GalaSoft.MvvmLight;
using System.IO;
using System.Drawing;
using ZemnaFileRenamer.Rule.Basic.Properties;
using ZemnaFileRenamer.Rule.View;

namespace ZemnaFileRenamer.Rule
{
    public class SequenceRuleImpl : ViewModelBase, IRuleImpl
    {
        #region " Constructor "

        public SequenceRuleImpl(SequenceRule rule)
        {
            _Rule = rule;
        }

        #endregion

        private SequenceRule _Rule = null;
        public IRule Rule
        {
            get { return _Rule; }
        }

        public Type GetSettingViewType()
        {
            return typeof(SequenceRuleSetting);
        }

        public string Summary
        {
            get
            {
                return this.LeftString + this.SequenceFormat + this.RightString;
            }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.SequenceFormat))
                    return false;

                return true;
            }
        }

        #region " LeftString "

        /// <summary>
        /// The <see cref="LeftString" /> property's name.
        /// </summary>
        public const string LeftStringPropertyName = "LeftString";

        private string _LeftString = string.Empty;

        /// <summary>
        /// Gets the LeftString property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string LeftString
        {
            get
            {
                return _LeftString;
            }

            set
            {
                if (_LeftString == value)
                {
                    return;
                }

                var oldValue = _LeftString;
                _LeftString = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LeftStringPropertyName);
            }
        }

        #endregion

        #region " SequenceFormat "

        /// <summary>
        /// The <see cref="SequenceFormat" /> property's name.
        /// </summary>
        public const string SequenceFormatPropertyName = "SequenceFormat";

        private string _SequenceFormat = "#";

        /// <summary>
        /// Sets and gets the SequenceFormat property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SequenceFormat
        {
            get
            {
                return _SequenceFormat;
            }

            set
            {
                if (_SequenceFormat == value)
                {
                    return;
                }

                RaisePropertyChanging(SequenceFormatPropertyName);
                _SequenceFormat = value;
                RaisePropertyChanged(SequenceFormatPropertyName);
            }
        }

        #endregion

        #region " RightString "

        /// <summary>
        /// The <see cref="RightString" /> property's name.
        /// </summary>
        public const string RightStringPropertyName = "RightString";

        private string _RightString = string.Empty;

        /// <summary>
        /// Gets the RightString property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string RightString
        {
            get
            {
                return _RightString;
            }

            set
            {
                if (_RightString == value)
                {
                    return;
                }

                var oldValue = _RightString;
                _RightString = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RightStringPropertyName);
            }
        }

        #endregion

        #region " StartNum "

        /// <summary>
        /// The <see cref="StartNum" /> property's name.
        /// </summary>
        public const string StartNumPropertyName = "StartNum";

        private int _StartNum = 0;

        /// <summary>
        /// Gets the StartNum property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public int StartNum
        {
            get
            {
                return _StartNum;
            }

            set
            {
                if (_StartNum == value)
                {
                    return;
                }

                var oldValue = _StartNum;
                _StartNum = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(StartNumPropertyName);
            }
        }

        #endregion

        public IList<ZemnaFileInfo> Apply(IList<ZemnaFileInfo> srcFiles)
        {
            List<ZemnaFileInfo> result = new List<ZemnaFileInfo>();

            int x = this.StartNum;

            foreach (var file in srcFiles)
            {
                ZemnaFileInfo zfi = new ZemnaFileInfo();

                zfi.FileName = Path.GetFileNameWithoutExtension(file.FileName) +
                               this.LeftString +
                               (x++).ToString(this.SequenceFormat) +
                               this.RightString +
                               Path.GetExtension(file.FileName);

                zfi.FullPath = Path.Combine(Path.GetDirectoryName(file.FullPath), zfi.FileName);

                result.Add(zfi);
            }

            return result;
        }


        public IRuleImpl Clone()
        {
            SequenceRuleImpl obj = new SequenceRuleImpl(_Rule);

            obj.LeftString = this.LeftString;
            obj.SequenceFormat = this.SequenceFormat;
            obj.RightString = this.RightString;
            obj.StartNum = this.StartNum;

            return obj;
        }
    }
}
