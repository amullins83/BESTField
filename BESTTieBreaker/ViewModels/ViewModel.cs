namespace BESTTieBreaker.ViewModels
{
    using BESTTieBreaker.Models;

    public class ViewModel : Model
    {
        private static ViewModel Singleton = new ViewModel();

        public static ViewModel Default
        {
            get { return BESTTieBreaker.ViewModels.ViewModel.Singleton; }
        }
    }
}