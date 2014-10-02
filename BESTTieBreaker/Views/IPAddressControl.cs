namespace BESTTieBreaker.Views
{
    using System.Windows;

    using ViewModels;

    public class IPAddressControl : View
    {
        /// <summary>
        /// First IPv4 octet
        /// </summary>
        public static readonly DependencyProperty Octet1Property =
            DependencyProperty.Register("Octet1", typeof(string), typeof(IPAddressControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Second IPv4 octet
        /// </summary>
        public static readonly DependencyProperty Octet2Property =
            DependencyProperty.Register("Octet2", typeof(string), typeof(IPAddressControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Third IPv4 octet
        /// </summary>
        public static readonly DependencyProperty Octet3Property =
            DependencyProperty.Register("Octet3", typeof(string), typeof(IPAddressControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Last IPv4 octet
        /// </summary>
        public static readonly DependencyProperty Octet4Property =
            DependencyProperty.Register("Octet4", typeof(string), typeof(IPAddressControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the octet1 value
        /// </summary>
        public string Octet1
        {
            get { return (string)GetValue(Octet1Property); }
            set { SetCurrentValue(Octet1Property, value); }
        }

        /// <summary>
        /// Gets or sets the octet2 value
        /// </summary>
        public string Octet2
        {
            get { return (string)GetValue(Octet2Property); }
            set { SetCurrentValue(Octet2Property, value); }
        }

        /// <summary>
        /// Gets or sets the octet3 value
        /// </summary>
        public string Octet3
        {
            get { return (string)GetValue(Octet3Property); }
            set { SetCurrentValue(Octet3Property, value); }
        }

        /// <summary>
        /// Gets or sets the octet4 value
        /// </summary>
        public string Octet4
        {
            get { return (string)GetValue(Octet4Property); }
            set { SetCurrentValue(Octet4Property, value); }
        } 
    }
}
