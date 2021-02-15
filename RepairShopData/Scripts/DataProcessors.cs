using ItVitae_RepairShop_project8.RepairShopData.Access;
using ItVitae_RepairShop_project8.RepairShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ItVitae_RepairShop_project8.RepairShopData.Scripts
{
    public static class DataProcessors
    {
        public static int Create<T>(T Data, string DatabaseTable, bool autoIncrease = true, string IdString = "Id")
        {
            string databaseString = "insert into dbo." + DatabaseTable + " ";
            Type t = Data.GetType();
            PropertyInfo[] props = t.GetProperties();

            if (autoIncrease)
            {
                var pid = props.First(p => p.Name == IdString);

                pid.SetValue(Data, Count(Data, DatabaseTable));
            }

            databaseString += "(";
            for (int i = 0; i < props.Length; i++)
            {
                databaseString += props[i].Name;
                if (i < props.Length - 1)
                    databaseString += ", ";
            }

            databaseString += ") values (";
            for (int i = 0; i < props.Length; i++)
            {
                databaseString += "'";
                databaseString += props[i].GetValue(Data);
                databaseString += "'";
                if (i < props.Length - 1)
                    databaseString += ", ";
            }
            databaseString += ")";

            return SqlDataAccess.ExecuteDataString(databaseString);
        }

        public static int Update<T>(T Data, string DatabaseTable, string wherePropertie)
        {
            string databaseString = "update dbo." + DatabaseTable + " set ";
            Type t = Data.GetType();
            PropertyInfo[] props = t.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                databaseString += props[i].Name + "=" + props[i].GetValue(Data);
                if (i < props.Length - 1)
                    databaseString += ", ";
            }

            PropertyInfo whereCase = props.First(p => p.Name == wherePropertie);

            if (whereCase != null)
                databaseString += " where " + wherePropertie + "=" + whereCase.GetValue(Data);

            return SqlDataAccess.ExecuteDataString(databaseString);
        }

        public static int Delete<T>(string DatabaseTable, string wherePropertie, string whereValue)
        {
            string databaseString = "delete from dbo." + DatabaseTable;
            databaseString += " where " + wherePropertie + "=" + whereValue;

            return SqlDataAccess.ExecuteDataString(databaseString);
        }

        public static List<T> ListAll<T>(T Data, string DatabaseTable)
        {
            string databaseString = "select ";
            Type t = Data.GetType();
            PropertyInfo[] props = t.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                databaseString += props[i].Name;
                if (i < props.Length - 1)
                    databaseString += ", ";
            }

            databaseString += " from dbo." + DatabaseTable;


            return SqlDataAccess.LoadData<T>(databaseString);
        }

        public static int Count<T>(T Data, string DatabaseTable)
        {
            return ListAll<T>(Data, DatabaseTable).Count();
        }

        public static T ListOne<T>(T Data, string DatabaseTable, string wherePropertie)
        {
            string databaseString = "select ";
            Type t = Data.GetType();
            PropertyInfo[] props = t.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                databaseString += props[i].Name;
                if (i < props.Length - 1)
                    databaseString += ", ";
            }

            databaseString += " from dbo." + DatabaseTable;

            PropertyInfo whereCase = props.First(p => p.Name == wherePropertie);

            if (whereCase != null)
                databaseString += " where " + wherePropertie + "=" + whereCase.GetValue(Data);

            return SqlDataAccess.LoadData<T>(databaseString)[0];
        }
    }

    public static class PartProcessor
    {

        public static int Create(int PartId, string PartName, double PartPrice)
        {
            return SqlDataAccess.SaveData(
                            @"insert into dbo.ComputerParts (Id, PartId, PartName, PartPrice)
                            values (@Id, @PartId, @PartName, @PartPrice)",
                            new PartModel
                            {
                                Id = Load().Count,
                                PartId = PartId,
                                PartName = PartName,
                                PartPrice = PartPrice
                            });
        }
        public static int Update(int Id, int PartId, string PartName, double PartPrice)
        {
            return SqlDataAccess.UpdateData(
                            @"Update dbo.ComputerParts 
                            SET PartId=@PartId, PartName=@PartName, PartPrice=@PartPrice
                            where Id=@Id",
                            new PartModel
                            {
                                PartId = PartId,
                                PartName = PartName,
                                PartPrice = PartPrice
                            });
        }
        public static List<PartModel> Load()
        {
            return SqlDataAccess.LoadData<PartModel>(
                            @"select Id, PartId, PartName, PartPrice
                            from dbo.ComputerParts");
        }
    }
    public static class EmployeeProcessor
    {
        public static int Create(int EmployeeId, string EmployeeEmailAddress, string EmployeeFirstName, string EmployeeLastName, double EmployeePay, int Auth)
        {
            return SqlDataAccess.SaveData(
                            @"insert into dbo.Employees (@Id, EmployeeId, EmployeeEmailAddress, EmployeeFirstName, EmployeeLastName, EmployeePay, Auth)
                            values @Id, (@EmployeeId, @EmployeeEmailAddress, @EmployeeFirstName, @EmployeeLastName, @EmployeePay, @Auth)",
                            new EmployeeModel
                            {
                                Id = Load().Count,
                                EmployeeId = EmployeeId,
                                EmployeeEmailAddress = EmployeeEmailAddress,
                                EmployeeFirstName = EmployeeFirstName,
                                EmployeeLastName = EmployeeLastName,
                                EmployeePay = EmployeePay,
                                Auth = Auth
                            });
        }
        public static List<EmployeeModel> Load()
        {
            return SqlDataAccess.LoadData<EmployeeModel>(
                            @"select Id, EmployeeId, EmployeeEmailAddress, EmployeeFirstName, EmployeeLastName, EmployeePay, Auth
                            from dbo.Employees");
        }
        public static int Update(int Id, int EmployeeId, string EmployeeEmailAddress, string EmployeeFirstName, string EmployeeLastName, double EmployeePay, int Auth)
        {
            return 0;
        }
    }
    public static class ImageProcessor
    {
        public static int Create(int Id, int ImageId, string ImageData)
        {
            return SqlDataAccess.SaveData(
                            @"insert into dbo.Images (Id, ImageId, ImageData)
                            values (@Id, @ImageId, @ImageData)",
                            new ImageModel
                            {
                                Id = Load().Count,
                                ImageId = ImageId,
                                ImageData = ImageData
                            });
        }
        public static List<PartModel> Load()
        {
            return SqlDataAccess.LoadData<PartModel>(
                            @"select PartId, PartName, PartPrice
                            from dbo.ComputerParts");
        }
    }
    public static class ImageLinkProcessor
    {
        public static int Create(int Id, int ImageId, int OrderId)
        {
            return SqlDataAccess.SaveData(
                            @"insert into dbo.ImageLinks (Id, ImageId, OrderId)
                            values (@Id, @ImageId, OrderId)",
                            new ImageLinkModel
                            {
                                Id = Load().Count,
                                ImageId = ImageId,
                                OrderId = OrderId
                            });
        }
        public static int Update(int Id, int ImageId, int OrderId)
        {
            return SqlDataAccess.UpdateData(
                            @"Update dbo.ImageLinks 
                            SET ImageId=@ImageId, OrderId=OrderId
                            where Id=@Id",
                            new ImageLinkModel
                            {
                                ImageId = ImageId,
                                OrderId = OrderId
                            });
        }
        public static List<ImageLinkModel> Load()
        {
            return SqlDataAccess.LoadData<ImageLinkModel>(
                            @"select Id, ImageId, OrderId
                            from dbo.ImageLinks");
        }
        public static int Remove(int Id)
        {
            return SqlDataAccess.DeleteData<ImageLinkModel>(
                            @"delete from dbo.ImageLinks
                            where Id=@Id",
                            new ImageLinkModel
                            {
                                Id = Id,
                            });
        }
    }
    public static class OrderProcessor
    {
        public static int Create(int PartLinkId, int UserId, int EmployeeId, string OrderDiscription, int ImageID, double OrderPrice, int OrderStatus)
        {
            return SqlDataAccess.SaveData(
                            @"insert into dbo.Orders (Id, PartLinkId, UserId, EmployeeId, OrderDiscription, ImageID, OrderPrice, OrderStatus)
                            values (@Id, @PartLinkId, @UserId, @EmployeeId, @OrderDiscription, @ImageID, @OrderPrice, @OrderStatus)",
                            new OrderModel
                            {
                                OrderId = Load().Count,
                                PartLinkId = PartLinkId,
                                UserId = UserId,
                                EmployeeId = EmployeeId,
                                OrderDiscription = OrderDiscription,
                                ImageID = ImageID,
                                OrderPrice = OrderPrice,
                                OrderStatus = OrderStatus
                            });
        }
        public static int Update(int Id, int OrderId, int PartLinkId, int UserId, int EmployeeId, string OrderDiscription, int ImageID, double OrderPrice, int OrderStatus)
        {
            return SqlDataAccess.UpdateData(
                            @"Update dbo.Orders 
                            SET PartLinkId=@PartLinkId, UserId=@UserId, EmployeeId=@EmployeeId, OrderDiscription=@OrderDiscription, ImageID=@ImageID, OrderPrice=@OrderPrice, OrderStatus=@OrderStatus 
                            where Id=@Id",
                            new OrderModel
                            {
                                OrderId = OrderId,
                                PartLinkId = PartLinkId,
                                UserId = UserId,
                                EmployeeId = EmployeeId,
                                OrderDiscription = OrderDiscription,
                                ImageID = ImageID,
                                OrderPrice = OrderPrice,
                                OrderStatus = OrderStatus
                            });
        }
        public static List<OrderModel> Load()
        {
            return SqlDataAccess.LoadData<OrderModel>(
                            @"select Id, OrderId, PartLinkId, UserId, EmployeeId, OrderDiscription, ImageID, OrderPrice, OrderStatus
                            from dbo.Orders");
        }
    }
    public static class PartLinkProcessor
    {
        public static int Create(int Id, int PartId)
        {
            return SqlDataAccess.SaveData(
                            @"insert into dbo.PartLinks (Id, PartId)
                            values (@Id, @PartId)",
                            new PartLinkModel
                            {
                                Id = Load().Count,
                                PartId = PartId,
                            });
        }
        public static int Update(int Id, int PartId)
        {
            return SqlDataAccess.UpdateData(
                            @"Update dbo.PartLinks 
                            SET PartId=@PartId
                            where Id=@Id",
                            new PartLinkModel
                            {
                                PartId = PartId,
                            });
        }
        public static List<PartLinkModel> Load()
        {
            return SqlDataAccess.LoadData<PartLinkModel>(
                            @"select Id, PartId
                            from dbo.PartLinks");
        }
        public static int Remove(int Id)
        {
            return SqlDataAccess.DeleteData<PartLinkModel>(
                            @"delete from dbo.PartLinks
                            where Id=@Id",
                            new PartLinkModel
                            {
                                Id = Id,
                            });
        }
    }
    public static class UserProcessor
    {
        public static int Create(int UserId, string UserName, string UserEmail, int Auth, bool register = false)
        {
            //Do something smart with registered boolean
            return SqlDataAccess.SaveData(
                            @"insert into dbo.Users (Id, UserId, UserName, UserEmail, Auth)
                            values (@Id, @UserId, @UserName, @UserEmail, @Auth)",
                            new UserModel
                            {
                                Id = Load().Count,
                                UserId = UserId,
                                UserName = UserName,
                                UserEmail = UserEmail,
                                Auth = Auth
                            });
        }
        public static int Update(int Id, int UserId, string UserName, string UserEmail, int Auth, bool register = false)
        {
            //Do something smart with registered boolean
            return SqlDataAccess.UpdateData(
                            @"Update dbo.Users 
                            SET UserId=@UserId, UserName=@UserName, UserEmail=@UserEmail, Auth=@Auth
                            where Id=@Id",
                            new UserModel
                            {
                                UserId = UserId,
                                UserName = UserName,
                                UserEmail = UserEmail,
                                Auth = Auth,
                            });
        }
        public static List<UserModel> Load()
        {
            return SqlDataAccess.LoadData<UserModel>(
                            @"select Id, UserId, UserName, UserEmail, Auth
                            from dbo.Users");
        }
        public static int Remove(int Id)
        {
            return SqlDataAccess.DeleteData<UserModel>(
                            @"delete from dbo.Users
                            where Id=@Id",
                            new UserModel
                            {
                                Id = Id,
                            });
        }
    }
}
