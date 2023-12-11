using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Codigo}' , '{producto.Nombre}' , {producto.Piezas} , {producto.PrecioUnitario}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";

                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto}, '{producto.Codigo}' , '{producto.Nombre}' , {producto.Piezas} , {producto.PrecioUnitario}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var query = context.Productos.FromSqlRaw("ProductoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto()
                            {
                                IdProducto = obj.IdProducto,
                                Codigo = obj.Codigo,
                                Nombre = obj.Nombre,
                                Piezas = obj.Piezas,
                                PrecioUnitario = obj.PrecioUnitario
                            };
                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var objquery = context.Productos.FromSql($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();
                    if (objquery != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = objquery.IdProducto;
                        producto.Codigo = objquery.Codigo;
                        producto.Nombre = objquery.Nombre;
                        producto.Piezas = objquery.Piezas;
                        producto.PrecioUnitario = objquery.PrecioUnitario;

                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo completar los registros de la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var producto = context.Productos.FirstOrDefault(e => e.IdProducto == IdProducto);

                    if (producto != null)
                    {
                        context.Productos.Remove(producto);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el empleado para eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
