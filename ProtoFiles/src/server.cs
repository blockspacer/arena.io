//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: ProtoFiles/server.proto
namespace proto_server
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Request")]
  public partial class Request : global::ProtoBuf.IExtensible
  {
    public Request() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private proto_server.Commands _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto_server.Commands type
    {
      get { return _type; }
      set { _type = value; }
    }
    private proto_server.RegisterGameNode.Request _register_game_node = null;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"register_game_node", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto_server.RegisterGameNode.Request register_game_node
    {
      get { return _register_game_node; }
      set { _register_game_node = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Response")]
  public partial class Response : global::ProtoBuf.IExtensible
  {
    public Response() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private proto_server.Commands _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto_server.Commands type
    {
      get { return _type; }
      set { _type = value; }
    }
    private int _error;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"error", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int error
    {
      get { return _error; }
      set { _error = value; }
    }
    private long _timestamp;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"timestamp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long timestamp
    {
      get { return _timestamp; }
      set { _timestamp = value; }
    }
    private proto_server.RegisterGameNode.Response _register_game_node = null;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"register_game_node", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto_server.RegisterGameNode.Response register_game_node
    {
      get { return _register_game_node; }
      set { _register_game_node = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Event")]
  public partial class Event : global::ProtoBuf.IExtensible
  {
    public Event() {}
    
    private proto_server.Events _type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto_server.Events type
    {
      get { return _type; }
      set { _type = value; }
    }
    private long _timestamp;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"timestamp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long timestamp
    {
      get { return _timestamp; }
      set { _timestamp = value; }
    }
    private proto_server.GameNodeStatus _game_node_status = null;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"game_node_status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto_server.GameNodeStatus game_node_status
    {
      get { return _game_node_status; }
      set { _game_node_status = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RegisterGameNode")]
  public partial class RegisterGameNode : global::ProtoBuf.IExtensible
  {
    public RegisterGameNode() {}
    
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Request")]
  public partial class Request : global::ProtoBuf.IExtensible
  {
    public Request() {}
    
    private string _ip;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"ip", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string ip
    {
      get { return _ip; }
      set { _ip = value; }
    }
    private proto_server.RegisterGameNode.Request _register_game_node = null;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"register_game_node", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto_server.RegisterGameNode.Request register_game_node
    {
      get { return _register_game_node; }
      set { _register_game_node = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Response")]
  public partial class Response : global::ProtoBuf.IExtensible
  {
    public Response() {}
    
    private proto_server.RegisterGameNode.Response _register_game_node = null;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"register_game_node", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto_server.RegisterGameNode.Response register_game_node
    {
      get { return _register_game_node; }
      set { _register_game_node = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameNodeStatus")]
  public partial class GameNodeStatus : global::ProtoBuf.IExtensible
  {
    public GameNodeStatus() {}
    
    private proto_server.FeedbackLevel _workload_level;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"workload_level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto_server.FeedbackLevel workload_level
    {
      get { return _workload_level; }
      set { _workload_level = value; }
    }
    private int _active_games;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"active_games", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int active_games
    {
      get { return _active_games; }
      set { _active_games = value; }
    }
    private int _players_connected;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"players_connected", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int players_connected
    {
      get { return _players_connected; }
      set { _players_connected = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"Commands")]
    public enum Commands
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"REGISTER_GAME_NODE", Value=11)]
      REGISTER_GAME_NODE = 11
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"Events")]
    public enum Events
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"NODE_STATUS", Value=11)]
      NODE_STATUS = 11
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"FeedbackLevel")]
    public enum FeedbackLevel
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"Highest", Value=4)]
      Highest = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"High", Value=3)]
      High = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Normal", Value=2)]
      Normal = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Low", Value=1)]
      Low = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Lowest", Value=0)]
      Lowest = 0
    }
  
}