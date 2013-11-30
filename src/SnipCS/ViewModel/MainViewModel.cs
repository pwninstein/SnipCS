using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnipCS.Services;
using System.Windows.Input;

namespace SnipCS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(ICompileService compileService)
        {
            _compileService = compileService;
            _buildCommand = new RelayCommand(Build, CanBuild);
        }

        private RelayCommand _buildCommand;

        private ICompileService _compileService;


        public string Code { get; set; }

        public ICommand BuildCommand
        {
            get { return _buildCommand; }
        }

        private void Build()
        {
            _compileService.Compile(Code);
        }

        private bool CanBuild()
        {
            return !string.IsNullOrWhiteSpace(Code);
        }
    }
}