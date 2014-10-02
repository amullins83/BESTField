namespace BESTTieBreaker.ViewModels
{
    using System.ComponentModel;
    using System.Net;

    public interface IIPv4ViewModel : INotifyPropertyChanged
    {
        string Octet1 { get; set; }
        string Octet2 { get; set; }
        string Octet3 { get; set; }
        string Octet4 { get; set; }

        IPAddress Address { get; set; }
    }
}
