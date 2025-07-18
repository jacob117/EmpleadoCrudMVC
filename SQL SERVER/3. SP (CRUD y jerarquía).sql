USE [EmpresaDB]
GO
/****** Catalogos ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Catalogos]
    @TipoCatalgo INT
AS
BEGIN
    SET NOCOUNT ON;

   IF @TipoCatalgo = 1 BEGIN
        SELECT CodigoPuesto Codigo, Nombre FROM Puesto;
        END

END;




/****** Delete del empleado ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteEmpleado]
    @Codigo INT
AS
BEGIN
    DELETE FROM Empleado WHERE Codigo = @Codigo;
END;



/****** SELECT de jerarquia o empleados ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Empleado]
 @Tipo INT,
 @Codigo INT = null
AS
BEGIN
SET NOCOUNT ON;
    IF @Tipo = 1 BEGIN

    WITH Jerarquia AS (
        SELECT 
            Codigo, p.Nombre Puesto, e.Nombre, CodigoJefe, 0 AS Nivel,e.codigoPuesto
        FROM Empleado e 
        INNER JOIN Puesto p 
        on p.CodigoPuesto = e.codigoPuesto 
        WHERE CodigoJefe IS NULL

        UNION ALL

        SELECT 
            e.Codigo, p.Nombre, e.Nombre, e.CodigoJefe, j.Nivel + 1,e.codigoPuesto
        FROM Empleado e 
        INNER JOIN Puesto p 
        on p.CodigoPuesto = e.codigoPuesto 
        INNER JOIN Jerarquia j ON e.CodigoJefe = j.Codigo
    )
    SELECT * FROM Jerarquia ORDER BY Nivel, Codigo;
    END

      IF @Tipo = 2 BEGIN    

    SELECT Codigo, codigoPuesto, Nombre, CodigoJefe FROM Empleado WHERE Codigo = @Codigo;
    END
END;



/****** INSERT ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_InsertEmpleado]
    @codigoPuesto INT,
    @Nombre VARCHAR(100),
    @CodigoJefe INT = NULL
AS
BEGIN
    INSERT INTO Empleado (CodigoPuesto, Nombre, CodigoJefe)
    VALUES (@codigoPuesto, @Nombre, @CodigoJefe);
END;


/****** UPDATE ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_UpdateEmpleado]
    @Codigo INT,
    @codigoPuesto INT,
    @Nombre VARCHAR(100),
    @CodigoJefe INT = NULL
AS
BEGIN
    UPDATE Empleado
    SET CodigoPuesto = @codigoPuesto,
        Nombre = @Nombre,
        CodigoJefe = @CodigoJefe
    WHERE Codigo = @Codigo;
END;