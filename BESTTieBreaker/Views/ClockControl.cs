namespace BESTTieBreaker.Views
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClockControl : Control
    {
        /// <summary>
        /// The client for UDP control
        /// </summary>
        private UdpClient udpClient = new UdpClient();

        /// <summary>
        /// Intializes a new instance of the <see cref="ClockControl"/> class
        /// </summary>
        public ClockControl()
        {
            this.DataContext = this;
            this.udpClient.EnableBroadcast = true;
            this.udpClient.ExclusiveAddressUse = false;
        }

        /// <summary>
        /// Start the countdown
        /// </summary>
        public void Start()
        {
            this.SendMessage("START");
        }

        /// <summary>
        /// Reset the countdown
        /// </summary>
        public void Reset()
        {
            this.SendMessage("STOP");
        }

        /// <summary>
        /// Broadcast the given message on the clock control port
        /// </summary>
        /// <param name="message">The message to send to the clock</param>
        private void SendMessage(string message)
        {
            var bytes = Encoding.ASCII.GetBytes(message);
            this.udpClient.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.None, 32260));
        }
    }
}
