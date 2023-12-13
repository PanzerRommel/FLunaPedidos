using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Transaccion
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var query = context.Transacciones.FromSqlRaw("TransaccionGetAll").ToList();
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Transaccion transaccion = new ML.Transaccion()
                            {
                                IdTransaccion = obj.IdTransaccion,
                                Fecha = obj.Fecha,
                                Usuario = new ML.Usuario()
                                {
                                    IdUsuario = obj.IdUsuario,
                                    Nombre = obj.NombreUsuario
                                },
                                Producto = new ML.Producto()
                                {
                                    IdProducto = obj.IdProducto,
                                    Nombre = obj.NombreProducto
                                },
                                Cantidad = obj.Cantidad,
                                Total = obj.Total,
                                MontoIngresado = obj.MontoIngresado,
                                Cambio = obj.Cambio
                            };
                            result.Objects.Add(transaccion);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result Add(ML.Transaccion transaccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var query = context.Database.ExecuteSqlRaw(
                        "TransaccionAdd @Fecha, @IdUsuario, @IdProducto, @Cantidad, @MontoIngresado",
                        new SqlParameter("@Fecha", transaccion.Fecha),
                        new SqlParameter("@IdUsuario", transaccion.Usuario.IdUsuario),
                        new SqlParameter("@IdProducto", transaccion.Producto.IdProducto),
                        new SqlParameter("@Cantidad", transaccion.Cantidad),
                        new SqlParameter("@MontoIngresado", transaccion.MontoIngresado)
                    );

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
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

        public static ML.Result GetById(int IdTransaccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var objquery = context.Transacciones.FromSqlRaw($"TransaccionGetById {IdTransaccion}").AsEnumerable().FirstOrDefault();
                    if (objquery != null)
                    {
                        ML.Transaccion transaccion = new ML.Transaccion();
                        transaccion.IdTransaccion = objquery.IdTransaccion;
                        transaccion.Fecha = objquery.Fecha;

                        transaccion.Usuario = new ML.Usuario();
                        transaccion.Usuario.IdUsuario = objquery.IdUsuario;
                        transaccion.Usuario.Nombre = objquery.NombreUsuario;

                        transaccion.Producto = new ML.Producto();
                        transaccion.Producto.IdProducto = objquery.IdProducto;
                        transaccion.Producto.Nombre = objquery.NombreProducto;

                        transaccion.Cantidad = objquery.Cantidad;
                        transaccion.Total = objquery.Total;
                        transaccion.MontoIngresado = objquery.MontoIngresado;
                        transaccion.Cambio = objquery.Cambio;

                        result.Object = transaccion;
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
                result.ErrorMessage = "Ocurrió un error al intentar obtener la transacción.";
            }
            return result;
        }
        public static ML.Result Delete(int Idtransaccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"TransaccionDelete {Idtransaccion}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Eliminar Registro";
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
        public static ML.Result Update(ML.Transaccion transaccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
                {
                    // Puedes usar ExecuteSqlRaw para ejecutar un procedimiento almacenado de actualización
                    var query = context.Database.ExecuteSqlRaw(
                        "EXEC TransaccionUpdate @IdTransaccion, @Fecha, @IdUsuario, @IdProducto, @Cantidad, @MontoIngresado, @Cambio OUTPUT",
                        new SqlParameter("@IdTransaccion", transaccion.IdTransaccion),
                        new SqlParameter("@Fecha", transaccion.Fecha),  // Agregado el parámetro @Fecha
                        new SqlParameter("@IdUsuario", transaccion.Usuario.IdUsuario),
                        new SqlParameter("@IdProducto", transaccion.Producto.IdProducto),
                        new SqlParameter("@Cantidad", transaccion.Cantidad),
                        new SqlParameter("@MontoIngresado", transaccion.MontoIngresado != null ? transaccion.MontoIngresado : (object)DBNull.Value),
                        new SqlParameter("@Cambio", SqlDbType.Decimal)
                        {
                            Direction = ParameterDirection.Output
                        }
                    );


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

    }
}
