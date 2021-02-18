using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ItVitae_RepairShop_project8.Models;

namespace ItVitae_RepairShop_project8.Scripts
{
    public static class ModelProcessor
    {
        //Old poop solution
        //public static PartModel ToModel(int Id, int PartId, string PartName, double PartPrice)
        //{
        //    return new PartModel { Id = Id, PartId = PartId, PartName = PartName, PartPrice = PartPrice };
        //}
        //public static EmployeeModel ToModel(int Id, int EmployeeId, string EmployeeEmailAddress, string EmployeeFirstName, string EmployeeLastName, double EmployeePay, int Auth)
        //{
        //    return new EmployeeModel { Id=Id, EmployeeId=EmployeeId, EmployeeEmailAddress = EmployeeEmailAddress, EmployeeFirstName=EmployeeFirstName,EmployeeLastName=EmployeeLastName,EmployeePay=EmployeePay,Auth=Auth };
        //}
        //public static ImageLinkModel ToModel(int Id, int ImageId, int OrderId )
        //{
        //    return new ImageLinkModel { Id=Id, ImageId=ImageId, OrderId=OrderId};
        //}
        //public static ImageModel ToModel(int Id, int ImageId, string ImageData)
        //{
        //    return new ImageModel { Id=Id, ImageId=ImageId, ImageData=ImageData };
        //}
        //public static OrderModel ToModel(int Id, int OrderId, int PartLinkId, int UserId, int EmployeeId, string OrderDiscription, int ImageID, double OrderPrice, int OrderStatus)
        //{
        //    return new OrderModel { Id=Id,OrderId=OrderId,PartLinkId=PartLinkId, UserId=UserId,EmployeeId=EmployeeId, OrderDiscription=OrderDiscription, ImageID=ImageID,OrderPrice=OrderPrice,OrderStatus=OrderStatus };
        //}
        //public static PartLinkModel ToModel(int Id, int PartId)
        //{
        //    return new PartLinkModel { Id=Id,PartId=PartId };
        //}
        //public static UserModel ToModel(int Id, int UserId, string UserName, string UserEmail, int Auth)
        //{
        //    return new UserModel { Id = Id, UserId = UserId, UserEmail = UserEmail, UserEmailConfirm = UserEmail, Auth = Auth, UserName = UserName };
        //}
    }
}