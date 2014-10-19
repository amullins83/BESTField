namespace BEST2014
{
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for interacting with <see cref="UdpClient"/>'s
    /// </summary>
    public interface IUdpClient
    {  
        /// <summary>
        /// Synchronously listen for UDP data
        /// </summary>
        /// <returns>A byte array with the received data</returns>
        byte[] Receive();

        /// <summary>
        /// Synchronously listen for UDP data
        /// </summary>
        /// <param name="endpoint">[Output] Takes the value of the endpoint from which data was received</param>
        /// <returns>A byte array with the received data</returns>
        byte[] Receive(ref IPEndPoint endpoint);

        /// <summary>
        /// Asynchronously listen for UDP data
        /// </summary>
        /// <returns>An await-able promise object, which yields a <see cref="UdpReceiveResult"/></returns>
        Task<UdpReceiveResult> ReceiveAsync();

        /// <summary>
        /// Synchronously send UDP data
        /// </summary>
        /// <param name="message">A byte array to send</param>
        /// <param name="length">The length of the byte array sent</param>
        void Send(byte[] message, int length);

        /// <summary>
        /// Asynchronously send UDP data
        /// </summary>
        /// <param name="message">A byte array to send</param>
        /// <param name="length">The length of the byte array sent</param>
        /// <returns>An await-able promise object</returns>
        Task SendAsync(byte[] message, int length);

        /// <summary>
        /// Establish a connection to a particular device and port
        /// </summary>
        /// <param name="address">The IP address of the connecting device</param>
        /// <param name="port">The port for the connection</param>
        void Connect(IPAddress address, int port);
    }
}
