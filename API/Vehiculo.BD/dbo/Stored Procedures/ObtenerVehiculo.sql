
CREATE   PROCEDURE [dbo].[ObtenerVehiculo]
(
    @Id UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.Id,
        v.Placa,
        v.Color,
        v.Anio,
        v.Precio,
        v.CorreoPropietario,
        v.Telefono,
        mo.Nombre AS Modelo,
        ma.Nombre AS Marca
    FROM dbo.Vehiculo v
    INNER JOIN dbo.Modelos mo ON mo.Id = v.IdModelo
    INNER JOIN dbo.Marcas  ma ON ma.Id = mo.IdMarca
    WHERE v.Id = @Id;
END