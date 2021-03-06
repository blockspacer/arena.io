﻿using ExitGames.Diagnostics.Counter;
using ExitGames.Diagnostics.Monitoring;

namespace arena.serv.perfomance
{
    public static class Counter
    {
        [PublishCounter("IsMasterServer")]
        public static readonly NumericCounter IsMasterServer = new NumericCounter("IsMasterServer");

        [PublishCounter("ServerState")]
        public static readonly NumericCounter ServerState = new NumericCounter("ServerState");

        [PublishCounter("LoadLevel")]
        public static readonly NumericCounter LoadLevel = new NumericCounter("LoadLevel");

        [PublishCounter("CpuAvg")]
        public static readonly NumericCounter CpuAvg = new NumericCounter("CpuAvg");

        [PublishCounter("BusinessQueueAvg")]
        public static readonly NumericCounter BusinessQueueAvg = new NumericCounter("BusinessQueueAvg");

        [PublishCounter("EnetQueueAvg")]
        public static readonly NumericCounter EnetQueueAvg = new NumericCounter("EnetQueueAvg");

        [PublishCounter("BytesInAndOutAvg")]
        public static readonly NumericCounter BytesInAndOutAvg = new NumericCounter("BytesInAndOutAvg");

        [PublishCounter("TimeInServerInAndOutAvg")]
        public static readonly NumericCounter TimeInServerInAndOutAvg = new NumericCounter("TimeInServerInAndOutAvg");

        [PublishCounter("EnetThreadsProcessingAvg")]
        public static readonly NumericCounter EnetThreadsProcessingAvg = new NumericCounter("EnetThreadsProcessingAvg");

        // The number of disconnected TCP peers (per second) / number of total TCP peers
        [PublishCounter("TcpDisconnectRateAvg")]
        public static readonly NumericCounter TcpDisconnectRateAvg = new NumericCounter("TcpDisconnectRateAvg");

        // The number of disconnected UDP peers (per second) / number of total UDP peers
        [PublishCounter("UdpDisconnectRateAvg")]
        public static readonly NumericCounter UdpDisconnectRateAvg = new NumericCounter("UdpDisconnectRateAvg");

        [PublishCounter("PeerCount")]
        public static readonly NumericCounter PeerCount = new NumericCounter("PeerCount");
    }
}
