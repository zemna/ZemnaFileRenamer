using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZemnaFileRenamer.Model;

namespace ZemnaFileRenamer.ViewModel
{
    public class PreviewViewModel : ViewModelBase
    {
        #region " Properties "

        #region " FileRenameResults "

        /// <summary>
        /// The <see cref="FileRenameResults" /> property's name.
        /// </summary>
        public const string FileRenameResultsPropertyName = "FileRenameResults";

        private ObservableCollection<FileRenameResult> _FileRenameResults = new ObservableCollection<FileRenameResult>();

        /// <summary>
        /// Gets the FileRenameResults property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<FileRenameResult> FileRenameResults
        {
            get
            {
                return _FileRenameResults;
            }

            set
            {
                if (_FileRenameResults == value)
                {
                    return;
                }

                var oldValue = _FileRenameResults;
                _FileRenameResults = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(FileRenameResultsPropertyName);
            }
        }

        #endregion

        public Action RenameCallback { get; set; }

        #endregion

        #region " Constructor "

        /// <summary>
        /// Initializes a new instance of the PreviewViewModel class.
        /// </summary>
        public PreviewViewModel(IList<ZemnaFileInfo> srcFiles, IList<ZemnaFileInfo> resFiles)
        {
            for (int i = 0; i < srcFiles.Count; i++)
            {
                FileRenameResult frr = new FileRenameResult();

                frr.SourceFileName = srcFiles[i].FileName;
                frr.ResultFileName = resFiles[i].FileName;

                this.FileRenameResults.Add(frr);
            }
        }

        #endregion

        #region " Commands "

        #region " CmdRename "

        public ICommand CmdRename
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (RenameCallback != null)
                    {
                        RenameCallback();
                    }
                });
            }
        }

        #endregion

        #endregion
    }
}