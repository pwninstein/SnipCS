using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnipCS.Services;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnipCS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(ISnipService compileService)
        {
            _compileService = compileService;
            _buildCommand = new RelayCommand(Build, CanBuild);
        }

        #region Instance Variables

        private RelayCommand _buildCommand;

        private ISnipService _compileService;

        private string _status;

        private StringBuilder _buildOutput;

        private StringBuilder _programOutput;

        private bool _isBuildOutputSelected = true;

        private bool _isProgramOutputSelected;

        #endregion

        public bool IsBuildOutputSelected
        {
            get { return _isBuildOutputSelected; }
            set
            {
                _isBuildOutputSelected = value;
                RaisePropertyChanged(() => IsBuildOutputSelected);
            }
        }

        public bool IsProgramOutputSelected
        {
            get { return _isProgramOutputSelected; }
            set
            {
                _isProgramOutputSelected = value;
                RaisePropertyChanged(() => IsProgramOutputSelected);
            }
        }

        public string Code { get; set; }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public string BuildOutput { get { return _buildOutput != null ? _buildOutput.ToString() : string.Empty; } }
        public string ProgramOutput { get { return _programOutput != null ? _programOutput.ToString() : string.Empty; } }

        public ICommand BuildCommand
        {
            get { return _buildCommand; }
        }

        #region Private Methods

        private void Build()
        {
            _buildOutput = new StringBuilder();
            _programOutput = new StringBuilder();

            RaisePropertyChanged(() => BuildOutput);
            RaisePropertyChanged(() => ProgramOutput);

            IsBuildOutputSelected = true;

            Status = "Building...";

            Task.Factory.StartNew(() => _compileService.Build(Code, AppendBuildOutputLine)).ContinueWith(AfterBuild);
        }

        private bool CanBuild()
        {
            return !string.IsNullOrWhiteSpace(Code);
        }

        private void AppendBuildOutputLine(string line)
        {
            _buildOutput.AppendLine(line);

            RaisePropertyChanged(() => BuildOutput);
        }

        private void AppendProgramOutputLine(string line)
        {
            _programOutput.AppendLine(line);

            RaisePropertyChanged(() => ProgramOutput);
        }

        private void AfterBuild(Task<BuildResult> compileTask)
        {
            if (compileTask.Result.IsSuccess)
            {
                Status = "Running...";

                IsProgramOutputSelected = true;

                Task.Factory.StartNew(() => _compileService.Run(compileTask.Result.ExecutableName, AppendProgramOutputLine)).ContinueWith(AfterRun);
            }
            else
            {
                Status = "Build Failed";
            }

        }

        private void AfterRun(Task runTask)
        {
            Status = "Done";
        }

        #endregion
    }
}