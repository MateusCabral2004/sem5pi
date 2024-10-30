using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.SurgeryRoomAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.SurgeryRoomAggregate.Integration;

public class SurgeryRoomTest
{

    [Fact]
    public void TestBuilderWithValidObjects()
    {
        var roomType = RoomTypeEnum.OPERATING_ROOM;
        var roomCapacity = new RoomCapacity(1);
        var equipment = new List<string> { "equipment1", "equipment2" };
        var roomStatus = RoomStatusEnum.AVAILABLE;
        var maintenanceSlots = new List<string> { "slot1", "slot2" };
        
        var room = new SurgeryRoom(roomType, roomCapacity, equipment, roomStatus, maintenanceSlots);
        
        Assert.NotNull(room);
    }
    
    [Theory]
    [InlineData(RoomTypeEnum.OPERATING_ROOM,-1, new string[] { "equipment1", "equipment2" }, RoomStatusEnum.AVAILABLE, new string[] { "slot1", "slot2" })]
    [InlineData(RoomTypeEnum.OPERATING_ROOM, 0, new string[] { "equipment1", "equipment2" }, RoomStatusEnum.AVAILABLE, new string[] { "slot1", "slot2" })]
    [InlineData(RoomTypeEnum.OPERATING_ROOM, 1, null, RoomStatusEnum.AVAILABLE, new string[] { "slot1", "slot2" })]
    [InlineData(RoomTypeEnum.OPERATING_ROOM, 1, new string[] { "equipment1", "equipment2" }, RoomStatusEnum.AVAILABLE, null)]
    public void TestBuilderWithNullObjects(RoomTypeEnum roomType, int roomCapacity, string[] equipment, RoomStatusEnum roomStatus, string[] maintenanceSlots)
    {
        if (roomCapacity <= 0)
        {
            Assert.Throws<ArgumentException>(() => new RoomCapacity(roomCapacity));
        }
        else
        {
            var roomCapacityObj = new RoomCapacity(roomCapacity);
            Assert.Throws<ArgumentNullException>(() => new SurgeryRoom(roomType, roomCapacityObj, equipment.ToList(), roomStatus, maintenanceSlots.ToList()));
        } 
    }
}