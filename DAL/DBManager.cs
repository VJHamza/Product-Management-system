using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities;

namespace MyDAL
{
    public class DBManager
    {
        public int DataInsert(UserDTO u)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO [User](Login, Name, Password, CreatedOn, IsActive,PicURL, IsAdmin)VALUES('" + u.login + "', '" + u.name + "', '" + u.password + "', '" + u.createdOn + "', '" + u.IsActive + "','" + u.picURL + "','" + u.IsAdmin + "')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return count;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: ", ex);
                }
                return 0;
            }
        }

        public UserDTO Validate(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();

                    string sql = "select * from [User] where Login='"+login+"' and Password='"+password+"'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    UserDTO u = new UserDTO();
                    if (reader.HasRows && reader.Read())
                    {
                        u.userID = Convert.ToInt32(reader["UserID"]);
                        u.login=Convert.ToString(reader["Login"]);
                        u.IsAdmin = Convert.ToInt32(reader["IsAdmin"]);
                        return u;
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }

        }
        public List<ProductDTO> getAllProducts()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    List<ProductDTO> lis = new List<ProductDTO>();
                    
                    string sql = "SELECT Product.ProductId,Name, Product.TypeId, Type.TypeName, Product.Price,Product.Description, Product.PicURL, Product.UpdatedOn, Product.UpdatedBy,Product.IsActive FROM Product INNER JOIN Type ON Product.TypeId = Type.TypeId where IsActive=1 ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        ProductDTO prod = new ProductDTO();
                        //TypeDTO typ = new TypeDTO();
                        prod.productId = Convert.ToInt32(reader["ProductId"]);
                        prod.typeid = Convert.ToInt32(reader["TypeId"]);
                        prod.typeName = Convert.ToString(reader["TypeName"]);
                        prod.name = Convert.ToString(reader["Name"]);
                        prod.price = Convert.ToDouble(reader["Price"]);
                        prod.description = Convert.ToString(reader["Description"]);
                        prod.picURL = Convert.ToString(reader["PicURL"]);
                        prod.updatedOn = Convert.ToDateTime(reader["UpdatedOn"]);
                        prod.updatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                        prod.IsActive = Convert.ToInt32(reader["IsActive"]);
                        lis.Add(prod);
                    }
                    return lis;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

        }
        public List<TypeDTO> getAllType()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    List<TypeDTO> list = new List<TypeDTO>();
                    
                    conn.Open();
                    string sql = "SELECT [TypeId] ,[TypeName] FROM[dbo].[Type]";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TypeDTO ty = new TypeDTO();
                        ty.typeId = Convert.ToInt32(reader["TypeId"]);
                        ty.typeName = Convert.ToString(reader["TypeName"]);
                        list.Add(ty);
                    }
                    return list;
                }
                catch
                {
                    return null;
                }
            }
        }
        public int getTypeId(string type)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM Type where TypeName = '" + type + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["TypeId"]);
                        return id;
                    }
                    return -1;
                }
                catch
                {
                    return -1;
                }

            }
        }
        public int saveProduct(ProductDTO p)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO Product (Name,TypeId,Price,Description,PicURL,UpdatedOn,UpdatedBy,IsActive) VALUES('" + p.name + "', '" + p.typeid + "',  '"+p.price+"' , '"+ p.description+"' , '" + p.picURL + "','" + p.updatedOn + "','" + p.updatedBy + "','"+p.IsActive+"')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        return count;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch 
                {
                    return -1;
                }
            }

        }
        public List<UserDTO> getUserById(int ID)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    List<UserDTO> list = new List<UserDTO>();
                    conn.Open();

                    string sql = "select * from [User] where UserId='"+ID+"'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.HasRows && reader.Read())
                    {
                        UserDTO u = new UserDTO();
                        u.userID = Convert.ToInt32(reader["UserID"]);
                        u.name = Convert.ToString(reader["Name"]);
                        u.login = Convert.ToString(reader["Login"]);
                        u.IsAdmin = Convert.ToInt32(reader["IsAdmin"]);
                        u.picURL = Convert.ToString(reader["PicURL"]);
                        list.Add(u);
                        return list;
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }
        public int updateUser(UserDTO us,string Login)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE [User] SET Login='"+Login+"' Name ='" + us.name + "', Password ='" + us.password + "',CreatedOn ='" + us.createdOn + ", IsActive ='" + us.IsActive + "'',PicURL ='" + us.picURL + "',IsAdmin='" + us.IsAdmin+"' WHERE Login='"+Login+"'";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;

                }
            }

        }
        public List<ProductDTO> getProductById(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    List<ProductDTO> lis = new List<ProductDTO>();

                    string sql = "SELECT Product.ProductId,Name, Product.TypeId, Type.TypeName, Product.Price,Product.Description, Product.PicURL, Product.UpdatedOn, Product.UpdatedBy,Product.IsActive FROM Product INNER JOIN Type ON Product.TypeId = Type.TypeId  where Product.ProductId='"+id+"'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read() == true)
                    {
                        ProductDTO prod = new ProductDTO();
                        prod.productId = Convert.ToInt32(reader["ProductId"]);
                        prod.typeid = Convert.ToInt32(reader["TypeId"]);
                        prod.typeName = Convert.ToString(reader["TypeName"]);
                        prod.name = Convert.ToString(reader["Name"]);
                        prod.price = Convert.ToDouble(reader["Price"]);
                        prod.description = Convert.ToString(reader["Description"]);
                        prod.picURL = Convert.ToString(reader["PicURL"]);
                        prod.updatedOn = Convert.ToDateTime(reader["UpdatedOn"]);
                        prod.updatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                        prod.IsActive = Convert.ToInt32(reader["IsActive"]);
                        lis.Add(prod);
                    }
                    return lis;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    
        public int updateProductById(ProductDTO p,int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE Product SET Name ='" + p.name + "', TypeId ='" + p.typeid + "', Price ='" + p.price + "', Description ='" + p.description + "' , PicURL ='" + p.picURL + "', UpdatedOn ='" + p.updatedOn + "', UpdatedBy ='" + p.updatedBy + "', IsActive ='" + p.IsActive + "' WHERE ProductId = '"+id+"'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;

                }
            }
        }
        public int deleteProductById(ProductDTO p,int ID)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4V6I868\\SQLEXPRESS2008;Initial Catalog=Assignment5;User ID=sa;Password=Iluvumerijan12"))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE Product SET  IsActive ='" + p.IsActive + "' WHERE ProductId = '" + ID + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;

                }
            }

        }
    }
} 