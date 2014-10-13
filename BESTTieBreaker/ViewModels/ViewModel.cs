namespace BESTTieBreaker.ViewModels
{
    using BESTTieBreaker.Models;

    public class ViewModel : Model
    {
        private static ViewModel Singleton = new ViewModel();

        protected System.Windows.Threading.Dispatcher MainDispatcher = 
            System.Windows.Threading.Dispatcher.CurrentDispatcher;

        public static ViewModel Default
        {
            get { return BESTTieBreaker.ViewModels.ViewModel.Singleton; }
        }
    }
}