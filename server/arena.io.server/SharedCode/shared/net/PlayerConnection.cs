﻿using System;
using System.IO;
using System.Collections.Generic;

using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using ProtoBuf;
using ExitGames.Concurrency.Core;
using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;

using shared.net.interfaces;


namespace shared.net
{
    public class PlayerConnection : ClientPeer, IGameConnection
    {
        private RequestHandler controller_;
        private SendParameters defaultSendParams_;
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public PlayerConnection(InitRequest initRequest)
            :base(initRequest)
        {
            defaultSendParams_.ChannelId = 0;
            defaultSendParams_.Encrypted = false;
            defaultSendParams_.Flush = false;
            defaultSendParams_.Unreliable = false;
        }

        public void SetController(RequestHandler controller)
        {
            controller_ = controller;
            controller_.Connection = this;
        }

        public void Send(proto_common.Response response)
        {
            Send(response, defaultSendParams_);
        }

        public void Send(proto_common.Response response, SendParameters sendParameters)
        {
            if (!Connected)
                return;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, response);
                stream.Flush();
                stream.Position = 0;
                var parameters = new Dictionary<byte, object>();
                parameters[OperationParameters.ProtoData] = stream.ToArray();
                var opResponse = new OperationResponse
                {
                    OperationCode = OperationParameters.ProtoCmd,
                    ReturnCode = 0,
                    DebugMessage = "OK",
                    Parameters = parameters
                };

                var result = SendOperationResponse(opResponse, sendParameters);
                if (result != SendResult.Ok)
                {
                    log.Error("Response send failed with result code: " + result.ToString() + ". Response type " + response.type);
                }
                //Flush();
            }
        }

        public void Send(proto_common.Event evt)
        {
            Send(evt, defaultSendParams_);
        }

        public void Send(proto_common.Event evt, SendParameters sendParameters)
        {
            if (!Connected)
                return;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, evt);
                stream.Flush();
                stream.Position = 0;
                var parameters = new Dictionary<byte, object>();
                parameters[OperationParameters.ProtoData] = stream.ToArray();
                var evtData = new EventData
                {
                    Code = OperationParameters.ProtoCmd,
                    Parameters = parameters
                };

                var result = SendEvent(evtData, sendParameters);
                if (result != SendResult.Ok)
                {
                    log.Error("Event send failed with result code: " + result.ToString() + ". Event type " + evt.type);
                }
               // Flush();
            }
        }

#region ClientPeer implementation
        protected override void OnDisconnect(DisconnectReason disconnectCode, string reasonDetail)
        {
            controller_.HandleDisconnect();
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            proto_common.Request request;
            byte[] data = (byte[])operationRequest.Parameters[OperationParameters.ProtoData];
            using (var stream = new MemoryStream(data))
            {
                request = ProtoBuf.Serializer.Deserialize<proto_common.Request>(stream);
            }

            //check conditions on connection layer level
            try
            {
                controller_.HandleRequest(request);
            }
            catch (ApplicationException exc)
            {
                log.Error(exc.Message);
                //TODO disconnect after N bad packets?
                //Disconnect();
            }
        }
#endregion
    }
}
