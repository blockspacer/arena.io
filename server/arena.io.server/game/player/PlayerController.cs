﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using ExitGames.Concurrency.Fibers;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;

using shared.net;
using shared.net.interfaces;
using shared.factories;
using shared.account;
using shared.helpers;
using shared.database;

using proto_game;
using proto_profile;

using Request = proto_common.Request;
using Commands = proto_common.Commands;

namespace arena.net
{
    using OperationHandler = OperationHandler<proto_common.Request>;

    class PlayerController : RequestHandler, IActionInvoker
    {
        private static ILogger log = LogManager.GetCurrentClassLogger();
 
        private battle.Player player_;
        private PoolFiber fiber_ = new PoolFiber(new DebugExecutor());
        private bool loginInProcess_ = false;
        private AuthEntry currentAuthEntry_ = null;

        public PlayerController()
        {
            AddOperationHandler(Commands.AUTH, new OperationHandler(HandleAuth));
            AddOperationHandler(Commands.PING, new OperationHandler(HandlePing));
            AddOperationHandler(Commands.LEAVE_GAME, new OperationHandler(HandleLeaveGame));
            AddOperationHandler(Commands.ADMIN_AUTH, new OperationHandler(HandleAdminAuth));
            AddOperationHandler(Commands.STAT_UPGRADE, new OperationHandler(HandleStatUpgrade));
            AddOperationHandler(Commands.PLAYER_INPUT, new OperationHandler(HandleUserInput));
            AddOperationHandler(Commands.SYNC_TICK, new OperationHandler(HandleSyncTick));
            AddOperationHandler(Commands.DAMAGE_APPLY, new OperationHandler(HandleDamageApply));
            AddOperationHandler(Commands.DOWNLOAD_MAP, new OperationHandler(HandleDownloadMap));
            AddOperationHandler(Commands.JOIN_SERVER, new OperationHandler(HandleJoinServer));
            AddOperationHandler(Commands.JOIN_GAME, new OperationHandler(HandleJoinGame));

            fiber_.Start();
        }

        protected override IActionInvoker GetActionInvoker()
        {
            if (player_ != null && player_.Game != null)
            {
                return player_.Game;
            }

            return this;
        }

        public void SendEvent(battle.Net.EventPacket eventPacket)
        {
            if (eventPacket.IsValid)
                SendEvent(eventPacket.EventID, eventPacket.Packet);
        }

        public override void HandleDisconnect()
        {
            base.HandleDisconnect();

            LeaveGame();

            RemoveState(ClientState.InBattle);
            fiber_.Stop();
        }

        public void OnGameFinished()
        {
            RemoveState(ClientState.InBattle);
        }

        public void SaveToDB()
        {
            /*database.Database.Instance.GetPLayerDB().SaveProfile(player_.Profile, (QueryResult result) =>
            {
                if (result == QueryResult.Fail)
                {
                    log.ErrorFormat("Failed saving of profile with id {0}", player_.Profile.UniqueID);
                }
            });*/
        }

#region Request Handlers
        private void HandleStatUpgrade(proto_common.Request request)
        {
            if (player_.UpgradePointsLeft <= 0)   
                return;
            var statReq = request.Extract<proto_game.StatUpgrade.Request>(proto_common.Commands.STAT_UPGRADE);
            var stat = player_.Stats.Get(statReq.stat);
            var response = new proto_game.StatUpgrade.Response();
            int error = 0;
            if (stat.Steps == 8)
            {
                error = 1;
            }
            else
            {
                stat.IncreaseByStep();
            }
            player_.UpgradePointsLeft--;
            SendResponse(request, response, error);
        }

        

        private void HandleAuth(proto_common.Request request)
        {
            if (loginInProcess_)
            {
                return;
            }

            var authReq =
                ProtoBuf.Extensible.GetValue<proto_auth.Auth.Request>(request, (int)proto_common.Commands.AUTH);

            if (authReq.m_oauth.uid.Trim() == "")
            {
                var response = new proto_auth.Auth.Response();
                SendResponse(proto_common.Commands.AUTH, response, request.id, -1); 
            }
            else
            {
                var authEntry = new AuthEntry();
                authEntry.authUserID = authReq.m_oauth.uid;
                currentAuthEntry_ = authEntry;

                // database.Database.Instance.GetAuthDB().LoginUser(authEntry, HandleDBLogin);
                loginInProcess_ = true;
            }
        }

        private void HandleDBLogin(QueryResult result, IDataReader data)
        {
            if (data.Read())
            {
                LoadUserFromDB(data);
            }
            else
            {
                Connection.Disconnect();
            }
        }

        private void LoadUserFromDB(IDataReader data)
        {
            var authRes = new proto_auth.ConnectToLobby.Response();
            loginInProcess_ = false;

            var profile = new Profile(data);
            player_ = new battle.Player(this, profile);

            var info = player_.Profile.GetInfoPacket();
            authRes.info = info;

            foreach (var entry in PlayerClassFactory.Instance.GetAllClasses())
            {
                proto_profile.ClassInfo clInfo = new proto_profile.ClassInfo();
                clInfo.@class = entry.Value.Class;
                info.classesInfo.Add(clInfo);
            }

            ResetState(ClientState.Logged);
            SendResponse(proto_common.Commands.AUTH, authRes);
        }

        private void HandleCreateUser(QueryResult result, IDataReader data)
        {
            if (data.Read())
            {
                LoadUserFromDB(data);
            }
            else
            {
                //something gone wrong
                SendResponse(proto_common.Commands.AUTH, null, 0, (int)proto_common.Common.CommonErrors.CE_ERROR);
            }
        }

        private void HandleJoinServer(Request request)
        {
            var joinReq = request.Extract<JoinServer.Request>(Commands.JOIN_SERVER);
            
        }

        private void HandleJoinGame(Request request)
        {
            var joinReq = request.Extract<proto_game.JoinGame.Request>(proto_common.Commands.JOIN_GAME);

            player_.Game.PlayerJoin(player_);

            RemoveState(ClientState.SwitchGameServer);
            SetState(ClientState.InBattle);
        }

        private void HandleSyncTick(proto_common.Request request)
        {
            var req = request.Extract<proto_game.SyncTick.Request>(proto_common.Commands.SYNC_TICK);

            var syncPacket = new proto_game.SyncTick.Response();
            syncPacket.tick = player_.Game.Tick;
            SendResponse(request, syncPacket);
        }

        private void HandlePing(proto_common.Request request)
        {
            var ping =
               ProtoBuf.Extensible.GetValue<proto_game.Ping.Request>(request, (int)proto_common.Commands.PING);

            var pong = new proto_game.Ping.Response();
            pong.timestamp = CurrentTime.Instance.CurrentTimeInMs;

            SendResponse(request, pong);

            if (player_ != null)
            {
                player_.Ping = ping.current_ping;
            }
        }

        private void LeaveGame()
        {
            if (player_ != null)
            {
                RemoveState(ClientState.InBattle);
            }
        }

        private void HandleLeaveGame(proto_common.Request request) 
        {
            LeaveGame();
            SendResponse(request, request.Extract<proto_game.LeaveGame>(proto_common.Commands.LEAVE_GAME));
        }

        private void HandleUserInput(proto_common.Request request)
        {
            var req = request.Extract<proto_game.PlayerInput.Request>(proto_common.Commands.PLAYER_INPUT);
            //we use request id as way to syncronize out attack, because client's ticks is unreliable to be used as unique id
            player_.AddInput(req, request.id);
        }

        private void HandleDamageApply(proto_common.Request request)
        {
            var req = request.Extract<proto_game.DamageApply.Request>(proto_common.Commands.DAMAGE_APPLY);
            player_.Game.DamageApply(player_, req);
        }

        private void HandleDownloadMap(proto_common.Request request)
        {
            var req = request.Extract<proto_game.DownloadMap.Request>(proto_common.Commands.DOWNLOAD_MAP);
            var res = new proto_game.DownloadMap.Response();
            res.map = player_.Game.Map.Serialize();
            SendResponse(request, res);
        }

#endregion

#region Admin Handlers
        private void HandleAdminAuth(proto_common.Request request)
        {
            if (loginInProcess_)
            {
                return;
            }

            var authReq =
                ProtoBuf.Extensible.GetValue<proto_auth.AdminAuth.Request>(request, (int)proto_common.Commands.ADMIN_AUTH);

            var authEntry = new AuthEntry();
            authEntry.authUserID = authReq.name;
            currentAuthEntry_ = authEntry;

            Database.Instance.GetAuthDB().LoginUserByNickname(authReq.name, HandleAdminDBLogin);
            loginInProcess_ = true;
        }

        private void HandleAdminDBLogin(QueryResult result, IDataReader data)
        {
            if (data.Read())
            {
                LoadUserFromDB(data);
            }
            else
            {
                //create user if no one found
                Database.Instance.GetAuthDB().CreateUserWithNickname(currentAuthEntry_.authUserID, HandleCreateUser);
            }
        }
#endregion

        void IActionInvoker.Execute(Action action)
        {
            fiber_.Enqueue(action);
        }
    }
}
