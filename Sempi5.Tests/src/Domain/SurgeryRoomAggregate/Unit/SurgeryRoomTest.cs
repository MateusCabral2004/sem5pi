using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.SurgeryRoomAggregate;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.Domain.SurgeryRoomAggregate.Unit;

public class SurgeryRoomTest
{

    [Fact]
    public void TestBuilderWithValidObjects()
    {
        var roomType = RoomTypeEnum.OPERATING_ROOM;
        var roomCapacity = new Mock<RoomCapacity>(1);
        var equipment = new List<string> { "equipment1", "equipment2" };
        var roomStatus = RoomStatusEnum.AVAILABLE;
        var maintenanceSlots = new List<string> { "slot1", "slot2" };
        
        var room = new SurgeryRoom(roomType, roomCapacity.Object, equipment, roomStatus, maintenanceSlots);
        
        Assert.NotNull(room);
    }
    
    [Theory]
    [InlineData(RoomTypeEnum.OPERATING_ROOM, 1, null, RoomStatusEnum.AVAILABLE, new string[] { "slot1", "slot2" })]
    [InlineData(RoomTypeEnum.OPERATING_ROOM, 1, new string[] { "equipment1", "equipment2" }, RoomStatusEnum.AVAILABLE, null)]
    public void TestBuilderWithNullObjects(RoomTypeEnum roomType, int roomCapacity, string[] equipment, RoomStatusEnum roomStatus, string[] maintenanceSlots)
    {
        var mockRoomCapacity = new Mock<RoomCapacity>(roomCapacity);
        
        Assert.Throws<ArgumentNullException>(() => new SurgeryRoom(roomType, mockRoomCapacity.Object, equipment.ToList(), roomStatus, maintenanceSlots.ToList()));
    }
    
    [Fact]
    public void TestBuilderWithNullRoomCapacity()
    {
        Assert.Throws<ArgumentNullException>(() => new SurgeryRoom(RoomTypeEnum.OPERATING_ROOM, null, new List<string> { "equipment1", "equipment2" }, RoomStatusEnum.AVAILABLE, new List<string> { "slot1", "slot2" }));
    }
}