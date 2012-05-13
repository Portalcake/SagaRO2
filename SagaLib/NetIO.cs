using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace SagaLib
{
    public class NetIO
    {
        private byte[] serverKey = new byte[16];
        private byte[] clientKey = new byte[16];

        private byte[] buffer = new byte[2];

        private AsyncCallback callbackSize;
        private AsyncCallback callbackData;

        public Socket sock;
        private NetworkStream stream;

        private Client client;

        private Logger Log = new Logger("server.log");

        private bool isDisconnected;
        private bool disconnecting;

        //only for gateway connction
        private bool isGateway = false;
        public bool fullHeader = false;
        private ClientManager currentClientManager;

        private ReaderWriterLock nlock;
        
        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type
        /// </summary>
        private Dictionary<ushort, Packet> commandTable;

        /// <summary>
        /// The key to use for crypting packets that are send to the client.
        /// </summary>
        public byte[] ClientKey { get { return this.clientKey; } set { this.clientKey = value; } }

        /// <summary>
        /// The key to use for crypting packets that are received by the server
        /// </summary>
        public byte[] ServerKey { get { return this.serverKey; } set { this.serverKey = value; } }

        /// <summary>
        /// Create a new netIO class using a given socket.
        /// </summary>
        /// <param name="sock">The socket for this netIO class.</param>
        public NetIO(Socket sock, Dictionary<ushort, Packet> commandTable, Client client ,ClientManager manager)
        {
            this.sock = sock;
            this.stream = new NetworkStream(sock);
            this.commandTable = commandTable;
            this.client = client;
            this.currentClientManager = manager;

            this.callbackSize = new AsyncCallback(this.ReceiveSize);
            this.callbackData = new AsyncCallback(this.ReceiveData);
            this.nlock = new ReaderWriterLock();
            // Use the static key untill the keys have been exchanged
            this.clientKey = new byte[16];
            Encryption.StaticKey.CopyTo(this.clientKey, 0);
            this.serverKey = new byte[16];
            Encryption.StaticKey.CopyTo(this.serverKey, 0);

            this.isDisconnected = false;

            // Receive the size of the next packet and call ReceiveSize when finished
            if (sock.Connected)
            {
                try { stream.BeginRead(buffer, 0, 2, this.callbackSize, null); }
                catch (Exception ex) {
                    Logger.ShowError(ex, null);
                    try//this could crash the gateway somehow,so better ignore the Exception
                    {
                        this.Disconnect(); 
                    }
                    catch (Exception)
                    {
                    }
                    Logger.ShowWarning("Invalid packet head from:" + sock.RemoteEndPoint.ToString(), null);
                    return;
                }
            }
            else { this.Disconnect(); return; }
            
        }

        public NetIO(Socket sock, Dictionary<ushort, Packet> commandTable, Client client, bool isgateway)
        {
            this.sock = sock;
            this.stream = new NetworkStream(sock);
            this.commandTable = commandTable;
            this.client = client;
            this.isGateway = isgateway;

            this.callbackSize = new AsyncCallback(this.ReceiveSize);
            this.callbackData = new AsyncCallback(this.ReceiveData);
            this.nlock = new ReaderWriterLock();
            // Use the static key untill the keys have been exchanged
            this.clientKey = new byte[16];
            Encryption.StaticKey.CopyTo(this.clientKey, 0);
            this.serverKey = new byte[16];
            Encryption.StaticKey.CopyTo(this.serverKey, 0);

            this.isDisconnected = false;

            // Receive the size of the next packet and call ReceiveSize when finished
            if (sock.Connected)
            {
                try { stream.BeginRead(buffer, 0, 2, this.callbackSize, null); }
                catch (Exception ex)
                {
                    Logger.ShowError(ex, null);
                    try//this could crash the gateway somehow,so better ignore the Exception
                    {
                        this.Disconnect();
                    }
                    catch (Exception)
                    {
                    }
                    Logger.ShowWarning("Invalid packet head from:" + sock.RemoteEndPoint.ToString(), null);
                    return;
                }
            }
            else { this.Disconnect(); return; }

        }


        /// <summary>
        /// Disconnect the client
        /// </summary>
        public void Disconnect()
        {
            try
            {
                //this.nlock.AcquireWriterLock(Timeout.Infinite);

                if (this.isDisconnected)
                {
                    //this.nlock.ReleaseWriterLock(); 
                    return;
                }
                try
                {
                    if (!disconnecting)
                        this.client.OnDisconnect();
                    disconnecting = true;
                }
                catch (Exception e) { Logger.ShowError(e, null); }
                try
                {
                    Logger.ShowInfo(sock.RemoteEndPoint.ToString() + " disconnected", null);
                }
                catch (Exception)
                {
                }
                try { stream.Close(); }
                catch (Exception) { }

                //try { sock.Disconnect(true); }
                try { sock.Close(); }
                catch (Exception) { }

                this.isDisconnected = true;

                
            }
            catch (Exception e)
            {
                Logger.ShowError(e, null); 
                try { stream.Close(); }
                catch (Exception) { }

                //try { sock.Disconnect(true); }
                try { sock.Close(); }
                catch (Exception) { }
                //Logger.ShowInfo(sock.RemoteEndPoint.ToString() + " disconnected", null);
            }
            //this.nlock.ReleaseWriterLock(); 
        }


        private void ReceiveSize(IAsyncResult ar)
        {
            this.nlock.AcquireWriterLock(Timeout.Infinite);
            try
            {
                if (this.isDisconnected)
                {
                    this.nlock.ReleaseWriterLock();
                    return;
                }
                
                if (buffer[0] == 0xFF && buffer[1] == 0xFF)
                {
                    // if the buffer is marked as "empty", there was an error during reading
                    // normally happens if the client disconnects
                    // note: this is required as sock.Connected still can be true, even the client
                    // is already disconnected
                    this.nlock.ReleaseWriterLock();
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();   
                    return;
                }

                if (!sock.Connected)
                {
                    this.nlock.ReleaseWriterLock();
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();   
                    return;
                }

                try { stream.EndRead(ar); }
                catch (Exception)
                {
                    this.nlock.ReleaseWriterLock();
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea(); 
                    return;
                }

                ushort size = BitConverter.ToUInt16(buffer, 0);

                if (size < 4)
                {
                   Logger.ShowWarning(sock.RemoteEndPoint.ToString() + " error: packet size is < 4",null);
                   /*try//this could crash the gateway somehow,so better ignore the Exception
                   {
                       ClientManager.EnterCriticalArea();
                       this.Disconnect();
                       ClientManager.LeaveCriticalArea();   
                   }
                   catch (Exception ex)
                   {
                       Logger.ShowError(ex, null);
                       //sock.Disconnect(true);
                       sock.Close();
                       ClientManager.LeaveCriticalArea(); 
                   }*/
                  
                    this.nlock.ReleaseWriterLock();                    
                    return;
                }

                if (sock.Available < (size - 2))
                {
                    Logger.ShowWarning(sock.RemoteEndPoint.ToString() + string.Format(" error: packet data is too short, should be {0:G}", size - 2), null);

                    /*try//this could crash the gateway somehow,so better ignore the Exception
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();                    
                    }
                    catch (Exception)
                    {
                        //sock.Disconnect(true);
                        sock.Close();
                        ClientManager.LeaveCriticalArea();     
                    }*/
                    //this.nlock.ReleaseWriterLock();
                    //return;
                }

                byte[] data = new byte[size];
                data[0] = buffer[0];
                data[1] = buffer[1];

                // mark buffer as "empty"
                buffer[0] = 0xFF;
                buffer[1] = 0xFF;

                // Receive the data from the packet and call the receivedata function
                // The packet is stored in AsyncState
                //Console.WriteLine("New packet with size " + p.size);
                try { this.nlock.ReleaseWriterLock(); stream.BeginRead(data, 2, size - 2, this.callbackData, data); }
                catch (Exception)
                {
                    //Logger.ShowError(ex, null);                    
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
            }
                        
            catch (Exception e) { Logger.ShowError(e, null); }
            
        }

        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                this.nlock.AcquireWriterLock(Timeout.Infinite);
                if (this.isDisconnected)
                {
                    this.nlock.ReleaseWriterLock();
                    return;
                }
                if (!sock.Connected)
                {
                    this.nlock.ReleaseWriterLock();
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                try { stream.EndRead(ar); }
                catch (Exception)
                {
                    this.nlock.ReleaseWriterLock();
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                byte[] raw = (byte[])ar.AsyncState;
                if (this.isGateway) raw = Encryption.Decrypt(raw, 2, this.ClientKey);
                if (!isGateway)
                {
                    ushort messageID = (ushort)(raw[7] + (raw[6] << 8));

                    if (!this.commandTable.ContainsKey((messageID)))
                    {
                        if (!this.fullHeader)
                        {
                            Logger.ShowWarning(string.Format("Got unknown packet {0:X} {1:X} from " + sock.RemoteEndPoint.ToString(), raw[6], raw[7]), null);
                            
                        }
                        else
                        {
                            if (commandTable.ContainsKey((ushort)0xFFFF))
                            {
                                if (this.commandTable[(ushort)0xFFFF].SizeIsOk((ushort)raw.Length))
                                {
                                    Packet p = this.commandTable[(ushort)0xFFFF].New();
                                    p.data = raw;
                                    p.size = (ushort)(raw.Length);

                                    ClientManager.EnterCriticalArea();
                                    try
                                    {
                                        p.Parse(this.client);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.ShowError(ex);
                                    }
                                    ClientManager.LeaveCriticalArea();                                    
                                }
                                else
                                {
                                    string error = "Invalid packet size from client " + sock.RemoteEndPoint.ToString();
                                    Console.WriteLine(error);
                                    Log.WriteLog(error);
                                    ClientManager.EnterCriticalArea();
                                    this.Disconnect();
                                    ClientManager.LeaveCriticalArea();
                                    return;
                                }
                            }
                            else
                            {
                                Logger.ShowWarning("Universal Packet 0xFFFF not defined!", null);
                            }
                        }
                    }
                    else
                    {
                        if (this.commandTable[messageID].SizeIsOk((ushort)raw.Length))
                        {
                            Packet p = this.commandTable[messageID].New();
                            p.data = raw;
                            p.size = (ushort)(raw.Length);
                            Client client;
                            if (p.SessionID != 0)
                            {
                                client = this.currentClientManager.GetClient(p.SessionID);
                                if (client == null) client = this.client;                                
                            }
                            else
                            {
                                client = this.client;
                            }
                            ClientManager.EnterCriticalArea();
                            try
                            {
                                if (client.netIO == null) client.netIO = this;
                                p.Parse(client);
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                            ClientManager.LeaveCriticalArea();
                        }
                        else
                        {
                            string error = string.Format("Invalid packet size(Packet:{0:X4}) from client {1}",messageID, sock.RemoteEndPoint.ToString());
                            Console.WriteLine(error);
                            Log.WriteLog(error);
                            this.nlock.ReleaseWriterLock();
                            ClientManager.EnterCriticalArea();
                            this.Disconnect();
                            ClientManager.LeaveCriticalArea();
                            return;
                        }
                    }
                }
                else
                {
                    ushort messageID;                    
                    if (!fullHeader)
                    {
                        messageID = (ushort)(raw[7] + (raw[6] << 8));

                        if (!this.commandTable.ContainsKey((messageID)))
                        {
                            Logger.ShowWarning(string.Format("Got unknown packet {0:X} {1:X} from " + sock.RemoteEndPoint.ToString(), raw[6], raw[7]), null);
                            
                        }
                        else
                        {
                            if (this.commandTable[messageID].SizeIsOk((ushort)raw.Length))
                            {
                                Packet p = this.commandTable[messageID].New();
                                p.data = raw;
                                p.size = (ushort)(raw.Length);
                                p.isGateway = this.isGateway;
                                ClientManager.EnterCriticalArea();
                                try
                                {
                                    p.Parse(this.client);
                                }
                                catch (Exception ex)
                                {
                                    Logger.ShowError(ex);
                                }
                                ClientManager.LeaveCriticalArea();
                            }
                            else
                            {
                                string error = "Invalid packet size from client " + sock.RemoteEndPoint.ToString();
                                Console.WriteLine(error);
                                Log.WriteLog(error);
                                this.nlock.ReleaseWriterLock();
                                ClientManager.EnterCriticalArea();
                                this.Disconnect();
                                ClientManager.LeaveCriticalArea();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (commandTable.ContainsKey((ushort)0xFFFF))
                        {
                            if (this.commandTable[(ushort)0xFFFF].SizeIsOk((ushort)raw.Length))
                            {
                                Packet p = this.commandTable[(ushort)0xFFFF].New();
                                p.data = raw;
                                p.size = (ushort)(raw.Length);
                                p.isGateway = this.isGateway;
                                ClientManager.EnterCriticalArea();
                                try
                                {
                                    p.Parse(this.client);
                                }
                                catch (Exception ex)
                                {
                                    Logger.ShowError(ex);
                                }
                                ClientManager.LeaveCriticalArea();
                            }
                            else
                            {
                                string error = "Invalid packet size from client " + sock.RemoteEndPoint.ToString();
                                Console.WriteLine(error);
                                Log.WriteLog(error);
                                this.nlock.ReleaseWriterLock();
                                ClientManager.EnterCriticalArea();
                                this.Disconnect();
                                ClientManager.LeaveCriticalArea();
                                return;
                            }
                        }
                        else
                        {
                            Logger.ShowWarning("Universal Packet 0xFFFF not defined!", null);
                        }
                    }
                }
                try { this.nlock.ReleaseWriterLock(); stream.BeginRead(buffer, 0, 2, this.callbackSize, null); }
                catch (Exception)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
            }
            catch (Exception e) { Logger.ShowError(e, null); }
        }

        /// <summary>
        /// Sends data, which is not yet encrypted, to the client.
        /// </summary>
        /// <param name="data">The unencrypted data</param>
        public void SendData(byte[] data, uint SessionID)
        {
                Packet p = new Packet(data);
                SendPacket(p, SessionID);
        }

        /// <summary>
        /// Sends a packet, which is not yet encrypted, to the client.
        /// </summary>
        /// <param name="p">The packet containing all info.</param>
        private void SendPacket(Packet p, uint SessionID, bool nosession)
        {
                p.SetLength();

                try
                {
                    if (this.isGateway)
                    {
                        byte[] data;
                        if (p.doNotEncryptBuffer) data = Encryption.Encrypt((byte[])p.data.Clone(), 2, this.serverKey);
                        else data = Encryption.Encrypt(p.data, 2, this.serverKey);
                        sock.BeginSend(data, 0, data.Length, SocketFlags.None, null, null);
                    }
                    else
                    {
                        if (!nosession)
                        {
                            byte[] data;
                            data = new byte[p.data.Length + 4];
                            Array.Copy(p.data, 2, data, 6, p.data.Length - 2);
                            p.data = data;
                            p.SessionID = SessionID;
                            p.SetLength();
                        }
                        sock.BeginSend(p.data, 0, p.data.Length, SocketFlags.None, null, null);
                        //sock.Send(p.data);
                    }

                }
                catch (Exception)
                {
                    this.Disconnect();
                }

        }

        public void SendPacket(Packet p, uint SessionID)
        {
            SendPacket(p, SessionID, false);
        }

        public void SendPacket(Packet p, bool nosession)
        {
            SendPacket(p, 0, true);
        }

    }
}


