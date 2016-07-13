//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: ProtoFiles/common.proto
namespace proto_common
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Common")]
  public partial class Common : global::ProtoBuf.IExtensible
  {
    public Common() {}
    
    [global::ProtoBuf.ProtoContract(Name=@"CommonErrors")]
    public enum CommonErrors
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"CE_NO_ERROR", Value=0)]
      CE_NO_ERROR = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CE_ERROR", Value=-1)]
      CE_ERROR = -1
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
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
    private proto_common.Commands _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto_common.Commands type
    {
      get { return _type; }
      set { _type = value; }
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
    private proto_common.Commands _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto_common.Commands type
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
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Event")]
  public partial class Event : global::ProtoBuf.IExtensible
  {
    public Event() {}
    
    private proto_common.Events _type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto_common.Events type
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
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"Commands")]
    public enum Commands
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"UNKNOWN_CMD", Value=-1)]
      UNKNOWN_CMD = -1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"AUTH", Value=10)]
      AUTH = 10,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SIMULATE_BATTLE", Value=11)]
      SIMULATE_BATTLE = 11,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PING", Value=12)]
      PING = 12,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ADMIN_AUTH", Value=13)]
      ADMIN_AUTH = 13,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ENTER_AREA", Value=2001)]
      ENTER_AREA = 2001,
            
      [global::ProtoBuf.ProtoEnum(Name=@"DAMAGE", Value=2004)]
      DAMAGE = 2004,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_MOVE", Value=2005)]
      PLAYER_MOVE = 2005,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ATTACK", Value=2006)]
      ATTACK = 2006,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TURN", Value=2009)]
      TURN = 2009,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_UPGRADE", Value=2010)]
      PLAYER_UPGRADE = 2010,
            
      [global::ProtoBuf.ProtoEnum(Name=@"JOIN_GAME", Value=2011)]
      JOIN_GAME = 2011,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHANGE_NICKNAME", Value=2012)]
      CHANGE_NICKNAME = 2012,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FIND_ROOM", Value=2013)]
      FIND_ROOM = 2013,
            
      [global::ProtoBuf.ProtoEnum(Name=@"STAT_UPGRADE", Value=2014)]
      STAT_UPGRADE = 2014,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GRAB_POWERUP", Value=2015)]
      GRAB_POWERUP = 2015
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"Events")]
    public enum Events
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"UNKNOWN_EVT", Value=-1)]
      UNKNOWN_EVT = -1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BLOCK_APPEARED", Value=2001)]
      BLOCK_APPEARED = 2001,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UNIT_DIE", Value=2003)]
      UNIT_DIE = 2003,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UNIT_MOVE", Value=2004)]
      UNIT_MOVE = 2004,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_APPEARED", Value=2005)]
      PLAYER_APPEARED = 2005,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UNIT_ATTACK", Value=2006)]
      UNIT_ATTACK = 2006,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_DISCONNECTED", Value=2007)]
      PLAYER_DISCONNECTED = 2007,
            
      [global::ProtoBuf.ProtoEnum(Name=@"DAMAGE_DONE", Value=2008)]
      DAMAGE_DONE = 2008,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_TURN", Value=2009)]
      PLAYER_TURN = 2009,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_UPGRADE_CHANGES", Value=2010)]
      PLAYER_UPGRADE_CHANGES = 2010,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_STATUS_CHANGED", Value=2011)]
      PLAYER_STATUS_CHANGED = 2011,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_EXPERIENCE", Value=2012)]
      PLAYER_EXPERIENCE = 2012,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_LEVEL_UP", Value=2013)]
      PLAYER_LEVEL_UP = 2013,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PLAYER_SCORE", Value=2014)]
      PLAYER_SCORE = 2014,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GAME_FINISHED", Value=2015)]
      GAME_FINISHED = 2015,
            
      [global::ProtoBuf.ProtoEnum(Name=@"POWER_UP_APPEARED", Value=2016)]
      POWER_UP_APPEARED = 2016,
            
      [global::ProtoBuf.ProtoEnum(Name=@"POWER_UP_GRABBED", Value=2017)]
      POWER_UP_GRABBED = 2017
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"PacketFlags")]
    public enum PacketFlags
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"EVENT", Value=1)]
      EVENT = 1
    }
  
}