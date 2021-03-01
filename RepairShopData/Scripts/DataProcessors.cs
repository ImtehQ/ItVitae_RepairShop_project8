using ItVitae_RepairShop_project8.RepairShopData.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ItVitae_RepairShop_project8.RepairShopData.Scripts
{

    public static class DataProcessors
    {
        /// <summary>
        /// Creates a table enterie of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="DatabaseTable"></param>
        /// <param name="autoIncrease"></param>
        /// <param name="IdString"></param>
        /// <returns></returns>
        public static int Create<T>(T Data, string DatabaseTable, bool autoIncrease = true, List<string> excludeStrings = null, string IdString = "Id")
        {
            string databaseString = $"insert into dbo.{DatabaseTable} ";
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
                if (excludeStrings != null)
                {
                    if (excludeStrings.Contains(props[i].Name)) { continue; }
                }

                databaseString += props[i].Name;
                if (i < props.Length - 1)
                    databaseString += ", ";
            }

            databaseString += ") values (";
            for (int i = 0; i < props.Length; i++)
            {
                if (excludeStrings != null)
                {
                    if (excludeStrings.Contains(props[i].Name)) { continue; }
                }

                databaseString += $"'{props[i].GetValue(Data)}'";

                if (i < props.Length - 1)
                    databaseString += ", ";
            }
            databaseString += ")";

            return SqlDataAccess.ExecuteDataString(databaseString);
        }

        /// <summary>
        /// Update single enterie in the table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="DatabaseTable"></param>
        /// <param name="wherePropertie"></param>
        /// <param name="excludeString"></param>
        /// <returns></returns>
        public static int Update<T>(T Data, string DatabaseTable, List<string> excludeStrings = null, string wherePropertie = "Id", bool excludeDefaultString = true)
        {
            if(excludeDefaultString)
                if(excludeStrings != null)
                    excludeStrings.Add("Id");

            string databaseString = $"update dbo.{DatabaseTable} set ";
            Type t = Data.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                if (excludeStrings != null)
                {
                    if (excludeStrings.Contains(props[i].Name)) { continue; }
                }

                if (props[i].PropertyType.Name == "String")
                {
                    if (props[i].GetValue(Data) != null && props[i].GetValue(Data).ToString().Length > 0)
                    {
                        databaseString += $"{props[i].Name}='{props[i].GetValue(Data)}'";
                    }
                    else
                    {
                        databaseString += $"{props[i].Name}=''";
                    }
                }
                else
                    databaseString += $"{props[i].Name}={props[i].GetValue(Data)}";

                if (i < props.Length - 1)
                    databaseString += ", ";
            }

            PropertyInfo whereCase = props.First(p => p.Name == wherePropertie);

            if (databaseString.Substring(databaseString.Length - 2, 1) == ",")
                databaseString = databaseString.Substring(0, databaseString.Length-2);

            if (whereCase != null)
            {
                databaseString += $" where {wherePropertie} = {whereCase.GetValue(Data)}";
            }

            return SqlDataAccess.ExecuteDataString(databaseString);
        }

        /// <summary>
        /// Deletes from table where case
        /// </summary>
        /// <param name="DatabaseTable"></param>
        /// <param name="whereValue"></param>
        /// <param name="wherePropertie"></param>
        /// <returns></returns>
        public static int Delete(string DatabaseTable, string whereValue, string wherePropertie = "Id")
        {
            string databaseString = "delete from dbo." + DatabaseTable;
            databaseString += " where " + wherePropertie + "=" + whereValue;

            return SqlDataAccess.ExecuteDataString(databaseString);
        }

        /// <summary>
        /// Returns everything out of the database from this table
        /// Include type and it will auto convert.
        /// doing it wrong and breaking it is your own fault
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DatabaseTable"></param>
        /// <returns></returns>
        public static List<T> ListEverything<T>(string DatabaseTable)
        {
            return SqlDataAccess.LoadData<T>($"select * from dbo.{DatabaseTable}");
        }

        /// <summary>
        /// Returns list of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="DatabaseTable"></param>
        /// <returns></returns>
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

            databaseString += $" from dbo.{DatabaseTable}";


            return SqlDataAccess.LoadData<T>(databaseString);
        }

        /// <summary>
        /// Counts stuffs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="DatabaseTable"></param>
        /// <returns></returns>
        public static int Count<T>(T Data, string DatabaseTable)
        {
            return ListAll<T>(Data, DatabaseTable).Count();
        }

        /// <summary>
        /// Loads 1 of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="DatabaseTable"></param>
        /// <param name="wherePropertie"></param>
        /// <returns></returns>
        public static T Details<T>(T Data, string DatabaseTable, string wherePropertie = "Id")
        {
            string databaseString = "select ";
            Type t = Data.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                if (wherePropertie == "Id" && props[i].Name == "Id")
                    return OneById<T>((int)props[i].GetValue(Data), DatabaseTable);

                databaseString += props[i].Name;
                if (i < props.Length - 1)
                    databaseString += ", ";
            }

            databaseString += $" from dbo.{DatabaseTable}";

            PropertyInfo whereCase = props.First(p => p.Name == wherePropertie);

            if (whereCase != null)
                databaseString += $" where {wherePropertie}={whereCase.GetValue(Data)}";

            return SqlDataAccess.LoadData<T>(databaseString).FirstOrDefault();
        }

        /// <summary>
        /// Load 1 of type T by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <param name="DatabaseTable"></param>
        /// <param name="wherePropertie"></param>
        /// <returns></returns>
        public static T OneById<T>(int Id, string DatabaseTable, string wherePropertie = "Id")
        {
            return SqlDataAccess.LoadData<T>($"select * from dbo.{DatabaseTable} where {wherePropertie}={Id}").FirstOrDefault();
        }


        /// <summary>
        /// Converts type From(F) => type To(T)
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static void ConvertFromTo<F,T>(F From, T To)
        {
            Type Tt = To.GetType();
            PropertyInfo[] Tprops = Tt.GetProperties();

            Type Ft = To.GetType();
            PropertyInfo[] Fprops = Ft.GetProperties();

            foreach (PropertyInfo Fprop in Fprops)
            {
                foreach (PropertyInfo Tprop in Tprops)
                {
                    if (Fprop.Name == Tprop.Name)
                    {
                        Tprop.SetValue(Tt,
                            Fprop.GetValue(Ft));
                    }
                }
            }
        }
        public static T ConvertFromToReturn<F, T>(F From, T To)
        {
            Type Tt = To.GetType();
            PropertyInfo[] Tprops = Tt.GetProperties();

            Type Ft = From.GetType();
            PropertyInfo[] Fprops = Ft.GetProperties();

            foreach (PropertyInfo Fprop in Fprops)
            {
                foreach (PropertyInfo Tprop in Tprops)
                {
                    if (Fprop.Name == Tprop.Name)
                    {
                        Tprop.SetValue(Tt,
                            Fprop.GetValue(Ft));
                    }
                }
            }

            return To;
        }

        public static List<T> ConvertFromToReturn<F, T>(this List<F> From, T ToType)
        {
            List<T> ts = new List<T>();
            foreach (var item in From)
            {
                ts.Add(ConvertFromToReturn<F, T>(item, ToType));
            }
            return ts;
        }
    }

    //public static class PartProcessor
    //{

    //    public static int Create(int PartId, string PartName, double PartPrice)
    //    {
    //        return SqlDataAccess.SaveData(
    //                        @"insert into dbo.ComputerParts (Id, PartId, PartName, PartPrice)
    //                        values (@Id, @PartId, @PartName, @PartPrice)",
    //                        new PartModel
    //                        {
    //                            Id = Load().Count,
    //                            PartId = PartId,
    //                            PartName = PartName,
    //                            PartPrice = PartPrice
    //                        });
    //    }
    //    public static int Update(int Id, int PartId, string PartName, double PartPrice)
    //    {
    //        return SqlDataAccess.UpdateData(
    //                        @"Update dbo.ComputerParts 
    //                        SET PartId=@PartId, PartName=@PartName, PartPrice=@PartPrice
    //                        where Id=@Id",
    //                        new PartModel
    //                        {
    //                            PartId = PartId,
    //                            PartName = PartName,
    //                            PartPrice = PartPrice
    //                        });
    //    }
    //    public static List<PartModel> Load()
    //    {
    //        return SqlDataAccess.LoadData<PartModel>(
    //                        @"select Id, PartId, PartName, PartPrice
    //                        from dbo.ComputerParts");
    //    }
    //}
    //public static class EmployeeProcessor
    //{
    //    public static int Create(int EmployeeId, string EmployeeEmailAddress, string EmployeeFirstName, string EmployeeLastName, double EmployeePay, int Auth)
    //    {
    //        return SqlDataAccess.SaveData(
    //                        @"insert into dbo.Employees (@Id, EmployeeId, EmployeeEmailAddress, EmployeeFirstName, EmployeeLastName, EmployeePay, Auth)
    //                        values @Id, (@EmployeeId, @EmployeeEmailAddress, @EmployeeFirstName, @EmployeeLastName, @EmployeePay, @Auth)",
    //                        new EmployeeModel
    //                        {
    //                            Id = Load().Count,
    //                            EmployeeId = EmployeeId,
    //                            EmployeeEmailAddress = EmployeeEmailAddress,
    //                            EmployeeFirstName = EmployeeFirstName,
    //                            EmployeeLastName = EmployeeLastName,
    //                            EmployeePay = EmployeePay,
    //                            Auth = Auth
    //                        });
    //    }
    //    public static List<EmployeeModel> Load()
    //    {
    //        return SqlDataAccess.LoadData<EmployeeModel>(
    //                        @"select Id, EmployeeId, EmployeeEmailAddress, EmployeeFirstName, EmployeeLastName, EmployeePay, Auth
    //                        from dbo.Employees");
    //    }
    //    public static int Update(int Id, int EmployeeId, string EmployeeEmailAddress, string EmployeeFirstName, string EmployeeLastName, double EmployeePay, int Auth)
    //    {
    //        return 0;
    //    }
    //}
    //public static class ImageProcessor
    //{
    //    public static int Create(int Id, int ImageId, string ImageData)
    //    {
    //        return SqlDataAccess.SaveData(
    //                        @"insert into dbo.Images (Id, ImageId, ImageData)
    //                        values (@Id, @ImageId, @ImageData)",
    //                        new ImageModel
    //                        {
    //                            Id = Load().Count,
    //                            ImageId = ImageId,
    //                            ImageData = ImageData
    //                        });
    //    }
    //    public static List<PartModel> Load()
    //    {
    //        return SqlDataAccess.LoadData<PartModel>(
    //                        @"select PartId, PartName, PartPrice
    //                        from dbo.ComputerParts");
    //    }
    //}
    //public static class ImageLinkProcessor
    //{
    //    public static int Create(int Id, int ImageId, int OrderId)
    //    {
    //        return SqlDataAccess.SaveData(
    //                        @"insert into dbo.ImageLinks (Id, ImageId, OrderId)
    //                        values (@Id, @ImageId, OrderId)",
    //                        new ImageLinkModel
    //                        {
    //                            Id = Load().Count,
    //                            ImageId = ImageId,
    //                            OrderId = OrderId
    //                        });
    //    }
    //    public static int Update(int Id, int ImageId, int OrderId)
    //    {
    //        return SqlDataAccess.UpdateData(
    //                        @"Update dbo.ImageLinks 
    //                        SET ImageId=@ImageId, OrderId=OrderId
    //                        where Id=@Id",
    //                        new ImageLinkModel
    //                        {
    //                            ImageId = ImageId,
    //                            OrderId = OrderId
    //                        });
    //    }
    //    public static List<ImageLinkModel> Load()
    //    {
    //        return SqlDataAccess.LoadData<ImageLinkModel>(
    //                        @"select Id, ImageId, OrderId
    //                        from dbo.ImageLinks");
    //    }
    //    public static int Remove(int Id)
    //    {
    //        return SqlDataAccess.DeleteData<ImageLinkModel>(
    //                        @"delete from dbo.ImageLinks
    //                        where Id=@Id",
    //                        new ImageLinkModel
    //                        {
    //                            Id = Id,
    //                        });
    //    }
    //}
    //public static class OrderProcessor
    //{
    //    public static int Create(int PartLinkId, int UserId, int EmployeeId, string OrderDiscription, int ImageID, double OrderPrice, int OrderStatus)
    //    {
    //        return SqlDataAccess.SaveData(
    //                        @"insert into dbo.Orders (Id, PartLinkId, UserId, EmployeeId, OrderDiscription, ImageID, OrderPrice, OrderStatus)
    //                        values (@Id, @PartLinkId, @UserId, @EmployeeId, @OrderDiscription, @ImageID, @OrderPrice, @OrderStatus)",
    //                        new OrderModel
    //                        {
    //                            OrderId = Load().Count,
    //                            PartLinkId = PartLinkId,
    //                            UserId = UserId,
    //                            EmployeeId = EmployeeId,
    //                            OrderDiscription = OrderDiscription,
    //                            ImageID = ImageID,
    //                            OrderPrice = OrderPrice,
    //                            OrderStatus = OrderStatus
    //                        });
    //    }
    //    public static int Update(int Id, int OrderId, int PartLinkId, int UserId, int EmployeeId, string OrderDiscription, int ImageID, double OrderPrice, int OrderStatus)
    //    {
    //        return SqlDataAccess.UpdateData(
    //                        @"Update dbo.Orders 
    //                        SET PartLinkId=@PartLinkId, UserId=@UserId, EmployeeId=@EmployeeId, OrderDiscription=@OrderDiscription, ImageID=@ImageID, OrderPrice=@OrderPrice, OrderStatus=@OrderStatus 
    //                        where Id=@Id",
    //                        new OrderModel
    //                        {
    //                            OrderId = OrderId,
    //                            PartLinkId = PartLinkId,
    //                            UserId = UserId,
    //                            EmployeeId = EmployeeId,
    //                            OrderDiscription = OrderDiscription,
    //                            ImageID = ImageID,
    //                            OrderPrice = OrderPrice,
    //                            OrderStatus = OrderStatus
    //                        });
    //    }
    //    public static List<OrderModel> Load()
    //    {
    //        return SqlDataAccess.LoadData<OrderModel>(
    //                        @"select Id, OrderId, PartLinkId, UserId, EmployeeId, OrderDiscription, ImageID, OrderPrice, OrderStatus
    //                        from dbo.Orders");
    //    }
    //}
    //public static class PartLinkProcessor
    //{
    //    public static int Create(int Id, int PartId)
    //    {
    //        return SqlDataAccess.SaveData(
    //                        @"insert into dbo.PartLinks (Id, PartId)
    //                        values (@Id, @PartId)",
    //                        new PartLinkModel
    //                        {
    //                            Id = Load().Count,
    //                            PartId = PartId,
    //                        });
    //    }
    //    public static int Update(int Id, int PartId)
    //    {
    //        return SqlDataAccess.UpdateData(
    //                        @"Update dbo.PartLinks 
    //                        SET PartId=@PartId
    //                        where Id=@Id",
    //                        new PartLinkModel
    //                        {
    //                            PartId = PartId,
    //                        });
    //    }
    //    public static List<PartLinkModel> Load()
    //    {
    //        return SqlDataAccess.LoadData<PartLinkModel>(
    //                        @"select Id, PartId
    //                        from dbo.PartLinks");
    //    }
    //    public static int Remove(int Id)
    //    {
    //        return SqlDataAccess.DeleteData<PartLinkModel>(
    //                        @"delete from dbo.PartLinks
    //                        where Id=@Id",
    //                        new PartLinkModel
    //                        {
    //                            Id = Id,
    //                        });
    //    }
    //}
    //public static class UserProcessor
    //{
    //    public static int Create(int UserId, string UserName, string UserEmail, int Auth, bool register = false)
    //    {
    //        //Do something smart with registered boolean
    //        return SqlDataAccess.SaveData(
    //                        @"insert into dbo.Users (Id, UserId, UserName, UserEmail, Auth)
    //                        values (@Id, @UserId, @UserName, @UserEmail, @Auth)",
    //                        new UserModel
    //                        {
    //                            Id = Load().Count,
    //                            UserId = UserId,
    //                            UserName = UserName,
    //                            UserEmail = UserEmail,
    //                            Auth = Auth
    //                        });
    //    }
    //    public static int Update(int Id, int UserId, string UserName, string UserEmail, int Auth, bool register = false)
    //    {
    //        //Do something smart with registered boolean
    //        return SqlDataAccess.UpdateData(
    //                        @"Update dbo.Users 
    //                        SET UserId=@UserId, UserName=@UserName, UserEmail=@UserEmail, Auth=@Auth
    //                        where Id=@Id",
    //                        new UserModel
    //                        {
    //                            UserId = UserId,
    //                            UserName = UserName,
    //                            UserEmail = UserEmail,
    //                            Auth = Auth,
    //                        });
    //    }
    //    public static List<UserModel> Load()
    //    {
    //        return SqlDataAccess.LoadData<UserModel>(
    //                        @"select Id, UserId, UserName, UserEmail, Auth
    //                        from dbo.Users");
    //    }
    //    public static int Remove(int Id)
    //    {
    //        return SqlDataAccess.DeleteData<UserModel>(
    //                        @"delete from dbo.Users
    //                        where Id=@Id",
    //                        new UserModel
    //                        {
    //                            Id = Id,
    //                        });
    //    }
    //}
}
