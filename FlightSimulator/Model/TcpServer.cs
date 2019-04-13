using System;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace FlightSimulator.Model
{
    class TcpServer : IDisposable
    {
        public class DataReceivedEventArgs : SocketAsyncEventArgs
        {
            public NetworkStream Stream { get; private set; }

            public DataReceivedEventArgs(NetworkStream stream)
            {
                Stream = stream;
            }
        }

        private readonly TcpListener _listener;
        private CancellationTokenSource _tokenSource;
        private bool _listening;
        private CancellationToken _token;
        private ApplicationSettingsModel asm;
        public event EventHandler<DataReceivedEventArgs> OnDataReceived;

        public TcpServer()
        {
            this.asm = new ApplicationSettingsModel();
            this._listener = new TcpListener(IPAddress.Any, this.asm.FlightInfoPort);
        }

        public bool Listening => _listening;

        public async Task StartAsync(CancellationToken? token = null)
        {
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token ?? new CancellationToken());
            _token = _tokenSource.Token;
            _listener.Start();
            _listening = true;

            try
            {
                while (!_token.IsCancellationRequested)
                {
                    await Task.Run(async () =>
                    {
                        var tcpClientTask = _listener.AcceptTcpClientAsync();
                        var result = await tcpClientTask;
                        NetworkStream networkStream = result.GetStream();
                        OnDataReceived?.Invoke(this, new DataReceivedEventArgs(networkStream));
                    }, _token);
                }
            }
            finally
            {
                _listener.Stop();
                _listening = false;
            }
        }

        public void Stop()
        {
            _tokenSource?.Cancel();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
