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
    public class SetNameRuleImpl : ViewModelBase, IRuleImpl
    {
        #region " Constructor "

        public SetNameRuleImpl(SetNameRule rule)
        {
            _Rule = rule;
        }

        #endregion

        private SetNameRule _Rule;

        public IRule Rule
        {
            get { return _Rule; }
        }

        public Type GetSettingViewType()
        {
            return typeof(SetNameRuleSetting);
        }

        public string Summary
        {
            get
            {
                return this.Name;
            }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Name))
                    return false;

                return true;
            }
        }

        #region " Name "

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _Name = string.Empty;

        /// <summary>
        /// Gets the Name property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                if (_Name == value)
                {
                    return;
                }

                var oldValue = _Name;
                _Name = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(NamePropertyName);
            }
        }

        #endregion

        public IList<ZemnaFileInfo> Apply(IList<ZemnaFileInfo> srcFiles)
        {
            List<ZemnaFileInfo> result = new List<ZemnaFileInfo>();

            foreach (var file in srcFiles)
            {
                ZemnaFileInfo zfi = new ZemnaFileInfo();

                zfi.FileName = this.Name +
                               Path.GetExtension(file.FileName);

                zfi.FullPath = Path.Combine(Path.GetDirectoryName(file.FullPath), zfi.FileName);

                result.Add(zfi);
            }

            return result;            
        }


        public IRuleImpl Clone()
        {
            var obj = new SetNameRuleImpl(_Rule);

            obj.Name = this.Name;

            return obj;
        }
    }
}
