using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using ZemnaFileRenamer.Enum;
using ZemnaFileRenamer.Message;
using ZemnaFileRenamer.Model;
using ZemnaFileRenamer.Properties;
using ZemnaFileRenamer.Rule;

namespace ZemnaFileRenamer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region " Properties "

        private bool _CancelLoadingFile = false;

        #region " WindowTitle "

        /// <summary>
        /// The <see cref="WindowTitle" /> property's name.
        /// </summary>
        public const string WindowTitlePropertyName = "WindowTitle";

        private string _WindowTitle = "";

        /// <summary>
        /// Sets and gets the WindowTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WindowTitle
        {
            get
            {
                return _WindowTitle;
            }

            set
            {
                if (_WindowTitle == value)
                {
                    return;
                }

                RaisePropertyChanging(WindowTitlePropertyName);
                _WindowTitle = value;
                RaisePropertyChanged(WindowTitlePropertyName);
            }
        }

        #endregion

        #region " Files "

        /// <summary>
        /// The <see cref="Files" /> property's name.
        /// </summary>
        public const string FilesPropertyName = "Files";

        private ObservableCollection<ZemnaFileInfo> _Files = new ObservableCollection<ZemnaFileInfo>();

        /// <summary>
        /// Gets the Files property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<ZemnaFileInfo> Files
        {
            get
            {
                return _Files;
            }

            set
            {
                if (_Files == value)
                {
                    return;
                }

                var oldValue = _Files;
                _Files = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(FilesPropertyName);
            }
        }

        #endregion

        #region " RuleTypes "

        /// <summary>
        /// The <see cref="RuleTypes" /> property's name.
        /// </summary>
        public const string RuleTypesPropertyName = "RuleTypes";

        private ObservableCollection<IRule> _RuleTypes = new ObservableCollection<IRule>();

        /// <summary>
        /// Gets the RuleTypes property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<IRule> RuleTypes
        {
            get
            {
                return _RuleTypes;
            }

            set
            {
                if (_RuleTypes == value)
                {
                    return;
                }

                var oldValue = _RuleTypes;
                _RuleTypes = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RuleTypesPropertyName);
            }
        }

        #endregion

        #region " SelectedRuleType "

        /// <summary>
        /// The <see cref="SelectedRuleType" /> property's name.
        /// </summary>
        public const string SelectedRuleTypePropertyName = "SelectedRuleType";

        private IRule _SelectedRuleType = null;

        /// <summary>
        /// Gets the SelectedRuleType property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public IRule SelectedRuleType
        {
            get
            {
                return _SelectedRuleType;
            }

            set
            {
                if (_SelectedRuleType == value)
                {
                    return;
                }

                var oldValue = _SelectedRuleType;
                _SelectedRuleType = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedRuleTypePropertyName);
            }
        }

        #endregion

        #region " Rules "

        /// <summary>
        /// The <see cref="Rules" /> property's name.
        /// </summary>
        public const string RulesPropertyName = "Rules";

        private ObservableCollection<IRuleImpl> _Rules = new ObservableCollection<IRuleImpl>();

        /// <summary>
        /// Gets the Rules property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<IRuleImpl> Rules
        {
            get
            {
                return _Rules;
            }

            set
            {
                if (_Rules == value)
                {
                    return;
                }

                var oldValue = _Rules;
                _Rules = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RulesPropertyName);
            }
        }

        #endregion

        #region " SelectedRule "

        /// <summary>
        /// The <see cref="SelectedRule" /> property's name.
        /// </summary>
        public const string SelectedRulePropertyName = "SelectedRule";

        private IRuleImpl _SelectedRule = null;

        /// <summary>
        /// Gets the SelectedRule property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public IRuleImpl SelectedRule
        {
            get
            {
                return _SelectedRule;
            }

            set
            {
                if (_SelectedRule == value)
                {
                    return;
                }

                var oldValue = _SelectedRule;
                _SelectedRule = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedRulePropertyName);
            }
        }

        #endregion

        #region " EditRule "

        /// <summary>
        /// The <see cref="EditRule" /> property's name.
        /// </summary>
        public const string EditRulePropertyName = "EditRule";

        private IRuleImpl _EditRule = null;

        /// <summary>
        /// Sets and gets the EditRule property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IRuleImpl EditRule
        {
            get
            {
                return _EditRule;
            }

            set
            {
                if (_EditRule == value)
                {
                    return;
                }

                RaisePropertyChanging(EditRulePropertyName);
                _EditRule = value;
                RaisePropertyChanged(EditRulePropertyName);
            }
        }

        #endregion

        #region " EditMode "

        /// <summary>
        /// The <see cref="EditMode" /> property's name.
        /// </summary>
        public const string EditModePropertyName = "EditMode";

        private bool _EditMode = false;

        /// <summary>
        /// Sets and gets the EditMode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool EditMode
        {
            get
            {
                return _EditMode;
            }

            set
            {
                if (_EditMode == value)
                {
                    return;
                }

                RaisePropertyChanging(EditModePropertyName);
                _EditMode = value;
                RaisePropertyChanged(EditModePropertyName);
            }
        }

        #endregion

        #region " RenameModes "

        /// <summary>
        /// The <see cref="RenameModes" /> property's name.
        /// </summary>
        public const string RenameModesPropertyName = "RenameModes";

        private ObservableCollection<RenameModeItem> _RenameModes = null;

        /// <summary>
        /// Gets the RenameModes property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<RenameModeItem> RenameModes
        {
            get
            {
                return _RenameModes;
            }

            set
            {
                if (_RenameModes == value)
                {
                    return;
                }

                var oldValue = _RenameModes;
                _RenameModes = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RenameModesPropertyName);
            }
        }

        #endregion

        #region " SelectedRenameMode "

        /// <summary>
        /// The <see cref="SelectedRenameMode" /> property's name.
        /// </summary>
        public const string SelectedRenameModePropertyName = "SelectedRenameMode";

        private RenameModeItem _SelectedRenameMode = null;

        /// <summary>
        /// Gets the SelectedRenameMode property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public RenameModeItem SelectedRenameMode
        {
            get
            {
                return _SelectedRenameMode;
            }

            set
            {
                if (_SelectedRenameMode == value)
                {
                    return;
                }

                var oldValue = _SelectedRenameMode;
                _SelectedRenameMode = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedRenameModePropertyName);
            }
        }

        #endregion

        #region " IsBusy "

        /// <summary>
        /// The <see cref="IsBusy" /> property's name.
        /// </summary>
        public const string IsBusyPropertyName = "IsBusy";

        private bool _IsBusy = false;

        /// <summary>
        /// Gets the IsBusy property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }

            set
            {
                if (_IsBusy == value)
                {
                    return;
                }

                var oldValue = _IsBusy;
                _IsBusy = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsBusyPropertyName);
            }
        }

        #endregion

        #region " BusyMessage "

        /// <summary>
        /// The <see cref="BusyMessage" /> property's name.
        /// </summary>
        public const string BusyMessagePropertyName = "BusyMessage";

        private string _BusyMessage = "Wait...";

        /// <summary>
        /// Gets the BusyMessage property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string BusyMessage
        {
            get
            {
                return _BusyMessage;
            }

            set
            {
                if (_BusyMessage == value)
                {
                    return;
                }

                var oldValue = _BusyMessage;
                _BusyMessage = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(BusyMessagePropertyName);
            }
        }

        #endregion

        #endregion

        #region " Constructor "

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.WindowTitle = "Zemna File Renamer - v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (!IsInDesignMode)
            {
                LoadRules();

                this.SelectedRuleType = this.RuleTypes.First();
            }

            this.RenameModes = new ObservableCollection<RenameModeItem>(RenameModeItem.GetRenameModeList());
            this.SelectedRenameMode = this.RenameModes.First();
        }

        #endregion

        #region " Commands "

        #region " CmdDropFile "

        public ICommand CmdDropFile
        {
            get
            {
                return new RelayCommand<DragEventArgs>((e) =>
                {
                    string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                    BackgroundWorker worker = new BackgroundWorker();

                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((s, arg) =>
                    {
                        this.IsBusy = false;
                    });

                    worker.DoWork += new DoWorkEventHandler((s, arg) =>
                    {
                        foreach (var fileName in fileNames)
                        {
                            AddFile(fileName);
                            if (_CancelLoadingFile)
                            {
                                arg.Cancel = true;
                                return;
                            }
                        }
                    });

                    _CancelLoadingFile = false;
                    this.IsBusy = true;
                    worker.RunWorkerAsync();
                });
            }
        }

        private void AddFile(string path)
        {
            var fa = File.GetAttributes(path);
            if ((fa & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var pathes = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);
                foreach (var p in pathes)
                {
                    AddFile(p);

                    if (_CancelLoadingFile)
                        return;
                }

                return;
            }

            ZemnaFileInfo zfi = new ZemnaFileInfo();

            zfi.FileName = Path.GetFileName(path);
            zfi.FullPath = path;

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this.Files.Add(zfi);
            });
        }

        #endregion

        #region " CmdCancelLoadingFile "

        public ICommand CmdCancelLoadingFile
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _CancelLoadingFile = true;
                });
            }
        }

        #endregion

        #region " CmdAddFile "

        public ICommand CmdAddFile
        {
            get
            {
                return new RelayCommand(() =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();

                    dlg.Multiselect = true;

                    if (dlg.ShowDialog() == true)
                    {
                        foreach (var fileName in dlg.FileNames)
                        {
                            ZemnaFileInfo fi = new ZemnaFileInfo();

                            fi.FileName = Path.GetFileName(fileName);
                            fi.FullPath = fileName;

                            this.Files.Add(fi);
                        }
                    }
                });
            }
        }

        #endregion

        #region " CmdDelFile "

        public ICommand CmdDelFile
        {
            get
            {
                return new RelayCommand<ListView>((lv) =>
                {
                    DeleteFiles(lv.SelectedItems);

                }, (lv) =>
                {
                    if (lv == null)
                        return false;

                    if (lv.SelectedItems.Count < 1)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdFileUp "

        public ICommand CmdFileUp
        {
            get
            {
                return new RelayCommand<ListView>((lv) =>
                {
                    this.Files.Move(lv.SelectedIndex, lv.SelectedIndex - 1);

                }, (lv) =>
                {
                    if (lv == null)
                        return false;

                    if (lv.SelectedItems.Count != 1)
                        return false;

                    if (lv.SelectedIndex == 0)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdFileDown "

        public ICommand CmdFileDown
        {
            get
            {
                return new RelayCommand<ListView>((lv) =>
                {
                    this.Files.Move(lv.SelectedIndex, lv.SelectedIndex + 1);

                }, (lv) =>
                {
                    if (lv == null)
                        return false;

                    if (lv.SelectedItems.Count != 1)
                        return false;

                    if (lv.SelectedIndex == (lv.Items.Count - 1))
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdFileKeyDown "

        public ICommand CmdFileKeyDown
        {
            get
            {
                return new RelayCommand<KeyEventArgs>((e) =>
                {
                    if (e.Key == Key.Delete)
                    {
                        ListView lv = (ListView)e.Source;
                        DeleteFiles(lv.SelectedItems);
                    }
                });
            }
        }

        #endregion

        #region " CmdAddRule "

        public ICommand CmdAddRule
        {
            get
            {
                return new RelayCommand(() =>
                {
                    this.EditMode = false;
                    this.EditRule = this.SelectedRuleType.CreateObject();
                }, () =>
                {
                    if (this.SelectedRuleType == null)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdDelRule "

        public ICommand CmdDelRule
        {
            get
            {
                return new RelayCommand(() =>
                {
                    int idx = this.Rules.IndexOf(this.SelectedRule);

                    this.Rules.Remove(this.SelectedRule);

                    if (idx > 0)
                        this.SelectedRule = this.Rules[idx - 1];
                    else if (this.Rules.Count > 0)
                        this.SelectedRule = this.Rules[0];

                }, () =>
                {
                    if (this.SelectedRule == null)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdRuleUp "

        public ICommand CmdRuleUp
        {
            get
            {
                return new RelayCommand(() =>
                {
                    int idx = this.Rules.IndexOf(this.SelectedRule);
                    this.Rules.Move(idx, idx - 1);

                }, () =>
                {
                    if (this.SelectedRule == null)
                        return false;

                    if (this.Rules.IndexOf(this.SelectedRule) == 0)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdRuleDown "

        public ICommand CmdRuleDown
        {
            get
            {
                return new RelayCommand(() =>
                {
                    int idx = this.Rules.IndexOf(this.SelectedRule);
                    this.Rules.Move(idx, idx + 1);

                }, () =>
                {
                    if (this.SelectedRule == null)
                        return false;

                    if (this.Rules.IndexOf(this.SelectedRule) == (this.Rules.Count - 1))
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdRuleDoubleClick "

        public ICommand CmdRuleDoubleClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    this.EditMode = true;
                    this.EditRule = this.SelectedRule.Clone();
                });
            }
        }

        #endregion

        #region " CmdOKEditRule "

        public ICommand CmdOKEditRule
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (this.EditMode)
                    {
                        int index = this.Rules.IndexOf(this.SelectedRule);
                        this.Rules.Remove(this.SelectedRule);
                        this.Rules.Insert(index, this.EditRule);
                    }
                    else
                    {
                        this.Rules.Add(this.EditRule);
                    }

                    this.EditRule = null;

                }, () =>
                {
                    if (this.EditRule == null)
                        return false;

                    if (this.EditRule.IsValid == false)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdCancelEditRule "

        public ICommand CmdCancelEditRule
        {
            get
            {
                return new RelayCommand(() =>
                {
                    this.EditRule = null;
                });
            }
        }

        #endregion

        #region " CmdResetEditRule "

        public ICommand CmdResetEditRule
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (this.EditMode)
                    {
                        this.EditRule = this.SelectedRule.Clone();
                    }
                    else
                    {
                        this.EditRule = this.SelectedRuleType.CreateObject();
                    }

                });
            }
        }

        #endregion

        #region " CmdPreview "

        public ICommand CmdPreview
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var result = ApplyRules();

                    PreviewViewModel vm = new PreviewViewModel(this.Files, result);
                    vm.RenameCallback = new Action(() =>
                    {
                        this.CmdRename.Execute(null);
                    });

                    PreviewMessage msg = new PreviewMessage();

                    msg.ViewModel = vm;

                    Messenger.Default.Send<PreviewMessage>(msg);

                }, () =>
                {
                    if (this.Files == null)
                        return false;

                    if (this.Files.Count < 1)
                        return false;

                    if (this.Rules.Count < 1)
                        return false;

                    if (this.Rules.Count(a => a.IsValid == false) > 0)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdRename "

        public ICommand CmdRename
        {
            get
            {
                return new RelayCommand(() =>
                {
                    BackgroundWorker worker = new BackgroundWorker();

                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((s, e) =>
                    {
                        if (e.Cancelled)
                            return;

                        this.Files.Clear();

                        MessageBox.Show("Success", "Rename", MessageBoxButton.OK, MessageBoxImage.Information);
                    });

                    worker.DoWork += new DoWorkEventHandler((s, e) =>
                    {
                        RenameMode mode = this.SelectedRenameMode.Mode;

                        IList<ZemnaFileInfo> result = ApplyRules();

                        for (int i = 0; i < this.Files.Count; i++)
                        {
                            if (File.Exists(result[i].FullPath))
                            {
                                var mbr = MessageBox.Show("'" + result[i].FileName + "' file is already exist. Overwrite?", "Rename", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

                                if (mbr == MessageBoxResult.Cancel)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                else if (mbr == MessageBoxResult.No)
                                    continue;
                                else
                                    File.Delete(result[i].FullPath);
                            }

                            if (mode == RenameMode.Move)
                                File.Move(this.Files[i].FullPath, result[i].FullPath);
                            else if (mode == RenameMode.Copy)
                                File.Copy(this.Files[i].FullPath, result[i].FullPath);
                        }
                    });

                    worker.RunWorkerAsync();

                }, () =>
                {
                    if (this.Files == null)
                        return false;

                    if (this.Files.Count < 1)
                        return false;

                    if (this.Rules.Count < 1)
                        return false;

                    if (this.Rules.Count(a => a.IsValid == false) > 0)
                        return false;

                    return true;
                });
            }
        }

        #endregion

        #region " CmdHelp "

        public ICommand CmdHelp
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ProcessStartInfo psi = new ProcessStartInfo();

                    psi.Verb = "open";
                    psi.FileName = Settings.Default.HelpURL;

                    Process.Start(psi);
                });
            }
        }

        #endregion

        #region " CmdClose "

        public ICommand CmdClose
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Application.Current.Shutdown();
                });
            }
        }

        #endregion

        #endregion

        #region " Private Methods "

        private void LoadRules()
        {
            this.RuleTypes.Clear();

            try
            {
                var files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Rules"), "*.dll", SearchOption.AllDirectories);


                foreach (var file in files)
                {
                    var assm = Assembly.LoadFile(file);

                    var ruleType = typeof(IRule);
                    var types = assm.GetTypes()
                                    .Where(p => ruleType.IsAssignableFrom(p));

                    foreach (var type in types)
                    {
                        IRule rule = (IRule)Activator.CreateInstance(type);

                        this.RuleTypes.Add(rule);

                        var ruleImpl = rule.CreateObject();

                        var template = CreateTemplate(ruleImpl.GetType(), ruleImpl.GetSettingViewType());
                        var key = template.DataTemplateKey;
                        Application.Current.Resources.Add(key, template);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
            }
        }

        private DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><view:{1} /></DataTemplate>";
            var xaml = String.Format(xamlTemplate, viewModelType.Name, viewType.Name, viewModelType.Namespace, viewType.Namespace);

            var context = new ParserContext();

            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("view", viewType.Namespace, viewType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("view", "view");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
        }

        private void DeleteFiles(IList files)
        {
            List<ZemnaFileInfo> list = new List<ZemnaFileInfo>();

            foreach (var file in files)
            {
                list.Add((ZemnaFileInfo)file);
            }

            foreach (var item in list)
            {
                this.Files.Remove(item);
            }
        }

        private IList<ZemnaFileInfo> ApplyRules()
        {
            IList<ZemnaFileInfo> result = this.Files;

            foreach (var rule in this.Rules)
            {
                result = rule.Apply(result);
            }

            return result;
        }

        #endregion
    }
}